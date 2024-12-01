function Show_User_Image(input) {
  if (input.files) {
    var Reader = new FileReader();
    Reader.onload = function (event) {
      $("#Imagen_Usuario").attr("src", event.target.result);
    };
    Reader.readAsDataURL(input.files[0]);
  }
}

jQuery.validator.addMethod("Valid_Nombre_Usuario", function (value, element) {
  return (
    this.optional(element) ||
    /^[a-zA-ZÀ-ÿ\u00f1\u00d1\u00E0-\u00FC]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1\u00E0-\u00FC]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1\u00E0-\u00FC]+$/.test(
      value
    )
  );
});

jQuery.validator.addMethod("Valid_Apellido_Usuario", function (value, element) {
  return (
    this.optional(element) ||
    /^[a-zA-ZÀ-ÿ\u00f1\u00d1\u00E0-\u00FC]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1\u00E0-\u00FC]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1\u00E0-\u00FC]+$/.test(
      value
    )
  );
});

jQuery.validator.addMethod("Valid_E_Mail_Usuario", function (value, element) {
  return (
    this.optional(element) || /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/.test(value)
  );
});

$(document).ready(function () {
  $("#Alert_Div").hide();

  $("#Form_User").validate({
    rules: {
      Nombre_Usuario: {
        required: true,
        Valid_Nombre_Usuario: true,
        maxlength: 50,
      },
      Apellido_Usuario: {
        required: true,
        Valid_Apellido_Usuario: true,
        maxlength: 50,
      },
      E_Mail_Usuario: {
        required: true,
        Valid_E_Mail_Usuario: true,
        maxlength: 30,
      },
      Imagen_Usuario_Input: {
        required: true,
      },
    },
    messages: {
      Nombre_Usuario: {
        required: "Campo Requerido: Nombres del Usuario",
        Valid_Nombre_Usuario: "Ingrese Nombres V\xe1lidos",
        maxlength:
          "El Nombre del Usuario debe Contener un M\xe1ximo de 50 Caracteres",
      },
      Apellido_Usuario: {
        required: "Campo Requerido: Apellidos del Usuario",
        Valid_Apellido_Usuario: "Ingrese Apellidos V\xe1lidos",
        maxlength:
          "El Apellido del Usuario debe Contener un M\xe1ximo de 50 Caracteres",
      },
      E_Mail_Usuario: {
        required: "Campo Requerido: Correo Electr\xf3nico del Usuario",
        Valid_E_Mail_Usuario: "Ingrese un Correo Electr\xf3nico V\xe1lido",
        maxlength:
          "El Correo Electr\xf3nico del Usuario debe Contener un M\xe1ximo de 30 Caracteres",
      },
      Imagen_Usuario_Input: {
        required: "Campo Requerido: Im\xe1gen del Usuario",
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
  if (!$("#Form_User").valid()) {
    return;
  } else {
    var E_Mail_Usuario = $.trim($("#E_Mail_Usuario").val());
    jQuery.ajax({
      url:
        "https://emailvalidation.abstractapi.com/v1/?api_key=b4e60d1d263944809648ac5b6aa14ec8&email=" +
        E_Mail_Usuario +
        "",
      type: "GET",
      dataType: "json",
      success: function (data) {
        // debugger; // TODO: Punto de Depuración

        if (data.deliverability == "DELIVERABLE") {
          var Imagen_Usuario_Input = $("#Imagen_Usuario_Input")[0].files[0];

          var Usuario = {
            nombre_Usuario: $("#Nombre_Usuario").val(),
            apellido_Usuario: $("#Apellido_Usuario").val(),
            e_Mail_Usuario: $.trim($("#E_Mail_Usuario").val()),
            obj_Class_Entity_Tipo_Usuario: {
              iD_Tipo_Usuario: 1,
            },
          };

          var Request = new FormData();
          Request.append("Obj_Class_Entity_Usuario", JSON.stringify(Usuario));
          Request.append("Obj_IFormFile", Imagen_Usuario_Input);

          jQuery.ajax({
            // ? url: "@Url.Action("Access_Controller_Sign_Up", "Access")",
            url: "https://localhost:44381/Access/Access_Controller_Sign_Up",
            type: "POST",
            data: Request,
            processData: false,
            contentType: false,
            success: function (data) {
              // debugger; // TODO: Punto de Depuración

              $("body").LoadingOverlay("hide");

              if (data.iD_Auto_Generated != 0) {
                window.location.replace("/Access/Log_In");
              } else {
                $("#Alert_Container").html("");
                $("<div>")
                  .addClass("alert alert-danger")
                  .attr({ role: "alert" })
                  .append(
                    $("<i>").addClass("fa-solid fa-triangle-exclamation"),
                    $("<label>").html("&nbsp;&nbsp;" + data.message)
                  )
                  .appendTo("#Alert_Container");
              }
            },
            error: function (xhr, status, error) {
              $("body").LoadingOverlay("hide");
              alert(xhr.responseText);
            },
            beforeSend: function () {
              $("body").LoadingOverlay("show", {
                background: "rgba(0, 0, 0, 0.5)",
                image: "../img/clock-regular.svg",
                imageAnimation: "1.5s fadein",
                imageAutoResize: true,
                imageResizeFactor: 1,
                imageColor: "#FFD43B",
              });
            },
          });
        } else {
          Swal.fire({
            title: "Error",
            text: "El Correo Electrónico Ingresado No Existe",
            icon: "error",
          });
        }
      },
    });
  }
}