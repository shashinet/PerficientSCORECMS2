define([
    // dojo core
    "dojo",
    "dojo/on",
    "dojo/_base/declare", // Used to declare the actual widget
    "dojo/query",
    "dojo/NodeList-traverse",

    // dijit core
    "dijit/_Widget", // Base class for all widgets
    "dijit/_TemplatedMixin", // Widgets will be based on an external template (string literal, external file, or URL request)
    "epi/shell/widget/_ValueRequiredMixin",

    "./vendor/choices",

    // css
    'xstyle/css!./theme/choices.css',
    'xstyle/css!./theme/style.css'
], function (
    // dojo core
    dojo,
    on,
    declare,
    query,
    traverse,

    // dijit core
    _Widget,
    _TemplatedMixin,
    _ValueRequiredMixin,

    // vendor
    Choices
) {
    return declare("score.editors.style.main",
        [_Widget, _TemplatedMixin, _ValueRequiredMixin],
        {
            templateString: dojo.cache("score", "./editors/style/main.html"),
            baseClass: "style",

            selections: null, // set by editor descriptor through attribution
            value: null,
            initialized: false,

            // initialization, firing in this order: https://dojotoolkit.org/reference-guide/1.10/dijit/_WidgetBase.html
            /* Your constructor method will be called before the parameters are mixed into the widget, and can be used to initialize arrays, etc. */
            constructor: function (widget, widgetsInTemplateMixin, contextMixin) {
                _Widget = widget;
            },
            /* If you provide a postMixInProperties method for your widget, it will be invoked before rendering occurs, and before any dom nodes are created. If you need to add or change the instance?s properties before the widget is rendered - this is the place to do it. */
            postMixInProperties: function () {
                this.inherited(arguments);
            },
            /* The template is fetched/read, nodes created and events hooked up during buildRendering. The end result is assigned to this.domNode. */
            buildRendering: function () {
                this.inherited(arguments);
            },
            /* This is typically the workhorse of a custom widget. The widget has been rendered (but note that child widgets in the containerNode have not!). The widget though may not be attached to the DOM yet so you shouldn?t do any sizing calculations in this method. */
            postCreate: function () {
                this.inherited(arguments);

                var module = this;

                // If not initialized already, call for initilization
                if (module.choices === undefined) {
                    module._initChoiceModule();
                }

                on(module.Input, 'change', function (ev) {
                    console.log("Change fired");

                    var options = [];
                    for (var i = 0; i < module.Input.selectedOptions.length; i++) {
                        options.push(module.Input.selectedOptions[i].value);
                    }

                    var result = options.join(",");
                    module._setValueAttr(result);
                });

            },
            /* If you need to be sure parsing and creation of any child widgets has completed, use startup. This is often used for layout widgets like BorderContainer. If the widget does JS sizing, then startup() should call resize(), which does the sizing. */
            startup: function () {
                this.inherited(arguments);

                // find all parent elements with "overflow: hidden" and 'fix' them ;)
                if (this.domNode && this.domNode.parentNode) {
                    // these classes have overflow hidden and are wrapping this element, we need to change their overflow behavior
                    var invalidCss = [".epi-form-container__section__row", ".dijitLayoutContainer", ".dijitTabContainerTop-container"];

                    //If there is a parent, we assume this is in an option bar, lets resize
                    if (this.parent !== undefined) {
                        //set height based on number of selections, but set maximum of 10 (because of scrollability)
                        var height = 50 * (this.selections.length <= 10 ? this.selections.length : 10);
                        dojo.style(this.domNode.id, "height", height + "px");
                        //Set min/max height
                        dojo.style(this.domNode.id, "min-height", "150px");
                        dojo.style(this.domNode.id, "max-height", "450px");
                    }

                    for (var i = 0; i < invalidCss.length; i++) {
                        var elem = query(this.domNode).closest(invalidCss[i]);
                        if (elem && elem.length > 0) {
                            dojo.style(elem[0], "overflow", "visible"); // allow choicesjs to be visible when spanning outside
                        }
                    }
                }
            },


            // Setter for value property (invoked by EPiServer on load)
            _setValueAttr: function (value) {
                this.inherited(arguments);
                var module = this;

                // If not initialized already, call for initilization
                // This gets hit first in an option bar on the OPE view
                if (module.choices === undefined) {
                    module._initChoiceModule();
                }

                module.choices.removeActiveItems();

                if (value) {
                    var selections = value.split(",");
                    for (var i = 0; i < selections.length; i++) {
                        if (selections[i]) {
                            module.choices.setChoiceByValue(selections[i]);
                        }
                    }
                }

                console.log("Setting value to ", value);
                // Update the widget (i.e. property) value

                this.onFocus();
                this._set("value", value);

                if (this.onChange !== undefined) {
                    this.onChange(value);
                }
            },
            _initChoiceModule: function () {
                // initialize choices plugin
                this.choices = new Choices(
                    this.Input,
                    {
                        addItems: true,
                        duplicateItemsAllowed: false,
                        removeItemButton: true,
                        itemSelectText: ''
                    }
                );

                //If there is a parent, we assume this is in an option bar
                if (this.parent !== undefined) {
                    this.selections = this.parent.property.metadata.settings.selections;
                }

                // set available choices based on the module.selections, which is established by editor descriptor
                if (this.selections && this.selections.length > 0) {
                    this.choices.setChoices(this.selections, 'scoreClassName', 'name', false);
                }
            }
        });
})
