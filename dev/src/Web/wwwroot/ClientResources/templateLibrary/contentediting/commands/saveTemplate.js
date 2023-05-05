define([
  // dojo core
  'dojo',
  'dojo/_base/Deferred',
  'dojo/_base/declare',
  'dojo/topic',
  'epi/dependency',
  'dojo/request/xhr',
  'dojo/promise/all',

  // Opti
  'epi-cms/contentediting/command/_ContentCommandBase',
  'epi-cms/contentediting/ContentActionSupport',
  'epi/shell/DialogService',
  'epi-cms/widget/ContentSelectorDialog',

  // template library
  'templates/helper/dataHelper',

  // resources
  'templates/moduleSettings',
], function (
  dojo,
  Deferred,
  declare,
  topic,
  dependency,
  xhr,
  all,
  _ContentCommandBase,
  ContentActionSupport,
  DialogService,
  ContentSelectorDialog,
  dataHelper,
  ModuleSettings
) {
  return declare([_ContentCommandBase], {
    name: 'SaveTemplate',
    label: 'Save As Template',
    tooltip: 'Save the current content as a template.',
    iconClass: 'epi-iconCopy', //Define your own icon css class here. -> http://ux.episerver.com/#icons

    _contentActionSupport: ContentActionSupport,
    _topic: topic,

    _execute: function () {
      var self = this;
      var request = new XMLHttpRequest();
      var contentId = self.model.contentLink.split('_')[0];
      var contentTypeId = self.model.contentData.contentTypeID;

      this._openTemplateSelector(contentTypeId).then(function (folderID) {
        if (folderID && folderID > 0) {
          request.open(
            'POST',
            `/api/template/save?contentId=${contentId}` +
            `&contentTypeId=${contentTypeId}&folderId=${folderID}`,
            true
          );
          request.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

          request.onload = function () {
            if (request.status >= 200 && request.status < 400) {

              // refresh current edit view
              self._topic.publish(
                '/epi/shell/context/request',
                { uri: self.model.contentData.uri },
                {
                  sender: this,
                  viewName: this.view,
                  forceContextChange: true,
                  forceReload: true,
                }
              );

              // refresh content tree
              topic.publish("/epi/cms/contentdata/updated", {
                contentLink: request.responseText,
                recursive: true
              });

            } else {
              console.log(request.status);
              alert('Failed to save this content as template');
            }
          };

          request.send();
        }
      });
    },

    _onModelChange: function () {
      var self = this;
      if (self.model) {
        self._setVisibility(self);
      }
    },

    _getCanExecute: function (contentData, versionStatus, me) {
      return (
        contentData.publishedBy !== null &&
        contentData.publishedBy !== undefined && // This condition indicates that the content has published version.
        contentData.status !== versionStatus.Expired && // Expired content is basically Published content with a expireDate set to the past
        me._contentActionSupport.hasAccess(
          contentData.accessMask,
          me._contentActionSupport.accessLevel.Publish
        )
      ); // Ensure has delete action to the user
    },

    _setVisibility: function (me) {
      var contentId = me.model.contentLink.split('_')[0];

      xhr('/api/template/isSupported?id=' + contentId, {
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        method: 'GET',
      }).then(
        function (isSupported) {
          var isTrue = isSupported === 'true';
          me.set('isAvailable', isTrue);
          me._setCanExecute(me);
        },
        function (err) {
          // Handle the error condition
        }
      );
    },

    _createDialogContent: function (templateRoot, baseType) {
      // summary:
      //    Create dialog content
      // tags:
      //    protected
      var allowedTypes = [ModuleSettings.identifiers.folder];
      if (baseType === "block") {
        allowedTypes.push(ModuleSettings.identifiers.blockFolder);
      }
      else if (baseType === "page") {
        allowedTypes.push(ModuleSettings.identifiers.pageFolder);
      }

      return new ContentSelectorDialog({
        canSelectOwnerContent: false,
        showButtons: true,
        roots: [templateRoot],
        allowedTypes: allowedTypes,
        showAllLanguages: false,
      });
    },

    _setCanExecute: function (me) {
      var contentData = me.model.contentData,
        versionStatus = me._contentActionSupport.versionStatus;

      //Executable when available and not published, have published version and have edit access right
      me.set('canExecute', me.get('isAvailable') && me._getCanExecute(contentData, versionStatus, me));
    },

    _openTemplateSelector: function (contentTypeId) {
      var deferred = new Deferred();
      // check if content type is supported and there's already a template root
      all([dataHelper.getTemplateRoot(), dataHelper.getBaseType(contentTypeId)]).then(
        function (results) {
          var templateRoot = results[0];
          var baseType = results[1];
          if (templateRoot !== '0') {
            var folderInput = this._createDialogContent(templateRoot, baseType);

            DialogService.dialog({
              content: folderInput,
              title: 'Where would you like to save the template?',
              cancelActionText: 'Cancel',
              dialogClass: 'epi-dialog-portrait',
              destroyOnHide: true,
            })
              .then(
                function () {
                  var saveFolder = folderInput.get('value');

                  deferred.resolve(saveFolder);
                }.bind(this)
              )
              .otherwise(function () {
                // click skip
                deferred.resolve('0');
              });
          } else {
            // Not supported
            deferred.resolve('0');
          }
        }.bind(this)
      );

      return deferred.promise;
    }
  });
});
