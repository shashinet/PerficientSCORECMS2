define([
    "dojo/_base/array",
    "dojo/_base/connect",
    "dojo/_base/declare",
    "dojo/_base/lang",

    "dijit/_CssStateMixin",
    "dijit/_Widget",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",
    "dijit/dijit", // loads the optimized dijit layer
    "dijit/Calendar",

    "epi/shell/widget/DateTimeSelectorDropDown",
    "epi/shell/widget/DateTimeSelector",
    "epi/shell/widget/TimeSpinner",
    "epi/epi"
],
function (
    array,
    connect,
    declare,
    lang,

    _CssStateMixin,
    _Widget,
    _TemplatedMixin,
    _WidgetsInTemplateMixin,
    dijit,
    Calendar,

    DateTextBox,
    DateTimeSelector,
    TimeSelector,
    epi
) {
    // Modified date time widget to force Optimizely to calculate UTC times instead of taking user input as UTC directly without converting
    // https://web.archive.org/web/20210923203438/http://fellow.aagaardrasmussen.dk/2016/04/23/get-predictive-time-zones-in-datetime-properties/
    return declare("score/editors/utcDateTimeTextBox", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin, _CssStateMixin], {

        templateString: "<div class=\"dijitInline\"> \
                            <input data-dojo-attach-point=\"dateTextBox\" data-dojo-type=\"epi/shell/widget/DateTimeSelectorDropDown\" required=\"true\"/> \
                        </div>",

        selector: null,        

        _onLoad: function () {

            this.dateTextBox.constraints = {
                formatLength: "short",
                fullYear: "true"
            };

            switch (this.selector) {
                case 'date':
                    this.dateTextBox.popupClass = Calendar;
                    this.dateTextBox.constraints.selector = this.selector
                    break;

                case 'time':
                    this.dateTextBox.popupClass = TimeSelector;
                    this.dateTextBox.constraints.selector = this.selector                        
                    break;

                default:
                    this.dateTextBox.popupClass = DateTimeSelector;
                    break;
            }

            this.inherited(arguments);

            var dd = new Date(this.value);

            if (dd != undefined) {

                if (!isNaN(dd.getTime())) {

                    // Convert to local time
                    // DateTime arithmetic is done in milliseconds but getTimezoneOffset returns minutes
                    dd = new Date(dd.getTime() + (dd.getTimezoneOffset() * 60000));
                }
            }
            var _this = this;

            this.dateTextBox.set('value', dd);

            dojo.connect(this.dateTextBox, "onChange", function (value) {

                _this._setValue(value);

            });
        },
        // Setter for value property
        _setValueAttr: function (value) {

            this._setValue(value, true);

            this._onLoad();
        },

        onChange: function (value) {
            console.log(value);
            // Event that tells EPiServer when the widget's value has changed.
        },

        _getValueAttr: function () {

            var date = this.dateTextBox.get('value');

            if (date == undefined)
                return date;

            if (isNaN(date.getTime()))
                return date;

            // Convert to UTC time to make it locale independent
            // DateTime arithmetic is done in milliseconds but getTimezoneOffset returns minutes
            date = new Date(date.getTime() - (date.getTimezoneOffset() * 60000));
            
            return date;
        },

        _setReadOnlyAttr: function (value) {
            this._set("readOnly", value);
        },

        _setValue: function (value) {
            if (this._started && epi.areEqual(this.value, value)) {
                return;
            }

            // set value to this widget (and notify observers)
            this._set("value", value);
        }
    });
});
