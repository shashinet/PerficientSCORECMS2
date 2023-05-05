define('score/editors/dynamicPropertiesEditor', [
  'dojo/_base/declare',
  'dojo/_base/lang',
  "dojo/when",
  "dojo/promise/all",
  "epi/dependency",
  'epi-cms/contentediting/FormEditing',
  "dojo/query",
  "dojo/NodeList-traverse"
],
  function (
    declare,
    lang,
    when,
    all,
    dependency,
    FormEditing,
    query
  ) {
    return declare([FormEditing], {
      dynamicProperties: null,
      widgetsList: {},
      dynamicMethods: {},
      typeIdentifier: '',
      store: null,
      _registry: null,
      _selectDOName: 'epi.cms.dynamicproperties',
      _selectDOPath: '/episerver/cms/customstores/dynamicproperties/',

      constructor: function () {
        this._initStore();
      },

      onSetupEditModeComplete: function () {
        this.inherited(arguments);
        this.typeIdentifier = this.viewModel.contentData.typeIdentifier;
        this._initDynamicPropertiesDependencies();
      },

      _initDynamicPropertiesDependencies: function () {
        when(this.store.get(`getdynamicproperties/${this.typeIdentifier}`), lang.hitch(this, function (results) {
          this.dynamicProperties = results;
          this._setupDynamicProperties();
        }));
      },

      _initStore: function () {
        this._registry = dependency.resolve("epi.storeregistry");
        this.contentStore = dependency.resolve("epi.cms.ContentTypeService");
        this.contextService = dependency.resolve("epi.shell.ContextService");

        try {
          this.store = this._registry.get(this._selectDOName);
        }
        catch {
          this.store = this._registry.create(this._selectDOName, this._selectDOPath);
        }
      },

      _modifyFields: function (widgetsList, fields, style) {
        if (!fields || !style | !widgetsList) {
          return;
        }

        fields.forEach(function (field) {
          var widget = widgetsList[field.toLowerCase()];

          if (widget) {
            widget.domNode.parentNode.style.display = style;
          }
        });
      },

      _hideShowBlockFieldSections: function (widgetsList, fields) {
        if (!fields || !widgetsList) {
          return;
        }

        fields.forEach(function (field) {
          if (field.indexOf('.') === -1) {
            return;
          }

          var widget = widgetsList[field.toLowerCase()];

          if (!widget) {
            return;
          }

          var fieldSection = query(widget.domNode).closest('.epi-form-container__section__row--parent')[0];

          if (!fieldSection) {
            return;
          }

          var fieldSectionElements = fieldSection.querySelectorAll('.epi-form-container__section__row--field');
          var fieldSectionStyling = 'none';

          fieldSectionElements.forEach(function (fieldElement) {
            if (fieldElement.style.display !== 'none') {
              fieldSectionStyling = 'block';
            }
          });

          fieldSection.style.display = fieldSectionStyling;
        });
      },

      _setupDynamicProperties: function () {
        this.hideShowMethod.call(this);

        this.dynamicProperties.forEach(lang.hitch(this, function (dyn) {
          var dynamicPropertyName = dyn.dynamicProperty.toLowerCase();
          var currentWidget = this.widgetsList[dynamicPropertyName];

          if (!currentWidget) {
            return;
          }

          this.own(currentWidget.on('change', lang.hitch(this, this.hideShowMethod)));
        }));
      },

      hideShowMethod: function () {
        var hideFields = [];
        var showFields = [];

        this.dynamicProperties.forEach(lang.hitch(this, function (dyn) {
          var dynamicPropertyName = dyn.dynamicProperty.toLowerCase();
          var currentWidget = this.widgetsList[dynamicPropertyName];

          if (!currentWidget) {
            return;
          }

          var currentWidgetValue = currentWidget.value;

          if (currentWidgetValue === null || currentWidgetValue === undefined) {
            currentWidgetValue = false;
          }

          var localValue = currentWidgetValue.toString().toLowerCase();
          var localHideFields = dyn.hideFields[localValue];
          var localShowFields = dyn.showFields[localValue];

          if (localHideFields) {
            hideFields = hideFields.concat(localHideFields);
          }

          if (localShowFields) {
            showFields = showFields.concat(localShowFields);
          }
        }));

        this.dynamicProperties.forEach(lang.hitch(this, function (dyn) {
          var dynamicPropertyName = dyn.dynamicProperty.toLowerCase();
          if (!hideFields.includes(dynamicPropertyName) || !dyn.hideFields) {
            return;
          }

          for (var hiddenFieldKey in dyn.hideFields) {
            hideFields = hideFields.concat(dyn.hideFields[hiddenFieldKey]);
          }
        }));

        showFields = _.difference(showFields, hideFields);
        this._modifyFields(this.widgetsList, showFields, 'block');
        this._modifyFields(this.widgetsList, hideFields, 'none');
        this._hideShowBlockFieldSections(this.widgetsList, showFields);
        this._hideShowBlockFieldSections(this.widgetsList, hideFields);
      },

      onFieldCreated: function (fieldName, widget) {
        this.inherited(arguments);
        this.widgetsList[fieldName.toLowerCase()] = widget;
      }
    });
  })
