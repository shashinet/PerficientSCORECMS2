define([
    // dojo core
    "dojo",
    "dojo/on",
    "dojo/_base/declare", // Used to declare the actual widget
    "dojo/query",
    "dojo/mouse",
    "dojo/dom-class",
    "dojo/dom-style",

    // dijit core
    "dijit/_WidgetBase", // Base class for all widgets
    "dijit/_TemplatedMixin", // Widgets will be based on an external template (string literal, external file, or URL request)

    // Episerver dnd classes
    "epi/shell/dnd/Target",
    "epi-cms/widget/_DndStateMixin",

    // service dependencies
    "../services/contentService",
    "../services/validationService",

    // sub-widgets
    "./loader",

    // 3rd party dependencies
    "./../vendor/cropper",
    "./../vendor/html5tooltips",

    // 3rd party css
    'xstyle/css!./../theme/cropper.css'
], function (
    dojo,
    on,
    declare,
    query,
    mouse,
    domClass,
    domStyle,

    _WidgetBase,
    _TemplatedMixin,

    // episerver dnd mixins
    Target,
    _DndStateMixin,

    // service dependencies
    contentService,
    validationService,

    // sub-widgets
    Loader,

    Cropper,
    html5tooltips
) {
        return declare("score.editors.picture.thumbnailCropper",
            [_WidgetBase, _TemplatedMixin, _DndStateMixin],
            {
                templateString: dojo.cache("score", "./editors/picture/widgets/thumbnailCropper.html"),
                baseClass: "thumbnailCropper",
                device: null, // set via widget declaration

                // initialization, firing in this order: https://dojotoolkit.org/reference-guide/1.10/dijit/_WidgetBase.html
                /* Your constructor method will be called before the parameters are mixed into the widget, and can be used to initialize arrays, etc. */
                constructor: function (widget, widgetsInTemplateMixin, contextMixin) {
                    _Widget = widget;
                },
                /* If you provide a postMixInProperties method for your widget, it will be invoked before rendering occurs, and before any dom nodes are created. If you need to add or change the instance???s properties before the widget is rendered - this is the place to do it. */
                postMixInProperties: function () {
                    this.inherited(arguments);
                    this.device = this.params.device;
                },
                /* The template is fetched/read, nodes created and events hooked up during buildRendering. The end result is assigned to this.domNode. */
                buildRendering: function () {
                    this.inherited(arguments);
                },
                /* This is typically the workhorse of a custom widget. The widget has been rendered (but note that child widgets in the containerNode have not!). The widget though may not be attached to the DOM yet so you shouldn???t do any sizing calculations in this method. */
                postCreate: function () {
                    this.inherited(arguments);
                    var module = this;

                    on(query(this.container).query(".thumb-dropzone"), mouse.enter, function (e) {
                        if (module.dnd && !domClass.contains(module.thumbDropZoneElement, "hover")) {
                            dojo.addClass(module.thumbDropZoneElement, "hover");
                            domStyle.set(module.thumbDropZoneElement, "display", "");
                        }
                    });

                    on(query(this.container).query(".thumb-dropzone"), mouse.leave, function (e) {
                        if (module.dnd && domClass.contains(module.thumbDropZoneElement, "hover")) {
                            dojo.removeClass(module.thumbDropZoneElement, "hover");
                            domStyle.set(module.thumbDropZoneElement, "display", "none");
                        }
                    });

                    module.loadAnimation = new Loader({
                        message: "",
                        animation: "spin"
                    }).placeAt(module.container);
                },
                /* If you need to be sure parsing and creation of any child widgets has completed, use startup. This is often used for layout widgets like BorderContainer. If the widget does JS sizing, then startup() should call resize(), which does the sizing. */
                startup: function () {
                    this.inherited(arguments);
                    var module = this;

                    html5tooltips.autoinit();

                    // wrap each cropperjs box with max width and height
                    module.wrapper.style.width = module.device.drawWidth + "px";
                    module.wrapper.style.height = module.device.drawHeight + "px";

                    // set max width of the overall thumbnail as well
                    module.wrapper.style.width = module.device.drawWidth + "px";

                    module.cropper = new Cropper(module.image,
                        {
                            aspectRatio: module.device.width / module.device.height,
                            viewMode: 3,
                            dragMode: 'move',
                            autoCrop: true,
                            restore: false,
                            modal: true,
                            center: true,
                            guides: false,
                            highlight: false,
                            background: false,
                            cropBoxMovable: false,
                            cropBoxResizable: false,
                            toggleDragModeOnDblclick: false,
                            minCropBoxHeight: module.device.drawHeight,
                            minContainerWidth: module.device.drawWidth,
                            ready: function () {
                                module.loadAnimation.hide();

                                var cropper = this.cropper;

                                var containerData = cropper.getContainerData();
                                var cropboxData = cropper.getCropBoxData();
                                var deviceSpec = module.device;

                                // Zoom out to 33% from the center of the container.
                                var zoomRatio = cropboxData.width / deviceSpec.width;

                                cropper.zoomTo(zoomRatio / 2, {
                                    x: containerData.width / 2,
                                    y: containerData.height / 2,
                                });
                            },
                            // prevent zooming past device constraints
                            zoom: function(event) {
                                var cropper = this.cropper;

                                // ratio is a factor of crop box to device e.g., 1920x640 shrunk down to 600x200
                                var deviceToCropperRatio = cropper.getCropBoxData().width / module.device.width;
                                if (event && event.target && event.detail && event.detail.ratio && event.detail.ratio > deviceToCropperRatio) {
                                    event.preventDefault();
                                }
                            }
                        });

                    module.eventHandler.addEventListener('focalMoved', function (ev) {
                        if (module.cropper && module.cropper.ready && !module.cropper.disabled) {
                            var localUrlPath = contentService.ParseUrl(module.cropper.url);
                            var focalUrlPath = contentService.ParseUrl(ev.detail.image);

                            if (localUrlPath.pathname === focalUrlPath.pathname) { // only actually move focal if the images are the same
                                var canvas = module.cropper.getCanvasData();
                                var cropbox = module.cropper.getCropBoxData();

                                var fpX = ev.detail.relativeFocal.x;
                                var fpY = ev.detail.relativeFocal.y;
                                var focalWidth = ev.detail.bounds.width;
                                var focalHeight = ev.detail.bounds.height;

                                var canvasWidth = canvas.width;
                                var canvasHeight = canvas.height;

                                var canvasFocalX = fpX * canvasWidth / focalWidth;
                                var canvasFocalY = fpY * canvasHeight / focalHeight;

                                var left = 0 - (canvasFocalX - (cropbox.width / 2));
                                var top = 0 - (canvasFocalY - (cropbox.height / 2));

                                module.cropper.moveTo(left, top);
                            }
                        }
                    });

                    module._setupDropZone();
                    module._bindControls(module.cropper);

                    if (module.device.drawWidth < 200 || module.device.drawHeight < 150) {
                        dojo.addClass(module.thumbDropZoneElement, "small");
                    }
                },

                // events
                resize: function () {
                    this.inherited(arguments);
                },
                destroy: function () {
                    if (this.cropper) {
                        this.cropper.destroy();
                    }
                },

                _lock: function() {
                    var module = this;

                    module.cropper.disable();
                            
                    dojo.addClass(module.cropButton, "disable");
                    dojo.addClass(module.zoomOutButton, "disable");
                    dojo.addClass(module.zoomInButton, "disable");

                    dojo.removeClass(module.lockIcon, "lock-open-icon");
                    dojo.addClass(module.lockIcon, "lock-close-icon");
                },
                _unlock: function() {
                    var module = this;

                    module.cropper.enable();

                    dojo.removeClass(module.cropButton, "disable");
                    dojo.removeClass(module.zoomOutButton, "disable");
                    dojo.removeClass(module.zoomInButton, "disable");

                    dojo.addClass(module.lockIcon, "lock-open-icon");
                    dojo.removeClass(module.lockIcon, "lock-close-icon");
                },

                _bindControls: function(cropper) {
                    var module = this;
                    
                    on(module.lockButton, "click", function () {
                        if (cropper.disabled) {
                            module._unlock();
                        } else {
                            module._lock();
                        }
                        
                    });

                    on(module.cropButton, "click", function() {
                        var event = new CustomEvent('cropStart', {
                            detail : {
                                thumbnailDevice: module.device,
                                data: cropper.getData(),
                                cropBoxData: cropper.getCropBoxData(),
                                url: cropper.url
                            }
                        });

                        module.eventHandler.dispatchEvent(event);
                    });

                    on(module.zoomOutButton, "click", function() {
                        if (!cropper.disabled) {
                            cropper.zoom(-0.1);
                        }
                    });

                    on(module.zoomInButton, "click", function () {
                        if (!cropper.disabled) {
                            cropper.zoom(0.1);
                        }
                    });

                    module.eventHandler.addEventListener('cropFinish', function(event) {
                        // detail: {
                        //     device: device,
                        //     canvasData: canvasData,
                        //     cropBoxData: cropBoxData
                        // }

                        // only accept the event if it has details and the device is the same as our device
                        if (event && event.detail) {
                            if (event.detail.device && event.detail.device.device === module.device.device) {
                                var localCropBox = module.cropper.getCropBoxData();

                                var eventCanvasData = event.detail.canvasData;
                                var eventCropBoxData = event.detail.cropBoxData;

                                // calculate relative zoom, and zoom to it
                                var zoom = (eventCanvasData.width / eventCanvasData.naturalWidth) *
                                    (localCropBox.width / eventCropBoxData.width);

                                module.cropper.zoomTo(zoom);

                                // calculate relative x and y, and move to it
                                var localData = module.cropper.getData();
                                var eventData = event.detail.data;
                                var relativeX = (localData.x - eventData.x) * zoom;
                                var relativeY = (localData.y - eventData.y) * zoom;

                                module.cropper.move(relativeX, relativeY);
                                module._lock();
                            }
                        }
                    });

                    module.eventHandler.addEventListener('focalImageSelected', function(ev) {
                        if (module.cropper && module.cropper.ready && !module.cropper.disabled) {
                            module.cropper.replace(ev.detail.image.downloadUrl);
                            module.device.originalImage = ev.detail.image.contentLink;
                            module.device.epiData = ev.detail.image;
                            module.loadAnimation.show();
                        }
                    });

                    module.eventHandler.addEventListener('adoption', function(ev) {
                        if (module.cropper && module.cropper.ready) {
                            for (var i = 0; i < ev.detail.result.length; i++) {
                                if (module.device.device === ev.detail.result[i].device) {
                                    module.loadAnimation.show();

                                    contentService.GetContentByReference(ev.detail.result[i].image, function(image) {
                                        if (module.cropper.disabled) {
                                            module.cropper.enable();
                                        }

                                        module.cropper.replace(image.downloadUrl);
                                        module.device.originalImage = image.contentLink;
                                        module.device.epiData = image;
                                        
                                        module.loadAnimation.hide();
                                    });
                                }
                            }
                        }
                    });
                },

                getResult: function() {
                    var module = this;
                    var result = {};
                    result.data = module.cropper.getData();
                    result.deviceSpec = module.device;
                    result.epiData = module.device.epiData;

                    return result;
                },

                _setupDropZone: function () {
                    var target = new Target(this.wrapper,
                        {
                            accept: ["link"],
                            //Set createItemOnDrop if you're only interested to receive the data, and not create a new node.
                            createItemOnDrop: false
                        });
                    this.connect(target, "onDropData", this._onThumbDropData);
                },
                _onThumbDropData: function (dndData, source, nodes, copy) {
                    //Drop item is an array with dragged items. This example just handles the first item.
                    var dropItem = dndData ? (dndData.length ? dndData[0] : dndData) : null;

                    var module = this;

                    if (dropItem) {
                        //The data property might be a deffered so we need to call dojo/when just in case.
                        dojo.when(dropItem.data, function (value) {
                            //Do something with the data, here we just log it to the console.
                            contentService.GetContentDataFromPreviewUrl(value.previewUrl, function (epiImage) {
                                module.loadAnimation.show();
                                var img = document.createElement("IMG");
                                img.onload = function() {
                                    //validate image constraints
                                    var devices = [];
                                    devices.push(module.device);
                                    var result = validationService.ValidateAgainstDevices(this.width, this.height, devices);

                                    if (result) {
                                        module.device.epiData = epiImage;
                                        module.device.originalImage = epiImage.contentLink;
                                        module.cropper.replace(epiImage.downloadUrl);
                                    } else {
                                        module.loadAnimation.hide();
                                    }
                                };
                                img.src = epiImage.downloadUrl;
                                domStyle.set(module.thumbDropZoneElement, "display", "none");
                            });
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
    });
