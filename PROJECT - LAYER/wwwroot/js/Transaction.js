$(document).ready(function () {
  Count_Middle();
  Show_Category();
  Show_Supply(0, 0);
  Listar_Departamento();
  List_Middle();
});

function Count_Middle() {
  if ($("#Count_Middle").length > 0) {
    jQuery.ajax({
      // ? url: "@Url.Action("Transaction_Controller_Middle_Count", "Transaction")",
      url: "https://localhost:44381/Transaction/Transaction_Controller_Middle_Count",
      type: "POST",
      success: function (data) {
        var Result = data.result;

        if (Result == 0) {
          $("#Count_Middle").removeClass("badge rounded-pill text-bg-success");
          $("#Count_Middle").addClass("badge rounded-pill text-bg-danger");
          $("#Count_Middle").text(Result);
        } else {
          $("#Count_Middle").removeClass("badge rounded-pill text-bg-danger");
          $("#Count_Middle").addClass("badge rounded-pill text-bg-success");
          $("#Count_Middle").text(Result);
        }
      },
    });
  }
}

function Show_Category() {
  jQuery.ajax({
    // ? url: "@Url.Action("Transaction_Controller_Categoria_Insumo_Listar", "Transaction")",
    url: "https://localhost:44381/Transaction/Transaction_Controller_Categoria_Insumo_Listar",
    type: "GET",
    dataType: "json",
    success: function (data) {
      $("<li>")
        .addClass("list-group-item list-group-item-primary")
        .append(
          $("<input>").addClass("form-check-input ml-1").attr({
            type: "radio",
            name: "Category",
            value: "0",
            id: "Category_Filter",
            checked: "checked",
            style: "cursor:pointer",
          }),
          $("<label>")
            .addClass("form-check-label ml-5")
            .text("Todos")
            .attr({ for: "Category_Filter", style: "cursor:pointer" })
        )
        .appendTo("#Category_Container");

      $.each(
        data.obj_List_Class_Entity_Categoria_Insumo,
        function (i, element) {
          $("<li>")
            .addClass("list-group-item list-group-item-primary")
            .append(
              $("<input>")
                .addClass("form-check-input ml-1")
                .attr({
                  type: "radio",
                  name: "Category",
                  value: element.iD_Categoria_Insumo,
                  id: "Category_Filter_" + i,
                  style: "cursor:pointer",
                }),
              $("<label>")
                .addClass("form-check-label ml-5")
                .text(element.nombre_Categoria_Insumo)
                .attr({ for: "Category_Filter_" + i, style: "cursor:pointer" })
            )
            .appendTo("#Category_Container");
        }
      );
      Show_Supplier();
    },
  });
}

function Show_Supplier() {
  var ID_Categoria_Insumo = $("input[name=Category]:checked").val();

  jQuery.ajax({
    // ? url: "@Url.Action("Transaction_Controller_Proveedor_Insumo_Listar_Alternative", "Transaction")",
    url: "https://localhost:44381/Transaction/Transaction_Controller_Proveedor_Insumo_Listar_Alternative",
    type: "POST",
    data: { ID_Categoria_Insumo: ID_Categoria_Insumo },
    success: function (data) {
      $("#Supplier_Container").html("");

      $("#Supplier_Container").LoadingOverlay("hide");

      $("<li>")
        .addClass("list-group-item list-group-item-danger")
        .append(
          $("<input>").addClass("form-check-input ml-1").attr({
            type: "radio",
            name: "Supplier",
            value: "0",
            id: "Supplier_Filter",
            checked: "checked",
            style: "cursor:pointer",
          }),
          $("<label>")
            .addClass("form-check-label ml-5")
            .text("Todos")
            .attr({ for: "Supplier_Filter", style: "cursor:pointer" })
        )
        .appendTo("#Supplier_Container");

      $.each(
        data.obj_List_Class_Entity_Proveedor_Insumo,
        function (i, element) {
          $("<li>")
            .addClass("list-group-item list-group-item-danger")
            .append(
              $("<input>")
                .addClass("form-check-input ml-1")
                .attr({
                  type: "radio",
                  name: "Supplier",
                  value: element.iD_Proveedor_Insumo,
                  id: "Supplier_Filter_" + i,
                  style: "cursor:pointer",
                }),
              $("<label>")
                .addClass("form-check-label ml-5")
                .text(element.nombre_Proveedor_Insumo)
                .attr({ for: "Supplier_Filter_" + i, style: "cursor:pointer" })
            )
            .appendTo("#Supplier_Container");
        }
      );
    },
    beforeSend: function () {
      $("#Supplier_Container").LoadingOverlay("show", {
        background: "rgba(0, 0, 0, 0.5)",
        image: "../img/clock-regular.svg",
        imageAnimation: "1.5s fadein",
        imageAutoResize: true,
        imageResizeFactor: 1,
        imageColor: "#FFD43B",
      });
    },
  });
}

