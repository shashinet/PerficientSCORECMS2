define(
  "epi-cms/contentediting/command/SelectDisplayOption",
  //"shell/commands/SelectTargetedDisplayOption",
  [
    // General application modules
    "dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/when",
    "dojo/promise/all",

    "epi/dependency",

    "epi-cms/contentediting/command/_ContentAreaCommand",
    "epi-cms/contentediting/viewmodel/ContentBlockViewModel",
    "epi/routes",
    "epi-cms/widget/DisplayOptionSelector",

    // Resources
    "epi/i18n!epi/cms/nls/episerver.cms.contentediting.editors.contentarea.displayoptions"
  ],
  function (declare, lang, when, all, dependency, _ContentAreaCommand, ContentBlockViewModel, routes, DisplayOptionSelector, resources) {

    return declare([_ContentAreaCommand], {
      // tags:
      //      internal

      // label: [public] String
      //      The action text of the command to be used in visual elements.
      label: resources.label,

      // category: [readonly] String
      //      A category which hints that this item should be displayed as an popup menu.
      category: "popup",

      typeIdentifier: null,

      containerDO: null,  // displayOptions specified on the parent ContentArea
      globalDisplayOptions: null,

      _labelAutomatic: lang.replace(resources.label, [resources.automatic]),

      _registry: null,

      _selectDOName: "epi.cms.selectdisplayoptions",
      _selectDOPath: "/episerver/cms/customstores/displayoptions/",


      constructor: function (containerDisplayOptions) {
        // this gets set when the contentAreaEditor calls the constructor.
        // later this value is set to the model for normalizing purposes
        if (typeof containerDisplayOptions !== undefined && containerDisplayOptions != null) {
          this.containerDO = JSON.parse(containerDisplayOptions);
        }
        this.popup = new DisplayOptionSelector();
        this._registry = dependency.resolve("epi.storeregistry");
        this.contentStore = dependency.resolve("epi.cms.ContentTypeService");
        this.contextService = dependency.resolve("epi.shell.ContextService");
        // this could be moved to an initializer script if desired
        try {
          this.store = this._registry.get(this._selectDOName);
        }
        catch {
          this.store = this._registry.create(this._selectDOName, this._selectDOPath);
        }
      },

      buildRendering: function () {
        console.log("build rendering");
      },

      postscript: function () {
        this.inherited(arguments);

        if (!this.store) {
          this.store = this._registry.get(this._selectDOName);
        }

        when(this.store.get(), lang.hitch(this, function (results) {
          // Reset command's available property in order to reset dom's display property of the given node

          // this is where the DisplayOptions are received from the system. What we need to do is identify what is acceptable to the current block calling this popup now. We can filter here instead of another spot.
          this._setCommandAvailable(results);

          this.popup.set("displayOptions", results);
          this.globalDisplayOptions = results;    // set here as fallback for filtering
        }));
      },

      destroy: function () {
        this.inherited(arguments);

        this.popup && this.popup.destroyRecursive();
      },

      _setDisplayOptions: function (options) {
        this.popup.set("displayOptions", options);
        this._setCommandAvailable(options);
      },

      _onModelChange: function (data) {
        // summary:
        //      Updates canExecute after the model value has changed.
        // tags:
        //      protected

        this.inherited(arguments);
        // use this assignment from contentAreaDisplay setting the object.
        // allows normalizing the setting of container DisplayOptions specs
        if (typeof this.model.containerDO === "undefined") {
          this.model.containerDO = this.containerDO;
        }
        var containerDO;
        if (typeof this.model.containerDO === 'string' || this.model.containerDO instanceof String) {
          containerDO = JSON.parse(this.model.containerDO);
        }
        else {
          containerDO = this.model.containerDO;
        }

        var options = this.globalDisplayOptions, // this more or less serves as a fallback now
          selectedOption = this.model.get("displayOption"),
          isAvailable = options && options.length > 0;

        isAvailable = isAvailable && (this.model instanceof ContentBlockViewModel);

        // modify displayoptions
        if (this.model instanceof ContentBlockViewModel) {
          if (containerDO != null && !containerDO.showDisplayOptions) {
            this._setDisplayOptions(options = null);
          }
          else {
            var filters = "";
            if (containerDO != null) {
              filters = `/${containerDO.displayOptions}`;
            }
            when(this.store.get(`availableoptions/${this.model.contentLink}${filters}`), lang.hitch(this, function (results) {
              if (results.length >= 1) {
                options = (results == "disabled" ? null : results);
              }
              this._setDisplayOptions(options);
            }));
          }
        }


        if (!isAvailable) {
          this.set("label", this._labelAutomatic);
          return;
        }

        this.popup.set("model", this.model);

        if (!selectedOption) {
          this.set("label", this._labelAutomatic);
        } else {
          this._setLabel(selectedOption);
        }

        this._watch("displayOption", function (property, oldValue, newValue) {
          if (!newValue) {
            this.set("label", this._labelAutomatic);
          } else {
            this._setLabel(newValue);
          }
        }, this);
      },

      _setCommandAvailable: function (/*Array*/displayOptions) {
        // summary:
        //      Set command available
        // displayOptions: [Array]
        //      Collection of a content display mode
        // tags:
        //      private

        this.set("isAvailable", displayOptions && displayOptions.length > 0 && this.model instanceof ContentBlockViewModel);
      },

      _setLabel: function (displayOption) {
        when(this.store.get("getdisplayoption/" + displayOption), lang.hitch(this, function (option) {
          this.set("label", lang.replace(resources.label, [option.name]));

        }), lang.hitch(this, function (error) {

          console.log("Could not get the option for: ", displayOption, error);

          this.set("label", this._labelAutomatic);
        }));
      },

      _onModelValueChange: function () {
        this.set("canExecute", !!this.model && this.model.contentLink && !this.model.get("readOnly"));
      }
    });
  });
