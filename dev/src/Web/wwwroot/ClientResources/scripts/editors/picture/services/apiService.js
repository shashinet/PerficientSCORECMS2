define([
    "dojo",
    "dojo/request"
], function (dojo, request) {

    function ApiService() { }

    ApiService.prototype.Post = function callApi(data, url, callback) {
        request.post(url, {
            data: JSON.stringify(data),
            headers: { "content-type": "application/json" }
        }).then(function (response) {
                // Display the text file content
                var results = JSON.parse(response);
                console.log("Ajax success: ", results);

                if (callback) {
                    callback(results);
                }
            },
            function (error) {
                // Display the error returned
                console.log("Ajax error", error);
            }
        );
    };

    return new ApiService();

});
