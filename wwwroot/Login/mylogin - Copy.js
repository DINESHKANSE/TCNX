var specialKeys = new Array();
specialKeys.push(8);
function IsNumeric(e) {
    var keyCode = e.which ? e.which : e.keyCode
    var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
    return ret;
}

let speech = new SpeechSynthesisUtterance();
speech.lang = 'en';


//$("#txtMobileNo").on("keypress keyup blur change", function (e) {
//    return IsNumeric(e);
//});

//$("#txtusername,#txtpassword").on("keypress keyup blur change", function (event) {
//    var keycode = (event.keyCode ? event.keyCode : event.which);
//    if (keycode == '13') {
//        Login();
//    }
//});

$(document).on('click', '#btnLogSubmit', async () => {
    alert('in the login function');
    Login();
});


    function Login() {

        alert('Login click');
        if ($("#txtsponsor").val().length < 2) {
            //speech.text = 'Hi,Please enter valid sponsor ID';
            alert('Hi,Please enter valid sponsor ID');
            $("#lblmsg").val('Hi,Please enter valid sponsor ID');
            //msgshow("Hi,Please enter valid sponsor ID", 'warning');
            $("#txtsponsor").focus();
            return false;
        }
       

        if ($("#txtwalletaddress").val().length < 2) {
            speech.text = 'Hi,Please select valid Address';
            alert('Unauthorized Access.Please enter valid Address.');
            $("#lblmsg").val('Unauthorized Access.Please enter valid Address');
            // msgshow("Hi,Please select valid Address", 'warning');
            $("#txtwalletaddress").focus();
            return false;
        }


        dataString = new FormData($("#LogForm").get(0));
        if (dataString != '') {
            $('#btnLogSubmit').hide();
            alert('here Data ' + dataString);

            $.ajax({
                type: "POST",
                url: "/Login/LoginAjax",
                //url: "/Login/GetVideoMappings",
                data: dataString,
                contentType: false,
                //dataType: 'json', // expects response as JSON
                processData: false,
                success: function (result) {
                    alert('here after result');
                    alert(result);
                    var res = result.split('-');
                    if (res[0] == "True") {
                        //alert('hi 1');
                        window.location = "/User/Index";
                    }
                    else {
                        $('#Logloader').hide();
                        $('#btnLogSubmit').show();
                    }
                },
                error: function (er) {
                    $('#RegLoader').hide();
                    $('#btnSubmit').show();
                    msgshow(er, 'warning');
                }
            });

        }
    }