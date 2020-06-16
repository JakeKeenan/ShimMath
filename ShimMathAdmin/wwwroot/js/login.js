/*//var forge = require('node-forge');
var md = forge.md.sha1.create();
md.update('The quick brown fox jumps over the lazy dog');
console.log(md.digest().toHex())

//password is hashed on the client side in-order to prove
//ShimMath is not farming passwords. Further Secuirty measures
//are taken on the server side to protect the password (See: UserService.cs)
function HashPassword(unHashedPassword, publicSalt){
    var md = forge.md.sha256.create();
    md.update(unHashedPassword + publicSalt, 'utf8');
    hashedPassword = md.digest().toHex()
    //console.log(md.digest().toHex())
}*/

$(document).ready(function () {

    $('#nextRegisterForm').on('click', function () {
        emailInput = $('#emailInput').val();

        //var email = new RegExp('^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$');
        email_regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i;
        if (email_regex.test(emailInput)) {
            checkIsNotUsedEmail(emailInput,
                function (errorMessage) {
                    $('#invalidEmailMessege').removeClass('d-none');
                    $('#invalidEmailMessege').html(errorMessage);
                },
                function () {
                    $('#registerEmail').hide();
                    $('#registerUserNamePassword').show();
                    $('#registerUserNamePassword').css('display', 'flex');
                    $backButton = $('#backButton');
                    $backButton.addClass('pointer');
                    $backButton.addClass('backButton');
                    $backButton.children('.d-none').removeClass('d-none');
                    $backButton.on('click', function () {
                        $('#registerEmail').show();
                        $('#registerUserNamePassword').hide();
                        $('#registerUserNamePassword').css('display', 'none');

                        $(this).removeClass('pointer');
                        $(this).removeClass('backButton');
                        $(this).children().addClass('d-none');
                    });
                });
        }
        else {
            $('#invalidEmailMessege').removeClass('d-none');
            $('#invalidEmailMessege').html("You need a valid email to continue!");
        }
    });

    $('#username').blur(function () {
        usernameInput = $('#username').val();
        if (usernameInput.length <= 4) {
            $('#invalidUsernameMessage').removeClass('d-none');
            $('#invalidUsernameMessege').html("Username must be longer than 3 characters");
        }
        else {
            checkIsNotUsedUsername(usernameInput,
                function (errorMessage) {
                    $('#invalidUsernameMessage').removeClass('d-none');
                    $('#invalidUsernameMessege').html(errorMessage);
                },
                function () {
                    $('#invalidUsernameMessage').addClass('d-none');
                });
        }
    });

    function checkIsNotUsedUsername(usernameInput, responseFailResult, responseSuccessResult) {
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                returnStatus = JSON.parse(this.responseText);

                if (returnStatus.isSuccessful == false) {
                    responseFailResult(returnStatus.errorMessage);
                }
                else {
                    responseSuccessResult();
                }
            }
        }
        var WebAPIUrl = window.location.hostname;
        console.log(WebAPIUrl);
        //if (WebAPIUrl.charAt(WebAPIUrl.length - 1) == '/') {
        //    //WebAPIUrl = WebAPIUrl.substr(0, WebAPIUrl.length - 1);
        //    WebAPIUrl = WebAPIUrl + "Home";
        //    console.log(WebAPIUrl);
        //}
        requestText = "/account/IsNotUsedUsername?username=" + encodeURIComponent(usernameInput);
        xhttp.open("GET", requestText, true);
        xhttp.send();
    }

    function checkIsNotUsedEmail(emailInput, responseFailResult, responseSuccessResult) {
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            console.log(this.status);
            console.log(this.readyState);
            if (this.readyState == 4 && this.status == 200) {
                returnStatus = JSON.parse(this.responseText);
                if (returnStatus.isSuccessful == false) {

                    responseFailResult(returnStatus.errorMessage);
                }
                else {
                    responseSuccessResult();
                }
            }
        };
        var WebAPIUrl = window.location.hostname;
        console.log(WebAPIUrl);
        //if (WebAPIUrl.charAt(WebAPIUrl.length - 1) == '/') {
        //    //WebAPIUrl = WebAPIUrl.substr(0, WebAPIUrl.length - 1);
        //    WebAPIUrl = WebAPIUrl + "Home";
        //    console.log(WebAPIUrl);
        //}
        requestText = "/account/IsNotUsedEmail?email=" + encodeURIComponent(emailInput);
        console.log(requestText);
        xhttp.open("GET", requestText, true);
        xhttp.send();
    }

    function register(RegisterModel) {
        //var WebAPIUrl = window.location.href;
        requestText = "account/Register";

        $.ajax({
            url: requestText,
            type: 'POST',
            dataType: 'json',
            data: JSON.stringify(RegisterModel),
            contentType: 'application/json; charset=utf-8',
            success: function() {
                var message = data.Message;
                location.reload();
                console.log(message);
            },
            error: function () {
d                //console.log(message);
            }
        });
    }

    $('#emailInput').keyup(function (e) {
        if (e.key == 'Enter') {
            $('#nextRegisterForm').click();
        }
    });

    $('#signIn').on('click', function () {
        console.log("signing in...");
        usernameOrEmail = $('#usernameOrEmailSignIn').val();
        passwordInput = $('#passwordSignIn').val();
        unacceptablePassword = false;
        unacceptableUsername = false;
        unacceptableEmail = false;
        // Validate lowercase letters
        var lowerCaseLetters = /[a-z]/g;
        if (!$password.val().match(lowerCaseLetters)) {
            unacceptablePassword = true;
        }

        // Validate capital letters
        var upperCaseLetters = /[A-Z]/g;
        if (!$password.val().match(upperCaseLetters)) {
            unacceptablePassword = true;
        }

        // Validate numbers
        var numbers = /[0-9]/g;
        if (!$password.val().match(numbers)) {
            unacceptablePassword = true;
        }

        // Validate length
        if (!$password.val().length >= 8) {
            unacceptablePassword = true;
        }

        //Check Username/Email
        email_regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i;
        if (usernameOrEmail.length <= 4 || !email_regex.test(usernameOrEmail)) {
            unacceptableUsername = false;
            unacceptableEmail = false;
        }

        if (unacceptablePassword) {
            $('#incorrectPasswordResponse').removeClass('d-none');
        }
        else if (unacceptableUsername && unacceptableEmail) {
            $('#incorrectEmailOrUsernameResponse').removeClass('d-none');
        }
            /*
        else {
            signInWasSuccessful = false;
            if () {
                signInWasSuccessful = signInUserWithEmail(usernameOrEmail, passwordInput);
                if (signInWasSuccessful) {
                    location.reload();
                }
                else (){
                    $('#incorrectPasswordResponse').addClass('text-danger');
                }
            }
            else if ( && !signInWasSuccessful) {
                signInWasSuccessful = signInUserWithUserName(usernameOrEmail, passwordInput);
                if (signInWasSuccessful) {
                    location.reload();
                }
                else (){
                    $('#incorrectPasswordResponse').addClass('text-danger');
                }
            }
        }*/
    })

    function signInUserWithUsername(user, password) {

    }

    function signInUserWithEmail(email, password) {

    }

    $('#registerNewUser').on('click', function () {
        console.log("registering...");
        userEmail = $('#emailInput').val();
        usernameInput = $('#username').val();
        passwordInput = $('#password').val();
        confirmedPasswordInput = $('#passwordConfirm').val();

        if (passwordInput != confirmedPasswordInput) {
            $('#invalidConfirmedPasswordMessege').removeClass('d-none');
        }
        else {
            $('#invalidConfirmedPasswordMessege').addClass('d-none');
        }

        if (usernameInput.length <= 4) {
            $('#invalidUserNameMessege').removeClass('d-none');
        }
        else {
            $('#invalidUserNameMessege').addClass('d-none');
        }
        unHashedPassword = passwordInput;
        publicSalt = model.publicSalt;

        console.log(userEmail);
        console.log(usernameInput);
        console.log(passwordInput);
        console.log(confirmedPasswordInput);
        console.log(publicSalt);

        hashedPassword = HashPassword(unHashedPassword, publicSalt);
        hashedConfirmPassword = HashPassword(confirmedPasswordInput, publicSalt);
        console.log(hashedPassword);

        var RegisterModel = new Object();
        RegisterModel.Username = usernameInput;
        RegisterModel.Email = userEmail;
        RegisterModel.Password = hashedPassword;
        RegisterModel.ConfirmPassword = hashedConfirmPassword;


        register(RegisterModel)

    });

    function HashPassword(unHashedPassword, publicSalt) {
        var md = forge.md.sha256.create();
        md.update(unHashedPassword + publicSalt, 'utf8');
        hashedPassword = md.digest().toHex()
        //console.log(md.digest().toHex())
        return hashedPassword;
    }

    $('#password').focus(function () {
        $('#passwordRequirementsMessage').removeClass('d-none');
    });

    $('#password').blur(function () {
        if ($(this).val() == "") {
            $('#passwordRequirementsMessage').addClass('d-none');
        }

    });

    $('#password').keyup(function () {
        $password = $(this);
        $letter = $('#letter');
        $capital = $('#capital');
        $number = $('#number');
        $length = $('#length');

        unacceptablePassword = false;
        // Validate lowercase letters
        var lowerCaseLetters = /[a-z]/g;
        if ($password.val().match(lowerCaseLetters)) {
            $letter.removeClass("text-danger");
            $letter.addClass("text-success");
        } else {
            $letter.removeClass("text-success");
            $letter.addClass("text-danger");
            unacceptablePassword = true;
        }

        // Validate capital letters
        var upperCaseLetters = /[A-Z]/g;
        if ($password.val().match(upperCaseLetters)) {
            $capital.removeClass("text-danger");
            $capital.addClass("text-success");
        } else {
            $capital.removeClass("text-success");
            $capital.addClass("text-danger");
            unacceptablePassword = true;
        }

        // Validate numbers
        var numbers = /[0-9]/g;
        if ($password.val().match(numbers)) {
            $number.removeClass("text-danger");
            $number.addClass("text-success");
        } else {
            $number.removeClass("text-success");
            $number.addClass("text-danger");
            unacceptablePassword = true;
        }

        // Validate length
        if ($password.val().length >= 8) {
            $length.removeClass("text-danger");
            $length.addClass("text-success");
        } else {
            $length.removeClass("text-success");
            $length.addClass("text-danger");
            unacceptablePassword = true;
        }

        if (!unacceptablePassword) {
            $('#requirementsMessage').removeClass('text-danger');
            $('#requirementsMessage').addClass('text-success');
        }
        else {
            $('#requirementsMessage').removeClass('text-success');
            $('#requirementsMessage').addClass('text-danger');
        }
    });
});


