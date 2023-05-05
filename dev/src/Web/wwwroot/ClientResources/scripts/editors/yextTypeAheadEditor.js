define([
	"dojo",
	"dojo/on", // To connect events
	"dojo/_base/declare", // Used to declare the actual widget
	"dojo/_base/config", // Used to check if client code debugging is enabled
	"dojo/aspect", // Used to attach to events in an aspect-oriented way to inject behaviors
	"dojo/Deferred", // Used to allow code to wait for Yext block to be loaded
	"dojo/query",

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

	'xstyle/css!./themes/yexttypeaheadeditortemplate.css'
],
	function (
		dojo,
		on,
		declare,
		config,
		aspect,
		Deferred,
		query,

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
		return declare("score.editors.YextTypeahead", [_Widget, _TemplatedMixin, _WidgetsInTemplateMixin, _ValueRequiredMixin, _ContextMixin],
			{
				intermediateChanges: false,
				onPropertyView: false,
				value: null,

				// Property settings (set by editor descriptor)
				queryType: null,

				currentOffset: 0,
				currentLimit: 20,
				currentResultCount: 0,

				nextPageToken: '',
				prevPageToken: '',


				// Load HTML template from the same location as the widget
				templateString: dojo.cache("score", "./editors/templates/yexttypeaheadeditortemplate.html"),

				// Event used to notify EPiServer that the property value has changed
				onChange: function (value) {
					// Event
					this.inherited(arguments);

					if (this.onPropertyView) {
						this._getYextData(value.yextId, this);
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
					console.log("Yext Typeahead - Startup");
					this.inherited(arguments);

					if (!this.apiKey) {
						console.warn(
							"Yext API key not set.");
					}

					if (!this.experienceKey) {
						console.warn(
							"Yext Site Key not set.");
					}

					if (!this.queryType) {
						console.warn(
							"Yext Query Type not set.");
					}

					if (!this.version) {
						console.warn(
							"Yext Query Version not set.");
					}

					this._initYextValue();
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

				_initYextValue: function () {
					console.log('Yext - Init Yext Typehead Value: ' + this.value);
					// property view fix
					if (this.value === undefined || this.value === null) {
						this.onPropertyView = true;
					} else {
						//this.height = this.value.height;
						//this.width = this.value.width;
						this._getYextData(this.value.yextId, this);
					}
				},

				_bindEvents: function (myself) {
					console.log('Yext - Binding Events');
					on(query(this.yextContainer).query("button"), "click", function (e) {
						console.log('Yext - Search clicked');
						myself._doSearch($('#SearchQuery')[0].value, myself, 0);
						e.preventDefault();
					});

					on(query(this.yextContainer).query("a.previous"), "click", function (e) {
						console.log('Yext - Previous');

						var newOffset = myself.currentOffset - myself.currentLimit;
						if (newOffset < 0) {
							newOffset = 0;
                        }

						myself._doSearch($('#SearchQuery')[0].value, myself, newOffset);
						e.preventDefault();
					});

					on(query(this.yextContainer).query("a.next"), "click", function (e) {
						console.log('Yext - Next');

						var newOffset = myself.currentOffset + myself.currentLimit;
						if (newOffset >= myself.currentResultCount) {
							newOffset = myself.currentResultCount - myself.currentLimit;
						}

						myself._doSearch($('#SearchQuery')[0].value, myself, newOffset);
						e.preventDefault();
					});
				},

				_doSearch: function (query, myself, offset) {
					if (query !== "") {

						$.ajax({
							url: 'https://liveapi.yext.com/v2/accounts/me/answers/vertical/query',
							type: 'GET',
							data: {
								'v': myself.version,
								'api_key': myself.apiKey,
								'fields': 'name,address,c_locationPhoto,photoGallery',
								'sessionTrackingEnabled': false,
								'input': query,
								'experienceKey': myself.experienceKey,
								'version': 'PRODUCTION',
								'verticalKey': myself.queryType,
								'limit': myself.currentLimit,
								'offset': offset,
								'locale': 'en'
								//'pageToken': ((direction < 0) ? myself.prevPageToken : myself.nextPageToken) || ''
							},
							error: function (xhr, status, error) {
								//ToDo: add error rendering
								console.log(error);
							},
							success: function (response, status, xhr) {
								var results = JSON.parse(response);
								results.response.queryType = myself.queryType;
								myself.currentOffset = offset;
								myself._renderResults(results.response);
								$('.pager').css('display', 'block');
								myself._activatePagination(results.response);
							}
						});
					}
				},

				_getYextData: function (yextId, myself) {
					if (yextId !== undefined && yextId !== null) {
						var self = this;
						$.ajax({
							url: 'https://liveapi.yext.com/v2/accounts/me/entities/' + yextId,
							type: 'GET',
							data: {
								'v': myself.version,
								'api_key': myself.apiKey,
								'sessionTrackingEnabled': false,
								'locale': 'en'
							},

							error: function (xhr, status, error) {
								//ToDo: add error rendering
								console.log(error);
							},
							success: function (response, status, xhr) {
								var results = JSON.parse(response);
								results.response.queryType = myself.queryType;

								results.response.id = yextId;

								var result = new Object();
								result.data = results.response;

								self._renderYextDetails(new Array(result));
							}
						});
					}
				},

				_renderYextDetails: function (response) {
					var template;

					if (this.queryType === "healthcare_facilities") {
						template = _.template($("#YextLocationResultsTemplate").text());
					} else {
						template = _.template($("#YextProviderResultsTemplate").text());
					}

					var compiled = template({ results: response });

					$('#SelectedValue')[0].innerHTML = "";
					$('#SelectedValue').append(compiled);
					this._activateSelection(this);
				},

				_renderResults: function (response) {
					this.nextPageToken = response.nextPageToken;
					this.prevPageToken = response.prevPageToken;
					var template;
					var compiled;

					if (response.queryType === "healthcare_facilities") {
						template = _.template($("#YextLocationResultsTemplate").text());
					} else {
						template = _.template($("#YextProviderResultsTemplate").text());
					}
					if (response.allResultsForVertical) {
						this.currentResultCount = response.allResultsForVertical.resultsCount;
						compiled = template({ results: response.allResultsForVertical.results });
					}
					else {
						this.currentResultCount = response.resultsCount;
						compiled = template({ results: response.results });
					}

					$('#Results')[0].innerHTML = "";
					$('#Results').append(compiled);

					this._activateSelection(this);
				},

				_highlight: function (yextId) {
					$('.selected').removeClass('selected');

					if (yextId) {
						$('.' + yextId).addClass('selected');
						$('.' + yextId).find('.yext-selected-mark').addClass('selected');
					};

					var value = { yextId: yextId }; //, height: height, width: width

					this._setValueAttr(value);
				},

				_activateSelection: function (myself) {
					$('.yext-result').unbind('click.select');
					$('.yext-result').bind('click.select',
						function (e) {
							e.preventDefault();
							var yextId = e.currentTarget.classList[1];
							myself._highlight(yextId);
						});
				},

				_activatePagerButtons: function (selector, token) {
					if (token) {
						$(selector).removeClass('disabled');
					} else {
						$(selector).addClass('disabled');
					}
				},

				_activatePagination: function (results) {

					var totalResults = 0;

					if (results.allResultsForVertical) {
						totalResults = results.allResultsForVertical.resultsCount;
					} else {
						totalResults = results.resultsCount;
                    }

					$('.total-results').text(totalResults)

					if (results && totalResults > this.currentLimit) {
						$('.pager-buttonLink').css('display', totalResults > 0 ? 'inline' : 'none');

						this._activatePagerButtons('.pager-buttonLink.previous', this.currentOffset > 0);

						var nextOffset = this.currentOffset + this.currentLimit;
						this._activatePagerButtons('.pager-buttonLink.next', nextOffset < this.currentResultCount);

					} else {
						$('.pager-buttonLink').css('display', 'none');
					}
				}
			});
	});
