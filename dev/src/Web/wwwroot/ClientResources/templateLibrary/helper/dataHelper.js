define("templates/helper/dataHelper", [
  // dojo
  "dojo/_base/lang",
  "dojo/when",
  "dojo/request/xhr",
  "dojo/_base/Deferred",

  // epi
  "epi/dependency"
  ],

  function (
    // dojo
    lang,
    when,
    xhr,
    Deferred,

    // epi
    dependency
  ) {
    return {
      getTemplateRoot: function () {
        var deferred = new Deferred();
        xhr('/api/template/getTemplateRoot', {
          headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
          method: 'GET',
        }).then(
          function (rootId) {
            deferred.resolve(rootId);
          },
          function (err) {
            // Handle the error condition
            deferred.resolve(0);
          }
        );
        return deferred.promise;
      },

      contentTypeSupported: function (contentTypeId) {
        var deferred = new Deferred();
        xhr('/api/template/interfaceImplemented?contentTypeId=' + contentTypeId, {
          headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
          method: 'GET',
        }).then(
          function (isSupported) {
            deferred.resolve(isSupported);
          },
          function (err) {
            // Handle the error condition
            deferred.resolve('false');
          }
        );
        return deferred.promise;
      },

      getBaseType: function (contentTypeId) {
        var deferred = new Deferred();
        xhr('/api/template/getBaseType?contentTypeId=' + contentTypeId, {
          headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
          method: 'GET',
        }).then(
          function (baseType) {
            deferred.resolve(baseType);
          },
          function (err) {
            // Handle the error condition
            deferred.resolve("");
          }
        );
        return deferred.promise;
      },

      getContent: function (id) {
        // summary:
        //      Returns either the current content or a deferred which will resolve a content data as soon as it becomes available.
        // example:
        //      dojo.when(this.getCurrentContent(), function(content) {
        //          console.log("now we know we've got ", content);
        //      });
        // tags:
        //      protected
        if (!this.contentDataStore) {
          var registry = dependency.resolve("epi.storeregistry");
          this.contentDataStore = registry.get("epi.cms.contentdata");
        }

        var deferred = new Deferred();

        when(this.contentDataStore.get(id), function (contentData) {
          deferred.resolve(contentData);
        });

        return deferred.promise;
      },

      getContentTypeAssemblyName: function (contentId) {
        var deferred = new Deferred();
        xhr('/api/template/getContentTypeAssemblyName?contentId=' + contentId, {
          headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
          method: 'GET',
        }).then(
          function (name) {
            deferred.resolve(name);
          },
          function (err) {
            // Handle the error condition
            deferred.resolve('');
          }
        );
        return deferred.promise;
      }
    };
  });
