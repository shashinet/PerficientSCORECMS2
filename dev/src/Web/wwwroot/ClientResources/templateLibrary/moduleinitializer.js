define([
  //dojo core
  'dojo/when',
  'dojo/_base/Deferred',
  'dojo/_base/declare',
  'dojo/_base/lang',
  'dojo/request/xhr',
  'dojo/promise/all',
  'dojo/_base/array',
  "dojo/topic",

  // opti
  'epi/_Module',
  'epi/dependency',
  'epi/routes',
  'epi/string',
  'epi/shell/command/_Command',

  'templates/contentediting/commands/addSaveTemplateCommandProvider',
  'templates/contentediting/commands/selectTemplate',
  'templates/helper/dataHelper',

  'epi-cms/contentediting/CreateContent',
  'epi/shell/DialogService',
  'epi-cms/widget/ContentSelectorDialog',
  'epi-cms/plugin-area/navigation-tree',
  'epi-cms/contentediting/editors/_TextWithActionLinksMixin',
  'epi-cms/contentediting/editors/_TextWithActionsMixin',
  'epi-cms/widget/overlay/ContentArea',
  'epi-cms/contentediting/editors/ContentAreaEditor',
  'epi-cms/component/PageNavigationTree',
  'epi-cms/command/NewContent',

  //resources
  'templates/moduleSettings',
  'epi/i18n!epi/cms/nls/episerver.cms.widget.overlay.blockarea'
], function (
  when,
  Deferred,
  declare,
  lang,
  xhr,
  all,
  array,
  topic,
  _Module,
  dependency,
  routes,
  epiString,
  _Command,
  AddSaveTemplateCommandProvider,
  SelectTemplateCommand,
  dataHelper,
  CreateContent,
  DialogService,
  ContentSelectorDialog,
  navigationTreePluginArea,
  TextWithActionLinksMixin,
  _TextWithActionsMixin,
  ContentArea,
  ContentAreaEditor,
  PageNavigationTree,
  NewContentCommand,
  ModuleSettings,
  blockAreaResources
) {
  return declare('templates.moduleInitializer', [_Module], {

    initialize: function () {
      this.inherited(arguments);

      // adding command to navigation tree plugin area
      navigationTreePluginArea.add(SelectTemplateCommand);

      var commandRegistry = dependency.resolve('epi.globalcommandregistry');
      commandRegistry.registerProvider('epi.cms.publishmenu', new AddSaveTemplateCommandProvider());

      var self = this;

      // Customize PageNavigationTree
      var originalPostMixInProperties = PageNavigationTree.prototype.postMixInProperties;
      // Extends the original postMixInProperties
      PageNavigationTree.prototype.postMixInProperties = function() {
        // call the original function
        originalPostMixInProperties.apply(this, arguments);

        //update styling
        var customCommands = this._contextMenuCommandProvider.get("commands");
        var selectTemplateCmdIndex = 0;
        var newContentCmdIndex = 0;
        array.forEach(customCommands,
          function(command, i) {
            if (command instanceof NewContentCommand) {
              newContentCmdIndex = i;
              command.category = 'context';
            } else if (command instanceof SelectTemplateCommand) {
              selectTemplateCmdIndex = i;
              command.category = 'menuWithSeparator';
            }
          });

        // moving Select Template Command to right under the New Page Command
        customCommands.splice(newContentCmdIndex + 1, 0, customCommands.splice(selectTemplateCmdIndex, 1)[0]);

        this._contextMenuCommandProvider.set("commands", customCommands);
      };

      // Customize ContentArea
      var originalContentAreaExecuteAction = ContentArea.prototype.executeAction;
      // Extends the original executeAction
      (ContentArea.prototype.executeAction = function (actionName) {
        if (actionName === "selectatemplate") {
          dataHelper.getContent(this.contentModel["epi-icontent_contentlink"]).then(function (currentContent) {
            // TODO: destroy and dereference command when done
            var command = new SelectTemplateCommand({
              contentType: "episerver.core.blockdata",
              createAsLocalAsset: true,
              allowedTypes: this.allowedTypes,
              restrictedTypes: this.restrictedTypes
            });

            command.set("model", currentContent);
            command.set("destination", {
              save: function (block) {
                var value = Object.assign([], this.model.get("value") || []);
                value.push(block);

                this.onValueChange({
                  propertyName: this._source.propertyName,
                  value: value
                });
              }.bind(this)
            });

            command.execute();
          }.bind(this));
        } else {
          // call the original function
          originalContentAreaExecuteAction.apply(this, arguments);
        }
      });

      // Customize ContentAreaEditor executeAction
      var originalContentAreaEditorExecuteAction = ContentAreaEditor.prototype.executeAction;
      // Extends the original executeAction
      (ContentAreaEditor.prototype.executeAction = function (actionName) {
        if (actionName === "selectatemplate") {
          // HACK: Preventing the onBlur from being executed so the editor wrapper keeps this editor in editing state
          this._preventOnBlur = true;

          // since we're going to create a block, we need to hide all validation tooltips because onBlur is prevented here
          this.validate(false);
          this.getCurrentContent().then(function (currentContent) {
            var command = new SelectTemplateCommand({
              contentType: "episerver.core.blockdata",
              createAsLocalAsset: true,
              allowedTypes: this.allowedTypes,
              restrictedTypes: this.restrictedTypes
            });

            command.set("model", currentContent);
            command.set("destination", {
              save: function (block) {
                this._preventOnBlur = false;
                var value = Object.assign([], this.get("value"), true);
                value.push(block);
                this.set("value", value);

                // In order to be able to add a block when creating it from a floating editor
                // we need to set the editing parameter on the editors parent wrapper to true
                // since it has been set to false while being suspended when switching to
                // the secondaryView.
                this.parent = this.parent || this.getParent();
                this.parent.set("editing", true);
                this.onChange(value);

                // Now call onBlur since it's been prevented using the _preventOnBlur flag.
                this.onBlur();
              }.bind(this),
              cancel: function () {
                this._preventOnBlur = false;
                this.onBlur();
              }.bind(this)
            });
            command.execute();
          }.bind(this));
        } else {
          // call the original function
          originalContentAreaEditorExecuteAction.apply(this, arguments);
        }
      });

      // Customize CreateContent
      var originalOnContentTypeSelected = CreateContent.prototype._onContentTypeSelected;
      var originalSetWizardStepAttr = CreateContent.prototype._setWizardStepAttr;
      var originalPostCreate = CreateContent.prototype.postCreate;

      // Extends the original postCreate
      (CreateContent.prototype.postCreate = function () {
        // call the original function
        originalPostCreate.apply(this, arguments);
        // subscribe to custom event
        this.own(
          topic.subscribe("templates/setTemplateId", function (templateId) {
            // set the selected template value
            this.templateId = templateId;
          }.bind(this))
        );
      }),

        // When content type is selected, check if templateId has value
        (CreateContent.prototype._onContentTypeSelected = function (item) {
          if (this.templateId) {
            //workaround: temporarily set autoPublish = true to avoid content to be created automatically without moving to Content Creation screen
            this.model.autoPublish = true;
          }
          // call the original function
          originalOnContentTypeSelected.apply(this, arguments);
        }),

        // After content type is selected and before the screen switches to Content Creation mode,
        // modify the initial value if a template is chosen.
        (CreateContent.prototype._setWizardStepAttr = function (wizardStep) {
          if (this.contentTypeName) {
            if (this.templateId) {
              var newProperties = self._mapNewProperties(this.model.metadata.properties, this.templateId);
              this.model.metadata.properties = newProperties;

              //reset templateId and autoPublish
              this.templateId = null;
              this.model.autoPublish = false;
            }
            // call the original function
            originalSetWizardStepAttr.apply(this, arguments);
          } else {
            // call the original function
            originalSetWizardStepAttr.apply(this, arguments);
          }
        });
    },


    _getRestPath: function (name) {
      return routes.getRestPath({ moduleArea: 'templates', storeName: name });
    },

    _mapNewProperties: function (properties, templateId) {
      return array.map(properties, function (item) {
        var newItem = lang.clone(item);

        // make SelectedTemplate visible to assign initial value for it
        if (newItem.name === 'SelectedTemplate') {
          newItem.initialValue = templateId;
          newItem.showForEdit = true;
          newItem.displayOrder = 1000;

          // temporarily set SelectedTemplate property to required, because additional properties may not show up in Content Creation
          newItem.groupName = "required";
          var newSettings = {
            required: true,
            missingMessage: "A template must be chosen at this point."
          };
          Object.assign(newItem.settings, newSettings);
        }
        return newItem;
      });
    }
  });
});