$(document).on("change", "input[name=Category]", function () {
  Show_Supplier();
});

function Show_Supply(ID_Categoria_Insumo, ID_Proveedor_Insumo) {
  jQuery.ajax({
    // ? url: "@Url.Action("Transaction_Controller_Insumo_Listar", "Transaction")",
    url: "https://localhost:44381/Transaction/Transaction_Controller_Insumo_Listar",
    type: "POST",
    data: {
      ID_Categoria_Insumo: ID_Categoria_Insumo,
      ID_Proveedor_Insumo: ID_Proveedor_Insumo,
    },
    success: function (data) {
      $("#Supply_Container").html("");

      $("#Supply_Container").LoadingOverlay("hide");

      $.each(data.obj_List_Class_Entity_Insumo, function (i, element) {
        $("<div>")
          .addClass("col")
          .append(
            $("<div>")
              .addClass("card h-100")
              .append(
                $("<img>")
                  .addClass("card-img-top")
                  .attr({
                    src:
                      "data:image/" +
                      element.extension_Imagen_Insumo +
                      ";base64," +
                      element.base_64_Imagen_Insumo,
                  }),
                $("<div>")
                  .addClass("card-body p-4")
                  .append(
                    $("<div>")
                      .addClass("text-center")
                      .append(
                        $("<h5>")
                          .addClass("card-title")
                          .text(element.nombre_Insumo),
                        $("<p>")
                          .addClass("card-text")
                          .text(
                            "S/. " + element.precio_Insumo.toFixed(2) + " PEN"
                          )
                      )
                  ),
                $("<div>")
                  .addClass("card-footer p-2 pt-0 border-top-0 bg-transparent")
                  .append(
                    $("<div>")
                      .addClass("d-grid gap-2")
                      .append(
                        $("<button>")
                          .addClass("btn btn-success mt-auto Add_Middle")
                          .data("ID_Insumo", element.iD_Insumo)
                          .html(
                            "<i class='fa-solid fa-plus'></i>&nbsp;&nbsp;Agregar a Middle"
                          )
                      )
                  )
              )
          )
          .appendTo("#Supply_Container");
      });
    },
    beforeSend: function () {
      $("#Supply_Container").LoadingOverlay("show", {
        background: "rgba(0, 0, 0, 0.5)",
        image: "../img/clock-regular.svg",
        imageAnimation: "1.5s fadein",
        imageAutoResize: true,
        imageResizeFactor: 1,
        imageColor: "#FFD43B",
      });
    },
  });
}

$("#Filter_Button").on("click", function () {
  var ID_Categoria_Insumo = $("input[name=Category]:checked").val();
  var ID_Proveedor_Insumo = $("input[name=Supplier]:checked").val();
  Show_Supply(ID_Categoria_Insumo, ID_Proveedor_Insumo);
});

