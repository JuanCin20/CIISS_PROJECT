var Table_Proveedor_Insumo;
var Table_Proveedor_Insumo_Alternative;
var Selected_Row;

// ? var Url_01 = "@Url.Action("Management_Controller_Proveedor_Insumo_Listar", "Management")",
var Url_01 =
  "https://localhost:44381/Management/Management_Controller_Proveedor_Insumo_Listar" +
  "?Estado_Proveedor_Insumo=" +
  true;

$(document).ready(function () {
  Table_Proveedor_Insumo = $("#Table_Proveedor_Insumo").DataTable({
    fnDrawCallback: function () {
      // !
      $(document).ready(function () {
        $(".Pop_Trigger").popover({
          trigger: "hover focus",
          animation: true,
        });
      });
      // !
    },
    responsive: true,
    ordering: false,
    language: {
      url: "//cdn.datatables.net/plug-ins/2.1.8/i18n/es-MX.json",
    },
    ajax: {
      url: Url_01,
      type: "GET",
      dataType: "json",
    },
    columns: [
      { data: "iD_Proveedor_Insumo" },
      {
        data: "estado_Proveedor_Insumo",
        render: function (estado_Proveedor_Insumo) {
          if (estado_Proveedor_Insumo) {
            return '<span class="badge text-bg-success">Disponible</span>';
          } else {
            return '<span class="badge text-bg-danger">No Disponible</span>';
          }
        },
      },
      {
        data: null,
        render: function (data, type, row) {
          return (
            '<a tabindex="' +
            row.iD_Proveedor_Insumo +
            '" href="#/" class="Pop_Trigger" data-bs-html="true" data-bs-custom-class="custom-popover" data-bs-container="body" data-bs-toggle="popover" data-bs-title="Informaci\xf3n" data-bs-content="<p><b>Tel\xe9fono:</b> ' +
            row.telefono_Proveedor_Insumo +
            "</p><p><b>E-Mail:</b> " +
            row.e_Mail_Proveedor_Insumo +
            "</p><p><b>Direcci\xf3n:</b> " +
            row.direccion_Proveedor_Insumo +
            '</p>">' +
            row.nombre_Proveedor_Insumo +
            "</a>"
          );
        },
      },
      {
        data: "supply_Number",
        render: function (supply_Number) {
          if (supply_Number == 0) {
            return '<label class="text-danger">' + supply_Number + "</label>";
          } else {
            return '<label class="text-success">' + supply_Number + "</label>";
          }
        },
      },
      { data: "fecha_Registro_Proveedor_Insumo" },
      {
        defaultContent:
          '<button type="button" class="btn btn-primary btn-sm Edit_Button"><i class="fa-solid fa-pencil"></i></button>',
        orderable: false,
        searchable: false,
        width: "90px",
      },
    ],
  });
});

function Open_Form_Modal(data) {
  if (data == null) {
    $("#Nombre_Proveedor_Insumo").removeClass("is-valid");
    $("#Nombre_Proveedor_Insumo").removeClass("is-invalid");
    $("#Nombre_Proveedor_Insumo").prop("disabled", false);
    $("#Telefono_Proveedor_Insumo").removeClass("is-valid");
    $("#Telefono_Proveedor_Insumo").removeClass("is-invalid");
    $("#E_Mail_Proveedor_Insumo").removeClass("is-valid");
    $("#E_Mail_Proveedor_Insumo").removeClass("is-invalid");
    $("#Direccion_Proveedor_Insumo").removeClass("is-valid");
    $("#Direccion_Proveedor_Insumo").removeClass("is-invalid");
    $("#Direccion_Proveedor_Insumo").prop("disabled", false);
    $("#ID_Proveedor_Insumo").val(0);
    $("#Nombre_Proveedor_Insumo").val("");
    $("#Telefono_Proveedor_Insumo").val("");
    $("#E_Mail_Proveedor_Insumo").val("");
    $("#Direccion_Proveedor_Insumo").val("");
  } else {
    if (data != null) {
      $("#Nombre_Proveedor_Insumo").removeClass("is-valid");
      $("#Nombre_Proveedor_Insumo").removeClass("is-invalid");
      $("#Nombre_Proveedor_Insumo").prop("disabled", true);
      $("#Telefono_Proveedor_Insumo").removeClass("is-valid");
      $("#Telefono_Proveedor_Insumo").removeClass("is-invalid");
      $("#E_Mail_Proveedor_Insumo").removeClass("is-valid");
      $("#E_Mail_Proveedor_Insumo").removeClass("is-invalid");
      $("#Direccion_Proveedor_Insumo").removeClass("is-valid");
      $("#Direccion_Proveedor_Insumo").removeClass("is-invalid");
      $("#Direccion_Proveedor_Insumo").prop("disabled", true);
      $("#ID_Proveedor_Insumo").val(data.iD_Proveedor_Insumo);
      $("#Nombre_Proveedor_Insumo").val(data.nombre_Proveedor_Insumo);
      $("#Telefono_Proveedor_Insumo").val(data.telefono_Proveedor_Insumo);
      $("#E_Mail_Proveedor_Insumo").val(data.e_Mail_Proveedor_Insumo);
      $("#Direccion_Proveedor_Insumo").val(data.direccion_Proveedor_Insumo);
    }
  }
  $("#Form_Modal").modal("show");
}

