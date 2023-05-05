define("custom-scripts/editors/phonevalidationtextbox", [
  "dojo/_base/declare",
  "dojo/_base/lang",
  "dijit/form/ValidationTextBox"
],
  function (
    declare,
    lang,
    ValidationTextBox
  ) {

    var module = declare([ValidationTextBox], {
      // summary:
      //    Represents the phone number input textbox.
      // tags:
      //    internal

      invalidMessage: "Invalid phone number",

      // addTel: Boolean
      //      If true the value will always be prepended
      //      with the tel protocol
      addTel: true,

      validator: function (value, constraints) {
        // summary:
        //      If true the value will always be prepended
        //      Validate the text input with telephone number validation.
        // tags:
        //		override

        value = value || "";

        if (!this.required && this._isEmpty(value)) {
          return true;
        }

        // replace escaped sequences to enable/simplify regexp validation (\@ or "everythingInHereIsEsc@ped"
        value = value.replace(/\\.{1}/g, "replaced").replace(/".*?"/g, "replaced");

        return module.validationRegex.test(value);
      },

      _getValueAttr: function () {

        var value = this.inherited(arguments);

        if (this.addTel) {
          // make sure the hyper link has mailto: prefix
          value = value ? lang.trim(value) : "";
          if (value && value.indexOf("tel:") !== 0) {
            value = "tel:" + value;
          }
        }

        return value;
      },

      _setValueAttr: function (value) {
        value = value ? value.replace("tel:", "") : "";

        this.inherited(arguments, [value]);
      }
    });

    // Simple and incomplete test for phone number likeness
    // only trying to stop the most common mistakes
    // This regex can be changed as required but needs to start
    // with an optional "tel:" (like so: (tel:)?)
    module.validationRegex = /^(tel:)?(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$/i;

    return module;

  });