$(document).on("click", "button.Add_Middle", function () {
  var ID_Insumo = $(this).data("ID_Insumo");

  Swal.fire({
    title: "Confirmaci\xf3n",
    text: "\xbfDesea Agregar el Insumo Seleccionado a Middle?",
    icon: "success",
    showCancelButton: true,
    cancelButtonText: "Cancelar",
    cancelButtonColor: "#FF0000",
    confirmButtonText: "Aceptar",
    confirmButtonColor: "#3085D6",
  }).then((result) => {
    if (result.isConfirmed) {
      jQuery.ajax({
        // ? url: "@Url.Action("Transaction_Controller_Middle_Listar", "Transaction")",
        url: "https://localhost:44381/Transaction/Transaction_Controller_Middle_Listar",
        type: "POST",
        data: { ID_Insumo: ID_Insumo },
        success: function (data) {
          if (data.result_02) {
            Count_Middle();
          } else {
            Swal.fire({
              title: "Advertencia",
              text: data.message,
              icon: "warning",
            });
          }
        },
      });
    }
  });
});

function Listar_Departamento() {
  $("<option>")
    .attr({ value: "0", disabled: "disabled", selected: "true" })
    .text("Seleccionar")
    .appendTo("#ID_Departamento");
  jQuery.ajax({
    // ? url: "@Url.Action("Transaction_Controller_Ubication_Departamento_Listar", "Transaction")",
    url: "https://localhost:44381/Transaction/Transaction_Controller_Ubication_Departamento_Listar",
    type: "GET",
    dataType: "json",
    success: function (data) {
      if (data.obj_List_Class_Entity_Departamento != null) {
        $.each(data.obj_List_Class_Entity_Departamento, function (i, element) {
          $("<option>")
            .attr({ value: element.iD_Departamento })
            .text(element.nombre_Departamento)
            .appendTo("#ID_Departamento");
        });
        Listar_Provincia();
      }
    },
  });
}

$("#ID_Departamento").on("change", function () {
  Listar_Provincia();
});

function Listar_Provincia() {
  var ID_Departamento = $("#ID_Departamento option:selected").val();
  $("#ID_Provincia").html("");
  $("<option>")
    .attr({ value: "0", disabled: "disabled", selected: "true" })
    .text("Seleccionar")
    .appendTo("#ID_Provincia");
  jQuery.ajax({
    // ? url: "@Url.Action("Transaction_Controller_Ubication_Provincia_Listar", "Transaction")",
    url: "https://localhost:44381/Transaction/Transaction_Controller_Ubication_Provincia_Listar",
    type: "POST",
    data: { ID_Departamento: ID_Departamento },
    success: function (data) {
      if (data.obj_List_Class_Entity_Provincia != null) {
        $.each(data.obj_List_Class_Entity_Provincia, function (i, element) {
          $("<option>")
            .attr({ value: element.iD_Provincia })
            .text(element.nombre_Provincia)
            .appendTo("#ID_Provincia");
        });
        Listar_Distrito();
      }
    },
  });
}

$("#ID_Provincia").on("change", function () {
  Listar_Distrito();
});

function Listar_Distrito() {
  var ID_Provincia = $("#ID_Provincia option:selected").val();
  var ID_Departamento = $("#ID_Departamento option:selected").val();
  $("#ID_Distrito").html("");
  $("<option>")
    .attr({ value: "0", disabled: "disabled", selected: "true" })
    .text("Seleccionar")
    .appendTo("#ID_Distrito");
  jQuery.ajax({
    // ? url: "@Url.Action("Transaction_Controller_Ubication_Distrito_Listar", "Transaction")",
    url: "https://localhost:44381/Transaction/Transaction_Controller_Ubication_Distrito_Listar",
    type: "POST",
    data: { ID_Provincia: ID_Provincia, ID_Departamento: ID_Departamento },
    success: function (data) {
      if (data.obj_List_Class_Entity_Distrito != null) {
        $.each(data.obj_List_Class_Entity_Distrito, function (i, element) {
          $("<option>")
            .attr({ value: element.iD_Distrito })
            .text(element.nombre_Distrito)
            .appendTo("#ID_Distrito");
        });
      }
    },
  });
}

