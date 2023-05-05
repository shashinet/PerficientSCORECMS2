define([
    // dojo core
    "dojo",
    "dojo/on", // To connect events
    "dojo/_base/declare", // Used to declare the actual widget
    "dojo/query",
    "dojo/mouse",
    "dojo/dom-class",
    "dojo/dom-style",
    "dojo/dom-geometry",
    "dojo/_base/lang",

    // dijit core
    "dijit/_Widget", // Base class for all widgets
    "dijit/_TemplatedMixin", // Widgets will be based on an external template (string literal, external file, or URL request)
    "dijit/_WidgetsInTemplateMixin", // The widget will in itself contain additional widgets

    // epi core
    "epi/shell/widget/_ValueRequiredMixin", // In order to check if the property is in a readonly state
    "epi/shell/_ContextMixin",
    "epi/shell/DialogService",

    // Episerver dnd classes
    "epi/shell/dnd/Target",
    "epi-cms/widget/_DndStateMixin",

    // service dependencies
    "./services/contentService",
    "./services/validationService",
    "./services/apiService",

    // sub-widgets
    "./widgets/focalSelector",
    "./widgets/swiper",
    "./widgets/fullCropper",
    "./widgets/loader",

    // css
    'xstyle/css!./theme/style.css'
], function (
        // dojo core
        dojo,
        on,
        declare,
        query,
        mouse,
        domClass,
        domStyle,
        domGeom,
        lang,

        // dijit core
        _Widget,
        _TemplatedMixin,
        _WidgetsInTemplateMixin,

        // epi core
        _ValueRequiredMixin,
        _ContextMixin,
        DialogService,

        // episerver dnd mixins
        Target,
        _DndStateMixin,

        // service dependencies
        contentService,
        validationService,
        apiService,

        // sub-widgets
        FocalSelector,
        Swiper,
        FullCropper,
        Loader
        ) {
    return declare("score.editors.picture.main",
        [_Widget, _TemplatedMixin, _DndStateMixin],
        {
            templateString: dojo.cache("score", "./editors/picture/main.html"),

            intermediateChanges: false,
            onPropertyView: false,

            // editor descriptor variables
            devices: null,

            // private variables

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

                if (this.devices && typeof (this.devices) !== 'object') {
                    this.devices = JSON.parse(this.devices); // ensure devices is an object
                }

                var module = this;

                on(query(this.mainWindow).query(".btn-dark"),"click", function (e) {
                    dojo.addClass(module.mainWindow, "theme-dark");
                    query(".group-appearance buttonLink").removeClass("active");
                    dojo.addClass(e.target, "active");
                });

                on(query(this.mainWindow).query(".btn-light"), "click", function (e) {
                    dojo.removeClass(module.mainWindow, "theme-dark");
                    query(".group-appearance buttonLink").removeClass("active");
                    dojo.addClass(e.target, "active");
                });

                on(query(this.mainWindow).query(".js-dropzone"), mouse.enter, function (e) {
                    if (module.dnd && !domClass.contains(e.target, "hover")) {
                        dojo.addClass(e.target.parentElement, "hover");
                        dojo.addClass(e.target, "hover");
                    }
                });

                on(query(this.mainWindow).query(".js-dropzone"), mouse.leave, function (e) {
                    dojo.removeClass(e.target.parentElement, "hover");
                    dojo.removeClass(e.target, "hover");
                });

                on(module.cancelButton, "click", function(e) {
                    e.preventDefault();

                    module.parent._onButtonClose();
                });

                on(module.chooseButton, "click", function (e) {
                    e.preventDefault();

                    contentService.OpenImageSelector(function(image) {
                        // check if there are already cropped image for this selected one
                        module._checkCroppedImages(image);
                    });
                });

                on(module.clearButton, "click", function(e) {
                    e.preventDefault();

                    module._clearValue();

                    module.parent._onButtonClose();
                });

                on(module.finishButton, "click", function (e) {
                    e.preventDefault();
                    module.loadAnimation.show();

                    module._performCrop(function(results) {
                        var saveValue = {
                            originalImage: module._selectedImage,
                            croppings: module.devices
                        };

                        for (var j = 0; j < results.length; j++) {
                            for (var i = 0; i < saveValue.croppings.length; i++) {
                              if (saveValue.croppings[i].device === results[j].device) {
                                var identifier = "" + results[j].image;
                                saveValue.croppings[i].image = identifier;
                                }
                            }
                        }

                        module._setValueAttr(saveValue);

                        module.loadAnimation.hide();

                        module.parent._onButtonClose();
                    });
                });

                domStyle.set(module.cancelButton, "display", "none");
                domStyle.set(module.finishButton, "display", "none");
                domStyle.set(module.clearButton, "display", "none");

                module.loadAnimation = new Loader({
                    message: "Working..."
                }).placeAt(module.mainWindow);
            },
            /* If you need to be sure parsing and creation of any child widgets has completed, use startup. This is often used for layout widgets like BorderContainer. If the widget does JS sizing, then startup() should call resize(), which does the sizing. */
            startup: function() {
                this.inherited(arguments);

                var module = this;

                if (this.value && this.value.originalImage) { // on page editing has value always

                    // check for different thumb images and add them to the devices
                    // todo: at some point this.value.originalImage existed but this.value.croppings was null- this caused an
                    // invalid state that made the dialog crash.  Not sure why this happened, possibly due to the croppings
                    // being deleted within the Epi CMS
                    if (this.value.croppings && this.value.croppings.length > 0) {
                        for (var i = 0; i < this.value.croppings.length; i++) {
                            if (this.value.croppings[i].originalImage) {
                                var cropDevice = this.value.croppings[i].device;

                                for (var d = 0; d < this.devices.length; d++) {
                                    if (this.devices[d].device === cropDevice) {
                                        this.devices[d].originalImage = this.value.croppings[i].originalImage;
                                    }
                                }

                            }
                        }
                    }

                    contentService.GetContentByReference(this.value.originalImage,
                        function(epiImage) {
                            module._loadImage(epiImage);
                        });

                    if (!module.onPropertyView) {
                        domStyle.set(module.cancelButton, "display", "block");
                        domStyle.set(module.chooseButton, "display", "block");
                        domStyle.set(module.finishButton, "display", "block");
                        domStyle.set(module.clearButton, "display", "block");
                    }

                } else if (!this.value) { // property view doesn't have value at start
                    module.onPropertyView = true;
                    module.value = {
                        croppings: module.croppings
                    };

                    domStyle.set(module.chooseButton, "display", "none");
                    domStyle.set(module.dropzone, "display", "none");
                    domStyle.set(module.emptyDropzone, "display", "");
                }

                if (this.value && this.value.originalImage === null) { // new block, no value and is not in property view
                    domStyle.set(module.focalWrapper, "display", "none");
                    domStyle.set(module.swiperWrapper, "display", "none");
                }

                if (!module.onPropertyView) {
                    module._setupDropZone();
                }

                module.resize();
                module._bindEvents();
            },

            // Event used to notify EPiServer that the property value has changed
            onChange: function(value) {
                var module = this;

                if (this.onPropertyView) {

                    if (value.originalImage) {
                       module._loadDevicesForPropertyView();
                    }

                    // TODO clear the main window and display only the focal image
                    // template is parsed only once before postCreate method
                    module._hideElementsForPropertyView();
                }
            },

            _hideElementsForPropertyView: function () {
                var module = this;

                // hide everything
                domStyle.set(module.themeGroups, "display", "none");
                domStyle.set(module.cancelButton, "display", "none");
                domStyle.set(module.chooseButton, "display", "none");
                domStyle.set(module.finishButton, "display", "none");
                domStyle.set(module.focalWrapper, "display", "none");
                domStyle.set(module.swiperWrapper, "display", "none");
            },

            _loadDevicesForPropertyView: function () {
                var module = this;

                // show only property view div
                domStyle.set(module.emptyDropzone, "display", "none");
                domStyle.set(module.devicePropertyView, "display", "");
                // add all devices as images
                var maxDeviceHeight = 200;
                var maxSpecHeight = 0; // the largest device height
                for (var n = 0; n < module.devices.length; n++) {
                    if (module.devices[n].height > maxSpecHeight) {
                        maxSpecHeight = module.devices[n].height;
                    }
                }

                contentService.GetContentByReference(module.value.originalImage,
                    function (epiImage) {
                        dojo.place(
                            lang.replace("<div class='property-preview-wrapper'><p>Original Image: {name}</p><img src='{img}' style='max-height: {drawHeight}px;'/><p><a href='#' class='edit-original-image'>Edit Original Image</a></p></div>",
                                {
                                    name: epiImage.name,
                                    drawHeight: maxDeviceHeight,
                                    img: epiImage.downloadUrl,
                                    // editUrl: window.location.protocol + "//" + window.location.host + "/" + window.location.pathname + "#context=" + epiImage.uri
                                }), module.originalPropertyView);

                        on(query(".edit-original-image"), "click", function (e) {
                            e.preventDefault();
                            dojo.publish("/epi/shell/context/request", [epiImage]);
                        });
                        on(query(".device-property-view-show"), "click", function (e) {
                            e.preventDefault();
                            domStyle.set(module.devicePropertyViewList, "display", "block");
                            domStyle.set(module.viewVariationsView, "display", "none");
                            domStyle.set(module.hideVariationsView, "display", "block");
                        });
                        on(query(".device-property-view-hide"), "click", function (e) {
                            e.preventDefault();
                            domStyle.set(module.devicePropertyViewList, "display", "none");
                            domStyle.set(module.viewVariationsView, "display", "block");
                            domStyle.set(module.hideVariationsView, "display", "none");
                        });

                    });

                if (module.value.croppings) {
                    module.value.croppings.forEach(function (cropping) {
                        contentService.GetContentByReference(cropping.image,
                            function (epiImage) {
                                var drawHeight = maxDeviceHeight * cropping.height / maxSpecHeight; //  height percentage relative to maxHeight, not exceeding 300px
                                var drawWidth = cropping.width * drawHeight / cropping.height; // width should be relative to drawHeight
                                dojo.place(
                                    lang.replace("<div class=\"property-preview-wrapper\"><script type='text/json'>{diagnostics}</script><p>{name}</p><img src=\"{img}\" style=\"max-width: {drawWidth}px; max-height: {drawHeight}px;\"/><p style='font-size: 12px'>Dimensions: {width} x {height}<br>Query: {srcSet}</p></div>",
                                        {
                                            diagnostics: JSON.stringify(epiImage),
                                            name: cropping.device,
                                            img: epiImage.downloadUrl,
                                            drawWidth: drawWidth,
                                            drawHeight: drawHeight,
                                            width: cropping.width,
                                            height: cropping.height,
                                            srcSet: cropping.srcSet
                                        }),
                                    module.devicePropertyViewList);
                            });
                    });
                }
            },

            // Setter for value property (invoked by EPiServer on load)
            _setValueAttr: function (value) {
                // skip if value is nulled out
                if (!value) {
                    return;
                }

                // Skip if the new property value is identical to the current one
                if (value === this.value) {
                    return;
                }

                // Update the widget (i.e. property) value
                this._set("value", value);

                this.onChange(value);
            },
            _clearValue: function () {
                // reset the state but ensure we set it to this object, otherwise we'll get All Props view
                this._setValueAttr({
                        croppings: null,
                        originalImage: null
                    });
            },
            _setReadOnlyAttr: function (value) {
                this._set("readOnly", value);
            },
            _setIntermediateChangesAttr: function (value) {
                this._set("intermediateChanges", value);
            },

            // events
            resize: function () {
                this.inherited(arguments);
                var module = this;
            },
            destroy: function() {
                var module = this;
                if (module.focalWidget) {
                    module.focalWidget.destroy();
                }
                if (module.swiperWidget) {
                    module.swiperWidget.destroy();
                }
                if (module.fullcropWidget) {
                    module.fullcropWidget.destroy();
                }
            },

            _getBlockData: function () {
                // TODO added this check because once I had contentModel == undefined, didn't reproduce again
                if (this.parent) {
                    return {
                        blockId: this.parent.contentModel["epi-icontent_contentlink"],
                        blockParentId: this.parent.contentModel["epi-icontent_parentlink"],
                        blockName: this.parent.contentModel["epi-icontent_name"]
                    };
                } else {
                    return {
                        blockId: "",
                        blockParentId: "",
                        blockName: ""
                    };
                }
            },

            _loadImage: function (epiImage) {
                var module = this;
                module.loadAnimation.show();

                var img = document.createElement("IMG");
                img.onload = function() {
                    // todo: what happens when focal points are changed under the hood on existing images?
                    if (validationService.ValidateAgainstDevices(this.width, this.height, module.devices)) {
                        module._selectedImage = epiImage.contentLink;

                        dojo.destroy(module.dropzone);

                        if (module.focalWidget) {
                            module.focalWidget.destroy();
                            dojo.empty(module.focalWrapper);
                        }

                        if (module.swiperWidget) {
                            module.swiperWidget.destroy();
                            dojo.empty(module.swiperWrapper);
                        }

                        for (var i = 0; i < module.devices.length; i++) {
                            module.devices[i].epiData = epiImage;
                        }

                        module.focalWidget =
                            new FocalSelector({
                                image: epiImage,
                                propertyView: module.onPropertyView,
                                eventHandler: module.mainEventHandler
                            }).placeAt(module.focalWrapper);
                        module.swiperWidget =
                            new Swiper({
                                devices: module.devices,
                                epiImage: epiImage,
                                drawWidth: domGeom.getContentBox(module.mainWindow).w,
                                eventHandler: module.mainEventHandler
                            }).placeAt(module.swiperWrapper);

                        module.loadAnimation.hide();

                        if (!module.onPropertyView) {
                            domStyle.set(module.themeGroups, "display", "");
                            domStyle.set(module.cancelButton, "display", "");
                            domStyle.set(module.finishButton, "display", "");
                            domStyle.set(module.clearButton, "display", "");
                            domStyle.set(module.focalWrapper, "display", "");
                            domStyle.set(module.swiperWrapper, "display", "");
                        } else {
                            domStyle.set(module.emptyDropzone, "display", "none");
                        }
                    } else { // invalid dimensions for a device
                        module.loadAnimation.hide();
                    }
                };
                img.src = epiImage.downloadUrl;
            },

            _validateCropAdoption: function(image, adoptCallback, cancelCallback) {
                var module = this;

                var checkData = {
                    imageName: image.name,
                    baseBlockData: module._getBlockData(),
                    devices: module._getDeviceNames(),
                    parentId: image.parentLink,
                    imageId: image.contentLink
                };

                apiService.Post(checkData, "/api/picture/check", function (result) {
                    if (result && result.length > 0) {
                        DialogService.confirmation({
                            title: "Responsive Image",
                            heading: "Croppings Found",
                            content: "Selecting 'No' will open this image for cropping.",
                            description:
                                "Existing croppings have been found for this image used by this block type. <strong>Would you like to apply them?</strong>",
                            iconClass: "epi-iconObjectImage"
                        }).then(function() {
                            var event = new CustomEvent('adoption', {
                                detail: {
                                    result: result,
                                    image: image
                                }
                            });
                            module.mainEventHandler.dispatchEvent(event);

                            adoptCallback(result);
                            return;
                        }).otherwise(function() {
                            cancelCallback(image);
                            return;
                        });
                    } else {
                        cancelCallback(image);
                        return;
                    }
                });
            },

            _checkCroppedImages: function (image) {
                var module = this;

                module._validateCropAdoption(image, function(result) {
                    var saveValue = {
                        originalImage: image.contentLink,
                        croppings: module.devices
                    };

                    for (var j = 0; j < result.length; j++) {
                      for (var i = 0; i < saveValue.croppings.length; i++) {
                            if (saveValue.croppings[i].device === result[j].device) {
                                saveValue.croppings[i].image = result[j].image;
                            }
                        }
                    }

                    module._setValueAttr(saveValue);

                    module.parent._onButtonClose();

                }, module._loadImage.bind(module));
            },

            _bindEvents: function () {
                var module = this;

                // add all events to the mainEventHandler
                module.mainEventHandler.addEventListener('cropStart', function(event) {
                    var contentBox = domGeom.getContentBox(module.mainWindow);

                    domStyle.set(module.themeGroups, "display", "none");
                    domStyle.set(module.focalWrapper, "display", "none");
                    domStyle.set(module.swiperWrapper, "display", "none");
                    domStyle.set(module.primaryControls, "display", "none");

                    domStyle.set(module.fullcropWrapper, "display", "block");

                    if (module.fullcropWidget)
                        module.fullcropWidget.destroy();

                    dojo.empty(module.fullcropWrapper);

                    module.fullcropWidget = new FullCropper({
                        thumbnailDevice: event.detail.thumbnailDevice,
                        data: event.detail.data,
                        cropBoxData: event.detail.cropBoxData,
                        url: event.detail.url,
                        drawWidth: contentBox.w,
                        drawHeight: contentBox.h,
                        eventHandler: module.mainEventHandler
                    }).placeAt(module.fullcropWrapper);
                });

                module.mainEventHandler.addEventListener('cropFinish', function(event) {
                    if (module.fullcropWidget) {
                        module.fullcropWidget.destroy();
                    }

                    domStyle.set(module.themeGroups, "display", "");
                    domStyle.set(module.focalWrapper, "display", "");
                    domStyle.set(module.swiperWrapper, "display", "");
                    domStyle.set(module.primaryControls, "display", "");

                    domStyle.set(module.fullcropWrapper, "display", "none");
                    dojo.empty(module.fullcropWrapper);
                });

                module.mainEventHandler.addEventListener('cropCancel', function(event) {
                    if (module.fullcropWidget) {
                        module.fullcropWidget.destroy();
                    }

                    domStyle.set(module.themeGroups, "display", "");
                    domStyle.set(module.focalWrapper, "display", "");
                    domStyle.set(module.swiperWrapper, "display", "");
                    domStyle.set(module.primaryControls, "display", "");

                    domStyle.set(module.fullcropWrapper, "display", "none");
                    dojo.empty(module.fullcropWrapper);
                });

                module.mainEventHandler.addEventListener('focalImageDropped', function (ev) {
                    var img = document.createElement("img");
                    img.onload = function () {
                        // todo: what happens when focal points are changed under the hood on existing images?
                        if (validationService.ValidateAgainstDevices(this.width, this.height, module.devices)) {
                            module._selectedImage = ev.detail.image.contentLink;

                            dojo.destroy(module.dropzone);

                            module._validateCropAdoption(ev.detail.image,
                                function(result) {
                                    var saveValue = {
                                        originalImage: ev.detail.image.contentLink,
                                        croppings: module.devices
                                    };

                                    for (var j = 0; j < result.length; j++) {
                                      for (var i = 0; i < saveValue.croppings.length; i++) {
                                            if (saveValue.croppings[i].device === result[j].device) {
                                                saveValue.croppings[i].image = "" + result[j].image;
                                            }
                                        }
                                    }

                                    module._setValueAttr(saveValue);
                                    module.parent._onButtonClose();
                                },
                                function(image) {
                                    var event = new CustomEvent('focalImageSelected',
                                        { detail: ev.detail, eventHandler: module.mainEventHandler });
                                    module.mainEventHandler.dispatchEvent(event);
                                });
                        } else { // invalid dimensions for a device
                            module.loadAnimation.hide();
                        }
                    }
                    img.src = ev.detail.image.downloadUrl;
                });
            },

            _getDeviceNames: function () {
                var devices = [];
                for (var i = 0; i < this.devices.length; i++) {
                    devices.push(this.devices[i].device);
                }

                return devices;
            },

            _performCrop: function(callback) {
                var module = this;

                var blockData = module._getBlockData();

                if (module.swiperWidget) {
                    var results = module.swiperWidget.collectResults();
                    for (var i = 0; i < results.length; i++) {
                        results[i].baseBlockData = blockData;
                        results[i].mainImage = module._selectedImage;
                    }

                    apiService.Post(results, "/api/picture/crop", callback);
                }
            },

            _setupDropZone: function () {
                var target = new Target(this.dropzone,
                    {
                        accept: ["link"],
                        //Set createItemOnDrop if you're only interested to receive the data, and not create a new node.
                        createItemOnDrop: false
                    });
                this.connect(target, "onDropData", this._onDropData);
            },
            _onDropData: function (dndData, source, nodes, copy) {
                //Drop item is an array with dragged items. This example just handles the first item.
                var dropItem = dndData ? (dndData.length ? dndData[0] : dndData) : null;

                var module = this;
                module.dnd = false;

                if (dropItem) {
                    //The data property might be a deffered so we need to call dojo/when just in case.
                    dojo.when(dropItem.data, function (value) {
                        //Do something with the data, here we just log it to the console.
                        contentService.GetContentDataFromPreviewUrl(value.previewUrl, function(epiImage) {
                            module._checkCroppedImages(epiImage);
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
})
