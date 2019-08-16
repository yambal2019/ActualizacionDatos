$(document).ready(function () {

    //if ($.isFunction($.fn.validate)) {


    //});
    debugger;
    $("frmvalidacion").validate({
       
        rules: {
           
            vchEmail: {
                required: true,
                email: true
            }
            
        },
        messages: {

            //username: {
            //    required: "Please enter a username",
            //    minlength: "Your username must consist of at least 2 characters"
            //},
            //password: {
            //    required: "Please provide a password",
            //    minlength: "Your password must be at least 5 characters long"
            //},
            //confirm_password: {
            //    required: "Please provide a password",
            //    minlength: "Your password must be at least 5 characters long",
            //    equalTo: "Please enter the same password as above"
            //},
            email: "Please enter a valid email address"
           
        }
    });


});