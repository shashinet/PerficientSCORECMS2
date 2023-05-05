define([
    // General application modules
    "dojo/_base/declare",
    "dojo/_base/event",
    "dojo/_base/lang",
    "dojo/dom-class",
    "dojo/dom-style",
    "dojo/on",
    "dojo/when",
    "dojo",
    "epi/_Module",
    "dojo/topic",
    "dojo/query",

    // Parent classes
    "dijit",
    "dijit/_Widget",
    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",

    "epi/shell/_ContextMixin",
    "epi-cms/widget/overlay/ContentArea"

], function (
    // General application modules
    declare,
    event,
    lang,
    domClass,
    domStyle,
    on,
    when,
    dojo,
    _Module,
    topic,
    query,

    // Parent classes
    dijit,
    _Widget,
    _WidgetBase,
    _TemplatedMixin,
    _WidgetsInTemplateMixin,

    _ContextMixin,
    _contentArea
) {
    return declare([_contentArea],
        {
            _menu: null,

            initialize: function () {
                this.inherited(arguments);
            },

            //startup is one of the lifecycle methods in a widget.
            startup: function () {
                this.inherited(arguments);
            },

            buildRendering: function () {
                this.inherited(arguments);
            },

            postCreate: function () {
                this.inherited(arguments);
                var self = this;

                var items = query(".epi-overlay-block", this.domNode);
                var html = "<span class=\"epi-editor-menu-edit\" id=\"epi-editor-menu-edit\"></span>";

                for (var i = 0; i < items.length; i++) {
                    dojo.place(html, items[i].children[1], "before");
                }

                on(query(".epi-editor-menu-edit", this.domNode), "click", function (e) {
                    //console.log(e.currentTarget);

                    var element = dijit.byId(e.currentTarget.parentElement.id);

                    self._callApi(element, e.x, e.y);

                    e.stopImmediatePropagation();
                });
            },

            _onBlur: function () {
                //this._hideAllEditMenus();
                //document.body.children[0].style.visibility = "hidden";
            },

            showHideMenu: function (data) {
                console.log(data);
            },

            _generateMenuMarkup: function (element, data, x, y) {
                //var placementElement = query(".menu-dropdown-content", element.domNode)[0];
                //placementElement.style.display = "";

                var placementElement = query("body");
                var html = "<div class=\"menu-dropdown-content\" id=\"menu-dropdown-content\"><div class=\"epi-editor-header\"><h4>Block Hierarchy</h4><div class=\"epi-editor-close\"></div></div><div class=\"epi-editor-menu-edit-root\">";

                html += "<div class=\"epi-editor-element-wrapper\">";

                if (data.Children.length > 0) {
                    html += "<span class=\"epi-editor-menu-edit-child-icon\"></span>"; // ? // ?
                }

                html += this._generateMenuItemMarkup(data);

                html += "</div>";

                if (data.Children.length > 0) {
                    html += this._generateMenuSubMenu(data.Children);
                }

                html += "</div></div>";

                var dropdownMenu = query(".menu-dropdown-content");
                var menu = null;
                if (dropdownMenu.length > 0) {
                    menu = dojo.place(html, dropdownMenu[0], "replace");
                } else {
                    menu = dojo.place(html, placementElement[0], 0);
                }

                menu.style.position = "absolute";
                menu.style.left = x + "px";
                menu.style.top = "33vh";
                menu.style.zIndex = 9000;

                //this._hideAllEditMenus();
                //placementElement.style.visibility = "";
                //document.body.children[0].style.visibility = "";

                on(query(".epi-editor-menu-edit-child-icon", menu),
                    "click",
                    function (ev) {
                        console.log(ev.currentTarget.parentNode.children[1].innerText);
                        $(this).parent().parent().toggleClass('open');
                        ev.stopImmediatePropagation();
                    });

                var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
                on(query(".epi-editor-header", menu), "mousedown", function (ev) {
                    ev.preventDefault();
                    // get the mouse cursor position at startup:
                    pos3 = ev.clientX;
                    pos4 = ev.clientY;
                    document.onmouseup = function closeDragElement() {
                        /* stop moving when mouse buttonLink is released:*/
                        document.onmouseup = null;
                        document.onmousemove = null;
                    };
                    // call a function whenever the cursor moves:
                    document.onmousemove = function elementDrag(ev) {
                        ev.preventDefault();
                        // calculate the new cursor position:
                        pos1 = pos3 - ev.clientX;
                        pos2 = pos4 - ev.clientY;
                        pos3 = ev.clientX;
                        pos4 = ev.clientY;
                        // set the element's new position:
                        menu.style.top = (menu.offsetTop - pos2) + "px";
                        menu.style.left = (menu.offsetLeft - pos1) + "px";
                    };
                });

                on(query(".epi-editor-close", menu), "click", function (ev) {
                    menu.style.visibility = "hidden";
                });

                on(query(".edit-element", menu), "click", function (ev) {
                    menu.style.visibility = "hidden";
                });

                window.addEventListener("click", function (e) {
                    if (!menu.contains(e.target)) {
                        menu.style.visibility = "hidden";
                    }
                });
            },
            _generateMenuSubMenu: function (elements) {
                var html = "<div class=\"epi-editor-menu-edit-submenu\">";

                for (var i = 0; i < elements.length; i++) {
                    html += "<div class=\"epi-editor-menu-edit-submenu-root\">";

                    html += "<div class=\"epi-editor-element-wrapper\">";

                    if (elements[i].Children.length > 0) {
                        html += "<span class=\"epi-editor-menu-edit-child-icon\"></span>"; // ? // ?
                    }

                    html += this._generateMenuItemMarkup(elements[i]);

                    html += "</div>";

                    if (elements[i].Children.length > 0) {
                        html += this._generateMenuSubMenu(elements[i].Children);
                    }

                    html += "</div>";
                }

                html += "</div>";

                return html;
            },
            _generateMenuItemMarkup: function (element) {
                return "<a class='edit-element' href=\"" + element.EditUrl + "\">" + element.Name + " (Type: " + element.Type + ")</a>";
            },

            _hideAllEditMenus: function () {
                var editMenus = $(".menu-dropdown-content");
                for (var i = 0; i < editMenus.length; i++) {
                    editMenus[i].style.visibility = "hidden";
                }
            },

            _callApi: function (element, x, y) {
                var me = this;
                var req = new XMLHttpRequest();

                var data = {
                    Id: element.viewModel.contentLink,
                    ContentType: element.viewModel.contentTypeName,
                    BaseUri: element.domNode.baseURI.substring(0, element.domNode.baseURI.indexOf('EPiServer')),
                    Language: element.viewModel._currentContext.language
                };

                req.open("POST", "/api/blockhierarchy/editmenu?query=" + JSON.stringify(data), true);
                req.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

                req.onload = function () {
                    if (req.status >= 200 && req.status < 400) {
                        //console.log(JSON.parse(req.responseText));
                        me._generateMenuMarkup(element, JSON.parse(req.responseText), x, y);
                    } else {
                        console.log(req.status);
                    }
                }

                req.send();
            }
        });
});
