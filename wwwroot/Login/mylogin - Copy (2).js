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
    if ($('#msgallow').val() == "1") {
        alert('in the login function');
    }
    
    Login();
});

$(document).on('click', '#btnRegSubmit', async () => {
    alert('in the Join function');
    Registration();
});




    function Login() {
        if ($('#msgallow').val() == "1") {
            alert('Login click');
        }
       
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
            $('#btnRegSubmit').hide();
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
                        $('#btnRegSubmit').show();
                    }
                },
                error: function (er) {
                    $('#RegLoader').hide();
                    $('#btnLogSubmit').show();
                    $('#btnRegSubmit').show();
                    msgshow(er, 'warning');
                }
            });

        }
}


function Registration() {
    if ($('#txtwalletaddress').val().length < 20) {
        alert('Unauthorized Access');
        $('#lblmsg').val('Unauthorized Access');
        return false;
    }
    else if ($('#txtsponsor').val() == "") {
        alert('Please Enter Valid Sponsor Id');
        $('#lblmsg').val('Please Enter Valid Sponsor Id');
        return false;
    }
    else if ($('#lblsponsor').val() == "") {
       
        if ($('#msgallow').val() == "1") {
            alert('Please Enter Valid Sponsor Id');
        }
        $('#lblmsg').val('Please Enter Valid Sponsor Id');
       //////// return false; 
    }
    else if ($('#cmbposition').val() == "") {
        if ($('#msgallow').val() == "1") {
            alert('Please Select Position');
        }
        
        $('#lblmsg').val('Please Select Position');
        return false;
    }
    else {
        //msgshow("Please select terms and condition", 'warning');
        //$('#remember').focus();
        //return false;
    }
    alert('near data');
    dataString = new FormData($("#LogForm").get(0));
    if (dataString != '') {
        $('#Logloader').show();
        $('#btnLogSubmit').hide();
        $('#btnRegSubmit').hide();
        $.ajax({
            type: "POST",
            url: "/Login/RegisterAjax",
            data: dataString,
            contentType: false, // Not to set any content header  
            processData: false, // Not to process data  
            success: function (result1) {
                alert('Register success');
                var res = result1.split('-');
                if (res[0] == "True") {
                    alert(res);
                    $('#LogLoader').hide();
                    $('#btnLogSubmit').show();
                    $('#btnRegSubmit').show();
                    ////////window.location = "/Login/RegSuccess/0";
                    Login();
                }
                else {                    
                    regError(res[1]);
                    $('#LogLoader').hide();
                    $('#btnLogSubmit').show();
                    $('#btnRegSubmit').show();
                }
            },
            error: function (er) {
                $('#LogLoader').hide();
                $('#btnLogSubmit').show();
                $('#btnRegSubmit').show();
                $('#lblmsg').val(er);
            }
        });
    }


}

function regError(msg) {
    alert(msg);
    //$('.regErrmsg').show();
    //$('#regErrmsg').text(msg);
    //$('#register-form').animate({
    //    scrollTop: $("#regErrmsg").offset().top
    //}, 1000);
}
