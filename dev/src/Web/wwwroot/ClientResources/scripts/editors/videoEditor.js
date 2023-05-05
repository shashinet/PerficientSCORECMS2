define([
  "dojo",
  "dojo/on", // To connect events
  "dojo/_base/declare", // Used to declare the actual widget
  "dojo/_base/config", // Used to check if client code debugging is enabled
  "dojo/aspect", // Used to attach to events in an aspect-oriented way to inject behaviors
  "dojo/Deferred", // Used to allow code to wait for video block to be loaded
  "dojo/query",
  "dojo/request",

  "dijit/registry", // Used to get access to other dijits in the app
  "dijit/WidgetSet", // To be able to use 'byClass' when querying the dijit registry
  "dijit/_Widget", // Base class for all widgets
  "dijit/_TemplatedMixin", // Widgets will be based on an external template (string literal, external file, or URL request)
  "dijit/_WidgetsInTemplateMixin", // The widget will in itself contain additional widgets

  "epi/epi", // For example to use areEqual to compare property values
  "epi/shell/widget/_ValueRequiredMixin", // In order to check if the property is in a readonly state
  "epi/shell/widget/dialog/LightWeight", // Used to display the help message
  "epi/shell/_ContextMixin",

  "./util/async",

  'xstyle/css!./themes/videoEditorTemplate.css'
],
  function (
    dojo,
    on,
    declare,
    config,
    aspect,
    Deferred,
    query,
    request,

    registry,
    WidgetSet,
    _Widget,
    _TemplatedMixin,
    _WidgetsInTemplateMixin,

    epi,
    _ValueRequiredMixin,
    LightWeight,
    _ContextMixin,

    Async
  ) {
    return declare("score.editors.VideoEditor", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin, _ValueRequiredMixin, _ContextMixin],
      {
        intermediateChanges: false,
        onPropertyView: false,
        value: null,

        // Property settings (set by editor descriptor)
        apiKey: null,

        nextPageToken: '',
        prevPageToken: '',

        // Load HTML template from the same location as the widget
        templateString: dojo.cache("score", "./editors/templates/youTubeEditorTemplate.html"),

        // Event used to notify EPiServer that the property value has changed
        onChange: function (value) {
          // Event
          this.inherited(arguments);

          if (this.onPropertyView) {
            this._getVideoData(value.videoId);
          }
        },

        // Dojo event fired after all properties of a widget are defined, but before the fragment itself is added to the main HTML document
        postCreate: function () {
          // Call base implementation of postCreate, passing on any parameters
          this.inherited(arguments);

          this._bindEvents(this);
        },

        // Dojo event triggered after 'postCreate', for example when JS resizing needs to be done
        startup: function () {
          this.inherited(arguments);

          if (!this.apiKey) {
            console.warn(
              "YouTube API key not set, ensure custom editor setting 'apiKey' is set through editor descriptor");
          }

          this._initVideoValue();
        },

        // Dojo event triggered when widget is removed
        destroy: function () { },

        // Setter for value property (invoked by EPiServer on load)
        _setValueAttr: function (value) {
          this._setValue(value, true);
        },

        _setReadOnlyAttr: function (value) {
          this._set("readOnly", value);
        },
        _setIntermediateChangesAttr: function (value) {
          this._set("intermediateChanges", value);
        },

        _setValue: function (value) {
          //avoids running this if the widget already is started
          if (this._started && epi.areEqual(this.value, value)) {
            return;
          }

          // set value to this widget (and notify observers). 
          this._set("value", value);

          if (this._started && this.validate()) {
            // Trigger change event
            this.onChange(value);
          }
        },

        _initVideoValue: function () {
          console.log(this.value);
          // property view fix
          if (this.value === null) {
            this.onPropertyView = true;
          } else {
            this._getVideoData(this.value.videoId);
          }
        },

        _bindEvents: function (myself) {

          on(query(this.videoContainer).query("button"), "click", function (e) {
            myself._doSearch(myself.domNode.querySelector("#SearchQuery").value, myself, 0);
            e.preventDefault();
          });

          on(query(this.videoContainer).query("a.previous"), "click", function (e) {
            console.log("previous");
            myself._doSearch(myself.domNode.querySelector("#SearchQuery").value, myself, -1);
            e.preventDefault();
          });

          on(query(this.videoContainer).query("a.next"), "click", function (e) {
            console.log("next");
            myself._doSearch(myself.domNode.querySelector("#SearchQuery").value, myself, 1);
            e.preventDefault();
          });
        },

        _doSearch: function (query, myself, direction) {
          if (query !== '' && query !== null) {
            request.get("https://www.googleapis.com/youtube/v3/search",
              {
                query: {
                  'q': query,
                  'part': 'snippet',
                  'type': 'video',
                  'key': myself.apiKey,
                  'maxResults': 20,
                  'pageToken': ((direction < 0) ? myself.prevPageToken : myself.nextPageToken) || ''
                }
              }).then(
                function (response) {
                  var results = JSON.parse(response);

                  myself._renderResults(myself._convertVideoId(results));
                  myself._activatePagination(myself._convertVideoId(results));
                },
                function (error) {
                  //ToDo: add error rendering
                  console.log(error);
                }
              );
          }
        },

        _getVideoData: function (videoId) {
          var self = this;

          if (videoId !== '' && videoId !== null) {
            request.get("https://www.googleapis.com/youtube/v3/videos",
              {
                query: {
                  'id': videoId,
                  'part': 'snippet',
                  'key': this.apiKey,
                  'maxResults': 1
                }
              }).then(
                function (response) {
                  var result = JSON.parse(response);
                  self._renderVideoDetails(self._convertVideoId(result));
                },
                function (error) {
                  //ToDo: add error rendering
                  console.log(error);
                }
              );
          }
        },

        _convertVideoId: function (results) {
          //v3/videos returns video Id as id
          //v3/search return video Id as id.videoId
          //here we create another field videoId that will keep right value
          for (var i = 0; i < results.items.length; i++) {
            results.items[i].videoId = results.items[i].id.constructor === String ? results.items[i].id : results.items[i].id.videoId;
          }

          return results;
        },

        _renderVideoDetails: function (data) {
          var self = this;
          var templateMarkup = self.domNode.querySelector("#VideoResultsTemplate").textContent;
          var template = _.template(templateMarkup);
          var compiled = template({ results: data.items });

          self.domNode.querySelector("#SelectedValue").innerHTML = "";
          self.domNode.querySelector("#SelectedValue").innerHTML = compiled;
          this._activateSelection(this);
        },

        _renderResults: function (results) {
          var self = this;
          this.nextPageToken = results.nextPageToken;
          this.prevPageToken = results.prevPageToken;
          var templateMarkup = self.domNode.querySelector("#VideoResultsTemplate").textContent;
          var template = _.template(templateMarkup);
          var compiled = template({ results: results.items });

          self.domNode.querySelector("#Results").innerHTML = "";
          self.domNode.querySelector("#Results").innerHTML = compiled;

          this._activateSelection(this);
        },

        _highlight: function (videoId, videoName) {
          var self = this;

          if (self.domNode.querySelector(".selected")) {
            self.domNode.querySelector(".selected").classList.remove('selected');
          }

          if (videoId) {
            var element = self.domNode.querySelector('.' + videoId);
            element.classList.add('selected');
            if (element.querySelector('.video-selected-mark')) {
              element.querySelector('.video-selected-mark').classList.add('selected');
            }
          };

          var value = { videoId: videoId, videoName: videoName }; //, height: height, width: width

          this._setValueAttr(value);
        },

        _activateSelection: function (myself) {
          myself.domNode.querySelectorAll(".video-result").forEach(function (element) {
            element.addEventListener("click", function (ev) {
              ev.preventDefault();
              var videoId = ev.currentTarget.classList[1];
              var name = ev.currentTarget.dataset['name'];
              myself._highlight(videoId, name);
            });
          });
        },

        _activatePagerButtons: function (selector, token) {
          var self = this;
          if (token) {
            self.domNode.querySelectorAll(selector).forEach(function (element) {
              element.classList.remove('disabled');
            });
          } else {
            self.domNode.querySelectorAll(selector).forEach(function (element) {
              element.classList.add('disabled');
            });
          }
        },

        _activatePagination: function (results) {
          var self = this;
          if (results && results.pageInfo) {
            const elementDisplay = results.pageInfo.totalResults > 0 ? 'block' : 'none';

            self.domNode.querySelectorAll('.pager').forEach(function (element) {
              element.style.display = elementDisplay;
            });

            this._activatePagerButtons('.pager > .next', results.nextPageToken);
            this._activatePagerButtons('.pager > .previous', results.prevPageToken);

          } else {
            self.domNode.querySelectorAll('.pager').forEach(function (element) {
              element.style.display = "none";
            });
          }
        }
      }
    );
  });
