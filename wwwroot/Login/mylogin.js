


    //var specialKeys = new Array();
    //specialKeys.push(8);
    //function IsNumeric(e) {
    //    var keyCode = e.which ? e.which : e.keyCode
    //    var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
    //    return ret;
    //}

    //let speech = new SpeechSynthesisUtterance();
    //speech.lang = 'en';


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

        if ($('#msgallow').val() == "1") {
            alert('In the Click Registration Function.');
        }
        Registration();
    });




            function Login()
            {
                alert('Here login');
                $('#lblmsgbox').hide();
                if ($('#msgallow').val() == "1") {
                    regError('Login click');
                }

                if ($("#txtsponsor").val().length < 2)
                {
                    //speech.text = 'Hi,Please enter valid sponsor ID';                  
                    regError('Hi,Please enter valid sponsor ID');
                   
                    $("#txtsponsor").focus();
                    return false;
                }


                if ($("#txtwalletaddress").val().length < 2) {
                speech.text = 'Hi,Please select valid Address';
                regError('Unauthorized Access.Please enter valid Address');
              
                $("#txtwalletaddress").focus();
                return false;
            }


                dataString = new FormData($("#LogForm").get(0));
                if (dataString != '') {
                    $('#preloader').show();
                    $('#btnLogSubmit').hide();
                    $('#btnRegSubmit').hide();                   


                    $.ajax({
                        type: "POST",
                        url: "/Login/LoginAjax",
                        //url: "/Login/GetVideoMappings",
                        data: dataString,
                        contentType: false,
                        cache: false,
                        //dataType: 'json', // expects response as JSON
                        processData: false,
                        success: function (result) {
                            if ($('#msgallow').val() == "1") {
                                alert('here after result');
                                regError(result);
                           
                            }

                            var res = result.split('-');
                            if (res[0] == "True") {
                                //alert('hi 1');
                                $('#preloader').hide();
                                window.location = "/User/Index";
                            }
                            else {
                                $('#preloader').hide();
                                $('#btnLogSubmit').show();
                                $('#btnRegSubmit').show();
                            }
                        },
                        error: function (er) {
                            $('#preloader').hide();
                            $('#btnLogSubmit').show();
                            $('#btnRegSubmit').show();
                            regError('Something went wrong!Please try again.');                          
                          

                        }
                    });

                }
            }


    //        function Registration()
    //        {
                    
    //            $('#lblmsgbox').hide();
    //                if ($('#txtwalletaddress').val().length < 20) {
                       
    //                    regError('Unauthorized Access');                      
    //                    return false;
    //                }
    //                else if ($('#txtsponsor').val() == "") {
                        
    //                    regError('Please Enter Valid Sponsor Id');
    //                    //$('#lblmsgbox').show();
    //                    return false;
    //                }
    //                        //else if ($('#lblsponsor').val() == "") {

    //                        //    if ($('#msgallow').val() == "1") {
    //                        //         regError('Please Enter Valid Sponsor Id');
    //                        //    }
    //                        //   $('#lblmsg').html('Please Enter Valid Sponsor Id');
    //                        //   //////// return false; 
    //                        //}
    //                        else if ($('#cmbposition').val() == "") {
    //                            if ($('#msgallow').val() == "1") {
    //                                regError('Please Select Position');
    //                            }

    //                            regError('Please Select Position');
    //                            //$('#lblmsgbox').show();
    //                            return false;
    //                        }
    //                        else {
    //                            //msgshow("Please select terms and condition", 'warning');
    //                            //$('#remember').focus();
    //                            //return false;
    //                        }
    //                    if ($('#msgallow').val() == "1") {
    //                                    alert('near data');
    //                    }


    //            var _package =1000;
    //            var _amt =1000;
    //            _amt *= 10000;
    //            var _add = await window.tronWeb.defaultAddress.base58;
    //            var _hxadd = await tronWeb.address.toHex(_add)
    //            var _balance = await tronWeb.trx.getBalance(_add);
    //            var _sponsor = $("#txtsponsor").val();
    //            //var _place = $('input[name="optionsRadiosInline"]:checked').val();
    //            var tronweb = window.tronWeb
    //            var tx = await tronweb.transactionBuilder.sendTrx('TQJGCyiaib3VuVFUVxd1r4xSCkouNVizS4', _amt, window.tronWeb
    //                .defaultAddress.base58)
    //            var signedTx = await tronweb.trx.sign(tx)
    //            var broastTx = await tronweb.trx.sendRawTransaction(signedTx);
    //            var trxid = broastTx['transaction']['txID'];

    ////                        var tronweb = window.tronWeb
    ////                        var senderAddress = await window.tronWeb.defaultAddress.base58;
    ////                        //var tronweb = window.tronWeb
    ////                        const CONTRACT = "TAHx3VepqXcUAVSsKccJ56j2ZKMMsPKt3B";
    ////                        //cbx"TEYJmVaSjEhLyLF7BiwVJDEiNc4fM29761"; //contract address
    ////                        const ACCOUNT = "TQJGCyiaib3VuVFUVxd1r4xSCkouNVizS4";
    ////                        var receiverAddress = 'TQJGCyiaib3VuVFUVxd1r4xSCkouNVizS4';
    ////                        var _amt = 1000;
    ////                        //amount *= 1000000000000000000;
    ////                        const num = _amt * Math.pow(10, 18);
    ////                        const numAsHex = "0x" + num.toString(16);
    ////                         regError(num + 'Hex=' + numAsHex + 'Ac=' + ACCOUNT);
    ////                        try {
    ////                            let instance = await tronWeb.contract().at(CONTRACT);
    ////                            instance["Transfer"]().watch((err, eventResult) => {
    ////                                if (err) {
    ////                                    alert('Error with "method" event:');
    ////                                    return console.error('Error with "method" event:', err);
    ////                                }
    ////                                if (eventResult) {
    ////                                    alert('eventResult:');
    ////                                    console.log('eventResult:', eventResult);
    ////                                }
    ////                            });

    ////                            let res = await instance.transfer(ACCOUNT, numAsHex).send({});
    ////                            alert('UFS Transfer');

    //                        dataString = new FormData($("#LogForm").get(0));
    //                        if (dataString != '') {
    //                            $('#preloader').show();
    //                            $('#btnLogSubmit').hide();
    //                            $('#btnRegSubmit').hide();
    //                            $.ajax({
    //                                type: "POST",
    //                                url: "/Login/RegisterAjax",
    //                                data: dataString,
    //                                cache: false,
    //                                contentType: false, // Not to set any content header  
    //                                processData: false, // Not to process data  
    //                                success: function (result1) {
    //                                    if ($('#msgallow').val() == "1") {
    //                                        alert('Register success');
    //                                    }
    //                                    regError('Register success');
    //                                    var res = result1.split('-');
    //                                    if (res[0] == "True") {
    //                                        regError(res);
                                          

    //                                        $('#preloader').hide();
    //                                        $('#btnLogSubmit').show();
    //                                        $('#btnRegSubmit').show();
    //                                        ////////window.location = "/Login/RegSuccess/0";
    //                                        Login();
    //                                    }
    //                                    else {
    //                                        if ($('#msgallow').val() == "1") {
    //                                            regError(res[1]);
    //                                        }

    //                                        $('#preloader').hide();
    //                                        $('#btnLogSubmit').show();
    //                                        $('#btnRegSubmit').show();
    //                                    }
    //                                },
    //                                error: function (er) {
                                       
    //                                    $('#preloader').hide();
    //                                    $('#btnLogSubmit').show();
    //                                    $('#btnRegSubmit').show();
    //                                    regError(er);
    //                                }
    //                            });
    //                        }
    ////                        } catch (error) {
    ////                             regError("trigger smart contract error" + error);
    ////                        }

    //                        }

    function regError(msg) {

        if ($('#msgallow').val() == "1") {
            alert(msg);
        }
        $('#lblmsg').html(msg);
        $('#lblmsgbox').show();
        //$('.regErrmsg').show();
        //$('#regErrmsg').text(msg);
        //$('#register-form').animate({
        //    scrollTop: $("#regErrmsg").offset().top
        //}, 1000);
    }

