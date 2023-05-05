define('templates/component/command/newBlockTemplate', [
  // dojo core
  'dojo/_base/declare',
  //Opti
  'epi-cms/component/command/NewBlock',

  // resources
  '../resources/createtemplate',
], function (declare, NewBlock, resCreateTemplate) {
  return declare([NewBlock], {
    label: resCreateTemplate.create,
  });
});
