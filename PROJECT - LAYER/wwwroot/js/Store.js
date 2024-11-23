$(document).ready(function () {
  Count_Cart();
  Show_Category();
  Show_Product(0, 0);
  Listar_Departamento();
  List_Cart();
});

function Count_Cart() {
  var iD_Usuario = $("#ID_Usuario").val();
  if ($("#Count_Cart").length > 0) {
    jQuery.ajax({
      // ? url: "@Url.Action("Store_Controller_Cart_Count", "Store")",
      url: "https://localhost:44381/Store/Store_Controller_Cart_Count",
      type: "POST",
      data: { iD_Usuario: iD_Usuario },
      success: function (data) {
        var Value_Test = data.cantidad;

        if (Value_Test == 0) {
          $("#Count_Cart").removeClass("badge rounded-pill text-bg-success");
          $("#Count_Cart").addClass("badge rounded-pill text-bg-danger");
          $("#Count_Cart").text(Value_Test);
        } else {
          $("#Count_Cart").removeClass("badge rounded-pill text-bg-danger");
          $("#Count_Cart").addClass("badge rounded-pill text-bg-success");
          $("#Count_Cart").text(Value_Test);
        }
      },
    });
  }
}

function Show_Category() {
  jQuery.ajax({
    // ? url: "@Url.Action("Store_Controller_Categoria_Producto_Listar", "Store")",
    url: "https://localhost:44381/Store/Store_Controller_Categoria_Producto_Listar",
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

      $.each(data.data, function (i, element) {
        $("<li>")
          .addClass("list-group-item list-group-item-primary")
          .append(
            $("<input>")
              .addClass("form-check-input ml-1")
              .attr({
                type: "radio",
                name: "Category",
                value: element.iD_Categoria_Producto,
                id: "Category_Filter_" + i,
                style: "cursor:pointer",
              }),
            $("<label>")
              .addClass("form-check-label ml-5")
              .text(element.nombre_Categoria_Producto)
              .attr({ for: "Category_Filter_" + i, style: "cursor:pointer" })
          )
          .appendTo("#Category_Container");
      });
      Show_Brand();
    },
  });
}

