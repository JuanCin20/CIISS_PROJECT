jQuery.validator.addMethod("Valid_E_Mail_Usuario", function (value, element) {
  return (
    this.optional(element) || /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/.test(value)
  );
});

$(document).ready(function () {
  $("#Alert_Div").hide();

  $("#Form_Reset_Password").validate({
    rules: {
      E_Mail_Usuario: {
        required: true,
        Valid_E_Mail_Usuario: true,
        maxlength: 30,
      },
    },
    messages: {
      E_Mail_Usuario: {
        required: "Campo Requerido: Correo Electr\xf3nico del Usuario",
        Valid_E_Mail_Usuario: "Ingrese un Correo Electr\xf3nico V\xe1lido",
        maxlength:
          "El Correo Electr\xf3nico del Usuario debe Contener un M\xe1ximo de 30 Caracteres",
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
    highlight: function (element, errorClass, validClass) {
      $(element).addClass("is-invalid").removeClass("is-valid");
    },
    unhighlight: function (element, errorClass, validClass) {
      $(element).addClass("is-valid").removeClass("is-invalid");
    },
  });
});

$.validator.setDefaults({
  submitHandler: function () {
    console.log("Success!");
  },
});

function Procesar() {
  if (!$("#Form_Reset_Password").valid()) {
    return;
  } else {
    var e_Mail_Usuario = $.trim($("#E_Mail_Usuario").val());

    jQuery.ajax({
      // ? url: "@Url.Action("Access_Controller_Reset_Password", "Access")",
      url: "https://localhost:44381/Access/Access_Controller_Reset_Password",
      type: "POST",
      data: { E_Mail_Usuario: e_Mail_Usuario },
      success: function (data) {
        // debugger; // TODO: Punto de Depuración

        $("body").LoadingOverlay("hide");

        if (data.message == "Success!") {
          window.location.replace("https://localhost:44381/Access/Log_In");
        } else {
          if (data.message != "Success!") {
            $("#Alert_Div").show();
            $("#Alert_Div").html(
              "<i class='fa-solid fa-triangle-exclamation'></i>&nbsp;" +
                data.message +
                ""
            );
          }
        }
      },
      error: function (xhr, status, error) {
        $("body").LoadingOverlay("hide");
        alert(xhr.responseText);
      },
      beforeSend: function () {
        $("body").LoadingOverlay("show", {
          background: "rgba(0, 0, 0, 0.5)",
          image: "../../img/clock-regular.svg",
          imageAnimation: "1.5s fadein",
          imageAutoResize: true,
          imageResizeFactor: 1,
          imageColor: "#FFD43B",
        });
      },
    });
  }
}