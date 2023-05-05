define(function () {

    var _uid = 0;

    function injectScript(src, callback) {
        var s = document.createElement('script');
        s.type = 'text/javascript';
        s.async = true;
        s.onload = callback;
        s.src = src;

        var t = document.getElementsByTagName('script')[0];
        t.parentNode.insertBefore(s, t);
    }

    function uid() {
        _uid += 1;
        return '__async_req_' + _uid + '__';
    }

    return {
        load: function (url, callback) {
            var id = uid();
            //create a global variable that stores onLoad so callback
            //function can define new module after async load
            window[id] = callback;

            // Keep track of whether this script has already been loaded
            if (window._asyncScripts && window._asyncScripts[url]) { // Already loaded
                callback();
            }
            else {
                // Add to list of loaded scripts and then add script tag
                if (!window._asyncScripts) {
                    window._asyncScripts = {};
                }

                window._asyncScripts[url] = true;

                injectScript(url, window[id]);
            }
        }
    };
});