function List_Middle() {
  jQuery.ajax({
    // ? url: "@Url.Action("Transaction_Controller_Middle_Listar_Alternative", "Transaction")",
    url: "https://localhost:44381/Transaction/Transaction_Controller_Middle_Listar_Alternative",
    type: "POST",
    success: function (data) {
      $("#Middle_List").html("");

      $.each(data.obj_List_Class_Entity_Middle, function (i, element) {
        $("<div>")
          .addClass("card mb-2 Middle_Card")
          .append(
            $("<div>")
              .addClass("card-body")
              .append(
                $("<div>")
                  .addClass("row")
                  .append(
                    $("<div>")
                      .addClass(
                        "col-sm-2 align-self-center d-flex justify-content-center"
                      )
                      .append(
                        $("<img>")
                          .addClass("rounded")
                          .attr({
                            src:
                              "data:image/" +
                              element.obj_Class_Entity_Insumo
                                .extension_Imagen_Insumo +
                              ";base64," +
                              element.obj_Class_Entity_Insumo
                                .base_64_Imagen_Insumo,
                          })
                          .css({ width: "100px", height: "100px" })
                      ),
                    $("<div>")
                      .addClass("col-sm-4 align-self-center")
                      .append(
                        $("<span>")
                          .addClass("font-weight-bold d-block")
                          .text(
                            element.obj_Class_Entity_Insumo
                              .obj_Class_Entity_Proveedor_Insumo
                              .nombre_Proveedor_Insumo
                          ),
                        $("<span>").text(
                          element.obj_Class_Entity_Insumo.nombre_Insumo
                        )
                      ),
                    $("<div>")
                      .addClass("col-sm-2 align-self-center")
                      .append(
                        $("<span>").text(
                          "S/. " +
                            element.obj_Class_Entity_Insumo.precio_Insumo.toFixed(
                              2
                            )
                        )
                      ),
                    $("<div>")
                      .addClass("col-sm-2 align-self-center")
                      .append(
                        $("<div>")
                          .addClass("d-flex")
                          .append(
                            $("<button>")
                              .addClass(
                                "btn btn-outline-danger Button_Less rounded-0"
                              )
                              .append($("<i>").addClass("fa-solid fa-minus")),
                            $("<input>")
                              .addClass(
                                "form-control Input_Quantity p-1 text-center rounded-0"
                              )
                              .attr({ disabled: "disabled" })
                              .css({ width: "40px" })
                              .data(
                                "Obj_Class_Entity_Insumo",
                                element.obj_Class_Entity_Insumo
                              )
                              .val(element.cantidad_Insumo_Middle),
                            $("<button>")
                              .addClass(
                                "btn btn-outline-success Button_Plus rounded-0"
                              )
                              .append($("<i>").addClass("fa-solid fa-plus"))
                          )
                      ),
                    $("<div>")
                      .addClass("col-sm-2 align-self-center")
                      .append(
                        $("<button>")
                          .addClass(
                            "btn btn-outline-danger Button_Delete_Supply"
                          )
                          .append($("<i>").addClass("fa-solid fa-trash"))
                          .data(
                            "ID_Insumo",
                            element.obj_Class_Entity_Insumo.iD_Insumo
                          )
                      )
                  )
              )
          )
          .appendTo("#Middle_List");
      });
      Monto_Total_Movimiento_Inventario();
    },
  });
}

function Monto_Total_Movimiento_Inventario() {
  var Monto_Total_Movimiento_Inventario = parseFloat(0);

  $("input.Input_Quantity").each(function (i) {
    var precio_Insumo = $(this).data("Obj_Class_Entity_Insumo").precio_Insumo;
    var Input_Quantity = parseFloat($(this).val());
    var Monto_Total_Detalle_Movimiento_Inventario =
      precio_Insumo * Input_Quantity;
    Monto_Total_Movimiento_Inventario +=
      Monto_Total_Detalle_Movimiento_Inventario;
  });

  $("#Monto_Total_Movimiento_Inventario").text(
    Monto_Total_Movimiento_Inventario.toFixed(2)
  );
  $("#Monto_Total_Movimiento_Inventario").data(
    "Monto_Total_Movimiento_Inventario",
    Monto_Total_Movimiento_Inventario
  );
}

