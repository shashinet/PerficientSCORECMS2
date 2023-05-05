define('templates/component/viewmodels/templatesViewModel', [
  // dojo core
  'dojo/_base/declare',
  'dojo/_base/lang', // epi
  'epi-cms/asset/view-model/HierarchicalListViewModel',
  'epi-cms/command/ShowAllLanguages',

  // command
  '../command/newBlockTemplate',
  'templates/component/command/newPageTemplate',

  // resources
  'templates/moduleSettings'
], function (
  declare,
  lang,
  HierarchicalListViewModel,
  ShowAllLanguagesCommand,
  NewBlockTemplateCommand,
  NewPageTemplateCommand,
  ModuleSettings
) {
  return declare([HierarchicalListViewModel], {
    _updateCommandModels: function _updateCommandModels(selectedItems) {
      // summary:
      //      Update model of create and plugin commands.
      // tags:
      //      protected
      this.inherited(arguments);

      this._commandRegistry.newBlockTemplate.command.set('model', selectedItems);
      this._commandRegistry.newPageTemplate.command.set('model', selectedItems);

      const templateIdentifiers = Object.values(ModuleSettings.identifiers);

      const selectedItemType = selectedItems[0]?.typeIdentifier;
      if (!templateIdentifiers.includes(selectedItemType)) {
        return;
      }

      this._toggleCommandAvailabilities(selectedItemType);
    },

    _selectedTreeItemsSetter: function _selectedTreeItemsSetter(selectedItems) {
      // summary:
      //      Updates the commands with the specified value and updates the list query.
      // value:
      //      The selected tree items.
      // tags:
      //      protected
      this.inherited(arguments);

      var translateDelegate = lang.hitch(this.treeStoreModel, this.treeStoreModel.translate);

      this._commandRegistry.translate.command.set('model', selectedItems);

      this._commandRegistry.translate.command.set('executeDelegate', translateDelegate);
    },

    _setupCommands: function _setupCommands() {
      // summary:
      //      Creates and registers the commands used.
      // tags:
      //      protected
      this.inherited(arguments);
      var customCommands = {
        newBlockTemplate: {
          command: new NewBlockTemplateCommand({
            viewModel: this,
            category: 'context',
            iconClass: 'epi-iconSharedBlock',
          }),
          order: 2,
        },
        newPageTemplate: {
          command: new NewPageTemplateCommand({
            category: 'context'
          }),
          order: 3,
        },
        allLanguages: {
          command: new ShowAllLanguagesCommand({
            model: this,
          }),
          order: 55,
        },
      };
      this._commandRegistry = lang.mixin(this._commandRegistry, customCommands);
    },

    _toggleCommandAvailabilities: function (selectedItemType) {
      const isDeleteAvailable = selectedItemType === ModuleSettings.identifiers.folder;
      let isNewFolderAvailable =
        selectedItemType === ModuleSettings.identifiers.blockFolder ||
        selectedItemType === ModuleSettings.identifiers.pageFolder ||
        selectedItemType === ModuleSettings.identifiers.folder;

      const { templateFolder, blockTemplateFolder, pageTemplateFolder, rootTemplateFolder, deleteCommand } = this._getCommands();

      // Always disable when the command can't execute
      isNewFolderAvailable = templateFolder?.command.canExecute ? isNewFolderAvailable : false;
      templateFolder?.command?.set('isAvailable', isNewFolderAvailable);

      // Content authors should not be able to create or delete root folders or block/page root folders
      blockTemplateFolder?.command?.set('isAvailable', false);
      pageTemplateFolder?.command?.set('isAvailable', false);
      rootTemplateFolder?.command?.set('isAvailable', false);
      deleteCommand?.command?.set('canExecute', isDeleteAvailable);
    },

    _getCommands: function () {
      const commandRegistry = this._commandRegistry;
      const allCommandKeys = Object.keys(commandRegistry);

      let templateFolder, blockTemplateFolder, pageTemplateFolder, rootTemplateFolder, deleteCommand;

      allCommandKeys.forEach((key) => {
        if (key.endsWith(ModuleSettings.identifiers.folder)) {
          templateFolder = commandRegistry[key];
        } else if (key.endsWith(ModuleSettings.identifiers.blockFolder)) {
          blockTemplateFolder = commandRegistry[key];
        } else if (key.endsWith(ModuleSettings.identifiers.pageFolder)) {
          pageTemplateFolder = commandRegistry[key];
        } else if (key.endsWith(ModuleSettings.identifiers.rootFolder)) {
          rootTemplateFolder = commandRegistry[key];
        } else if (key === 'delete') {
          deleteCommand = commandRegistry[key];
        }
      });

      return { templateFolder, blockTemplateFolder, pageTemplateFolder, rootTemplateFolder, deleteCommand };
    },
  });
});
