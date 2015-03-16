$('.nameForm').validate({
    rules: {
            'firstName': {
                minlength: 3,
                maxlength: 15,
                required: true

            },
            'lastName': {
                minlength: 3,
                maxlength: 15,
                required: true
            },
    },
    messages: {
        firstName: {
            required: "Please provide a valid first name.",
            minlength: "Please enter at least 3 characters."
        },
        lastName: {
            required: "Please provide a valid last name.",
            minlength: "Please enter at least 3 characters."
        },
    },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
            $(element).closest('.form-group').addClass('has-success');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        validClass: 'has-success glyphicon glyphicon-ok',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
});
$('.emailForm').validate({
    rules: {
        'searchEmail': {
            email: true,
            required: true
        },
    },
    messages: {
        searchEmail: {
            required: "Please enter a valid email."
        },
    },

    highlight: function (element) {
        $(element).closest('.form-group').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
    },
    errorElement: 'span',
    errorClass: 'help-block',
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    }
});
$('#typeOfPayment').change(function () {

    /* Get the selected value of dropdownlist */
    var selectedID = $(this).val();

    /* Request the partial view with .get request. */
    if (selectedID == 1) //online
        {
            $.get('/MemberSponsorships/payPalAction/', function (data) {

                /* data is the pure html returned from action method, load it to your page */
                $('#renderPaypal').html(data);
                /* little fade in effect */
                $('#renderPaypal').fadeIn('fast');
            });
    }
    else
    {
        $('#renderPaypal').empty();
    }
});