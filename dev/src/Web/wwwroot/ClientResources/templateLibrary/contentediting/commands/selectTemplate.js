define("templates/contentediting/commands/selectTemplate", [
  // dojo
  'dojo/_base/Deferred',
  "dojo/_base/declare",
  "dojo/topic",
  'dojo/request/xhr',

  // command
  "epi/shell/command/_Command",

  // opti
  "epi/shell/TypeDescriptorManager",
  'epi/shell/DialogService',
  'epi-cms/widget/ContentSelectorDialog',

  // template library
  'templates/helper/dataHelper',

  // resource
  'templates/moduleSettings',
  'templates/component/resources/createtemplate'
],

  function (
    // dojo
    Deferred,
    declare,
    topic,
    xhr,

    // command
    _Command,

    // opti
    TypeDescriptorManager,
    DialogService,
    ContentSelectorDialog,

    dataHelper,

    // resource
    ModuleSettings,
    resCreateTemplate
  ) {

    return declare([_Command], {

      // contentType: [public] String
      //      The type of the content that the create content view should display.
      contentType: "episerver.core.pagedata",

      iconClass: "epi-iconCreatePage",
      label: resCreateTemplate.selectAPageTemplate,

      // createAsLocalAsset: [public] Boolean
      //      Indicate if the content should be created as local asset of its parent.
      createAsLocalAsset: false,

      // allowedTypes: [public] Array
      //      The types which are allowed for the given property. i.e used for filtering based on AllowedTypesAttribute
      allowedTypes: null,

      // restrictedTypes: [public] Array
      //      The types which are restricted.
      restrictedTypes: null,

      _execute: function () {
        this._openTemplateSelector(this.contentType).then(
          function (templateId) {
            if (templateId !== '0') {
              // get assembly name of the selected template
              dataHelper.getContentTypeAssemblyName(templateId).then(function (identifier) {
                // set templateId to CreateContent screen
                topic.publish("templates/setTemplateId", templateId);

                // go to create content screen
                topic.publish("/epi/shell/action/changeview", "epi-cms/contentediting/CreateContent",
                  { templateId: templateId },
                  {
                    requestedType: identifier,
                    parent: this.model,
                    addToDestination: this.destination,
                    createAsLocalAsset: this.createAsLocalAsset,
                    view: TypeDescriptorManager.getValue(identifier, "createView"),
                    allowedTypes: this.allowedTypes,
                    restrictedTypes: this.restrictedTypes
                  });
              }.bind(this));
            }
          }.bind(this)
        );
      },

      _onModelChange: function () {
        this.set("canExecute", true);
      },

      _openTemplateSelector: function (contentType) {
        var deferred = new Deferred();
        // check if content type is supported and there's already a template root
        dataHelper.getTemplateRoot().then(
          function (templateRoot) {
            if (templateRoot !== '0') {
              var templateInput = this._createDialogContent(templateRoot, contentType);

              DialogService.dialog({
                  content: templateInput,
                  title: 'Please select a template',
                  dialogClass: 'epi-dialog-portrait',
                  destroyOnHide: true,
                })
                .then(
                  function () {
                    var templateId = templateInput.get('value');

                    deferred.resolve(templateId);
                  }.bind(this)
                )
                .otherwise(function () {
                  // if editors click skip button
                  deferred.resolve('0');
                });
            } else {
              // Not supported
              deferred.resolve('0');
            }
          }.bind(this)
        );

        return deferred.promise;
      },

      _createDialogContent: function (templateRoot, contentType) {
        // summary:
        //    Create dialog content
        // tags:
        //    protected
        var allowType = "";
        if (contentType === "episerver.core.blockdata") {
          allowType = ModuleSettings.identifiers.blockTemplateInterface;
        }
        else if (contentType === "episerver.core.pagedata") {
          allowType = ModuleSettings.identifiers.pageTemplateInterface;
        }

        return new ContentSelectorDialog({
          canSelectOwnerContent: false,
          showButtons: true,
          roots: [templateRoot],
          allowedTypes: (this.allowedTypes === null) ? [allowType] : this.allowedTypes,
          showAllLanguages: false
        });
      }

    });
  });
