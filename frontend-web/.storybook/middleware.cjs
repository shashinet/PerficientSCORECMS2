const axios = require('axios');
const bodyParser = require('body-parser');

const expressMiddleWare = router => {
  router.use(bodyParser.urlencoded({ extended: false }));
  router.use(bodyParser.json());

  const baseUrl = 'https://episerver.localhost/api/feature';
  

  router.post('/api/feature/post', (req, res) => {
    const { body } = req;
    axios.post(`${baseUrl}/post`, body).then((data) => {
      res.send(data.data);
    }).catch((e) => {
      res.send(e)
    });
  });
};

module.exports = expressMiddleWare;
