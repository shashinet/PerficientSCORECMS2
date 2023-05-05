define([
    "dojo",
    "dojo/when",
    "dojo/request",
    "epi/dependency",
    "epi/shell/widget/dialog/Dialog",
    "epi-cms/widget/ContentSelectorDialog"
], function (dojo, dojoWhen, request, dependency, Dialog, ContentSelectorDialog) {

    function ContentService() { }

    ContentService.prototype.GetContentByReference = function getContentByReference(contentReference, callback) {
        var registry = dependency.resolve("epi.storeregistry");
        var store = registry.get("epi.cms.content.light");
        dojo.when(store.get(contentReference), function (returnValue) {
            callback(returnValue);
        });
    };

    ContentService.prototype.OpenImageSelector = function openImageSelector(callback) {
        var module = this;

        var contentRepositoryDescriptors = dependency.resolve("epi.cms.contentRepositoryDescriptors");
        var roots = contentRepositoryDescriptors["media"].roots;
        var imageSelectorDialog = new ContentSelectorDialog({
            canSelectOwnerContent: false,
            showButtons: false,
            roots: roots,
            allowedTypes: ["episerver.core.icontentimage"],
            showAllLanguages: true
        });

        var dialog = new Dialog({
            title: "Select a image",
            dialogClass: "epi-dialog-landscape",
            content: imageSelectorDialog
        });

        dojo.connect(dialog, "onExecute", function () {
            module.GetContentByReference(dialog.content._contentRef, callback);
        });

        dialog.show();
    };

    ContentService.prototype.GetContentDataFromPreviewUrl = function getContentDataFromPreviewUrl(previewUrl, callback) {
        var module = this;

        var startIndex = previewUrl.indexOf(",") + 2;
        var endIndex = previewUrl.indexOf("?");
        var length = endIndex * 1 - startIndex;
        var contentId = previewUrl.substr(startIndex, length);

        module.GetContentByReference(contentId,
            function (epiImage) {
                console.log("From DND: ", epiImage);
                callback(epiImage);
            });
    };

    ContentService.prototype.ParseUrl = function ParseUrl(url) {
        var parser = document.createElement('a'),
            searchObject = {},
            queries,
            split,
            i;

        // Let the browser do the work
        parser.href = url.toLowerCase();

        // Convert query string to object
        queries = parser.search.replace(/^\?/, '').split('&');
        for (i = 0; i < queries.length; i++) {
            split = queries[i].split('=');
            searchObject[split[0]] = split[1];
        }

        return {
            protocol: parser.protocol,
            host: parser.host,
            hostname: parser.hostname,
            port: parser.port,
            pathname: parser.pathname,
            search: parser.search,
            searchObject: searchObject,
            hash: parser.hash
        };
    };

    return new ContentService();

});
