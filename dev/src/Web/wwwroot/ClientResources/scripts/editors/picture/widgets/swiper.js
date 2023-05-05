define([
    // dojo core
    "dojo",
    "dojo/_base/declare", // Used to declare the actual widget

    // dijit core
    "dijit/_WidgetBase", // Base class for all widgets
    "dijit/_TemplatedMixin", // Widgets will be based on an external template (string literal, external file, or URL request)

    // service dependencies
    "../services/contentService",

    // 3rd party dependencies
    "./../vendor/swiper.min",
    "./thumbnailCropper",

    // styles
    'xstyle/css!./../theme/swiper.min.css',
], function (
        dojo,
        declare,

        _WidgetBase,
        _TemplatedMixin,

        // service dependencies
        contentService,

        Swiper,
        ThumbnailCropper
) {
    return declare("score.editors.picture.swiper",
        [_WidgetBase, _TemplatedMixin],
        {
            templateString: dojo.cache("score", "./editors/picture/widgets/swiper.html"),
            baseClass: "swiper",
            devices: null,
            maxCropperHeight: 200, // largest thumbnail cropper height, all the rest scale back to this form factor

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

                var module = this;

                module.drawWidth = module.params.drawWidth;

                if (module.params.devices) {
                    var devices = module.params.devices;

                    // if an image was passed in and doesn't exist on the device, set it on the device
                    if (module.params.epiImage) {
                        for (var i = 0; i < devices.length; i++) {
                            // check if the thumb image differs then the main (focal) image
                            if (devices[i].originalImage === module.params.epiImage.contentLink) {
                                devices[i].epiData = module.params.epiImage;
                            } else {
                                contentService.GetContentByReference(devices[i].originalImage,
                                    function (epiImage) {
                                        // if the originalImage is null or undefined this will be an array(0). This is a check against that
                                        // originalImage = null is a bad state from somewhere before, but is passed here
                                        // again, this is not a normal behaviour, can't reproduce it always
                                        if (epiImage.length === undefined) {
                                            devices[i].epiData = epiImage;
                                        }
                                    });
                            }
                        }
                    }

                    // set a drawWidth/Height to be used on each cropper
                    // set up variables to be used to constrain all croppers to relative sizes, not exceeding maxHeight in height
                    var maxSpecHeight = 0; // the largest device height
                    for (var n = 0; n < devices.length; n++) {
                        if (devices[n].height > maxSpecHeight) {
                            maxSpecHeight = devices[n].height;
                        }
                    }

                    // define draw widths/heights for each device
                    for (var k = 0; k < devices.length; k++) {
                        //devices[k].drawHeight = module.maxCropperHeight * devices[k].height / maxSpecHeight; //  height percentage relative to maxHeight, not exceeding 300px
                        var drawHeight = module.maxCropperHeight * devices[k].height / maxSpecHeight;  //  height percentage relative to maxHeight, not exceeding 300px

                        if (drawHeight < 100) { // don't let draw height drop below 100px as this is a lower limit of cropper.js
                            drawHeight = 100;
                        }

                        devices[k].drawHeight = drawHeight;
                        devices[k].drawWidth = devices[k].width * devices[k].drawHeight / devices[k].height; // width should be relative to drawHeight
                    }

                    this.devices = devices;
                }
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
            },
            /* If you need to be sure parsing and creation of any child widgets has completed, use startup. This is often used for layout widgets like BorderContainer. If the widget does JS sizing, then startup() should call resize(), which does the sizing. */
            startup: function () {
                this.inherited(arguments);
                console.log(this.baseClass + ": startup");
                var module = this;

                if (module.drawWidth && module.panel) {
                    module.panel.width = module.drawWidth + "px";
                }

                if (module.devices) {
                    module.thumbnailCroppers = [];

                    for (var i = 0; i < module.devices.length; i++) {
                        var thumbnailCropper = new ThumbnailCropper({ device: module.devices[i], eventHandler: module.eventHandler }).placeAt(module.children);
                        module.thumbnailCroppers.push(thumbnailCropper);
                    }

                    if (module._swiper)
                        module._swiper.destroy();

                    module._swiper = new Swiper('.swiper-container', {
                        slidesPerView: 'auto',
                        freeMode: true,
                        spaceBetween: 20,
                        scrollbar: {
                            el: module.scrollbar,
                            draggable: true,
                            freeMode: true
                        },
                    });
                }
            },

            // events
            resize: function () {
                this.inherited(arguments);
                console.log(this.baseClass + ": resize");

                if (module._swiper)
                    module._swiper.destroy();

                module._swiper = new Swiper('.swiper-container', {
                    slidesPerView: 'auto',
                    freeMode: true,
                    spaceBetween: 20,
                    scrollbar: {
                        el: module.scrollbar,
                        draggable: true,
                        freeMode: true
                    }
                });
            },
            destroy: function () {
                var module = this;

                if (module.thumbnailCroppers) {
                    for (var i = 0; i < module.thumbnailCroppers.length; i++) {
                        module.thumbnailCroppers[i].destroy();
                    }
                }

                console.log(this.baseClass + ": destroy");
            },

            // called by main.js to collect results from child croppers
            collectResults: function() {
                var module = this;
                var retVal = [];

                for (var i = 0; i < module.thumbnailCroppers.length; i++) {
                    retVal.push(module.thumbnailCroppers[i].getResult());
                }

                return retVal;
            }
        });
});
