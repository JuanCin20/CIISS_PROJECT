$(document).ready(function () {
  Function_Table_Insumo();
  Function_Table_Insumo_Alternative();
});

var Table_Insumo;
var Table_Insumo_Alternative;
var Selected_Row;

// ? var Url_01 = "@Url.Action("Management_Controller_Insumo_Listar", "Management")",
var Url_01 =
  "https://localhost:44381/Management/Management_Controller_Insumo_Listar" +
  "?Estado_Insumo=" +
  true;

// ? var Url_02 = "@Url.Action("Management_Controller_Insumo_Listar", "Management")",
var Url_02 =
  "https://localhost:44381/Management/Management_Controller_Insumo_Listar" +
  "?Estado_Insumo=" +
  false;

jQuery.ajax({
  url: "https://localhost:44381/Management/Management_Controller_Insumo_Categoria_Insumo_Listar",
  type: "GET",
  dataType: "json",
  contentType: "application/json; charset=UTF-8",
  success: function (data) {
    $("<option>")
      .attr({ value: "0", disabled: "true", selected: "true" })
      .text("Seleccionar")
      .appendTo("#Categoria_Insumo");
    $.each(data.data, function (index, item) {
      $("<option>")
        .attr({ value: item.iD_Categoria_Insumo })
        .text(item.nombre_Categoria_Insumo)
        .appendTo("#Categoria_Insumo");
    });
  },
});

jQuery.ajax({
  url: "https://localhost:44381/Management/Management_Controller_Insumo_Proveedor_Insumo_Listar",
  type: "GET",
  dataType: "json",
  contentType: "application/json; charset=UTF-8",
  success: function (data) {
    $("<option>")
      .attr({ value: "0", disabled: "true", selected: "true" })
      .text("Seleccionar")
      .appendTo("#Proveedor_Insumo");
    $.each(data.data, function (index, item) {
      $("<option>")
        .attr({ value: item.iD_Proveedor_Insumo })
        .text(item.nombre_Proveedor_Insumo)
        .appendTo("#Proveedor_Insumo");
    });
  },
});

function Show_Supply_Image(input) {
  if (input.files) {
    var Reader = new FileReader();
    Reader.onload = function (event) {
      $("#Imagen_Insumo").attr("src", event.target.result);
    };
    Reader.readAsDataURL(input.files[0]);
  }
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

function Function_Table_Insumo() {
  Table_Insumo = $("#Table_Insumo").DataTable({
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
      { data: "iD_Insumo" },
      {
        data: null,
        render: function (data, type, row) {
          return (
            '<a tabindex="' +
            row.iD_Insumo +
            '" href="#/" class="Pop_Trigger" data-bs-html="true" data-bs-custom-class="custom-popover" data-bs-container="body" data-bs-toggle="popover" data-bs-title="Informaci\xf3n" data-bs-content="<p><b>Categor\xeda:</b> ' +
            row.obj_Class_Entity_Categoria_Insumo.nombre_Categoria_Insumo +
            "</p><p><b>Proveedor:</b> " +
            row.obj_Class_Entity_Proveedor_Insumo.nombre_Proveedor_Insumo +
            "</p><p><b>Descripci\xf3n:</b> " +
            row.descripcion_Insumo +
            '</p>">' +
            row.nombre_Insumo +
            "</a>"
          );
        },
      },
      { data: "unidad_Medida_Insumo" },
      { data: "precio_Insumo" },
      { data: "stock_Insumo" },
      {
        data: "fecha_Vencimiento_Insumo",
        render: function (fecha_Vencimiento_Insumo) {
          if (fecha_Vencimiento_Insumo == null) {
            return '<label class="text-danger">NULL</label>';
          } else {
            return (
              '<label class="text-dark">' +
              fecha_Vencimiento_Insumo +
              "</label>"
            );
          }
        },
      },
      {
        data: "estado_Insumo",
        render: function (estado_Insumo) {
          if (estado_Insumo) {
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
            '<img style="width: 60px; height: 60px;" src="../Supply_Images/' +
            row.nombre_Imagen_Insumo +
            '" alt="Image_Error" class="border rounded img-fluid">'
          );
        },
      },
      {
        defaultContent:
          '<button type="button" class="btn btn-primary btn-sm Edit_Button"><i class="fa-solid fa-pencil"></i></button>' +
          '<button type="button" class="btn btn-danger btn-sm ms-2 Delete_Button"><i class="fa-solid fa-trash"></i></button>',
        orderable: false,
        searchable: false,
        width: "90px",
      },
    ],
  });
}

