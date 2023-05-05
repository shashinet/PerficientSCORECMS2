define("templates/component/command/newPageTemplate", [
  // dojo
  "dojo/_base/declare",

  // command
  "epi-cms/command/NewContent",

  // resource
  '../resources/createtemplate'
],

  function (
    // dojo
    declare,

    // command
    NewContentComand,

    // resource
    resCreateTemplate
  ) {

    return declare([NewContentComand], {
      
      contentType: "episerver.core.pagedata",
      iconClass: "epi-iconCreatePage",
      label: resCreateTemplate.createNewPageTemplate,
      
    });
  });
