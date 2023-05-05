define([
    "dojo",
    "epi/dependency"
], function (dojo, dependency) {

    function ValidationService() { }

    ValidationService.prototype.ValidateAgainstDevices = function validateAgainstDevices(width, height, devices) {
        for (var i = 0; i < devices.length; i++) {
            if (width < devices[i].width) {
                alert("This image has a width of " + width + " which is less than the " + devices[i].device + " viewport requirements of " + devices[i].width + ".");
                return false;
            }

            if (height < devices[i].height) {
                alert("This image has a height of " + height + " which is less than the " + devices[i].device + " viewport requirements of " + devices[i].height + ".");
                return false;
            }
        }
        
        return true;
    };

    return new ValidationService();
});