function Function_Table_Insumo_Alternative() {
  Table_Insumo_Alternative = $("#Table_Insumo_Alternative").DataTable({
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
      { data: "iD_Insumo" },
      {
        data: null,
        render: function (data, type, row) {
          return (
            '<a tabindex="' +
            row.iD_Insumo +
            '" href="#/" class="Pop_Trigger" data-bs-html="true" data-bs-custom-class="custom-popover" data-bs-container="body" data-bs-toggle="popover" data-bs-title="Informaci\xf3n" data-bs-content="<p><b>Categor\xeda:</b> ' +
            row.obj_Class_Entity_Categoria_Insumo.nombre_Categoria_Insumo +
            "</p><p><b>Proveedor:</b> " +
            row.obj_Class_Entity_Proveedor_Insumo.nombre_Proveedor_Insumo +
            "</p><p><b>Descripci\xf3n:</b> " +
            row.descripcion_Insumo +
            '</p>">' +
            row.nombre_Insumo +
            "</a>"
          );
        },
      },
      { data: "unidad_Medida_Insumo" },
      { data: "precio_Insumo" },
      { data: "stock_Insumo" },
      {
        data: "fecha_Vencimiento_Insumo",
        render: function (fecha_Vencimiento_Insumo) {
          if (fecha_Vencimiento_Insumo == null) {
            return '<label class="text-danger">NULL</label>';
          } else {
            return (
              '<label class="text-dark">' +
              fecha_Vencimiento_Insumo +
              "</label>"
            );
          }
        },
      },
      {
        data: "estado_Insumo",
        render: function (estado_Insumo) {
          if (estado_Insumo) {
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
            '<img style="width: 60px; height: 60px;" src="../Supply_Images/' +
            row.nombre_Imagen_Insumo +
            '" alt="Image_Error" class="border rounded img-fluid">'
          );
        },
      },
      {
        defaultContent:
          '<button type="button" class="btn btn-warning btn-sm Reset_Button"><i class="fa-solid fa-trash-arrow-up"></i></button>',
        orderable: false,
        searchable: false,
        width: "90px",
      },
    ],
  });
}

