var Table_Categoria_Insumo;
var Table_Categoria_Insumo_Alternative;
var Selected_Row;

// ? var Url_01 = "@Url.Action("Management_Controller_Categoria_Insumo_Listar", "Management")",
var Url_01 =
  "https://localhost:44381/Management/Management_Controller_Categoria_Insumo_Listar" +
  "?Estado_Categoria_Insumo=" +
  true;

$(document).ready(function () {
  Table_Categoria_Insumo = $("#Table_Categoria_Insumo").DataTable({
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
      { data: "iD_Categoria_Insumo" },
      {
        data: "estado_Categoria_Insumo",
        render: function (estado_Categoria_Insumo) {
          if (estado_Categoria_Insumo) {
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
            row.iD_Categoria_Insumo +
            '" href="#/" class="Pop_Trigger" data-bs-custom-class="custom-popover" data-bs-container="body" data-bs-toggle="popover" data-bs-title="Descripci\xf3n" data-bs-content="' +
            row.descripcion_Categoria_Insumo +
            '">' +
            row.nombre_Categoria_Insumo +
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
      { data: "fecha_Registro_Categoria_Insumo" },
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
    $("#Nombre_Categoria_Insumo").removeClass("is-valid");
    $("#Nombre_Categoria_Insumo").removeClass("is-invalid");
    $("#Nombre_Categoria_Insumo").prop("disabled", false);
    $("#Descripcion_Categoria_Insumo").removeClass("is-valid");
    $("#Descripcion_Categoria_Insumo").removeClass("is-invalid");
    $("#ID_Categoria_Insumo").val(0);
    $("#Nombre_Categoria_Insumo").val("");
    $("#Descripcion_Categoria_Insumo").val("");
  } else {
    if (data != null) {
      $("#Nombre_Categoria_Insumo").removeClass("is-valid");
      $("#Nombre_Categoria_Insumo").removeClass("is-invalid");
      $("#Nombre_Categoria_Insumo").prop("disabled", true);
      $("#Descripcion_Categoria_Insumo").removeClass("is-valid");
      $("#Descripcion_Categoria_Insumo").removeClass("is-invalid");
      $("#ID_Categoria_Insumo").val(data.iD_Categoria_Insumo);
      $("#Nombre_Categoria_Insumo").val(data.nombre_Categoria_Insumo);
      $("#Descripcion_Categoria_Insumo").val(data.descripcion_Categoria_Insumo);
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

$("#Table_Categoria_Insumo").on("click", ".Edit_Button", function () {
  Selected_Row = Selected_Row_Function(this);
  var data = Table_Categoria_Insumo.row(Selected_Row).data();
  // console.log(data); // ? Good 'console.log'
  Open_Form_Modal(data);
});

function Open_Form_Modal_Alternative() {
  // ? var Url_02 = "@Url.Action("Management_Controller_Categoria_Insumo_Listar", "Management")",
  var Url_02 =
    "https://localhost:44381/Management/Management_Controller_Categoria_Insumo_Listar" +
    "?Estado_Categoria_Insumo=" +
    false;

  Table_Categoria_Insumo_Alternative = $(
    "#Table_Categoria_Insumo_Alternative"
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
      { data: "iD_Categoria_Insumo" },
      {
        data: "estado_Categoria_Insumo",
        render: function (estado_Categoria_Insumo) {
          if (estado_Categoria_Insumo) {
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
            row.iD_Categoria_Insumo +
            '" href="#/" class="Pop_Trigger" data-bs-custom-class="custom-popover" data-bs-container="body" data-bs-toggle="popover" data-bs-title="Descripci\xf3n" data-bs-content="' +
            row.descripcion_Categoria_Insumo +
            '">' +
            row.nombre_Categoria_Insumo +
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
      { data: "fecha_Registro_Categoria_Insumo" },
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
  "Valid_Nombre_Categoria_Insumo",
  function (value, element) {
    return (
      this.optional(element) ||
      /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
    );
  }
);

jQuery.validator.addMethod(
  "Valid_Descripcion_Categoria_Insumo",
  function (value, element) {
    return (
      this.optional(element) ||
      /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
    );
  }
);

$(document).ready(function () {
  $("#Form_Category").validate({
    rules: {
      Nombre_Categoria_Insumo: {
        required: true,
        Valid_Nombre_Categoria_Insumo: true,
        maxlength: 50,
      },
      Descripcion_Categoria_Insumo: {
        required: true,
        Valid_Descripcion_Categoria_Insumo: true,
        maxlength: 255,
      },
    },
    messages: {
      Nombre_Categoria_Insumo: {
        required: "Campo Requerido: Nombre de la Categor\xeda del Insumo",
        Valid_Nombre_Categoria_Insumo:
          "Campo Requerido: Nombre de la Categor\xeda del Insumo",
        maxlength:
          "El Nombre de la Categor\xeda del Insumo debe Contener un M\xe1ximo de 50 Caracteres",
      },
      Descripcion_Categoria_Insumo: {
        required:
          "Campo Requerido: Descripci\xf3n de la Categor\xeda del Insumo",
        Valid_Descripcion_Categoria_Insumo:
          "Campo Requerido: Descripci\xf3n de la Categor\xeda del Insumo",
        maxlength:
          "La Descripci\xf3n de la Categor\xeda del Insumo debe Contener un M\xe1ximo de 255 Caracteres",
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
  if (!$("#Form_Category").valid()) {
    return;
  } else {
    var Categoria = {
      iD_Categoria_Insumo: $("#ID_Categoria_Insumo").val(),
      nombre_Categoria_Insumo: $.trim($("#Nombre_Categoria_Insumo").val()),
      descripcion_Categoria_Insumo: $.trim(
        $("#Descripcion_Categoria_Insumo").val()
      ),
    };

    if ($("#ID_Categoria_Insumo").val() == 0) {
      jQuery.ajax({
        // ? url: "@Url.Action("Management_Controller_Categoria_Insumo_Registrar", "Management")",
        url: "https://localhost:44381/Management/Management_Controller_Categoria_Insumo_Registrar",
        type: "POST",
        data: { Obj_Class_Entity_Categoria_Insumo: Categoria },
        success: function (data) {
          debugger; // TODO: Punto de Depuración

          $(".modal-body").LoadingOverlay("hide");

          if (data.iD_Auto_Generated != 0) {
            Categoria.iD_Categoria_Insumo = data.iD_Auto_Generated;
            Table_Categoria_Insumo.row.add(Categoria).draw(false);
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
            toastr["success"](
              "La Categor\xeda ha sido Registrada",
              "\xc9xito:"
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
    } else {
      if ($("#ID_Categoria_Insumo").val() != 0) {
        jQuery.ajax({
          // ? url: "@Url.Action("Management_Controller_Categoria_Insumo_Editar", "Management")",
          url: "https://localhost:44381/Management/Management_Controller_Categoria_Insumo_Editar",
          type: "POST",
          data: { Obj_Class_Entity_Categoria_Insumo: Categoria },
          success: function (data) {
            // debugger; // TODO: Punto de Depuración

            $(".modal-body").LoadingOverlay("hide");

            if (data.result) {
              Table_Categoria_Insumo.row(Selected_Row)
                .data(Categoria)
                .draw(false);
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
                "La Categor\xeda ha sido Modificada",
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