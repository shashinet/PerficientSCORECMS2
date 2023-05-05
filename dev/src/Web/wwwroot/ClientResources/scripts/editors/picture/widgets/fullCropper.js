define([
    // dojo core
    "dojo",
    "dojo/on",
    "dojo/_base/declare", // Used to declare the actual widget
    "dojo/dom-style",

    // dijit core
    "dijit/_WidgetBase", // Base class for all widgets
    "dijit/_TemplatedMixin", // Widgets will be based on an external template (string literal, external file, or URL request)

    // 3rd party dependencies
    "./../vendor/cropper",

    // sub-widgets
    "./loader",

    // 3rd party css
    'xstyle/css!./../theme/cropper.css'
], function (
    dojo,
    on,
    declare,
    domStyle,

    _WidgetBase,
    _TemplatedMixin,

    Cropper,

    // widgets
    Loader

) {
        return declare("score.editors.picture.fullCropper",
            [_WidgetBase, _TemplatedMixin],
            {
                templateString: dojo.cache("score", "./editors/picture/widgets/fullCropper.html"),
                baseClass: "fullCropper",
                device: null, // set via widget declaration

                // initialization, firing in this order: https://dojotoolkit.org/reference-guide/1.10/dijit/_WidgetBase.html
                /* Your constructor method will be called before the parameters are mixed into the widget, and can be used to initialize arrays, etc. */
                constructor: function (widget, widgetsInTemplateMixin, contextMixin) {
                    _Widget = widget;
                },
                /* If you provide a postMixInProperties method for your widget, it will be invoked before rendering occurs, and before any dom nodes are created. If you need to add or change the instance???s properties before the widget is rendered - this is the place to do it. */
                postMixInProperties: function () {
                    this.inherited(arguments);

                    this.thumbnailDevice = this.params.thumbnailDevice;
                    this.thumbnailData = this.params.data;
                    this.url = this.params.url;
                    this.drawWidth = this.params.drawWidth;
                    this.drawHeight = this.params.drawHeight;
                    this.thumbnailCropBoxData = this.params.cropBoxData;
                },
                /* The template is fetched/read, nodes created and events hooked up during buildRendering. The end result is assigned to this.domNode. */
                buildRendering: function () {
                    this.inherited(arguments);
                },
                /* This is typically the workhorse of a custom widget. The widget has been rendered (but note that child widgets in the containerNode have not!). The widget though may not be attached to the DOM yet so you shouldn???t do any sizing calculations in this method. */
                postCreate: function () {
                    this.inherited(arguments);

                    this.loadAnimation = new Loader({
                        message: "Working..."
                    }).placeAt(this.container);
                },
                /* If you need to be sure parsing and creation of any child widgets has completed, use startup. This is often used for layout widgets like BorderContainer. If the widget does JS sizing, then startup() should call resize(), which does the sizing. */
                startup: function () {
                    this.inherited(arguments);

                    var module = this;

                    module.loadAnimation.show();

                    domStyle.set(module.cropView, "max-width", module.drawWidth + "px");
                    domStyle.set(module.cropView, "max-height", (module.drawHeight - 60) + "px");
                    // module.cropView.height = "800px";

                    module.image.src = module.url;

                    if (module.cropper) {
                        module.cropper.destroy();
                    }

                    module.cropper = new Cropper(module.image,
                        {
                            aspectRatio: module.thumbnailDevice.width / module.thumbnailDevice.height,
                            viewMode: 1,
                            dragMode: 'move',
                            autoCrop: true,
                            background: false,
                            highlight: false,
                            cropBoxResizable: true,
                            toggleDragModeOnDblclick: false,
                            minCropBoxHeight: 200,
                            ready: function () {
                                module.loadAnimation.hide();

                                var cropper = this.cropper;
                                cropper.setData(module.thumbnailData);

                                module._bindControls(cropper);
                            },
                            // prevent zooming past device constraints
                            zoom: function(event) {
                                var cropper = this.cropper;
                                
                                // ratio is a factor of crop box to device e.g., 1920x640 shrunk down to 600x200
                                var deviceToCropperRatio = cropper.getCropBoxData().width / module.thumbnailDevice.width;
                                if (event && event.target && event.detail && event.detail.ratio && event.detail.ratio > deviceToCropperRatio) {
                                    event.preventDefault();
                                }
                            },
                            // prevent cropper from being scaled down past device constraints
                            cropmove: function (event) {
                                var cropper = this.cropper;
                                var data = cropper.getData();

                                // we're constrained by aspect ratio, so just check that the width isn't too small
                                if (data.width < module.thumbnailDevice.width) {
                                    event.preventDefault();
                                    data.width = module.thumbnailDevice.width;
                                    cropper.setData(data);
                                }
                            }
                        });
                },

                // events
                resize: function () {
                    this.inherited(arguments);
                },
                destroy: function () {
                    var module = this;
                    if (module.cropper) {
                        module.cropper.destroy();
                    }
                },

                _bindControls: function () {
                    var module = this;

                    on(module.cancelButton, "click", function (ev) {
                        var event = new CustomEvent('cropCancel', {
                            detail: {
                                thumbnailDevice: module.device,
                                originalEvent: ev
                            }
                        });

                        module.eventHandler.dispatchEvent(event);
                    });

                    on(module.finishButton, "click", function (ev) {
                        var device = module.thumbnailDevice;
                        var canvasData = module.cropper.getCanvasData();
                        var cropBoxData = module.cropper.getCropBoxData();
                        var data = module.cropper.getData();

                        var event = new CustomEvent('cropFinish', {
                            detail: {
                                device: device,
                                canvasData: canvasData,
                                cropBoxData: cropBoxData,
                                data: data
                            }
                        });
                        module.eventHandler.dispatchEvent(event);
                    });
                }
            });
    });
