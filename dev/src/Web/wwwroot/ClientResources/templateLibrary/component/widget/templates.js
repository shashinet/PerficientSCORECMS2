define('templates/component/widget/templates', [
  // dojo core
  'dojo/_base/declare',
  'dojo/aspect',
  'dojo/on',

  //Opti
  'epi-cms/asset/HierarchicalList',

  // Templates
  'templates/component/viewmodels/templatesViewModel',

  // resources
  '../resources/createtemplate',
  '../resources/sitetree',
  'templates/moduleSettings',
], function (
  declare,
  aspect,
  on,
  HierarchicalList,

  TemplatesViewModel,

  componentResources,
  sitetreeRes,
  ModuleSettings,
) {
  return declare([HierarchicalList], {
    showCreateContentArea: false,
    modelClassName: TemplatesViewModel,
    noDataMessages: {
      single: sitetreeRes.nodatamessages.single,
      multiple: sitetreeRes.nodatamessages.multiple,
    },

    // hierarchicalListClass: [readonly] String
    //      The CSS class to be used on the content list.
    hierarchicalListClass: 'epi-blockList',
    // createContentIcon: [public] String
    //      The icon class to be used in the create content area of the list.
    createContentIcon: 'epi-iconPlus',

    // createContentText: [public] String
    createContentText: componentResources.newitemdefaultname,

    buildRendering: function buildRendering() {
      this.inherited(arguments);

      // Aspect the renderQuery method on the grid to get the number of items rendered after a query has executed
      // We can't depend on the "dgrid-refresh-complete" event from the grid because it might not always return the correct result
      // e.g if two refresh were initiated at almost the same time we will only get the event for the first refresh call

      var self = this;
      //self.model.selectedTreeItems[0].typeIdentifier
      this.own(
        aspect.after(this.list.grid, 'renderQuery', function (results) {
          results.then(function (results) {
            const showCreateContentArea = self._getShowCreateContentarea(results);

            self._toggleCreateContentArea(showCreateContentArea);
          });
        }),
      );
    },

    _getShowCreateContentarea: function (results) {
      const selectedItemType = this.model.selectedTreeItems?.[0]?.typeIdentifier;

      if (selectedItemType && selectedItemType === ModuleSettings.identifiers.rootFolder) {
        return false;
      }

      if (selectedItemType && selectedItemType === ModuleSettings.identifiers.pageFolder) {
        return results.length > 0 ? false : this.model.getCommand('newPageTemplate').get('canExecute');
      }

      return results.length > 0 ? false : this.model.getCommand('newBlockTemplate').get('canExecute');
    },

    _onCreateAreaClick: function _onCreateAreaClick() {
      // summary:
      //      A callback function which is executed when the create area is clicked.
      // tags:
      //      protected
      this.inherited(arguments);

      const selectedItemType = this.model.selectedTreeItems?.[0]?.typeIdentifier;
      if (selectedItemType && selectedItemType === ModuleSettings.identifiers.pageFolder) {
        this.model._commandRegistry.newPageTemplate.command.execute();
      } else {
        this.model._commandRegistry.newBlockTemplate.command.execute();
      }
    },

    // =======================================================================
    // List setup
    _setupList: function _setupList() {
      this.inherited(arguments);
      const registry = this.model._commandRegistry;
      const listWidget = this.list;
      this.own(
        on(listWidget, 'copyOrCut', function copyOrCuthandler(copy) {
          copy ? registry.copy.command.execute() : registry.cut.command.execute();
        }),
        on(listWidget, 'delete', function () {
          registry['delete'].command.execute();
        }),
      );
    },
  });
});