function Selected_Row_Function(data) {
  // ? Obtener la Fila Actual
  var Selected_Row = $(data).parents("tr");
  // ? Compruebe si la Fila Actual es una Fila Secundaria
  if (Selected_Row.hasClass("child")) {
    // ? Si es así, Señale la Fila Anterior (It's "parent")
    Selected_Row = Selected_Row.prev();
  }
  return Selected_Row;
}

$("#Table_Proveedor_Insumo").on("click", ".Edit_Button", function () {
  Selected_Row = Selected_Row_Function(this);
  var data = Table_Proveedor_Insumo.row(Selected_Row).data();
  // console.log(data); // ? Good 'console.log'
  Open_Form_Modal(data);
});

function Open_Form_Modal_Alternative() {
  // ? var Url_02 = "@Url.Action("Management_Controller_Proveedor_Insumo_Listar", "Management")",
  var Url_02 =
    "https://localhost:44381/Management/Management_Controller_Proveedor_Insumo_Listar" +
    "?Estado_Proveedor_Insumo=" +
    false;

  Table_Proveedor_Insumo_Alternative = $(
    "#Table_Proveedor_Insumo_Alternative"
  ).DataTable({
    fnDrawCallback: function () {
      // !
      $(document).ready(function () {
        $(".Pop_Trigger").popover({
          trigger: "hover focus",
          animation: true,
        });
      });
      // !
    },
    retrieve: true,
    responsive: true,
    ordering: false,
    language: {
      url: "//cdn.datatables.net/plug-ins/2.1.8/i18n/es-MX.json",
    },
    ajax: {
      url: Url_02,
      type: "GET",
      dataType: "json",
    },
    columns: [
      { data: "iD_Proveedor_Insumo" },
      {
        data: "estado_Proveedor_Insumo",
        render: function (estado_Proveedor_Insumo) {
          if (estado_Proveedor_Insumo) {
            return '<span class="badge text-bg-success">Disponible</span>';
          } else {
            return '<span class="badge text-bg-danger">No Disponible</span>';
          }
        },
      },
      {
        data: null,
        render: function (data, type, row) {
          return (
            '<a tabindex="' +
            row.iD_Proveedor_Insumo +
            '" href="#/" class="Pop_Trigger" data-bs-html="true" data-bs-custom-class="custom-popover" data-bs-container="body" data-bs-toggle="popover" data-bs-title="Informaci\xf3n" data-bs-content="<p><b>Tel\xe9fono:</b> ' +
            row.telefono_Proveedor_Insumo +
            "</p><p><b>E-Mail:</b> " +
            row.e_Mail_Proveedor_Insumo +
            "</p><p><b>Direcci\xf3n:</b> " +
            row.direccion_Proveedor_Insumo +
            '</p>">' +
            row.nombre_Proveedor_Insumo +
            "</a>"
          );
        },
      },
      {
        data: "supply_Number",
        render: function (supply_Number) {
          if (supply_Number == 0) {
            return '<label class="text-danger">' + supply_Number + "</label>";
          } else {
            return '<label class="text-success">' + supply_Number + "</label>";
          }
        },
      },
      { data: "fecha_Registro_Proveedor_Insumo" },
      {
        defaultContent: '<label class="text-warning">En Espera...</label>',
        orderable: false,
        searchable: false,
        width: "90px",
      },
    ],
  });
  $("#Static_Backdrop").modal("show");
}

