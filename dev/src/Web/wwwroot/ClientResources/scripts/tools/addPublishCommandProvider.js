define([
    // General application modules
    'dojo/_base/declare',
    // Parent class
    'epi/shell/command/_CommandProviderMixin',
    // Other classes
    './publishAll'
], function (declare, _CommandProviderMixin, PublishAll) {

    return declare([_CommandProviderMixin], {

        constructor: function () {
            this.add('commands', new PublishAll());
        }
    });
});
