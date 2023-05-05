define([
    "dojo",
    "dojo/_base/array",
    "dojo/query",
    "dojo/on",
    "dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/dom-construct",

    "dijit/_CssStateMixin",
    "dijit/_Widget",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",
    "epi/shell/_ContextMixin",
    "dijit",

    "epi/epi",
    "epi/dependency",
    "epi/shell/widget/_ValueRequiredMixin"
],
    function (
        dojo,
        array,
        query,
        on,
        declare,
        lang,
        domConstruct,

        _CssStateMixin,
        _Widget,
        _TemplatedMixin,
        _WidgetsInTemplateMixin,
        _ContextMixin,
        dijit,

        epi,
        dependency,
        _ValueRequiredMixin
    ) {
        return declare("score.editors.PageLayoutPickerEditor", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin, _ContextMixin, _CssStateMixin, _ValueRequiredMixin],
        {
            constructor: function () {
                dojo.when(this.getCurrentContext(),
                    function (context) {
                        // context was loaded
                        //console.log("## context loaded", context);

                        //this.layoutType = context.dataType === "epijuice.models.blocks.layouts.innerlayoutblock" ? true : false;
                        //console.log("## context data type: ", context.dataType);
                        //console.log("## context layout type: ", this.layoutType);
                    });
            },

            templateString: "<div class=\"dijitInline\">\
                            <ul data-dojo-attach-point=\"pageLayoutPickerList\" class=\"pageLayoutPickerList\">\
                            </ul>\
                        </div>",
            intermediateChanges: false,
            value: null,
            pickedLayout: null,
            onChange: function (value) {
                // Event
                this.inherited(arguments);

                this._loadContentAreas();
                this._refreshContentAreas(value);
                this._hideContentAreas();
            },

            _hideContentAreas: function() {
                if (!this.areas && this.areas.length < 1)
                    return;

                var selection = this.layoutType
                    ? getContentAreaByInnerLayoutImage(this.pickedLayout)
                    : getContentAreaByLayoutCode(this.pickedLayout);

                var containers = this._getContentAreaContainers();

                if (this.layoutType) {
                    var containerPage = containers.length === 8 ? containers.slice(containers.length / 2) : containers;
                    if (containerPage.length > selection) {
                        for (var i = 0; i < containerPage.length; i++) {
                            if (i > selection) {
                                containerPage[i].style.display = "none";
                            } else {
                                containerPage[i].style.display = "";
                            }
                        }
                    }
                } else {
                    if (this.areas.length > selection) {
                        for (var j = 0; j < this.areas.length; j++) {
                            if (j > selection) {
                                containers[j].style.display = "none";
                            } else {
                                containers[j].style.display = "";
                            }
                        }
                    }
                }
                
            },

            _getContentAreaContainers: function() {
                var fields = query(".epi-form-container__section__row--field");

                var containers = [];
                for (var i = 0; i < fields.length; i++) {
                    if (fields[i].innerText.trimStart().startsWith("First")) {
                        containers.push(fields[i]);
                    }
                    if (fields[i].innerText.trimStart().startsWith("Second")) {
                        containers.push(fields[i]);
                    }
                    if (fields[i].innerText.trimStart().startsWith("Third")) {
                        containers.push(fields[i]);
                    }
                    if (fields[i].innerText.trimStart().startsWith("Fourth")) {
                        containers.push(fields[i]);
                        //break;
                    }
                }

                return containers;
            },

            layoutType: null,

            postCreate: function () {

                this._initLayouts();
                this.inherited(arguments);
                this._loadCssFile();

                this.pickedLayout = this.value;
                this._bindEvents(this);

            },
            startup: function () {
                //console.log('startup');
            },
            isValid: function () {
                return !this.required || lang.isArray(this.value) && this.value.length > 0 && this.value.join() !== "";
            },
            _setValueAttr: function (value) {
                this._setValue(value, true);
            },
            _setReadOnlyAttr: function (value) {
                this._set("readOnly", value);
            },
            _setIntermediateChangesAttr: function (value) {

                this._set("intermediateChanges", value);
            },
            _markChosenLayoutInList: function (value) {

                var listItems = query(this.pageLayoutPickerList).query("li");
                var tmpLayout = "";
                for (var i = 0; i < listItems.length; i++) {
                    tmpLayout = query(listItems[i]).query("a")[0].getAttribute("data-layout");
                    if (value === tmpLayout) {
                        listItems[i].setAttribute("class", "selectedLayout");
                    } else {
                        listItems[i].setAttribute("class", "");
                    }
                }
            },
            _bindEvents: function (myself) {

                on(query(this.pageLayoutPickerList).query("a"), "click", function (e) {
                    myself._chooseLayout(e.currentTarget, myself);
                    myself._markChosenLayoutInList(e.currentTarget.getAttribute("data-layout"));

                    e.preventDefault();
                });

            },
            _chooseLayout: function (clickedItem, myself) {
                var layout = clickedItem.getAttribute("data-layout");
                myself._setValue(layout, true);
            },
            _setValue: function (value, updateTextbox) {
                //avoids running this if the widget already is started
                if (this._started && epi.areEqual(this.value, value)) {
                    return;
                }

                // set value to this widget (and notify observers). 
                this._set("value", value);

                // set value to tmp value
                if (updateTextbox) {
                    this.pickedLayout = value;
                    this._markChosenLayoutInList(this.pickedLayout);
                }

                if (this._started && this.validate()) {
                    // Trigger change event
                    this.onChange(value);
                }
            },

            areas: null,

            _removeContentAreaItems: function (contentArea) {
                if (!contentArea.model.count)
                    contentArea.model.count = 0;
                while (contentArea.model.count > 0) {
                    dojo.when(contentArea.model.getChildByIndex(0), lang.hitch(this, function (contentData) {
                        contentArea.model.modify(lang.hitch(this, function () {
                            contentArea.model.removeChild(contentData);
                        }));

                        //console.log("ContentArea " + contentArea.name + " Remove: ", contentData);
                    }));
                }
            },

            _copyContentAreaItems: function (sourceContentArea, targetContentArea) {
                targetContentArea.focus();

                var emptyCa = false;
                if (!targetContentArea.model.count) {
                    emptyCa = true;
                } 

                for (let i = 0; i < sourceContentArea.model.count; i++) {
                    dojo.when(sourceContentArea.model.getChildByIndex(i), lang.hitch(this, function (contentData) {
                        targetContentArea.model.modify(lang.hitch(this, function () {
                            targetContentArea.model.addChild(contentData);
                        }));

                        //console.log("ContentArea copy from " + sourceContentArea.name + " to " + targetContentArea.name, contentData);
                    }));
                }
                //console.log("ContentArea copy target[" + targetContentArea.name + "] count: " + targetContentArea.model.count);

                if (emptyCa) {
                    targetContentArea.set("value", targetContentArea.value);
                }

                this.focus();
            },

            caItemsList: null,
            _refreshContentAreas: function (value) {

                var contentArea1 = this.areas[0];
                var contentArea2 = this.areas[1];
                var contentArea3 = this.areas[2];
                var contentArea4 = this.areas[3];

                if (this.layoutType) {
                    if (value === '1c') {
                        // 4ce >> 1c
                        // left (1) >> center (1)
                        // center (2) == center (1)
                        // center2 (3) >> center (1)
                        // right (4) >> center (1)
                        if (contentArea4.model.count > 0) {
                            // center (2) == center (1)
                            if (contentArea2.model.count > 0) {
                                this._copyContentAreaItems(contentArea2, contentArea1);
                                this._removeContentAreaItems(contentArea2);
                            }
                            // center2 (3) >> center (1)
                            if (contentArea3.model.count > 0) {
                                this._copyContentAreaItems(contentArea3, contentArea1);
                                this._removeContentAreaItems(contentArea3);
                            }
                            // right (4) >> center (1)
                            this._copyContentAreaItems(contentArea4, contentArea1);
                            this._removeContentAreaItems(contentArea4);

                            return;
                        }

                        // 3ce || 3clm || 3cv >> 1c
                        // left(1) >> center(1)
                        // center (2) == center (1)
                        // right (3) >> center (1)
                        if (contentArea3.model.count > 0) {
                            // center (2) == center (1)
                            if (contentArea2.model.count > 0) {
                                this._copyContentAreaItems(contentArea2, contentArea1);
                                this._removeContentAreaItems(contentArea2);
                            }
                            // right (3) >> center (1)
                            this._copyContentAreaItems(contentArea3, contentArea1);
                            this._removeContentAreaItems(contentArea3);

                            return;
                        }

                        // 2ce || 2cll || 2clr || 2cv >> 1c
                        if (contentArea2.model.count > 0) {
                            this._copyContentAreaItems(contentArea2, contentArea1);
                            this._removeContentAreaItems(contentArea2);

                            return;
                        }
                    }

                    if (value === '2ce' || value === '2clr' || value === '2cll' || value === '2cv') {
                        if (contentArea4.model.count > 0) {

                            // 4 column >> 2 column
                            // left (1) >> left (1)
                            // center (2) >> left (1)
                            // center2 (3) >> right (2)
                            // right (4) >> right (2)

                            // center (2) >> left (1)
                            if (contentArea2.model.count > 0) {
                                this._copyContentAreaItems(contentArea2, contentArea1);
                                // empty CA2
                                this._removeContentAreaItems(contentArea2);
                            }
                            // center2 (3) >> right (2)
                            if (contentArea3.model.count > 0) {
                                this._copyContentAreaItems(contentArea3, contentArea2);
                                // empty CA3
                                this._removeContentAreaItems(contentArea3);
                            }
                            // right (4) >> right (2)
                            this._copyContentAreaItems(contentArea4, contentArea2);
                            // empty CA4
                            this._removeContentAreaItems(contentArea4);

                            return;
                        }

                        if (contentArea3.model.count > 0) {
                            // 3 column >> 2ce || 2cll
                            // left (1) >> left (1)
                            // center (2) >> left (1)
                            // right (3) >> right (2)

                            if (value === '2ce' || value === '2cll') {
                                // center (2) >> left (1)
                                if (contentArea2.model.count > 0) {
                                    this._copyContentAreaItems(contentArea2, contentArea1);
                                    // empty CA2
                                    this._removeContentAreaItems(contentArea2);
                                }
                                // right (3) >> right (2)
                                this._copyContentAreaItems(contentArea3, contentArea2);
                                // empty CA3
                                this._removeContentAreaItems(contentArea3);

                                return;
                            }

                            // 3 column >> 2clr (2cv)
                            // left (1) >> left (1)
                            // center (2) >> right (2)
                            // right (3) >> left (1)

                            // right (3) >> left (1)
                            this._copyContentAreaItems(contentArea3, contentArea1);
                            // empty CA3
                            this._removeContentAreaItems(contentArea3);

                            return;
                        }
                    }

                    if (value === '3ce' || value === '3clm' || value === '3cv') {
                        // 4ce || 4cv >> 3ce || 3clm || 3cv
                        // left (1) >> left (1)
                        // center (2) >> center (2)
                        // center2 (3) >> center (2)
                        // right (4) >> right (3)

                        if (contentArea4.model.count < 1)
                            return;

                        // center2 (3) >> center (2)
                        if (contentArea3.model.count > 0) {
                            this._copyContentAreaItems(contentArea3, contentArea2);
                            this._removeContentAreaItems(contentArea3);
                        }

                        // right (4) >> right (3)
                        if (contentArea4.model.count > 0) {
                            this._copyContentAreaItems(contentArea4, contentArea3);
                            this._removeContentAreaItems(contentArea4);
                        }
                    }
                }
                if (value === '1c' || value === '1cw') {
                    // 4ce >> 1c || 1cw
                    // side (1) >> main (1)
                    // main (2) == main (1)
                    // main2 (3) >> main (1)
                    // side2 (4) >> main (1)
                    if (contentArea4.model.count > 0) {
                        // main (2) == main (1)
                        if (contentArea2.model.count > 0) {
                            this._copyContentAreaItems(contentArea2, contentArea1);
                            this._removeContentAreaItems(contentArea2);
                        }
                        // main2 (3) >> main (1)
                        if (contentArea3.model.count > 0) {
                            this._copyContentAreaItems(contentArea3, contentArea1);
                            this._removeContentAreaItems(contentArea3);
                        }
                        // side2 (4) >> main (1)
                        this._copyContentAreaItems(contentArea4, contentArea1);
                        this._removeContentAreaItems(contentArea4);

                        return;
                    }

                    // 3ce || 3clm >> 1c || 1cw
                    // side (1) >> main (1)
                    // main (2) == main (1)
                    // side2 (3) >> main (1)
                    if (contentArea3.model.count > 0) {
                        // main (2) == main (1)
                        if (contentArea2.model.count > 0) {
                            this._copyContentAreaItems(contentArea2, contentArea1);
                            this._removeContentAreaItems(contentArea2);
                        }
                        // side2 (3) >> main (1)
                        this._copyContentAreaItems(contentArea3, contentArea1);
                        this._removeContentAreaItems(contentArea3);

                        return;
                    }

                    // 2ce >> 1c || 1cw
                    // 2cll || 2clr >> 1c || 2cw
                    if (contentArea2.model.count > 0) {
                        this._copyContentAreaItems(contentArea2, contentArea1);
                        this._removeContentAreaItems(contentArea2);

                        return;
                    }
                }

                if (value === '2ce' || value === '2clr' || value === '2cll') {
                    if (contentArea4.model.count > 0) {

                        // 4ce >> 2ce
                        // side (1) >> side (1)
                        // main (2) >> side (1)
                        // main2 (3) >> side2 (2)
                        // side2 (4) >> side2 (2)
                        if (value === '2ce') {
                            // main (2) >> side (1)
                            if (contentArea2.model.count > 0) {
                                this._copyContentAreaItems(contentArea2, contentArea1);
                                // empty CA2
                                this._removeContentAreaItems(contentArea2);
                            }
                            // main2 (3) >> side2 (2)
                            if (contentArea3.model.count > 0) {
                                this._copyContentAreaItems(contentArea3, contentArea2);
                                // empty CA3
                                this._removeContentAreaItems(contentArea3);
                            }
                            // side2(4) >> side2(2)
                            this._copyContentAreaItems(contentArea4, contentArea2);
                            // empty CA4
                            this._removeContentAreaItems(contentArea4);

                            return;
                        }

                        // 4ce >> 2clr || 2cll
                        // side (1) >> side (1)
                        // main (2) >> main (2)
                        // main2 (3) >> main (2)
                        // side2 (4) >> side (1)

                        // main2 (3) >> main (1)
                        if (contentArea3.model.count > 0) {
                            this._copyContentAreaItems(contentArea3, contentArea2);
                            // empty CA3
                            this._removeContentAreaItems(contentArea3);
                        }
                        // side2 (4) >> side (1)
                        this._copyContentAreaItems(contentArea4, contentArea1);
                        // empty CA4
                        this._removeContentAreaItems(contentArea4);

                        return;
                    }

                    if (contentArea3.model.count > 0) {
                        // 3ce || 3clm >> 2ce
                        // side (1) >> side (1)
                        // main (2) >> side (1)
                        // side2 (3) >> side2 (2)

                        if (value === '2ce') {
                            // main (2) >> side (1)
                            if (contentArea2.model.count > 0) {
                                this._copyContentAreaItems(contentArea2, contentArea1);
                                // empty CA2
                                this._removeContentAreaItems(contentArea2);
                            }
                            // side2 (3) >> side2 (2)
                            this._copyContentAreaItems(contentArea3, contentArea2);
                            // empty CA3
                            this._removeContentAreaItems(contentArea3);

                            return;
                        }

                        // 3ce || 3clm >> 2clr || 2cll
                        // side (1) >> side (1)
                        // main (2) >> main (2)
                        // side2 (3) >> side (1)

                        // side2 (3) >> side (1)
                        this._copyContentAreaItems(contentArea3, contentArea1);
                        // empty CA3
                        this._removeContentAreaItems(contentArea3);

                        return;
                    }
                }

                if (value === '3ce' || value === '3clm') {
                    // 4ce >> 3ce || 3clm
                    // side (1) >> side (1)
                    // main (2) >> main (2)
                    // main2 (3) >> main (2)
                    // side2 (4) >> side2 (3)

                    if (contentArea4.model.count < 1)
                        return;

                    // main2 (3) >> main (2)
                    if (contentArea3.model.count > 0) {
                        this._copyContentAreaItems(contentArea3, contentArea2);
                        this._removeContentAreaItems(contentArea3);
                    }

                    // side2 (4) >> side2 (3)
                    if (contentArea4.model.count > 0) {
                        this._copyContentAreaItems(contentArea4, contentArea3);
                        this._removeContentAreaItems(contentArea4);
                    }
                }
            },
            _loadContentAreas: function () {
                this.inherited(arguments);
                var items = query(".epi-form-container__section__row--field");
                this.areas = [];
                var caDijit;
                for (var i = 0; i < items.length; i++) {
                    if (items[i].innerText.trimStart().startsWith("First")) {
                        caDijit = dijit.byId(items[i].id);
                        this.areas.push(dijit.byId(caDijit.containerNode.childNodes[3].id));
                    }
                    if (items[i].innerText.trimStart().startsWith("Second")) {
                        caDijit = dijit.byId(items[i].id);
                        this.areas.push(dijit.byId(caDijit.containerNode.childNodes[3].id));
                    }
                    if (items[i].innerText.trimStart().startsWith("Third")) {
                        caDijit = dijit.byId(items[i].id);
                        this.areas.push(dijit.byId(caDijit.containerNode.childNodes[3].id));
                    }
                    if (items[i].innerText.trimStart().startsWith("Fourth")) {
                        caDijit = dijit.byId(items[i].id);
                        this.areas.push(dijit.byId(caDijit.containerNode.childNodes[3].id));
                        //break;
                    }
                }
                //console.log("areas : ", this.areas);
            },

            _loadCssFile: function () {
                var $ = document;
                var cssId = 'LayoutPicker';
                if (!$.getElementById(cssId)) {
                    var head = $.getElementsByTagName('head')[0];
                    var link = $.createElement('link');
                    link.id = cssId;
                    link.rel = 'stylesheet';
                    link.type = 'text/css';
                    link.href = '/scripts/editors/themes/pagelayoutpickereditor.css';
                    link.media = 'all';
                    head.appendChild(link);
                }
            },
            _initLayouts: function () {
                var layoutPicker = this.pageLayoutPickerList;
                this.layoutType = this.selections.length === 10 ? true : false;
                var type = this.layoutType;
                array.forEach(this.selections, function (selection) {
                    var layoutImage = type
                        ? getInnerLayoutImage(selection.value)
                        : getLayoutImage(selection.value);
                    domConstruct.create('a', { href: '#', title: selection.text, 'data-layout': selection.value, style: 'background-image: url("' + layoutImage + '");' }
                        , domConstruct.create('li', null, layoutPicker));
                });
            }
        });
    });