function Open_Form_Modal(data) {
  if (data == null) {
    $("#Categoria_Insumo").removeClass("is-valid");
    $("#Categoria_Insumo").removeClass("is-invalid");
    $("#Categoria_Insumo").prop("disabled", false);
    $("#Proveedor_Insumo").removeClass("is-valid");
    $("#Proveedor_Insumo").removeClass("is-invalid");
    $("#Proveedor_Insumo").prop("disabled", false);
    $("#Nombre_Insumo").removeClass("is-valid");
    $("#Nombre_Insumo").removeClass("is-invalid");
    $("#Nombre_Insumo").prop("disabled", false);
    $("#Descripcion_Insumo").removeClass("is-valid");
    $("#Descripcion_Insumo").removeClass("is-invalid");
    $("#Unidad_Medida_Insumo").removeClass("is-valid");
    $("#Unidad_Medida_Insumo").removeClass("is-invalid");
    $("#Unidad_Medida_Insumo").prop("disabled", false);
    $("#Precio_Insumo").removeClass("is-valid");
    $("#Precio_Insumo").removeClass("is-invalid");
    $("#Stock_Insumo").removeClass("is-valid");
    $("#Stock_Insumo").removeClass("is-invalid");
    $("#Fecha_Vencimiento_Insumo").removeClass("is-valid");
    $("#Fecha_Vencimiento_Insumo").removeClass("is-invalid");
    $("#Fecha_Vencimiento_Insumo").prop("disabled", false);
    $("#Imagen_Insumo_Input").removeClass("is-valid");
    $("#Imagen_Insumo_Input").removeClass("is-invalid");
    $("#ID_Insumo").val(0);
    $("#Categoria_Insumo").val(0);
    $("#Proveedor_Insumo").val(0);
    $("#Nombre_Insumo").val("");
    $("#Descripcion_Insumo").val("");
    $("#Unidad_Medida_Insumo").val("");
    $("#Precio_Insumo").val("");
    $("#Stock_Insumo").val("");
    $("#Fecha_Vencimiento_Insumo")
      .datepicker({ dateFormat: "dd/mm/yy" })
      .datepicker("setDate", "");
    $("#Imagen_Insumo_Input").val("");
    $("#Imagen_Insumo").removeAttr("src");
  } else {
    if (data != null) {
      $("#Categoria_Insumo").removeClass("is-valid");
      $("#Categoria_Insumo").removeClass("is-invalid");
      $("#Categoria_Insumo").prop("disabled", true);
      $("#Proveedor_Insumo").removeClass("is-valid");
      $("#Proveedor_Insumo").removeClass("is-invalid");
      $("#Proveedor_Insumo").prop("disabled", true);
      $("#Nombre_Insumo").removeClass("is-valid");
      $("#Nombre_Insumo").removeClass("is-invalid");
      $("#Nombre_Insumo").prop("disabled", true);
      $("#Descripcion_Insumo").removeClass("is-valid");
      $("#Descripcion_Insumo").removeClass("is-invalid");
      $("#Unidad_Medida_Insumo").removeClass("is-valid");
      $("#Unidad_Medida_Insumo").removeClass("is-invalid");
      $("#Unidad_Medida_Insumo").prop("disabled", true);
      $("#Precio_Insumo").removeClass("is-valid");
      $("#Precio_Insumo").removeClass("is-invalid");
      $("#Stock_Insumo").removeClass("is-valid");
      $("#Stock_Insumo").removeClass("is-invalid");
      $("#Fecha_Vencimiento_Insumo").removeClass("is-valid");
      $("#Fecha_Vencimiento_Insumo").removeClass("is-invalid");
      $("#Fecha_Vencimiento_Insumo").prop("disabled", true);
      $("#Imagen_Insumo_Input").removeClass("is-valid");
      $("#Imagen_Insumo_Input").removeClass("is-invalid");
      $("#ID_Insumo").val(data.iD_Insumo);
      $("#Categoria_Insumo").val(
        data.obj_Class_Entity_Categoria_Insumo.iD_Categoria_Insumo
      );
      $("#Proveedor_Insumo").val(
        data.obj_Class_Entity_Proveedor_Insumo.iD_Proveedor_Insumo
      );
      $("#Nombre_Insumo").val(data.nombre_Insumo);
      $("#Descripcion_Insumo").val(data.descripcion_Insumo);
      $("#Unidad_Medida_Insumo").val(data.unidad_Medida_Insumo);
      $("#Precio_Insumo").val(data.precio_Insumo);
      $("#Stock_Insumo").val(data.stock_Insumo);
      $("#Fecha_Vencimiento_Insumo")
        .datepicker({ dateFormat: "dd/mm/yy" })
        .datepicker("setDate", data.fecha_Vencimiento_Insumo);
      $("#Imagen_Insumo_Input").val("");
      $("#Imagen_Insumo").removeAttr("src");
      jQuery.ajax({
        // ? url: "@Url.Action("Management_Controller_Insumo_Imagen", "Management")",
        url: "https://localhost:44381/Management/Management_Controller_Insumo_Imagen",
        type: "POST",
        data: { ID_Insumo: data.iD_Insumo },
        success: function (data) {
          if (data.conversion) {
            $("#Imagen_Insumo").attr({
              src:
                "data:image/" +
                data.extension_Imagen_Insumo +
                ";base64," +
                data.base_64_Imagen_Insumo,
            });
          }
        },
        error: function (xhr, status, error) {
          alert(xhr.responseText);
        },
      });
    }
  }
  $("#Form_Modal").modal("show");
}

$("#Table_Insumo").on("click", ".Edit_Button", function () {
  Selected_Row = Selected_Row_Function(this);
  var data = Table_Insumo.row(Selected_Row).data();
  // console.log(data); // ? Good 'console.log'
  Open_Form_Modal(data);
});

$("#Table_Insumo").on("click", ".Delete_Button", function () {
  Selected_Row = Selected_Row_Function(this);
  var data = Table_Insumo.row(Selected_Row).data();
  Swal.fire({
    title: "Confirmaci\xf3n",
    text: "\xbfDesea Eliminar el Insumo Seleccionado?",
    icon: "warning",
    showCancelButton: true,
    cancelButtonText: "Cancelar",
    cancelButtonColor: "#FF0000",
    confirmButtonText: "Eliminar",
    confirmButtonColor: "#3085D6",
  }).then((result) => {
    if (result.isConfirmed) {
      jQuery.ajax({
        // ? url: "@Url.Action("Management_Controller_Insumo_Eliminar", "Management")",
        url: "https://localhost:44381/Management/Management_Controller_Insumo_Eliminar",
        type: "POST",
        data: { ID_Insumo: data.iD_Insumo },
        success: function (data) {
          // debugger; // TODO: Punto de Depuración

          if (data.result) {
            Swal.fire({
              title: "Correcto",
              text: "El Insumo ha sido Eliminado",
              icon: "success",
            });
            Table_Insumo.row(Selected_Row).remove().draw();
            Table_Insumo_Alternative.ajax.url(Url_02).load();
          } else {
            Swal.fire({
              title: "Error",
              text: data.message,
              icon: "error",
            });
          }
        },
        error: function (xhr, status, error) {
          alert(xhr.responseText);
        },
      });
    }
  });
  // console.log(data); // ? Good 'console.log'
});

