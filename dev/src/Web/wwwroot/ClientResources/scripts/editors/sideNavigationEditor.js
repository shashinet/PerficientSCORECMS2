define([
  // dojo core
  'dojo/_base/declare', // Used to declare the actual widget
  'dojo/_base/lang',
  'dojo/dom-style',
  'dojo/aspect', // To connect events

  // Optimizely
  'epi/shell/widget/FormContainer', // Opti base widget to extend
], function (declare, lang, domStyle, aspect, FormContainer) {
  const CONTENT_AREA = 0;
  const TOP_LEVEL_LINKS = 1;
  const CURRENT_CONTENT = 2;

  return declare('score.editors.sideNavigation', [FormContainer], {
    widgetsList: {},

    postMixInProperties: function () {
      this.inherited(arguments);
      const self = this;

      self.own(
        aspect.after(self, '_onFormCreated', function () {
          self._bindContentAreaEvent();
        }),
      );
    },

    onFieldCreated: function (fieldName, widget) {
      this.inherited(arguments);
      this.widgetsList[fieldName] = widget;
    },

    _bindContentAreaEvent: function () {
      // Widgets
      var contentAreaWidget = this.widgetsList['navigationItems'];
      var siteNavigationTypeWidget = this.widgetsList['navigationType'];
      var topLevelLinksWidget = this.widgetsList['topLevelLinks'];
      var menuDirectionWidget = this.widgetsList['menuDirection'];

      // Setup on page load
      if (!siteNavigationTypeWidget.value || siteNavigationTypeWidget.value == CONTENT_AREA) {
        this._setWidgetDisplays(contentAreaWidget, topLevelLinksWidget, menuDirectionWidget);
      } else if (siteNavigationTypeWidget.value === TOP_LEVEL_LINKS) {
        this._setWidgetDisplays(topLevelLinksWidget, contentAreaWidget, menuDirectionWidget);
      } else if (siteNavigationTypeWidget.value === CURRENT_CONTENT) {
        this._setWidgetDisplays(menuDirectionWidget, topLevelLinksWidget, contentAreaWidget);
      }

      // Bind event
      this.own(
        siteNavigationTypeWidget.on(
          'change',
          lang.hitch(this, function (value) {
            if (!value || value == CONTENT_AREA) {
              this._setWidgetDisplays(contentAreaWidget, topLevelLinksWidget, menuDirectionWidget);
            } else if (value === TOP_LEVEL_LINKS) {
              this._setWidgetDisplays(topLevelLinksWidget, contentAreaWidget, menuDirectionWidget);
            } else if (value === CURRENT_CONTENT) {
              this._setWidgetDisplays(menuDirectionWidget, topLevelLinksWidget, contentAreaWidget);
            }
          }),
        ),
      );
    },

    _setWidgetDisplays: function (widgetToShow, ...widgetsToHide) {
      domStyle.set(widgetToShow.domNode.parentElement, 'display', 'block');
      widgetsToHide.forEach((node) => domStyle.set(node.domNode.parentElement, 'display', 'none'));
    },
  });
});