jQuery.validator.addMethod(
  "Valid_Nombre_Proveedor_Insumo",
  function (value, element) {
    return (
      this.optional(element) ||
      /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
    );
  }
);

jQuery.validator.addMethod(
  "Valid_Telefono_Proveedor_Insumo",
  function (value, element) {
    return (
      this.optional(element) ||
      /^(?:\+1)?\s?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{3}$/.test(value)
    );
  }
);

jQuery.validator.addMethod(
  "Valid_E_Mail_Proveedor_Insumo",
  function (value, element) {
    return (
      this.optional(element) || /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/.test(value)
    );
  }
);

jQuery.validator.addMethod(
  "Valid_Direccion_Proveedor_Insumo",
  function (value, element) {
    return (
      this.optional(element) ||
      /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
    );
  }
);

$(document).ready(function () {
  $("#Form_Supplier").validate({
    rules: {
      Nombre_Proveedor_Insumo: {
        required: true,
        Valid_Nombre_Proveedor_Insumo: true,
        maxlength: 50,
      },
      Telefono_Proveedor_Insumo: {
        required: true,
        number: true,
        Valid_Telefono_Proveedor_Insumo: true,
        digits: true,
      },
      E_Mail_Proveedor_Insumo: {
        required: true,
        Valid_E_Mail_Proveedor_Insumo: true,
        maxlength: 30,
      },
      Direccion_Proveedor_Insumo: {
        required: true,
        Valid_Direccion_Proveedor_Insumo: true,
        maxlength: 50,
      },
    },
    messages: {
      Nombre_Proveedor_Insumo: {
        required: "Campo Requerido: Nombre del Proveedor del Insumo",
        Valid_Nombre_Proveedor_Insumo:
          "Campo Requerido: Nombre del Proveedor del Insumo",
        maxlength:
          "El Nombre del Proveedor del Insumo debe Contener un M\xe1ximo de 50 Caracteres",
      },
      Telefono_Proveedor_Insumo: {
        required: "Campo Requerido: Tel\xe9fono del Proveedor del Insumo",
        number: "Ingrese un N\xfamero Tel\xe9fonico V\xe1lido",
        Valid_Telefono_Proveedor_Insumo:
          "Ingrese un N\xfamero Tel\xe9fonico V\xe1lido",
        digits:
          "El Teléfono del Proveedor del Insumo debe ser un N\xfamero Entero",
      },
      E_Mail_Proveedor_Insumo: {
        required:
          "Campo Requerido: Correo Electr\xf3nico del Proveedor del Insumo",
        Valid_E_Mail_Proveedor_Insumo:
          "Ingrese un Correo Electr\xf3nico V\xe1lido",
        maxlength:
          "El Correo Electr\xf3nico del Proveedor del Insumo debe Contener un M\xe1ximo de 30 Caracteres",
      },
      Direccion_Proveedor_Insumo: {
        required: "Campo Requerido: Direcci\xf3n del Proveedor del Insumo",
        Valid_Direccion_Proveedor_Insumo:
          "Campo Requerido: Direcci\xf3n del Proveedor del Insumo",
        maxlength:
          "La Direcci\xf3n del Proveedor del Insumo debe Contener un M\xe1ximo de 50 Caracteres",
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
  if (!$("#Form_Supplier").valid()) {
    return;
  } else {
    var Proveedor = {
      iD_Proveedor_Insumo: $("#ID_Proveedor_Insumo").val(),
      nombre_Proveedor_Insumo: $.trim($("#Nombre_Proveedor_Insumo").val()),
      telefono_Proveedor_Insumo: $("#Telefono_Proveedor_Insumo").val(),
      e_Mail_Proveedor_Insumo: $.trim($("#E_Mail_Proveedor_Insumo").val()),
      direccion_Proveedor_Insumo: $.trim(
        $("#Direccion_Proveedor_Insumo").val()
      ),
    };

    if ($("#ID_Proveedor_Insumo").val() == 0) {
      jQuery.ajax({
        // ? url: "@Url.Action("Management_Controller_Proveedor_Insumo_Registrar", "Management")",
        url: "https://localhost:44381/Management/Management_Controller_Proveedor_Insumo_Registrar",
        type: "POST",
        data: { Obj_Class_Entity_Proveedor_Insumo: Proveedor },
        success: function (data) {
          // debugger; // TODO: Punto de Depuración

          $(".modal-body").LoadingOverlay("hide");

          if (data.iD_Auto_Generated != 0) {
            Proveedor.iD_Proveedor_Insumo = data.iD_Auto_Generated;
            Table_Proveedor_Insumo_Alternative.ajax.reload();
            $("#Form_Modal").modal("hide");
            toastr.options = {
              closeButton: true,
              debug: false,
              newestOnTop: true,
              progressBar: true,
              positionClass: "toast-bottom-center",
              preventDuplicates: false,
              onclick: null,
              showDuration: "300",
              hideDuration: "1000",
              timeOut: "5000",
              extendedTimeOut: "1000",
              showEasing: "swing",
              hideEasing: "linear",
              showMethod: "fadeIn",
              hideMethod: "fadeOut",
            };
            toastr["success"]("El Proveedor ha sido Registrado", "\xc9xito:");
          } else {
            toastr.options = {
              closeButton: true,
              debug: false,
              newestOnTop: true,
              progressBar: true,
              positionClass: "toast-bottom-center",
              preventDuplicates: false,
              onclick: null,
              showDuration: "300",
              hideDuration: "1000",
              timeOut: "5000",
              extendedTimeOut: "1000",
              showEasing: "swing",
              hideEasing: "linear",
              showMethod: "fadeIn",
              hideMethod: "fadeOut",
            };
            toastr["error"](data.message, "Error:");
          }
        },
        error: function (xhr, status, error) {
          $(".modal-body").LoadingOverlay("hide");
          alert(xhr.responseText);
        },
        beforeSend: function () {
          $(".modal-body").LoadingOverlay("show", {
            background: "rgba(0, 0, 0, 0.5)",
            image: "../img/clock-regular.svg",
            imageAnimation: "1.5s fadein",
            imageAutoResize: true,
            imageResizeFactor: 1,
          });
        },
      });
    } else {
      if ($("#ID_Proveedor_Insumo").val() != 0) {
        jQuery.ajax({
          // ? url: "@Url.Action("Management_Controller_Proveedor_Insumo_Editar", "Management")",
          url: "https://localhost:44381/Management/Management_Controller_Proveedor_Insumo_Editar",
          type: "POST",
          data: { Obj_Class_Entity_Proveedor_Insumo: Proveedor },
          success: function (data) {
            // debugger; // TODO: Punto de Depuración

            $(".modal-body").LoadingOverlay("hide");

            if (data.result) {
              Table_Proveedor_Insumo.ajax.reload();
              Selected_Row = null;
              $("#Form_Modal").modal("hide");
              toastr.options = {
                closeButton: true,
                debug: false,
                newestOnTop: true,
                progressBar: true,
                positionClass: "toast-bottom-center",
                preventDuplicates: false,
                onclick: null,
                showDuration: "300",
                hideDuration: "1000",
                timeOut: "5000",
                extendedTimeOut: "1000",
                showEasing: "swing",
                hideEasing: "linear",
                showMethod: "fadeIn",
                hideMethod: "fadeOut",
              };
              toastr["info"](
                "El Proveedor ha sido Modificado",
                "Informaci\xf3n:"
              );
            } else {
              toastr.options = {
                closeButton: true,
                debug: false,
                newestOnTop: true,
                progressBar: true,
                positionClass: "toast-bottom-center",
                preventDuplicates: false,
                onclick: null,
                showDuration: "300",
                hideDuration: "1000",
                timeOut: "5000",
                extendedTimeOut: "1000",
                showEasing: "swing",
                hideEasing: "linear",
                showMethod: "fadeIn",
                hideMethod: "fadeOut",
              };
              toastr["error"](data.message, "Error:");
            }
          },
          error: function (xhr, status, error) {
            $(".modal-body").LoadingOverlay("hide");
            alert(xhr.responseText);
          },
          beforeSend: function () {
            $(".modal-body").LoadingOverlay("show", {
              background: "rgba(0, 0, 0, 0.5)",
              image: "../img/clock-regular.svg",
              imageAnimation: "1.5s fadein",
              imageAutoResize: true,
              imageResizeFactor: 1,
            });
          },
        });
      }
    }
  }
}