$("#Table_Insumo_Alternative").on("click", ".Reset_Button", function () {
  Selected_Row = Selected_Row_Function(this);
  var data = Table_Insumo_Alternative.row(Selected_Row).data();
  Swal.fire({
    title: "Confirmaci\xf3n",
    text: "\xbfDesea Restaurar el Insumo Seleccionado?",
    icon: "info",
    showCancelButton: true,
    cancelButtonText: "Cancelar",
    cancelButtonColor: "#FF0000",
    confirmButtonText: "Aceptar",
    confirmButtonColor: "#3085D6",
  }).then((result) => {
    if (result.isConfirmed) {
      jQuery.ajax({
        // ? url: "@Url.Action("Management_Controller_Insumo_Reset", "Management")",
        url: "https://localhost:44381/Management/Management_Controller_Insumo_Reset",
        type: "POST",
        data: { ID_Insumo: data.iD_Insumo },
        success: function (data) {
          // debugger; // TODO: Punto de Depuración

          if (data.result) {
            Swal.fire({
              title: "Correcto",
              text: "El Insumo ha sido Restaurado",
              icon: "success",
            });
            Table_Insumo_Alternative.row(Selected_Row).remove().draw();
            Table_Insumo.ajax.url(Url_01).load();
          } else {
            Swal.fire({
              title: "Error",
              text: data.message,
              icon: "error",
            });
          }
        },
        error: function (xhr, status, error) {
          alert(xhr.responseText);
        },
      });
    }
  });
  // console.log(data); // ? Good 'console.log'
});

jQuery.validator.addMethod("Valid_Nombre_Insumo", function (value, element) {
  return (
    this.optional(element) ||
    /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
  );
});

jQuery.validator.addMethod(
  "Valid_Unidad_Medida_Insumo",
  function (value, element) {
    return (
      this.optional(element) ||
      /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
    );
  }
);

jQuery.validator.addMethod(
  "Valid_Fecha_Vencimiento_Insumo",
  function (value, element) {
    return (
      this.optional(element) ||
      /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/.test(
        value
      )
    );
  }
);

jQuery.validator.addMethod(
  "Valid_Descripcion_Insumo",
  function (value, element) {
    return (
      this.optional(element) ||
      /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
    );
  }
);

