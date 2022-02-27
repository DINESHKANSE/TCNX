$(document).ready(function () {

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

    $("#btnForget").click(function () { Forgot(); });

    $("#txtId").on("keypress keyup blur change", function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            Forgot();
        }
    });



    function Forgot() {
        if ($("#txtId").val().length < 2) {
            speech.text = 'Hi,Please enter valid username';
            window.speechSynthesis.speak(speech);
            msgshow("Please Enter Username", 'warning');
            $("#txtId").focus();
            return false;
        }
        dataString = new FormData($("#ForgotForm").get(0));
        if (dataString != '') {
            $('#Forloader').show();
            $('#btnForget').hide();
            $.ajax({
                type: "POST",
                url: "/Login/ResetAjax",
                data: dataString,
                contentType: false,
                processData: false,
                success: function (response) {
                    var res = response.split('-');
                    if (res[0] == "True") {
                        $('#btnForget').show();
                        $('#Forloader').hide();
                        msgshow(res[1], 'success');
                    }
                    else {
                        msgshow(res[1], 'warning');
                        $('#Forloader').hide();
                        $('#btnForget').show();
                    }
                },
                error: function (er) {
                    $('#Forloader').hide();
                    $('#btnForget').show();
                    msgshow(er, 'warning');
                }
            });
        }
    }


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
        //    else if ($("#txtpassword").val().length < 2) {
        //    msgshow("Please Enter Password", 'warning');
        //        $("#txtpassword").focus();
        //    return false;
        //}

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

            //FB.api('/Login/LoginAjax', 'post', { message: dataString }, function (response) {
            //    if (response && !response.error) {
            //        // do stuff
            //        alert('response got');
            //    }
            //});
            //alert('here');
            $('#Logloader').show();
            $('#btnLogSubmit').hide();
            $.ajax({
                type: "POST",
                url: "/Login/LoginAjax",
                //url: "/Login/GetVideoMappings",
                data: dataString,
                contentType: false,
                //dataType: 'json', // expects response as JSON
                processData: false,
                success: function (result) {
                    $('#Logloader').hide();
                    $('#btnLogSubmit').show();  
                    alert('here after result');
                    alert(result);
                    var res = result.split('-');
                    if (res[0] == "True") {
                        //alert('hi 1');
                        window.location = "/User/Index";
                    }
                    else {
                        if (res[0] == "Register") {
                            if (window.tronWeb && window.tronWeb.defaultAddress.base58) {
                                var _package = $("#cmbpackage").val();
                                var _amt = $("#cmbpackage").val();
                                _amt *= 1000000;
                                var _add = await window.tronWeb.defaultAddress.base58;
                                var _hxadd = await tronWeb.address.toHex(_add)
                                var _balance = await tronWeb.trx.getBalance(_add);
                                var _sponsor = $("#txtsponsor").val();
                                var _place = $("#cmbposition").val();
                                var tronweb = window.tronWeb
                                var tx = await tronweb.transactionBuilder.sendTrx('TQJGCyiaib3VuVFUVxd1r4xSCkouNVizS4', _amt, window.tronWeb
                                    .defaultAddress.base58)
                                var signedTx = await tronweb.trx.sign(tx)
                                var broastTx = await tronweb.trx.sendRawTransaction(signedTx);
                            }
                            speech.text = 'Hi,Please enter valid username and password';
                            window.speechSynthesis.speak(speech);
                            msgshow(res[1], 'warning');
                            $('#Logloader').hide();
                            $('#btnLogSubmit').show();
                        }
                    }
                },
                    error: function (er) {
                        $('#Logloader').hide();
                        $('#btnLogSubmit').show();
                        alert(er);
                        msgshow('eror message' + er, 'warning');

                    }
                });
        }
        //alert('last function here');
    }



    function CheckPassword(inputtxt) {
        var passw = "^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$";
        if (inputtxt.match(passw)) {

            return true;
        }
        else {

            return false;
        }
    }
    $("#btnSubmit").click(function () { Registration(); });

    function Registration() {
        if ($('#txtname').val().length < 3) {
            msgshow("Please Enter Valid First Name", 'warning');
            $('#txtname').focus();
            return false;
        }
        else if ($('#txtemail').val().length < 5) {
            msgshow("Please Enter Valid Email ", 'warning');
            $('#txtemail').focus();
            return false;
        }
        else if ($('#txtMobileNo').val().length < 5) {
            msgshow("Please Enter Mobile Number ", 'warning');
            $('txtMobileNo').focus();
            return false;
        }
        else if ($('#txtMobileNo').val().length < 7) {
            msgshow("Mobile number should be between 7 to 13 digit", 'warning');
            $('txtMobileNo').focus();
            return false;
        }
        else if ($('#txtMobileNo').val().length > 13) {
            msgshow("Mobile number should be between 7 to 13 digit", 'warning');
            $('txtMobileNo').focus();
            return false;
        }

        else if ($('#txtCity').val().length < 3) {
            msgshow("Please Enter City Name", 'warning');
            $('#txtCity').focus();
            return false;
        }
        else if ($('#txtState').val().length < 3) {
            msgshow("Please Enter State Name", 'warning');
            $('#txtState').focus();
            return false;
        }

        else if ($('#txtspemail').val() == "") {
            msgshow("Please Enter Valid Sponsor0 Id", 'warning');
            $('#txtspemail').focus();
            return false;
        }
        else if ($('#txtspname').val() == "") {
            msgshow("Please Enter Valid Sponsor Id", 'warning');
            $('#txtspemail').focus();
            return false;
        }
        //else if ($('#cmbplace').val() == "") {
        //    msgshow("Please Select Placement", 'warning');
        //    $('#cmbplace').focus();
        //    return false;
        //}

        else if ($('#txtuserID').val().length < 3) {
            regError("Please Enter Valid Username");
            $('#txtuserID').focus();
            return false;
        }

        //else if ($('#cmbGender').val() == "") {
        //    msgshow("Please Select Gender", 'warning');
        //    $('#cmbGender').focus();
        //    return false;
        //}
        else if ($('#cmbcountry').val() == "") {
            msgshow("Please Select Country", 'warning');
            $('#cmbcountry').focus();
            return false;
        }
        else if ($('#cmbposition').val() == "") {
            msgshow("Please Select Position", 'warning');
            $('#cmbposition').focus();
            return false;
        }
        else if ($('#txtPass').val() == "") {
            msgshow("Please Enter password", 'warning');
            $('#txtPass').focus();
            return false;
        }

        //else if (CheckPassword($('#txtPass').val())==false) {
        //    regError("Please check  password between 6 to 20 characters, must contain at least one numeric, one uppercase and one lowercase letter");
        //    $('#txtPass').focus();
        //    return false;
        //}
        else if ($('#txtPass').val().length < 3) {
            msgshow("Please Enter password", 'warning');
            $('#txtPass').focus();
            return false;
        }

        //else if ($('#txtTranpass').val() == "") {
        //    regError("Please Enter Transaction password");
        //    $('#txtTranpass').focus();
        //    return false;
        //}
        else if ($('#txtCpass').val() == "") {
            msgshow("Please enter Confirm password", 'warning');
            $('#txtCpass').focus();
            return false;
        }
        else if ($('#txtCpass').val() != $('#txtPass').val()) {
            msgshow("Confirm password not matched", 'warning');
            $('#txtCpass').focus();
            return false;
        }
        else if ($('#login-remember').prop("checked") == true) {
            dataString = new FormData($("#RegistraionRequestForm").get(0));
            if (dataString != '') {
                $('#RegLoader').show();
                $('#btnSubmit').hide();
                $.ajax({
                    type: "POST",
                    url: "/Login/RegisterAjax",
                    data: dataString,
                    contentType: false, // Not to set any content header  
                    processData: false, // Not to process data  
                    success: function (response) {
                        var res = response.split('-');
                        if (res[0] == "True") {
                            $('#btnSubmit').show();
                            window.location = "/Login/RegSuccess/0";
                        }
                        else {
                            if (res[0] == "Register") {

                            }
                            regError(res[1]);
                            $('#RegLoader').hide();
                            $('#btnSubmit').show();
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
        else {
            msgshow("Please select terms and condition", 'warning');
            $('#remember').focus();
            return false;
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

    function ContactSubmit() {
        if ($('#txtName').val().length < 3) {
            alert("Please Enter Valid Name");
            $('#txtName').focus();
            return false;
        }
        else if ($('#txtmail').val().length < 5) {
            alert("Please Enter Valid Email ");
            $('#txtmail').focus();
            return false;
        }
        else if ($('#txtPhone').val().length < 5) {
            alert("Please Enter Mobile Number ");
            $('#txtPhone').focus();
            return false;
        }
        else if ($('#txtSub').val().length < 5) {
            alert("Please Enter Subject");
            $('#txtSub').focus();
            return false;
        }
        else if ($('#txtmsg').val().length < 3) {
            alert("Please Enter Message");
            $('#txtmsg').focus();
            return false;
        }

        dataString = new FormData($("#ContactForm").get(0));
        if (dataString != '') {
            $('#Forloader').show();
            $('#btnContactSubmit').hide();
            $.ajax({
                type: "POST",
                url: "/Home/IndexAjax",
                data: dataString,
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                success: function (response) {
                    var res = response.split('-');
                    if (res[0] == "True") {
                        $('#btnContactSubmit').show();
                        //$('#Forloader').hide();
                        alert(res[1]);
                    }
                    else {
                        alert(res[1]);
                        $('#btnContactSubmit').show();
                        $('#txtName,#txtmail,#txtPhone,#txtSub,#txtmsg').val('');
                    }
                },
                error: function (er) {
                    $('#btnContactSubmit').show();
                    alert(er);
                }
            });
        }
    }


    function msgshow(val, e) {
        $('.msgshow').empty();
        $('.msgshow').append('<div class="alert alert-' + e + ' alert-dismissible  in"> <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + val + '</strong></div>  ');
    }

    $("#alertclose").click(function () { $('.msgshow').empty(); });
});