$(document).on("click", ".Button_Plus", function () {
  var Display_Flex = $(this).parent("div.d-flex");
  var Input_Quantity = $(Display_Flex).find("input.Input_Quantity");
  var Button_Plus = $(this);
  var ID_Insumo = $(Input_Quantity).data("Obj_Class_Entity_Insumo").iD_Insumo;

  jQuery.ajax({
    // ? url: "@Url.Action("Transaction_Controller_Middle_Create_Update", "Transaction")",
    url: "https://localhost:44381/Transaction/Transaction_Controller_Middle_Create_Update",
    type: "POST",
    data: { ID_Insumo: ID_Insumo, Boolean_Operation: true },
    success: function (data) {
      $(Button_Plus).LoadingOverlay("hide");

      if (data.result) {
        var Input_Quantity_Alternative = parseInt($(Input_Quantity).val()) + 1;
        $(Input_Quantity).val(Input_Quantity_Alternative);
        Monto_Total_Movimiento_Inventario();
      } else {
        Swal.fire({
          title: "Error",
          text: data.message,
          icon: "error",
        });
      }
    },
    beforeSend: function () {
      $(Button_Plus).LoadingOverlay("show");
    },
  });
});

$(document).on("click", ".Button_Less", function () {
  var Display_Flex = $(this).parent("div.d-flex");
  var Input_Quantity = $(Display_Flex).find("input.Input_Quantity");
  var Button_Plus = $(this);
  var ID_Insumo = $(Input_Quantity).data("Obj_Class_Entity_Insumo").iD_Insumo;
  var Input_Quantity_Alternative = parseInt($(Input_Quantity).val()) - 1;

  if (Input_Quantity_Alternative >= 1) {
    jQuery.ajax({
      // ? url: "@Url.Action("Transaction_Controller_Middle_Create_Update", "Transaction")",
      url: "https://localhost:44381/Transaction/Transaction_Controller_Middle_Create_Update",
      type: "POST",
      data: { ID_Insumo: ID_Insumo, Boolean_Operation: false },
      success: function (data) {
        $(Button_Plus).LoadingOverlay("hide");

        if (data.result) {
          $(Input_Quantity).val(Input_Quantity_Alternative);
          Monto_Total_Movimiento_Inventario();
        } else {
          Swal.fire({
            title: "Error",
            text: data.message,
            icon: "error",
          });
        }
      },
      beforeSend: function () {
        $(Button_Plus).LoadingOverlay("show");
      },
    });
  }
});

$(document).on("click", ".Button_Delete_Supply", function () {
  var ID_Insumo = $(this).data("ID_Insumo");
  var Middle_Card = $(this).parents("div.Middle_Card");

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
        // ? url: "@Url.Action("Transaction_Controller_Middle_Delete", "Transaction")",
        url: "https://localhost:44381/Transaction/Transaction_Controller_Middle_Delete",
        type: "POST",
        data: { ID_Insumo: ID_Insumo },
        success: function (data) {
          if (data.result) {
            Middle_Card.remove();
            Count_Middle();
            Monto_Total_Movimiento_Inventario();
          } else {
            Swal.fire({
              title: "Error",
              text: data.message,
              icon: "error",
            });
          }
        },
      });
    }
  });
});

jQuery.validator.addMethod(
  "Valid_Restaurante_Movimiento_Inventario",
  function (value, element) {
    return (
      this.optional(element) ||
      /^[a-zA-ZÀ-ÿ\u00f1\u00d1\u00E0-\u00FC]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1\u00E0-\u00FC]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1\u00E0-\u00FC]+$/.test(
        value
      )
    );
  }
);

jQuery.validator.addMethod(
  "Valid_Direccion_Movimiento_Inventario",
  function (value, element) {
    return (
      this.optional(element) ||
      /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
    );
  }
);

jQuery.validator.addMethod(
  "Valid_Telefono_Movimiento_Inventario",
  function (value, element) {
    return (
      this.optional(element) ||
      /^(?:\+1)?\s?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{3}$/.test(value)
    );
  }
);

