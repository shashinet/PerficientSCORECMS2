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

    return declare("score.editors.alignmentEditor", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin, _CssStateMixin, _ValueRequiredMixin], {

      templateString: "<div class=\"dijitInline\">\
                            <ul data-dojo-attach-point=\"alignmentPickerList\" class=\"alignmentPickerList\">\
                            </ul>\
                        </div>",
      intermediateChanges: false,
      value: null,
      pickedAlignment: null,
      onPageEditMode: false,
      onChange: function (value) {
        // Event
      },
      postCreate: function () {

        this._initAlignments();
        this.inherited(arguments);
        this._loadCssFile();

        this.pickedAlignment = this.value;
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
      _markChosenAlignmentInList: function (value) {

        var listItems = query(this.alignmentPickerList).query("li");
        var tmpAlignment = "";
        for (var i = 0; i < listItems.length; i++) {
          tmpAlignment = query(listItems[i]).query("a")[0].getAttribute("data-alignment");
          if (value !== null && value === tmpAlignment) {
            listItems[i].setAttribute("class", "selectedAlignment");
          } else {
            listItems[i].setAttribute("class", "");
          }
        }
        if (listItems.length === 0) {
          this.onPageEditMode = true;
        }
      },
      _bindEvents: function (myself) {

        on(query(this.alignmentPickerList).query(".alignment-select"), "click", function (e) {
          myself._chooseAlignment(e.currentTarget, myself);
          myself._markChosenAlignmentInList(e.currentTarget.getAttribute("data-alignment"));

          e.preventDefault();
        });

        on(query(this.alignmentPickerList).query(".alignment-clear"), "click", function (e) {
          myself._setValue("", true);
          myself._markChosenAlignmentInList(null);
          e.preventDefault();
        });

      },
      _chooseAlignment: function (clickedItem, myself) {
        var alignment = clickedItem.getAttribute("data-alignment");
        myself._setValue(alignment, true);
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
          this.pickedAlignment = value;
          this._markChosenAlignmentInList(this.pickedAlignment);
        }

        if (this._started && this.validate()) {
          // Trigger change event
          this.onChange(value);
        }
      },
      _loadCssFile: function () {
        var $ = document;
        var cssId = 'AlignmentPicker';
        if (!$.getElementById(cssId)) {
          var head = $.getElementsByTagName('head')[0];
          var link = $.createElement('link');
          link.id = cssId;
          link.rel = 'stylesheet';
          link.type = 'text/css';
          link.href = '/ClientResources/scripts/editors/themes/alignmentPickerEditor.css';
          link.media = 'all';
          head.appendChild(link);
        }
      },
      _initAlignments: function () {
        var alignmentPicker = this.alignmentPickerList;
        array.forEach(this.selections, function (selection) {
          domConstruct.create('a', { href: '#', title: selection.text, 'data-alignment': selection.value, class: 'alignment-select ' + selection.value }
            , domConstruct.create('li', null, alignmentPicker));
        });

        domConstruct.create('li',
          { innerHTML: "<a href='#' class='alignment-clear'>X</a>" },
          alignmentPicker);

        if (this.onPageEditMode) {
          this.onPageEditMode = false;
          this._markChosenAlignmentInList(this.value);
        }
      }
    });
  });
