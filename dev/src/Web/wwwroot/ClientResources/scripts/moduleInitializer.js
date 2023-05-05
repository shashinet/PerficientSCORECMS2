define([
  //dojo core
  'dojo/_base/declare',

  // opti
  'epi/_Module',
  'epi/dependency',
  'epi/routes',

  './tools/addPublishCommandProvider',
  'epi/shell/store/JsonRest',

  // templates
  'templates/moduleinitializer',
], function (
  declare,
  _Module,
  dependency,
  routes,
  AddPublishCommandProvider,
  JsonRest,
  TemplatesInitializer,
) {
  return declare('app.moduleInitializer', [_Module], {
    initialize: function () {
      this.inherited(arguments);

      //get epi's store registry which we will add our own store to.
      var registry = this.resolveDependency('epi.storeregistry');

      //Register store
      registry.add(
        'moveContentStore',
        new JsonRest({
          target: this._getRestPath('moveContentStore'),
          idProperty: 'contentLink',
        }),
      );

      //define our stores
      var commandRegistry = dependency.resolve('epi.globalcommandregistry');
      commandRegistry.registerProvider('epi.cms.publishmenu', new AddPublishCommandProvider());

      // Do any custom module initialization here
      var templatesInitializer = new TemplatesInitializer();
      templatesInitializer.initialize();
    },

    _getRestPath: function (name) {
      return routes.getRestPath({ moduleArea: 'app', storeName: name });
    },
  });
});