function getLayoutImage(layout) {
    if (layout === '1c') return '/icons/score/epi_score128_page_1col.png';
    if (layout === '1cw') return '/icons/score/epi_score128_page_1colwidescreen.png';
    if (layout === '2ce') return '/icons/score/epi_score128_page_2colequal.png';
    if (layout === '2clr') return '/icons/score/epi_score128_page_2collargeright.png';
    if (layout === '2cll') return '/icons/score/epi_score128_page_2collargeleft.png';
    if (layout === '3ce') return '/icons/score/epi_score128_page_3colequal.png';
    if (layout === '3clm') return '/icons/score/epi_score128_page_3colwidemiddle.png';
    if (layout === '4ce') return '/icons/score/epi_score128_page_4colequal.png';
    return null;
};

function getInnerLayoutImage(layout) {
    if (layout === '1c') return '/Static/gfx/layouts/epi_score128_structure_1col.png';
    if (layout === '2ce') return '/Static/gfx/layouts/epi_score128_structure_2colEqual.png';
    if (layout === '2clr') return '/Static/gfx/layouts/epi_score128_structure_2colLargeRight.png';
    if (layout === '2cll') return '/Static/gfx/layouts/epi_score128_structure_2colLargeLeft.png';
    if (layout === '2cv') return '/Static/gfx/layouts/epi_score128_structure_2col_variable.png';
    if (layout === '3ce') return '/Static/gfx/layouts/epi_score128_structure_3colEqual.png';
    if (layout === '3clm') return '/Static/gfx/layouts/epi_score128_structure_3colLargeMiddle.png';
    if (layout === '3cv') return '/Static/gfx/layouts/epi_score128_structure_3col_variable.png';
    if (layout === '4ce') return '/Static/gfx/layouts/epi_score128_structure_4colEqual.png';
    if (layout === '4cv') return '/Static/gfx/layouts/epi_score128_structure_4col_variable.png';
    return null;
};

function getContentAreaByLayoutCode(layout) {
    if (layout === '1c') return 0;
    if (layout === '1cw') return 0;
    if (layout === '2ce') return 1;
    if (layout === '2clr') return 1;
    if (layout === '2cll') return 1;
    if (layout === '3ce') return 2;
    if (layout === '3clm') return 2;
    if (layout === '4ce') return 3;
    return null;
};

function getContentAreaByInnerLayoutImage(layout) {
    if (layout === '1c') return 0;
    if (layout === '2ce') return 1;
    if (layout === '2clr') return 1;
    if (layout === '2cll') return 1;
    if (layout === '2cv') return 1;
    if (layout === '3ce') return 2;
    if (layout === '3clm') return 2;
    if (layout === '3cv') return 2;
    if (layout === '4ce') return 3;
    if (layout === '4cv') return 3;
    return null;
};
