define([
    // dojo core
    "dojo",
    "dojo/_base/declare", // Used to declare the actual widget

    // dijit core
    "dijit/_WidgetBase", // Base class for all widgets
    "dijit/_TemplatedMixin", // Widgets will be based on an external template (string literal, external file, or URL request)

    // vendor
    "./../vendor/lottie"
], function (
        dojo,
        declare,

        _WidgetBase,
        _TemplatedMixin,

        lottie
) {
    return declare("score.editors.picture.swiper",
        [_WidgetBase, _TemplatedMixin],
        {
            templateString: dojo.cache("score", "./editors/picture/widgets/loader.html"),
            baseClass: "loader",
            
            message: "Loading...",
            animation: "bounce", //"/ClientResources/Scripts/Editors/picture/theme/bounce.json"
            

            // initialization, firing in this order: https://dojotoolkit.org/reference-guide/1.10/dijit/_WidgetBase.html
            /* Your constructor method will be called before the parameters are mixed into the widget, and can be used to initialize arrays, etc. */
            constructor: function(widget, widgetsInTemplateMixin, contextMixin) {
                console.log(this.baseClass + ": constructor");
                _Widget = widget;
            },
            /* If you provide a postMixInProperties method for your widget, it will be invoked before rendering occurs, and before any dom nodes are created. If you need to add or change the instance???s properties before the widget is rendered - this is the place to do it. */
            postMixInProperties: function () {
                this.inherited(arguments);
                console.log(this.baseClass + ": postmixinProperties");
            },
            /* The template is fetched/read, nodes created and events hooked up during buildRendering. The end result is assigned to this.domNode. */
            buildRendering: function () {
                this.inherited(arguments);
                console.log(this.baseClass + ": buildRendering");
            },
            /* This is typically the workhorse of a custom widget. The widget has been rendered (but note that child widgets in the containerNode have not!). The widget though may not be attached to the DOM yet so you shouldn???t do any sizing calculations in this method. */
            postCreate: function () {
                this.inherited(arguments);
                console.log(this.baseClass + ": postCreate");

                var module = this;

                module.animation = lottie.loadAnimation({
                    container: module.lottie, // Required
                    path: "/ClientResources/scripts/editors/picture/theme/" + module.animation + ".json",
                    renderer: 'svg', // Required
                    loop: true, // Optional
                    autoplay: true, // Optional
                    
                });
            },
            /* If you need to be sure parsing and creation of any child widgets has completed, use startup. This is often used for layout widgets like BorderContainer. If the widget does JS sizing, then startup() should call resize(), which does the sizing. */
            startup: function () {
                this.inherited(arguments);
                console.log(this.baseClass + ": startup");

                
            },

            // events
            resize: function () {
                this.inherited(arguments);
                console.log(this.baseClass + ": resize");
            },
            destroy: function () {
                var module = this;
                console.log(this.baseClass + ": destroy");
            },

            setMessage: function(message) {
                var module = this;
                module.message = message;
            },
            show: function() {
                var module = this;
                dojo.addClass(module.loader, "loading");
            },
            hide: function() {
                var module = this;
                dojo.removeClass(module.loader, "loading");
            }
        });
});
