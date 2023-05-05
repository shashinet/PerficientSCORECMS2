define([
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

        "epi/epi",
        "epi/shell/widget/_ValueRequiredMixin"
],
    function (
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

        epi,
        _ValueRequiredMixin
) {
        return declare("score.editors.DropdownEditor", [
            _Widget, _TemplatedMixin, _WidgetsInTemplateMixin, _CssStateMixin, _ValueRequiredMixin
        ], {
            templateString: "<div class=\"dijitInline\">\
                                <ul data-dojo-attach-point=\"dropdownList\" class=\"dropdownList\">\
                                </ul>\
                            </div>",

            intermediateChanges: false,
            value: null,
            pickedItem: null,
            onPageEditMode: false,
            onChange: function (value) {
                // Event
            },
            postCreate: function () {

                this._initList();
                this.inherited(arguments);
                this._loadCssFile();

                this.pickedItem = this.value;
                this._bindEvents(this);

            },
            startup: function () {
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
            _markChosenItemInList: function (value) {

                var listItems = query(this.dropdownList).query("li");
                var tmpItem = "";
                for (var i = 0; i < listItems.length; i++) {
                    tmpItem = query(listItems[i]).query("a")[0].getAttribute("data-item");
                    if (value === tmpItem) {
                        listItems[i].setAttribute("class", "selectedItem");
                    } else {
                        listItems[i].setAttribute("class", "");
                    }
                }
                if (listItems.length === 0) {
                    this.onPageEditMode = true;
                }
            },
            _bindEvents: function (myself) {

                on(query(this.dropdownList).query("li"), "click", function (e) {
                    myself._chooseItem(e.currentTarget.childNodes[0], myself);
                    myself._markChosenItemInList(e.currentTarget.childNodes[0].getAttribute("data-item"));

                    e.preventDefault();
                });

            },
            _chooseItem: function (clickedItem, myself) {
                var item = clickedItem.getAttribute("data-item");
                myself._setValue(item, true);
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
                    this.pickedItem = value;
                    this._markChosenItemInList(this.pickedItem);
                }

                if (this._started && this.validate()) {
                    // Trigger change event
                    this.onChange(value);
                }
            },
            _loadCssFile: function () {
                var $ = document;
                var cssId = 'ItemPicker';
                if (!$.getElementById(cssId)) {
                    var head = $.getElementsByTagName('head')[0];
                    var link = $.createElement('link');
                    link.id = cssId;
                    link.rel = 'stylesheet';
                    link.type = 'text/css';
                    link.href = '/ClientResources/scripts/editors/themes/dropdowneditor.css';
                    link.media = 'all';
                    head.appendChild(link);
                }
            },
            _initList: function () {
                var itemPicker = this.dropdownList;
                array.forEach(this.selections, function (selection) {
                    domConstruct.create('a', { href: '#', title: selection.text, innerHTML: selection.text, 'data-item': selection.value },
                            domConstruct.create('li', null, itemPicker));
                });

                if (this.onPageEditMode) {
                    this.onPageEditMode = false;
                    this._markChosenItemInList(this.value);
                }
            }
        });
    }
);
