define([
    // dojo core
    "dojo",
    "dojo/_base/declare", // Used to declare the actual widget
    "dojo/on", // To connect events
    "dojo/query",
    "dojo/mouse",
    "dojo/dom-class",
    "dojo/dom-style",

    // dijit core
    "dijit/_WidgetBase", // Base class for all widgets
    "dijit/_TemplatedMixin", // Widgets will be based on an external template (string literal, external file, or URL request)
    "dijit",

    // Episerver dnd classes
    "epi/shell/dnd/Target",
    "epi-cms/widget/_DndStateMixin",

    // service dependencies
    "../services/contentService",
    "../services/validationService",

    // sub-widgets
    "./loader",

    // 3rd party dependencies
    "./../vendor/draggable.min"
    
], function (
        dojo,
        declare,
        on,
        query,
        mouse,
        domClass,
        domStyle,

        _WidgetBase,
        _TemplatedMixin,
        dijit,

        // episerver dnd mixins
        Target,
        _DndStateMixin,

        // service dependencies
        contentService,
        validationService,

        // sub-widgets
        Loader,

        Draggable
        ) {
    return declare("score.editors.picture.focalSelector",
        [_WidgetBase, _TemplatedMixin, _DndStateMixin],
        {
            templateString: dojo.cache("score", "./editors/picture/widgets/focalSelector.html"),
            image: null,

            baseClass: "focal",

            // initialization, firing in this order: https://dojotoolkit.org/reference-guide/1.10/dijit/_WidgetBase.html
            /* Your constructor method will be called before the parameters are mixed into the widget, and can be used to initialize arrays, etc. */
            constructor: function(widget, widgetsInTemplateMixin, contextMixin) {
                _Widget = widget;
            },
            /* If you provide a postMixInProperties method for your widget, it will be invoked before rendering occurs, and before any dom nodes are created. If you need to add or change the instance???s properties before the widget is rendered - this is the place to do it. */
            postMixInProperties: function () {
                this.inherited(arguments);
            },
            /* The template is fetched/read, nodes created and events hooked up during buildRendering. The end result is assigned to this.domNode. */
            buildRendering: function () {
                this.inherited(arguments);
            },
            /* This is typically the workhorse of a custom widget. The widget has been rendered (but note that child widgets in the containerNode have not!). The widget though may not be attached to the DOM yet so you shouldn???t do any sizing calculations in this method. */
            postCreate: function () {
                this.inherited(arguments);

                var module = this;

                if (!module.params.propertyView) {
                    on(query(this.dropzone).query(".focal-dropzone"),
                        mouse.enter,
                        function(e) {
                            if (module.dnd && !domClass.contains(module.dropZoneElement, "hover")) {
                                dojo.addClass(module.dropZoneElement, "hover");
                                domStyle.set(module.dropZoneElement, "display", "");
                            }
                        });

                    on(query(this.dropzone).query(".focal-dropzone"),
                        mouse.leave,
                        function(e) {
                            if (module.dnd && domClass.contains(module.dropZoneElement, "hover")) {
                                dojo.removeClass(module.dropZoneElement, "hover");
                                domStyle.set(module.dropZoneElement, "display", "none");
                            }
                        });
                }

                // coming back from main after validation
                module.eventHandler.addEventListener('focalImageSelected', function(ev) {
                    if (ev.detail.image) {
                        module.loadAnimation.show();
                        module.imageNode.onload = function() {
                            module._setupFocal(); // ensure image has loaded before setting up focal so the maths works out
                            module.loadAnimation.hide();
                        };
                        module.imageNode.src = ev.detail.image.downloadUrl;
                    }
                });

                module.eventHandler.addEventListener('adoption', function(ev) {
                    if (ev.detail.image) {
                        module.loadAnimation.show();
                        module.imageNode.onload = function() {
                            module._setupFocal();
                            module.loadAnimation.hide();
                        }
                        module.imageNode.src = ev.detail.image.downloadUrl;
                    }
                });

                module.loadAnimation = new Loader({
                    message: "",
                    animation: "spin"
                }).placeAt(module.imageWrapper);
            },
            /* If you need to be sure parsing and creation of any child widgets has completed, use startup. This is often used for layout widgets like BorderContainer. If the widget does JS sizing, then startup() should call resize(), which does the sizing. */
            startup: function () {
                this.inherited(arguments);
                var module = this;
                
                if (module.params.image) {
                    module.imageNode.onload = function () {
                        if (!module.params.propertyView) {
                            module._setupFocal(); // ensure image has loaded before setting up focal so the maths works out
                        }
                    };
                    module.imageNode.src = module.params.image.downloadUrl;
                }

                if (!module.params.propertyView) {
                    module._setupDropZone();
                } else {
                    domStyle.set(module.focalPoint, "display", "none");
                    domStyle.set(module.dropZoneElement, "display", "none");
                    dojo.removeClass(module.imagePreview, "focal-dropzone");
                }
            },

            // events
            resize: function () {
                this.inherited(arguments);
            },
            destroy: function () {
                this._draggable.destroy();
            },

            _setupFocal: function () {
                var module = this;

                var focal = module.focalPoint;
                var imageWrapper = module.imageWrapper;

                var coords = dojo.coords(imageWrapper);

                // grant 10px of spacing around the edges to prevent focal from leaving image
                // it's styled to be roughly 20px in size
                var limit = {
                    x: [coords.l, coords.l + coords.w],
                    y: [coords.t, coords.t + coords.h]
                };

                if (this._draggable) {
                    this._draggable.destroy();
                }

                this._draggable = new Draggable(focal, {
                    limit: limit,
                    setCursor: true,
                    onDrag: module._dragEvent.bind(module)
                }).set(coords.l + (coords.w / 2), coords.t + (coords.h / 2));
            },

            _dragEvent: function (element, x, y) {
                var module = this;
                var coords = dojo.coords(module.imageWrapper);

                if (!module.bounds) {
                    module.bounds = module.imageWrapper.getBoundingClientRect();
                }

                var detail = {
                    relativeFocal: {
                        x: x - coords.l,
                        y: y - coords.t
                    },
                    bounds: module.bounds,
                    image: module.imageNode.src
                };

                var event = new CustomEvent('focalMoved', { detail: detail });
                module.eventHandler.dispatchEvent(event);
            },

            _setupDropZone: function () {
                var target = new Target(this.dropzone,
                    {
                        accept: ["link"],
                        //Set createItemOnDrop if you're only interested to receive the data, and not create a new node.
                        createItemOnDrop: false
                    });
                this.connect(target, "onDropData", this._onDropDataFocal);
            },

            _onDropDataFocal: function (dndData, source, nodes, copy) {
                //Drop item is an array with dragged items. This example just handles the first item.
                var dropItem = dndData ? (dndData.length ? dndData[0] : dndData) : null;

                if (dropItem) {
                    var module = this;
                    //The data property might be a deffered so we need to call dojo/when just in case.
                    dojo.when(dropItem.data, function (value) {
                        contentService.GetContentDataFromPreviewUrl(value.previewUrl, function (epiImage) {
                            var detail = {
                                image: epiImage
                            };

                            var event = new CustomEvent('focalImageDropped', { detail: detail });
                            module.eventHandler.dispatchEvent(event);
                        });
                        domStyle.set(this.dropZoneElement, "display", "none");
                    }.bind(this));
                }
            },

            _onDndStart: function () {
                this.dnd = true;
                this.inherited(arguments);
            },
            _onDndCancel: function () {
                this.dnd = false;
                this.inherited(arguments);
            },
            _onDndDrop: function () {
                this.dnd = false;
                this.inherited(arguments);
            }
        });
})
