define("epi-cms/contentediting/editors/_TextWithActionsMixin", [
  // dojo
  "dojo/_base/declare",
  "dojo/aspect",
  "dojo/when",

  // epi
  "epi/dependency",
  "epi/string",
  "epi/shell/command/_Command",

  // epi-cms
  "epi-cms/widget/ContentSelectorPlugAndPlay",

  // resources
  "epi/i18n!epi/cms/nls/episerver.cms.widget.overlay.blockarea"
], function (
  // dojo
  declare,
  aspect,
  when,

  // epi
  dependency,
  epiString,
  _Command,

  // epi-cms
  ContentSelectorPlugAndPlay,

  // Resources
  resource
) {

  return declare(null, {
    // summary:
    //      Handles action links
    // tags:
    //      public

    // actionsResource: [Object]
    //      The resource of actions link
    actionsResource: resource,

    // actionButtonLabel: [String]
    //      The label of action button.
    actionButtonLabel: null,

    isCreateLinkVisible: function () {
      // summary:
      //      Determines whether the "create" action in the action area is visible.
      //      Defaults to true, meaning that the action is shown.
      // returns:
      //      Promise|Boolean
      // tags:
      //      protected

      return true;
    },

    _setupCommands: function (actions, showCreateLink) {
      this._actionCommands = [];
      if (!showCreateLink) {
        return this._actionCommands;
      }

      for (var action in actions) {
        this._actionCommands.push(new _Command({
          label: epiString.capitalizeFirstLetter(actions[action]),
          isAvailable: true,
          canExecute: true,
          _execute: this.createExecuteActionFunc(action)
        }));
      }
    },

    _setupContentSelector: function (container, commands) {
      // construct instance with options commands & content selector dialog settings.
      this._contentSelectorPlugNPlay = this._contentSelectorPlugNPlay || new ContentSelectorPlugAndPlay({
        actionsNodeOptions: {
          settings: {
            label: this.actionButtonLabel
          }
        },
        commands: commands || [],
        contentSelectorDialogSettings: {
          allowedTypes: this.allowedTypes,
          restrictedTypes: this.restrictedTypes
        }
      });
      // place widget to container
      this._contentSelectorPlugNPlay.placeAt(container);
      // listen event when the widget's dialog is executed and save value.
      this.own(aspect.after(this._contentSelectorPlugNPlay, "onDialogExecute", function (selectedContentLink) {
        this.onFocus();
        if (!this.contentDataStore) {
          var registry = dependency.resolve("epi.storeregistry");
          this.contentDataStore = registry.get("epi.cms.contentdata");
        }
        when(this.contentDataStore.get(selectedContentLink)).then(function (selectedContent) {
          this.onDialogExecute(selectedContent);
        }.bind(this));
      }.bind(this), true));
    },

    _setupActions: function (/*Object*/container) {
      // summary:
      //      Initial ContentSelectorPlugAndPlay
      //      depend on context are create will show template without create, otherwise
      // container: [Object]
      //      The container will put content selector plug and play instance to.
      // tags:
      //      internal

      when(this.isCreateLinkVisible()).then(function (showCreateLink) {
        this._setupCommands(this.actionsResource.emptyactions.actions, showCreateLink);
        this._setupContentSelector(container, this._actionCommands);
      }.bind(this));
    },

    executeAction: function (/*String*/actionName) {
      // summary:
      //      Called when action clicked
      // tags:
      //      public
    },

    onDialogExecute: function (/*object*/value) {
      // summary:
      //      Called when dialog is executed
      // tags:
      //      public
    },

    createExecuteActionFunc: function(actionName) {
      return function() {
        this.executeAction(actionName);
      }.bind(this);
    }
  });
});