$(document).ready(function () {
  $("#Form_Supply").validate({
    rules: {
      Nombre_Insumo: {
        required: true,
        Valid_Nombre_Insumo: true,
        maxlength: 120,
      },
      Categoria_Insumo: {
        required: true,
      },
      Proveedor_Insumo: {
        required: true,
      },
      Unidad_Medida_Insumo: {
        required: true,
        Valid_Unidad_Medida_Insumo: true,
        maxlength: 20,
      },
      Precio_Insumo: {
        required: true,
        number: true,
      },
      Stock_Insumo: {
        required: true,
        number: true,
        digits: true,
      },
      Fecha_Vencimiento_Insumo: {
        required: true,
        Valid_Fecha_Vencimiento_Insumo: true,
        maxlength: 10,
      },
      Descripcion_Insumo: {
        required: true,
        Valid_Descripcion_Insumo: true,
        maxlength: 255,
      },
      Imagen_Insumo_Input: {
        required: true,
      },
    },
    messages: {
      Nombre_Insumo: {
        required: "Campo Requerido: Nombre de Insumo",
        Valid_Nombre_Insumo: "Campo Requerido: Nombre de Insumo",
        maxlength:
          "El Nombre del Insumo debe Contener un M\xe1ximo de 120 Caracteres",
      },
      Categoria_Insumo: {
        required: "Campo Requerido: Categor\xeda del Insumo",
      },
      Proveedor_Insumo: {
        required: "Campo Requerido: Proveedor del Insummo",
      },
      Unidad_Medida_Insumo: {
        required: "Campo Requerido: Unidad de Medida del Insumo",
        Valid_Unidad_Medida_Insumo:
          "Campo Requerido: Unidad de Medida del Insumo",
        maxlength:
          "La Unidad de Medida del Insumo debe Contener un M\xe1ximo de 20 Caracteres",
      },
      Precio_Insumo: {
        required: "Campo Requerido: Precio del Insumo",
        number: "Ingrese un Precio V\xe1lido",
      },
      Stock_Insumo: {
        required: "Campo Requerido: Stock del Insumo",
        number: "Ingrese un Stock V\xe1lido",
        digits: "El Stock del Insumo debe ser un N\xfamero Entero",
      },
      Fecha_Vencimiento_Insumo: {
        required: "Campo Requerido: Fecha de Vencimiento del Insumo",
        Valid_Fecha_Vencimiento_Insumo: "Ingrese una Fecha V\xe1lida",
        maxlength:
          "La Fecha de Vencimiento del Insumo debe Contener un M\xe1ximo de 10 Caracteres",
      },
      Descripcion_Insumo: {
        required: "Campo Requerido: Descripci\xf3n del Insumo",
        Valid_Descripcion_Insumo: "Campo Requerido: Descripci\xf3n del Insumo",
        maxlength:
          "La Descripci\xf3n del Insumo debe Contener un M\xe1ximo de 255 Caracteres",
      },
      Imagen_Insumo_Input: {
        required: "Campo Requerido: Im\xe1gen del Insumo",
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
  if (!$("#Form_Supply").valid()) {
    return;
  } else {
    var Date_Object = $("#Fecha_Vencimiento_Insumo").datepicker("getDate");
    var Date_String = $.datepicker.formatDate("yy-mm-dd", Date_Object);

    var Imagen_Insumo_Input = $("#Imagen_Insumo_Input")[0].files[0];

    var Insumo = {
      iD_Insumo: $("#ID_Insumo").val(),
      obj_Class_Entity_Categoria_Insumo: {
        iD_Categoria_Insumo: $("#Categoria_Insumo option:selected").val(),
        nombre_Categoria_Insumo: $("#Categoria_Insumo option:selected").text(),
      },
      obj_Class_Entity_Proveedor_Insumo: {
        iD_Proveedor_Insumo: $("#Proveedor_Insumo option:selected").val(),
        nombre_Proveedor_Insumo: $("#Proveedor_Insumo option:selected").text(),
      },
      nombre_Insumo: $.trim($("#Nombre_Insumo").val()),
      descripcion_Insumo: $.trim($("#Descripcion_Insumo").val()),
      unidad_Medida_Insumo: $.trim($("#Unidad_Medida_Insumo").val()),
      precio_Insumo: $("#Precio_Insumo").val(),
      precio_Insumo_String: $("#Precio_Insumo").val(),
      stock_Insumo: $("#Stock_Insumo").val(),
      fecha_Vencimiento_Insumo: Date_String,
    };

    if ($("#ID_Insumo").val() == 0) {
      var Request = new FormData();
      Request.append("Obj_Class_Entity_Insumo", JSON.stringify(Insumo));
      Request.append("Obj_IFormFile", Imagen_Insumo_Input);

      jQuery.ajax({
        // ? url: "@Url.Action("Management_Controller_Insumo_Registrar", "Management")",
        url: "https://localhost:44381/Management/Management_Controller_Insumo_Registrar",
        type: "POST",
        data: Request,
        processData: false,
        contentType: false,
        success: function (data) {
          // debugger; // TODO: Punto de Depuración

          $(".modal-body").LoadingOverlay("hide");

          if (data.iD_Auto_Generated != 0) {
            $("#Form_Modal").modal("hide");
            Table_Insumo.ajax.url(Url_01).load();
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
            toastr["success"]("El Insumo ha sido Registrado", "\xc9xito:");
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
      if ($("#ID_Insumo").val() != 0) {
        var Request = new FormData();
        Request.append("Obj_Class_Entity_Insumo", JSON.stringify(Insumo));
        Request.append("Obj_IFormFile", Imagen_Insumo_Input);

        jQuery.ajax({
          // ? url: "@Url.Action("Management_Controller_Insumo_Editar", "Management")",
          url: "https://localhost:44381/Management/Management_Controller_Insumo_Editar",
          type: "POST",
          data: Request,
          processData: false,
          contentType: false,
          success: function (data) {
            // debugger; // TODO: Punto de Depuración

            $(".modal-body").LoadingOverlay("hide");

            if (data.successful_Operation) {
              Selected_Row = null;
              $("#Form_Modal").modal("hide");
              Table_Insumo.ajax.url(Url_01).load();
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
              toastr["info"]("El Insumo ha sido Modificado", "Informaci\xf3n:");
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