$(document).ready(function () {
  $("#Form_Delivery").validate({
    rules: {
      ID_Departamento: {
        required: true,
      },
      ID_Provincia: {
        required: true,
      },
      ID_Distrito: {
        required: true,
      },
      Restaurante_Movimiento_Inventario: {
        required: true,
        Valid_Restaurante_Movimiento_Inventario: true,
      },
      Direccion_Movimiento_Inventario: {
        required: true,
        Valid_Direccion_Movimiento_Inventario: true,
      },
      Telefono_Movimiento_Inventario: {
        required: true,
        number: true,
        Valid_Telefono_Movimiento_Inventario: true,
      },
    },
    messages: {
      ID_Departamento: {
        required: "Campo Requerido: Departamento",
      },
      ID_Provincia: {
        required: "Campo Requerido: Provincia",
      },
      ID_Distrito: {
        required: "Campo Requerido: Distrito",
      },
      Restaurante_Movimiento_Inventario: {
        required: "Campo Requerido: Nombre Completo del Restaurante",
        Valid_Restaurante_Movimiento_Inventario: "Ingrese Nombres V\xe1lidos",
      },
      Direccion_Movimiento_Inventario: {
        required: "Campo Requerido: Direcci\xf3n del Restaurante",
        Valid_Direccion_Movimiento_Inventario:
          "Campo Requerido: Direcci\xf3n del Restaurante",
      },
      Telefono_Movimiento_Inventario: {
        required: "Campo Requerido: Tel\xe9fono del Restaurante",
        number: "Ingrese un N\xfamero Tel\xe9fonico V\xe1lido",
        Valid_Telefono_Movimiento_Inventario:
          "Ingrese un N\xfamero Tel\xe9fonico V\xe1lido",
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

function Confirm() {
  if (!$("#Form_Delivery").valid()) {
    return;
  } else {
    if (parseInt($("#Count_Middle").text()) == 0) {
      Swal.fire({
        title: "Advertencia",
        text: "El Middle se Encuentra Vacío",
        icon: "warning",
      });
      return;
    } else {
      var Obj_Class_Entity_Movimiento_Inventario = {
        tipo_Movimiento_Inventario: "Salida",
        cantidad_Insumo_Movimiento_Inventario: $("input.Input_Quantity").length,
        monto_Total_Movimiento_Inventario: 0.0,
        restaurante_Movimiento_Inventario: $(
          "#Restaurante_Movimiento_Inventario"
        ).val(),
        telefono_Movimiento_Inventario: $(
          "#Telefono_Movimiento_Inventario"
        ).val(),
        direccion_Movimiento_Inventario: $(
          "#Direccion_Movimiento_Inventario"
        ).val(),
        obj_Class_Entity_Distrito: {
          iD_Distrito: $("#ID_Distrito").val(),
        },
      };

      var Obj_List_Class_Entity_Middle = [];

      $("input.Input_Quantity").each(function (i) {
        var Obj_Class_Entity_Insumo = $(this).data("Obj_Class_Entity_Insumo");
        var Cantidad_Insumo_Middle = parseFloat($(this).val());

        Obj_List_Class_Entity_Middle.push({
          Obj_Class_Entity_Insumo: Obj_Class_Entity_Insumo,
          Cantidad_Insumo_Middle: Cantidad_Insumo_Middle,
        });
      });

      jQuery.ajax({
        // ? url: "@Url.Action("Transaction_Controller_Venta_Registrar", "Transaction")",
        url: "https://localhost:44381/Transaction/Transaction_Controller_Venta_Registrar",
        type: "POST",
        data: {
          Obj_Class_Entity_Movimiento_Inventario:
            Obj_Class_Entity_Movimiento_Inventario,
          Obj_List_Class_Entity_Middle: Obj_List_Class_Entity_Middle,
        },
        success: function (data) {
          if (data.result) {
            Swal.fire({
              allowOutsideClick: false,
              title: "Confirmaci\xf3n",
              text: "Transacci\xf3n Realizada",
              icon: "success",
              showCancelButton: true,
              cancelButtonText: "Cancelar",
              cancelButtonColor: "#FF0000",
              confirmButtonText: "Aceptar",
              confirmButtonColor: "#3085D6",
            }).then((result) => {
              if (result.isConfirmed) {
                window.location.replace("/Management/Supply");
              }
            });
          } else {
            Swal.fire({
              title: "Error",
              text: data.message,
              icon: "error",
            });
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
    }
  }
}