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


        }
        else {
            $('#invalidEmailMessege').removeClass('d-none');

        }
    });

    $('#emailInput').keyup(function (e) {
        if (e.key == 'Enter') {
            $('#nextRegisterForm').click();
        }
    });

    $('#registerNewUser').on('click', function () {
        console.log("registering...");
        userEmail = $('#emailInput').val();
        userNameInput = $('#username').val();
        passwordInput = $('#password').val();
        confirmedPasswordInput = $('#passwordConfirm').val();

        if (passwordInput != confirmedPasswordInput) {
            $('#invalidConfirmedPasswordMessege').removeClass('d-none');
        }
        else {
            $('#invalidConfirmedPasswordMessege').addClass('d-none');
        }

        if (userNameInput.length <= 4) {
            $('#invalidUserNameMessege').removeClass('d-none');
        }
        else {
            $('#invalidUserNameMessege').addClass('d-none');
        }

        console.log(userEmail);
        console.log(userNameInput);
        console.log(passwordInput);
        console.log(confirmedPasswordInput);
    })

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


