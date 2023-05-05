define([
  // General application modules
  'dojo/_base/declare',
  // Parent class
  'epi/shell/command/_CommandProviderMixin',
  // Other classes
  './saveTemplate'
], function (declare, _CommandProviderMixin, SaveTemplate) {

  return declare([_CommandProviderMixin], {

    constructor: function () {
      this.add('commands', new SaveTemplate());
    }
  });
});
