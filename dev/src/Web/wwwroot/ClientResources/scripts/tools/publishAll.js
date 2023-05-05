define([
    "dojo/_base/declare",
    "dojo/topic",
    'epi/dependency',
    "epi-cms/contentediting/command/_ContentCommandBase",
    "epi-cms/contentediting/ContentActionSupport"
],

    function (declare, topic, dependency, _ContentCommandBase, ContentActionSupport) {

        return declare([_ContentCommandBase], {

            name: "PublishAll",
            label: "RePublish All",
            tooltip: "Publish current page and all blocks.",
            iconClass: "epi-iconPublishProject", //Define your own icon css class here. -> http://ux.episerver.com/#icons

            _contentActionSupport: ContentActionSupport,
            _topic: topic,

            _execute: function () {
                var me = this;
                var req = new XMLHttpRequest();

                req.open("POST", "/api/data/publish?contentLink=" + me.model.contentLink, true);
                req.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

                req.onload = function () {
                    if (req.status >= 200 && req.status < 400) {
                        console.log(JSON.parse(req.responseText));
                        me._topic.publish("/epi/shell/context/request", { uri: me.model.contentData.uri },
                                {
                                    sender: this,
                                    viewName: this.view,
                                    forceContextChange: true,
                                    forceReload: true
                                });
                    } else {
                        console.log(req.status);
                    }
                }

                req.send();
            },

            _onModelChange: function () {
               var me = this;
                if (me.model) {
                    me._setVisibility(me);
                }
            },

            _getCanExecute: function (contentData, versionStatus, me) {
                return contentData.publishedBy !== null && contentData.publishedBy !== undefined && // This condition indicates that the content has published version.
                    contentData.status !== versionStatus.Published &&
                    contentData.status !== versionStatus.Expired &&     // Expired content is basically Published content with a expireDate set to the past
                    me._contentActionSupport.hasAccess(contentData.accessMask, me._contentActionSupport.accessLevel.Publish); // Ensure has delete action to the user
            },

            _setVisibility: function (me) {
                var contentData = me.model.contentData,
                    status = contentData.status,
                    versionStatus = me._contentActionSupport.versionStatus;

                var isAvailable = ((status === versionStatus.CheckedOut) ||
                        (status === versionStatus.Rejected) ||
                        ((status === versionStatus.Published || status === versionStatus.Expired) &&
                        contentData.isCommonDraft)) &&
                    ((contentData.currentLanguageBranch !== null) &&
                    (contentData.currentLanguageBranch.languageId === me.model.currentContentLanguage));  // MAR-1079
                me.set("isAvailable", isAvailable);
                me._setCanExecute(me);
            },

            _setCanExecute: function (me) {
                var contentData = me.model.contentData,
                    status = contentData.status,
                    versionStatus = me._contentActionSupport.versionStatus;

                //Executable when available and not published, have published version and have edit access right
                me.set("canExecute", me.get("isAvailable") && me._getCanExecute(contentData, versionStatus, me));
            }
        });
    });