function Show_Brand() {
  var iD_Categoria_Producto = $("input[name=Category]:checked").val();

  jQuery.ajax({
    // ? url: "@Url.Action("Store_Controller_Marca_Producto_Listar_Alter", "Store")",
    url: "https://localhost:44381/Store/Store_Controller_Marca_Producto_Listar_Alter",
    type: "POST",
    data: { iD_Categoria_Producto: iD_Categoria_Producto },
    success: function (data) {
      $("#Brand_Container").html("");

      $("#Brand_Container").LoadingOverlay("hide");

      $("<li>")
        .addClass("list-group-item list-group-item-danger")
        .append(
          $("<input>").addClass("form-check-input ml-1").attr({
            type: "radio",
            name: "Brand",
            value: "0",
            id: "Brand_Filter",
            checked: "checked",
            style: "cursor:pointer",
          }),
          $("<label>")
            .addClass("form-check-label ml-5")
            .text("Todos")
            .attr({ for: "Brand_Filter", style: "cursor:pointer" })
        )
        .appendTo("#Brand_Container");

      $.each(data.data, function (i, element) {
        $("<li>")
          .addClass("list-group-item list-group-item-danger")
          .append(
            $("<input>")
              .addClass("form-check-input ml-1")
              .attr({
                type: "radio",
                name: "Brand",
                value: element.iD_Marca_Producto,
                id: "Brand_Filter_" + i,
                style: "cursor:pointer",
              }),
            $("<label>")
              .addClass("form-check-label ml-5")
              .text(element.nombre_Marca_Producto)
              .attr({ for: "Brand_Filter_" + i, style: "cursor:pointer" })
          )
          .appendTo("#Brand_Container");
      });
    },
    beforeSend: function () {
      $("#Brand_Container").LoadingOverlay("show", {
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

$(document).on("change", "input[name=Category]", function () {
  Show_Brand();
});

function Show_Product(iD_Categoria_Producto, iD_Marca_Producto) {
  jQuery.ajax({
    // ? url: "@Url.Action("Store_Controller_Producto_Listar", "Store")",
    url: "https://localhost:44381/Store/Store_Controller_Producto_Listar",
    type: "POST",
    data: {
      iD_Categoria_Producto: iD_Categoria_Producto,
      iD_Marca_Producto: iD_Marca_Producto,
    },
    success: function (data) {
      var Log_Status = $("#Log_Status").val();

      if (Log_Status == "Initial_Status") {
        $("#Product_Container").html("");

        $("#Product_Container").LoadingOverlay("hide");

        $.each(data.data, function (i, element) {
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
                        element.extension_Imagen_Producto +
                        ";base64," +
                        element.base_64_Imagen_Producto,
                    }),
                  $("<div>")
                    .addClass("card-body p-4")
                    .append(
                      $("<div>")
                        .addClass("text-center")
                        .append(
                          $("<h5>")
                            .addClass("card-title")
                            .text(element.nombre_Producto),
                          $("<p>")
                            .addClass("card-text")
                            .text(
                              "S/. " +
                                element.precio_Producto.toFixed(2) +
                                " PEN"
                            )
                        )
                    ),
                  $("<div>")
                    .addClass(
                      "card-footer p-2 pt-0 border-top-0 bg-transparent"
                    )
                    .append(
                      $("<div>")
                        .addClass("d-grid gap-2")
                        .append(
                          $("<a>")
                            .addClass("btn btn-dark mt-auto")
                            .attr({
                              href:
                                "/Store/Product_Detail" +
                                "?iD_Producto=" +
                                element.iD_Producto,
                            })
                            .html(
                              "<i class='fa-solid fa-circle-info'></i>&nbsp;&nbsp;Ver Detalles"
                            )
                        )
                    )
                )
            )
            .appendTo("#Product_Container");
        });
      } else {
        $("#Product_Container").html("");

        $("#Product_Container").LoadingOverlay("hide");

        $.each(data.data, function (i, element) {
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
                        element.extension_Imagen_Producto +
                        ";base64," +
                        element.base_64_Imagen_Producto,
                    }),
                  $("<div>")
                    .addClass("card-body p-4")
                    .append(
                      $("<div>")
                        .addClass("text-center")
                        .append(
                          $("<h5>")
                            .addClass("card-title")
                            .text(element.nombre_Producto),
                          $("<p>")
                            .addClass("card-text")
                            .text(
                              "S/. " +
                                element.precio_Producto.toFixed(2) +
                                " PEN"
                            )
                        )
                    ),
                  $("<div>")
                    .addClass(
                      "card-footer p-2 pt-0 border-top-0 bg-transparent"
                    )
                    .append(
                      $("<div>")
                        .addClass("d-grid gap-2")
                        .append(
                          $("<button>")
                            .addClass("btn btn-success mt-auto Add_Cart")
                            .data("id_producto", element.iD_Producto)
                            .html(
                              "<i class='fa-solid fa-cart-plus'></i>&nbsp;&nbsp;Agregar al Carrito"
                            ),
                          $("<a>")
                            .addClass("btn btn-dark mt-auto")
                            .attr({
                              href:
                                "/Store/Product_Detail" +
                                "?iD_Producto=" +
                                element.iD_Producto,
                            })
                            .html(
                              "<i class='fa-solid fa-circle-info'></i>&nbsp;&nbsp;Ver Detalles"
                            )
                        )
                    )
                )
            )
            .appendTo("#Product_Container");
        });
      }
    },
    beforeSend: function () {
      $("#Product_Container").LoadingOverlay("show", {
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

$("#Filter_Button").on("click", function () {
  var iD_Categoria_Producto = $("input[name=Category]:checked").val();
  var iD_Marca_Producto = $("input[name=Brand]:checked").val();
  Show_Product(iD_Categoria_Producto, iD_Marca_Producto);
});

$(document).on("click", "button.Add_Cart", function () {
  var iD_Usuario = $("#ID_Usuario").val();
  var iD_Producto = $(this).data("id_producto");

  Swal.fire({
    title: "Confirmaci\xf3n",
    text: "\xbfDesea Agregar el Producto Seleccionado al Carrito?",
    icon: "success",
    showCancelButton: true,
    cancelButtonText: "Cancelar",
    cancelButtonColor: "#FF0000",
    confirmButtonText: "Aceptar",
    confirmButtonColor: "#3085D6",
  }).then((result) => {
    if (result.isConfirmed) {
      jQuery.ajax({
        // ? url: "@Url.Action("Store_Controller_Cart_List", "Store")",
        url: "https://localhost:44381/Store/Store_Controller_Cart_List",
        type: "POST",
        data: { iD_Usuario: iD_Usuario, iD_Producto: iD_Producto },
        success: function (data) {
          if (data.result) {
            Count_Cart();
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
    // ? url: "@Url.Action("Store_Controller_Ubication_Departamento_Listar", "Store")",
    url: "https://localhost:44381/Store/Store_Controller_Ubication_Departamento_Listar",
    type: "GET",
    dataType: "json",
    success: function (data) {
      if (data.data != null) {
        $.each(data.data, function (i, element) {
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
  var iD_Departamento = $("#ID_Departamento option:selected").val();
  $("#ID_Provincia").html("");
  $("<option>")
    .attr({ value: "0", disabled: "disabled", selected: "true" })
    .text("Seleccionar")
    .appendTo("#ID_Provincia");
  jQuery.ajax({
    // ? url: "@Url.Action("Store_Controller_Ubication_Provincia_Listar", "Store")",
    url: "https://localhost:44381/Store/Store_Controller_Ubication_Provincia_Listar",
    type: "POST",
    data: { iD_Departamento: iD_Departamento },
    success: function (data) {
      if (data.data != null) {
        $.each(data.data, function (i, element) {
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
  var iD_Provincia = $("#ID_Provincia option:selected").val();
  var iD_Departamento = $("#ID_Departamento option:selected").val();
  $("#ID_Distrito").html("");
  $("<option>")
    .attr({ value: "0", disabled: "disabled", selected: "true" })
    .text("Seleccionar")
    .appendTo("#ID_Distrito");
  jQuery.ajax({
    // ? url: "@Url.Action("Store_Controller_Ubication_Distrito_Listar", "Store")",
    url: "https://localhost:44381/Store/Store_Controller_Ubication_Distrito_Listar",
    type: "POST",
    data: { iD_Provincia: iD_Provincia, iD_Departamento: iD_Departamento },
    success: function (data) {
      if (data.data != null) {
        $.each(data.data, function (i, element) {
          $("<option>")
            .attr({ value: element.iD_Distrito })
            .text(element.nombre_Distrito)
            .appendTo("#ID_Distrito");
        });
      }
    },
  });
}

function List_Cart() {
  var iD_Usuario = $("#ID_Usuario").val();
  jQuery.ajax({
    // ? url: "@Url.Action("Store_Controller_Cart_Obtain_Cart", "Store")",
    url: "https://localhost:44381/Store/Store_Controller_Cart_Obtain_Cart",
    type: "POST",
    data: { iD_Usuario: iD_Usuario },
    success: function (data) {
      $("#Cart_Product").html("");

      $.each(data.data, function (i, element) {
        $("<div>")
          .addClass("card mb-2 Card_Product")
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
                              element.object_ID_Producto
                                .extension_Imagen_Producto +
                              ";base64," +
                              element.object_ID_Producto
                                .base_64_Imagen_Producto,
                          })
                          .css({ width: "100px", height: "100px" })
                      ),
                    $("<div>")
                      .addClass("col-sm-4 align-self-center")
                      .append(
                        $("<span>")
                          .addClass("font-weight-bold d-block")
                          .text(
                            element.object_ID_Producto.object_ID_Marca_Producto
                              .nombre_Marca_Producto
                          ),
                        $("<span>").text(
                          element.object_ID_Producto.nombre_Producto
                        )
                      ),
                    $("<div>")
                      .addClass("col-sm-2 align-self-center")
                      .append(
                        $("<span>").text(
                          "S/. " +
                            element.object_ID_Producto.precio_Producto.toFixed(
                              2
                            ) +
                            " PEN"
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
                                "object_id_producto",
                                element.object_ID_Producto
                              )
                              .val(element.cantidad_Carrito),
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
                            "btn btn-outline-danger Button_Delete_Product"
                          )
                          .append(
                            $("<i>").addClass("fa-solid fa-trash"),
                            "  Eliminar"
                          )
                          .data(
                            "id_producto",
                            element.object_ID_Producto.iD_Producto
                          )
                      )
                  )
              )
          )
          .appendTo("#Cart_Product");
      });
      Suma_Total();
    },
  });
}

function Suma_Total() {
  var Suma_Total = parseFloat(0);

  $("input.Input_Quantity").each(function (i) {
    var Price = $(this).data("object_id_producto").precio_Producto;
    var Quantity = parseFloat($(this).val());
    var Subtotal = Price * Quantity;
    Suma_Total += Subtotal;
  });

  $("#Total").text(Suma_Total.toFixed(2));
  $("#Total").data("suma_total", Suma_Total);
}

$(document).on("click", ".Button_Plus", function () {
  var Div_Container = $(this).parent("div.d-flex");
  var Input_Quantity = $(Div_Container).find("input.Input_Quantity");
  var Button = $(this);
  var iD_Usuario = $("#ID_Usuario").val();
  var iD_Producto = $(Input_Quantity).data("object_id_producto").iD_Producto;

  jQuery.ajax({
    // ? url: "@Url.Action("Store_Controller_Cart_List_Alter", "Store")",
    url: "https://localhost:44381/Store/Store_Controller_Cart_List_Alter",
    type: "POST",
    data: { iD_Usuario: iD_Usuario, iD_Producto: iD_Producto, sUM: true },
    success: function (data) {
      $(Button).LoadingOverlay("hide");

      if (data.result) {
        var Quantity = parseInt($(Input_Quantity).val()) + 1;
        $(Input_Quantity).val(Quantity);
        Suma_Total();
      } else {
        Swal.fire({
          title: "Error",
          text: data.message,
          icon: "error",
        });
      }
    },
    beforeSend: function () {
      $(Button).LoadingOverlay("show");
    },
  });
});

$(document).on("click", ".Button_Less", function () {
  var Div_Container = $(this).parent("div.d-flex");
  var Input_Quantity = $(Div_Container).find("input.Input_Quantity");
  var Button = $(this);
  var iD_Usuario = $("#ID_Usuario").val();
  var iD_Producto = $(Input_Quantity).data("object_id_producto").iD_Producto;
  var Quantity = parseInt($(Input_Quantity).val()) - 1;

  if (Quantity >= 1) {
    jQuery.ajax({
      // ? url: "@Url.Action("Store_Controller_Cart_List_Alter", "Store")",
      url: "https://localhost:44381/Store/Store_Controller_Cart_List_Alter",
      type: "POST",
      data: { iD_Usuario: iD_Usuario, iD_Producto: iD_Producto, sUM: false },
      success: function (data) {
        $(Button).LoadingOverlay("hide");

        if (data.result) {
          $(Input_Quantity).val(Quantity);
          Suma_Total();
        } else {
          Swal.fire({
            title: "Error",
            text: data.message,
            icon: "error",
          });
        }
      },
      beforeSend: function () {
        $(Button).LoadingOverlay("show");
      },
    });
  }
});

$(document).on("click", ".Button_Delete_Product", function () {
  var iD_Usuario = $("#ID_Usuario").val();
  var iD_Producto = $(this).data("id_producto");
  var Card_Product = $(this).parents("div.Card_Product");

  jQuery.ajax({
    // ? url: "@Url.Action("Store_Controller_Cart_Delete_Cart", "Store")",
    url: "https://localhost:44381/Store/Store_Controller_Cart_Delete_Cart",
    type: "POST",
    data: { iD_Usuario: iD_Usuario, iD_Producto: iD_Producto },
    success: function (data) {
      if (data.result) {
        Card_Product.remove();
        Count_Cart();
        Suma_Total();
      } else {
        Swal.fire({
          title: "Error",
          text: data.message,
          icon: "error",
        });
      }
    },
  });
});

jQuery.validator.addMethod(
  "Valid_Nombre_Apellido_Usuario",
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
  "Valid_Direccion_Usuario",
  function (value, element) {
    return (
      this.optional(element) ||
      /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
    );
  }
);

jQuery.validator.addMethod("Valid_Telefono_Usuario", function (value, element) {
  return (
    this.optional(element) ||
    /^(?:\+1)?\s?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{3}$/.test(value)
  );
});

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
      Nombre_Apellido_Usuario: {
        required: true,
        Valid_Nombre_Apellido_Usuario: true,
      },
      Direccion_Usuario: {
        required: true,
        Valid_Direccion_Usuario: true,
      },
      Telefono_Usuario: {
        required: true,
        number: true,
        Valid_Telefono_Usuario: true,
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
      Nombre_Apellido_Usuario: {
        required: "Campo Requerido: Nombre Completo del Usuario",
        Valid_Nombre_Apellido_Usuario: "Ingrese Nombres V\xe1lidos",
      },
      Direccion_Usuario: {
        required: "Campo Requerido: Direcci\xf3n del Usuario",
        Valid_Direccion_Usuario: "Campo Requerido: Direcci\xf3n del Usuario",
      },
      Telefono_Usuario: {
        required: "Campo Requerido: Tel\xe9fono del Usuario",
        number: "Ingrese un N\xfamero Tel\xe9fonico V\xe1lido",
        Valid_Telefono_Usuario: "Ingrese un N\xfamero Tel\xe9fonico V\xe1lido",
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
    console.log("Ok!");
  },
});

function Pay() {
  if (!$("#Form_Delivery").valid()) {
    return;
  } else {
    if (parseInt($("#Count_Cart").text()) == 0) {
      Swal.fire({
        title: "Advertencia",
        text: "El Carrito de Compras se Encuentra Vacío",
        icon: "warning",
      });
      return;
    } else {
      var iD_Usuario = $("#ID_Usuario").val();

      var venta = {
        total_Producto: $("input.Input_Quantity").length,
        monto_Total: 0,
        contacto: $("#Nombre_Apellido_Usuario").val(),
        iD_Distrito: $("#ID_Distrito").val(),
        telefono: $("#Telefono_Usuario").val(),
        direccion: $("#Direccion_Usuario").val(),
      };

      var cart_list = [];

      $("input.Input_Quantity").each(function (i) {
        var object_id_producto = $(this).data("object_id_producto");
        var cantidad_carrito = parseFloat($(this).val());

        cart_list.push({
          object_ID_Producto: object_id_producto,
          cantidad_carrito: cantidad_carrito,
        });
      });

      jQuery.ajax({
        // ? url: "@Url.Action("Store_Controller_Venta_Registrar", "Store")",
        url: "https://localhost:44381/Store/Store_Controller_Venta_Registrar",
        type: "POST",
        data: {
          obj_List_Class_Entity_Carrito: cart_list,
          obj_Class_Entity_Venta: venta,
          iD_Usuario: iD_Usuario,
        },
        success: function (data) {
          if (data.result) {
            Swal.fire({
              title: "Correcto!",
              text: "Gracias por su Compra!",
              icon: "success",
            });
          } else {
            Swal.fire({
              title: "Error",
              text: "Error",
              icon: "error",
            });
          }
        },
      });
    }
  }
}