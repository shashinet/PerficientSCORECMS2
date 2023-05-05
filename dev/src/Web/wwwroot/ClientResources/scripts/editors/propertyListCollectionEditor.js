define('custom-scripts/editors/propertyListCollectionEditor', [
  // dojo core
  'dojo/_base/declare', // Used to declare the actual widget
  'dojo/_base/array',
  'dojo/aspect',
  // Optimizely
  'epi-cms/contentediting/editors/CollectionEditor', // Opti base widget to extend,
], function (declare, array, aspect, CollectionEditor) {
  return declare([CollectionEditor], {
    _setupDnD: function () {
      // summary:
      //      Set up the dnd on the grid.
      // tags:
      //      private
      this.inherited(arguments);
      var self = this;

      self.own(
        aspect.after(
          self.grid.dndSource, // Target
          '_checkAcceptanceForItems', // Target's method to watch
          function (items, acceptedTypes) {
            // Run after target's method &
            // replace original return value with logic for PropertyLists
            return array.every(items, (item) => {
              return self.itemType.toLowerCase() === item.data.typeIdentifier;
            });
          },
          true, // Receive target method's original arguments
        ),
      );
    },
  });
});
