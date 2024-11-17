jQuery.validator.addMethod("Valid_E_Mail_Usuario", function (value, element) {
  return (
    this.optional(element) || /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/.test(value)
  );
});

jQuery.validator.addMethod("Valid_Password_Usuario", function (value, element) {
  return (
    this.optional(element) ||
    /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$/.test(
      value
    )
  );
});

$(document).ready(function () {
  var Count = 0;
  $(".Invisible").hide();

  $(".card-footer").on("click", function () {
    Count += 1;

    if (Count == 5) {
      $(".Invisible").show();
    }
  });

  $("#Input_Password_Usuario").on("click", function () {
    const Password_Usuario = document.getElementById("Password_Usuario");
    if (Password_Usuario.type === "password") {
      Password_Usuario.type = "text";
      $("#Control_Eye").removeClass("fa-solid fa-eye");
      $("#Control_Eye").addClass("fa-solid fa-eye-slash");
    } else {
      Password_Usuario.type = "password";
      $("#Control_Eye").addClass("fa-solid fa-eye");
      $("#Control_Eye").removeClass("fa-solid fa-eye-slash");
    }
  });

  $("#Form_Log_In").validate({
    rules: {
      E_Mail_Usuario: {
        required: true,
        Valid_E_Mail_Usuario: true,
        maxlength: 30,
      },
      Password_Usuario: {
        required: true,
        Valid_Password_Usuario: true,
      },
    },
    messages: {
      E_Mail_Usuario: {
        required: "Campo Requerido: Correo Electr\xf3nico del Usuario",
        Valid_E_Mail_Usuario: "Ingrese un Correo Electr\xf3nico V\xe1lido",
        maxlength:
          "El Correo Electr\xf3nico del Usuario debe Contener un M\xe1ximo de 30 Caracteres",
      },
      Password_Usuario: {
        required: "Campo Requerido: Contrase\xf1a del Usuario",
        Valid_Password_Usuario:
          "La Contrase\xf1a Debe Contener al Menos: Una Letra en Min\xfascula, Una Letra en May\xfascula, Un N\xfamero, Un Caracter Especial y Entre 8-20 Caracteres",
      },
    },
    errorElement: "em",
    errorPlacement: function (error, element) {
      // Add the "invalid-feedback" class to the error element
      error.addClass("invalid-feedback");

      if (element.prop("type") === "checkbox") {
        error.insertAfter(element.next("label"));
      } else {
        error.insertAfter(element);
      }
    },
    highlight: function (element) {
      $(element).removeClass("is-valid").addClass("is-invalid");
    },
    unhighlight: function (element) {
      $(element).removeClass("is-invalid").addClass("is-valid");
    },
  });
});