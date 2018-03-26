<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" CodeFile ="index.aspx.cs" Inherits="index"    %>

<html lang="en">
<head>
    <title><%= GetLocalResourceObject("title") %></title>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">
    <!-- Custom CSS -->
    <link rel="stylesheet" href="css/custom.css">
    <!-- Google Fonts CSS -->
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:400,700" rel="stylesheet">
    <script type="text/javascript">
        // Update the 'nojs'/'js' class on the html node
        document.documentElement.className = document.documentElement.className.replace(/\bnojs\b/g, 'js');

        // Check that all required assets are uploaded and up-to-date
        if (typeof Muse == "undefined") window.Muse = {}; window.Muse.assets = { "required": ["museutils.js", "museconfig.js", "jquery.watch.js", "webpro.js", "jquery.museresponsive.js", "require.js", "index.css"], "outOfDate": [] };
    </script>
    <script type="text/javascript">
        var __adobewebfontsappname__ = "muse";
    </script>
    <script src="https://webfonts.creativecloud.com/source-sans-pro:n4,n6,i4,n7:default.js" type="text/javascript"></script>
    <script src="https://www.paypalobjects.com/api/checkout.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.2.1.min.js"></script>



</head>
<!--script-->

<body>

    <div class="header">
        <div class="container py-3 back">
            <div class="logo py-2">
                <img src="images/logo.png" class="img-fluid logo-img">
            </div>
        </div>
    </div>

    <div class="title">
        <div class="container">
            <div class="row my-1">
                <div class="col-md-12">
                    <h1 class="logo"><span>
                        <img src="images/img_underbanner-crop-u4784_2x.jpg" alt=""></span><%= GetLocalResourceObject("title") %></h1>
                </div>
            </div>
        </div>
    </div>

    <div class="form">
        <div class="container py-2 back-form">
            <div class="row">

                <div class="col-md-8 offset-md-2">
                    <div class="col-md-12 pt-4">
                        <p>
                            <%= GetLocalResourceObject("intro1") %><br class="d-none d-sm-none d-md-block">
                            <%= GetLocalResourceObject("intro2") %>
                        </p>
                    </div>
                </div>

                <div class="col-md-12">
                    <hr>
                </div>

                <div class="col-md-8 offset-md-2" id="feedback"></div>

                <div class="col-md-8 offset-md-2">
                    <div class="col-md-12">
                        <p class="mb-0"><span><%= GetLocalResourceObject("title_payment_details") %></span></p>
                    </div>
                </div>

                <div class="col-md-12">
                    <hr>
                </div>

                <div class="col-md-8 offset-md-2">
                    <div class="col-md-12">
                        <form id="needs_validation" runat="server" role="form">
                            <div class="form-group" >
                                <label for="invoice-ieference"><%= GetLocalResourceObject("field_invoice_label") %></label>
                                <input type="text" class="form-control" id="invoice-ieference" required>
                                <small id="helpBlock-invoice-ieference" class="help-block"></small>
                            </div>

                            <label for="amount"><%= GetLocalResourceObject("field_amount_label") %></label>
                            <div class="form-row align-items-center">
                                <div class="col-md-6 col-sm-12 col-12">
                                    <input type="text" class="form-control mb-2" id="amount" name ="amount" runat="server" required >
                                    <small id="helpBlock-amount" class="help-block"></small>
                                </div>
                                <div class="col-md-1 hidden-sm hidden-xs col-1">
                                    <p class="text-center">-</p>
                                </div>
                                <div class="col-md-5 col-sm-12 col-12">
                                    <select class="custom-select w-100 mb-2" id="amount-input-3" required>
                                        <option selected value="<%=ConfigurationManager.AppSettings["CurrencyDollarID"]%>"><%= GetLocalResourceObject("field_currency_USD_label") %></option>
                                    </select>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="comments"><%= GetLocalResourceObject("field_comments_label") %></label>
                                <textarea class="form-control" id="comments" rows="3"></textarea>
                                <small id="helpBlock-comments" class="help-block"></small>
                            </div>


                            <%-- <div id="paypal-button-container" class="submit-btn NoWrap rounded-corners clearfix grpelem shared_content text-center mt-3 mb-5" data-content-guid="u260-4_0_content"></div>--%>
                            <label>
                                <input type="radio" name="payment-option" value="paypal" checked>
                                <img src="images/paypal-mark.jpg" alt="Pay with Paypal" style="height: 40px;">
                            </label>
                            <br />
                            <label>
                                <input type="radio" name="payment-option" value="card">
                                <img src="images/card-mark.png" alt="Accepting Visa, Mastercard, Discover and American Express" style="height: 40px;">
                            </label>

                            <div id="paypal-button-container" class="submit-btn NoWrap rounded-corners clearfix grpelem shared_content text-center mt-3 mb-5" data-content-guid="u260-4_0_content"></div>
                            <div id="card-button-container" class="hidden">
                                <div class="form-row align-items-center">
                                    <div class="col-md-5 col-sm-12 col-12">
                                        <select class="custom-select w-100 mb-2" id="cardtype" runat="server" required>
                                            <option selected value="Visa">Visa</option>
                                            <option value="MasterCard>">MasterCard</option>
                                            <option value="Discover">Discover</option>
                                            <option value="American Express">American Express</option>
                                        </select>
                                    </div>
                                    <div class="col-md-6 col-sm-12 col-12">
                                        <input type="number" class="form-control mb-2" id="acct" placeholder="Credit Card Number"  runat="server" required>
                                    </div>
                                </div>
                                <div class="form-row align-items-center">
                                    <div class="col-md-5 col-sm-12 col-12">
                                        <input type="number" class="form-control mb-2"  id="expdate" name="expdate" placeholder="Expiration Date"  runat="server" required>
                                    </div>
                                    <div class="col-md-6 col-sm-12 col-12">
                                        <input type="text" class="form-control mb-2" id="cvv2" runat="server" placeholder="CSC (CVV2)" required>
                                    </div>
                                </div>
                                <div class="form-row align-items-center">
                                    <div class="col-md-5 col-sm-12 col-12">
                                        <input type="text" class="form-control mb-2" id="fname" runat="server" placeholder="First Name" required>
                                    </div>
                                    <div class="col-md-6 col-sm-12 col-12">
                                        <input type="text" class="form-control mb-2" id="lname" runat="server" placeholder="Last Name" required>    
                                    </div>
                                </div>
                               <asp:Button id="btnPaymentCard"  runat="server" Text="Continue" OnClick="btnPaymentCard_Click"  />
                         
                            </div>

                            <div class="legal">
                                <p class="text-center">
                                    <%= GetLocalResourceObject("footer_instructions") %>
                                    <%= GetLocalResourceObject("footer_contact_text") %><a class="nonblock" href='mailto:<%= GetLocalResourceObject("footer_contact_email") %>'><span><%= GetLocalResourceObject("footer_contact_email") %></span></a>
                                </p>
                            </div>

                            <div class="img-pay">
                                <ul class="logos-cards">
                                    <li class="list-inline-item">
                                        <img src="images/logo-card-mastercard.jpg" class="img-fluid">
                                    </li>
                                    <li class="list-inline-item">
                                        <img src="images/logo-card-maestro.jpg" class="img-fluid">
                                    </li>
                                    <li class="list-inline-item">
                                        <img src="images/logo-card-visa.jpg" class="img-fluid">
                                    </li>
                                    <li class="list-inline-item">
                                        <img src="images/logo-card-jcb.jpg" class="img-fluid">
                                    </li>
                                    <li class="list-inline-item">
                                        <img src="images/logo-card-discover.jpg" class="img-fluid">
                                    </li>
                                    <li class="list-inline-item">
                                        <img src="images/logo-card-american-express.jpg" class="img-fluid">
                                    </li>
                                    <li class="list-inline-item">
                                        <img src="images/logo-card-diners-club.jpg" class="img-fluid">
                                    </li>
                                    <li class="list-inline-item">
                                        <img src="images/logo-card-union-pay.jpg" class="img-fluid">
                                    </li>
                                </ul>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container">
        <div class="row">
            <div class="space w-100">
            </div>
        </div>
    </div>

    <div class="footer">
        <div class="container py-3 px-5 back-footer">
            <div class="row">
                <div class="col-md-4">
                    <p>
                        <br>
                        <span>WTCPay</span><br class="d-none d-sm-none d-md-block">
                        <%= GetLocalResourceObject("footer_copyright") %>
                    </p>
                </div>
                <div class="col-md-4 text-center">
                    <p>
                        <br class="d-none d-sm-none d-md-block">
                        <br class="d-none d-sm-none d-md-block">
                        <a href="https://www.winterbotham.com/wtc/Pdf/Privacy_Policy.pdf"><%= GetLocalResourceObject("footer_policy_privacy") %></a> | <a href="https://www.winterbotham.com/wtc/Pdf/Refund_Policy.pdf"><%= GetLocalResourceObject("footer_policy_refund") %></a>
                    </p>
                </div>
                <div class="col-md-4 ">
                    <p class="float-right">
                        Support Information<br>
                        wtcpay@winterbotham.com<br>
                        + 1 242 356 5454
                    </p>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var paymentId = 0;
        var errorFeedbackShowed = false;

        function createPayment(amount, desc, amountCurrency, invoice) {
            var xhr = new XMLHttpRequest();
            //it is not asynchronous because the return of the actions.payment.create needs to be handle properly
            xhr.open('POST', '<%=ConfigurationManager.AppSettings["UrlAPI"]%>/Payments?type=json', false);
            xhr.setRequestHeader('Content-Type', 'application/json');
            xhr.setRequestHeader('authorization', 'Basic <%=ConfigurationManager.AppSettings["BasicCredentials"]%>');
            var createPayment = new Object();
            createPayment.payment = new Object();
            createPayment.payment.Amount = amount;
            createPayment.payment.Comment = desc;
            createPayment.payment.CurrencyId = amountCurrency;
            createPayment.payment.InvoiceReference = invoice;
            createPayment.GatewayId = '<%=ConfigurationManager.AppSettings["GatewayID"]%>';
            xhr.send(JSON.stringify(createPayment));
            return xhr;
        }

        function confirmPayment(paymentId, response, reference, statusId) {
            var xhr = new XMLHttpRequest();
            //it is not asynchronous because the return of the actions.payment.create needs to be handle properly
            xhr.open('PUT', '<%=ConfigurationManager.AppSettings["UrlAPI"]%>/Payments/' + paymentId + '?type=json', false);
                xhr.setRequestHeader('Content-Type', 'application/json');
                xhr.setRequestHeader('authorization', 'Basic <%=ConfigurationManager.AppSettings["BasicCredentials"]%>');
                var confirmPayment = new Object();
                createPayment.payment = new Object();
                confirmPayment.result = new Object();
                confirmPayment.result.StatusId = statusId;
                confirmPayment.result.GatewayReference = reference;
                confirmPayment.result.GatewayResponse = response;
                xhr.send(JSON.stringify(confirmPayment));
                return xhr;
            }

            function clearFeedback() {
                document.getElementById("feedback").innerHTML = "";
            }

            function addSuccessFeedbackMessage(message) {
                addFeedbackMessage(message, 'alert-success');
            }

            function addErrorFeedbackMessage(message) {
                addFeedbackMessage(message, 'alert-danger');
            }

            function addWarnFeedbackMessage(message) {
                addFeedbackMessage(message, 'alert-warning');
            }

            function addFeedbackMessage(message, classname) {
                var alerthtml = '<div class="alert alert-dismissible ' + classname + '" role="alert"><button class="close" type="button" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button><div>' + message + '</div></div>';
                document.getElementById("feedback").innerHTML += alerthtml;
            }

            function parseFeedback(message) {
                var categoryMessages = message.split('<%=ConfigurationManager.AppSettings["CategorySeparator"]%>');
                for (index = 0; index < categoryMessages.length; ++index) {
                    var category = categoryMessages[index];
                    if (category.indexOf('<%=ConfigurationManager.AppSettings["TagMessageError"]%>') > -1) {
                        category = category.replace('<%=ConfigurationManager.AppSettings["TagMessageError"]%>', '');
                        var messages = category.split('<%=ConfigurationManager.AppSettings["MessageSeparator"]%>');
                        for (indexb = 0; indexb < messages.length; ++indexb) {
                            var singlemessage = messages[indexb];
                            addErrorFeedbackMessage(singlemessage);
                        }
                    } else if (category.indexOf('<%=ConfigurationManager.AppSettings["TagMessageWarn"]%>') > -1) {
                        category = category.replace('<%=ConfigurationManager.AppSettings["TagMessageWarn"]%>', '');
                        var messages = category.split('<%=ConfigurationManager.AppSettings["MessageSeparator"]%>');
                        for (indexb = 0; indexb < messages.length; ++indexb) {
                            var singlemessage = messages[indexb];
                            addWarnFeedbackMessage(singlemessage);
                        }
                    }
            }
            errorFeedbackShowed = true;
        }

        $(document).ready().ready(function () {

            $("#amount").blur(function () {
                validateAmount(false)
            });

            $("#amount").keypress(function () {
                validateAmount(true)
            });

            $("#invoice-ieference").blur(function () {
                validateInvoice(false);
            });

            $("#invoice-ieference").keypress(function () {
                validateInvoice(true);
            });

            $("#comments").blur(function () {
                validateComment(false);
            });

            $("#comments").keypress(function () {
                validateComment(true);
            });

            $("#btnPaymentCard").click(function() {
                if (!isFormValid()) {
                    addErrorFeedbackMessage('<%= GetLocalResourceObject("feedback_form_error_message") %>');
                     return false;
                 }

                var amount = $('#amount').val();
                var amountCurrency = $('#amount-input-3').val();
                var desc = $('#comments').val();
                var invoice = $('#invoice-ieference').val();

                if($("#acct").val()=='' || $("#expdate").val()=='' || $("#cvv2").val()=='' || $("#fname").val()==''  || $("#lname").val()=='' || $("#amt").val()=='' ){
                    alert('All fields of payment are required.');
                    if (event.preventDefault) {
                        event.preventDefault();
                    }
                    else {
                        event.returnValue = false;
                    }
                    return false;
                }
                var creationResponse = createPayment(amount, desc, amountCurrency, invoice);
                paymentId = creationResponse.response;
                
            });


        });

         function validateAmount(changing) {
             var currentAmount = $("#amount").val();
             //max 8 numbers before separator and 2 after
             var myRegxp = <%= GetLocalResourceObject("field_amount_validation_regex") %>;
            if (myRegxp.test(currentAmount) == false && !changing) {
                addErrorInput("amount", "<%= GetLocalResourceObject("field_amount_error_label") %>");
                return false;
            } else {
                rmErrorInput("amount");
                return true;
            }
        }

        function validateInvoice(changing) {
            var currentInvoice = $("#invoice-ieference").val();
            //between 5 and 20 characters
            var myRegxp = <%= GetLocalResourceObject("field_invoice_validation_regex") %>;
            if (myRegxp.test(currentInvoice) == false && !changing) {
                addErrorInput("invoice-ieference", "<%= GetLocalResourceObject("field_invoice_error_label") %>");
                    return false;
                } else {
                    rmErrorInput("invoice-ieference");
                    return true;
                }
            }

            function validateComment(changing) {
                var currentComment = $("#comments").val();
                //between 0 and 1000 characters
                var myRegxp = <%= GetLocalResourceObject("field_comments_validation_regex") %>;
                if (myRegxp.test(currentComment) == false && !changing) {
                    addErrorInput("comments", "<%= GetLocalResourceObject("field_comments_error_label") %>");
                    return false;
                } else {
                    rmErrorInput("comments");
                    return true;
                }
            }

            function isFormValid() {
                return (validateAmount() && validateComment() && validateInvoice());
            }

            function addErrorInput(inputId, error) {
                $("#" + inputId).parent().addClass("has-error")
                $("#helpBlock-" + inputId).text(error)
            }

            function rmErrorInput(inputId) {
                $("#" + inputId).parent().removeClass("has-error")
                $("#helpBlock-" + inputId).text("")
            }


            function getElements(el) {
                return Array.prototype.slice.call(document.querySelectorAll(el));
            }

            function hideElement(el) {
                document.body.querySelector(el).style.display = 'none';
            }

            function showElement(el) {
                document.body.querySelector(el).style.display = 'block';
            }

            getElements('input[name=payment-option]').forEach(function(el) {
                el.addEventListener('change', function(event) {

                    if (event.target.value === 'paypal') {
                        hideElement('#card-button-container');
                        showElement('#paypal-button-container');
                    }

                    if (event.target.value === 'card') {
                        showElement('#card-button-container');
                        hideElement('#paypal-button-container');
                    }
                });
            });

            hideElement('#card-button-container');

            paypal.Button.render({
                env: '<%=ConfigurationManager.AppSettings["PaypalEnv"]%>', // sandbox | production

                // PayPal Client IDs - replace with your own

                client: {
                    //sandbox: 'AZDxjDScFpQtjWTOUtWKbyN_bDt4OgqaF4eYXlewfBP4-8aqX3PiV8e1GWU6liB2CUXlkA59kJXE7M6R',
                    <%=ConfigurationManager.AppSettings["PaypalEnv"]%>: '<%=ConfigurationManager.AppSettings["PaypalClientID"]%>'
                },

                // Show the buyer a 'Pay Now' button in the checkout flow
                commit: true,

                // payment() is called when the button is clicked
                payment: function (data, actions) {
                    clearFeedback();
                    if (!isFormValid()) {
                        addErrorFeedbackMessage('<%= GetLocalResourceObject("feedback_form_error_message") %>');
                        return false;
                    }

                    var amount = $('#amount').val();
                    var amountCurrency = $('#amount-input-3').val();
                    var desc = $('#comments').val();
                    var invoice = $('#invoice-ieference').val();

                    // Make a call to the REST api to create the payment
                    var creationResponse = createPayment(amount, desc, amountCurrency, invoice);
                    paymentId = creationResponse.response;

                    if (creationResponse.status == 200) {
                        paymentId = creationResponse.response;
                        return actions.payment.create({
                            payment: {
                                transactions: [
                                    {
                                        amount: { total: amount, currency: amountCurrency },
                                        description: desc,
                                        invoice_number: invoice
                                    }
                                ]
                            },
                            experience: {
                                presentation: {
                                    brand_name: '<%= GetLocalResourceObject("brand_name") %>',
                                    logo_image: 'https://www.winterbotham.com/mobile/images/logo270x115.png'
                                },
                                input_fields: {
                                    no_shipping: 1,
                                    allow_note: true
                                }
                            }
                        });
                    } else {
                        var responseObject = JSON.parse(creationResponse.response);
                        if (responseObject.hasOwnProperty("Message")) {
                            parseFeedback(responseObject.Message);
                        } else {
                            addErrorFeedbackMessage('<%= GetLocalResourceObject("feedback_paypal_error_message") %>');
                            errorFeedbackShowed = true;
                        }
                        return false;
                    }
                },
                // onAuthorize() is called when the buyer approves the payment
                onAuthorize: function (data, actions) {

                    // Make a call to the REST api to execute the payment
                    return actions.payment.execute().then(function () {
                        addSuccessFeedbackMessage('<%= GetLocalResourceObject("feedback_paypal_success_message") %>');
                        confirmPayment(paymentId, JSON.stringify(data), data.paymentID, "<%=ConfigurationManager.AppSettings["StatusApprovedID"]%>");
                    });
                },

                onCancel: function (data, actions) {
                    addErrorFeedbackMessage('<%= GetLocalResourceObject("feedback_paypal_cancelled_message") %>');
                    confirmPayment(paymentId, JSON.stringify(data), (data.paymentID ? data.paymentID : "no_reference"), "<%=ConfigurationManager.AppSettings["StatusCancelledID"]%>");
                },

                onError: function (err) {
                    if (isFormValid()) {
                        if (errorFeedbackShowed) {
                            errorFeedbackShowed = false;
                        } else {
                            addErrorFeedbackMessage('<%= GetLocalResourceObject("feedback_paypal_error_message") %>');
                        }
                        //if the error was after payment creation, confirm it.
                        if (paymentId != 0) {
                            confirmPayment(paymentId, JSON.stringify(err), "error", "<%=ConfigurationManager.AppSettings["StatusDeniedID"]%>");
                        }
                    }
                }

            }, '#paypal-button-container');

    </script>
    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js" integrity="sha384-vFJXuSJphROIrBnz7yo7oB41mKfc8JzQZiCq4NCceLEaO4IHwicKwpJf9c9IpFgh" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js" integrity="sha384-alpBpkh1PFOepccYVYDB4do5UnbKysX5WZXm3XxPqe5iKTfUKjNkCk9SaVuEZflJ" crossorigin="anonymous"></script>
</body>
</html>
