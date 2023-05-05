define([
  "dojo/_base/declare",
  "dojo/_base/array",
  "dojox/html/entities",
  "epi-cms/contentediting/editors/SelectionEditor"
],
  function (
    declare,
    array,
    entities,
    SelectionEditor
  ) {
    return declare("custom-scripts/editors/iconlibraryeditor", [SelectionEditor], {
      _setSelectionsAttr: function (newSelections) {

        this.set("options", array.map(newSelections, function (item) {
          let svghtml = "<img style='width:1.5rem;height:1.5rem' src='" + item.value + "' />";
          let html = entities.decode("<table style='width:20rem;'><tr><td>" + svghtml + "</td><td>" + item.text + "</td></tr></table>");
          return {
            label: html,
            value: item.value,
            selected: item.value === this.value || !item.value && !this.value
          };
        }, this));
      }
    });
  });
