var Table_Producto;
var Selected_Row;

function Show_Product_Image(input) {
    if (input.files) {
        var Reader = new FileReader();
        Reader.onload = function (event) {
            $("#Imagen_Producto").attr("src", event.target.result);
        };
        Reader.readAsDataURL(input.files[0]);
    }
}

/**
 * *jQuery.ajax({
 * *    url: "https://localhost:44381/Management/Management_Controller_Producto_Listar",
 * *    type: "GET",
 * *    dataType: "json",
 * *    contentType: "application/json; charset=UTF-8",
 * *    success: function (data) {
 * *        console.log(data); // ? Good 'console.log'
 * *    }
 * *});
 */
$(document).ready(function () {
    Table_Producto = $("#Table_Producto").DataTable({
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
            // ? url: "@Url.Action("Management_Controller_Producto_Listar", "Management")",
            url: "https://localhost:44381/Management/Management_Controller_Producto_Listar",
            type: "GET",
            dataType: "json",
        },
        columns: [
            { data: "iD_Producto" },
            {
                data: null,
                render: function (data, type, row) {
                    return (
                        '<a tabindex="' +
                        row.iD_Producto +
                        '" href="#/" class="Pop_Trigger" data-bs-html="true" data-bs-custom-class="custom-popover" data-bs-container="body" data-bs-toggle="popover" data-bs-title="Informaci\xf3n" data-bs-content="<p><b>Categor\xeda:</b> ' +
                        row.object_ID_Categoria_Producto.nombre_Categoria_Producto +
                        "</p><p><b>Marca:</b> " +
                        row.object_ID_Marca_Producto.nombre_Marca_Producto +
                        "</p><p><b>Descripci\xf3n:</b> " +
                        row.descripcion_Producto +
                        '</p>">' +
                        row.nombre_Producto +
                        "</a>"
                    );
                },
            },
            { data: "precio_Producto" },
            { data: "stock_Producto" },
            {
                data: "estado_Producto",
                render: function (estado_Producto) {
                    if (estado_Producto) {
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
                        '<img style="width: 60px; height: 60px;" src="../../Product_Images/' +
                        row.nombre_Imagen_Producto +
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
});

jQuery.ajax({
    // ? url: "@Url.Action("Management_Controller_Categoria_Producto_Listar", "Management")",
    url: "https://localhost:44381/Management/Management_Controller_Categoria_Producto_Listar",
    type: "GET",
    dataType: "json",
    contentType: "application/json; charset=UTF-8",
    success: function (data) {
        $("<option>")
            .attr({ value: "0", disabled: "true", selected: "true" })
            .text("Seleccionar")
            .appendTo("#Categoria_Producto");
        $.each(data.data, function (index, item) {
            $("<option>")
                .attr({ value: item.iD_Categoria_Producto })
                .text(item.nombre_Categoria_Producto)
                .appendTo("#Categoria_Producto");
        });
    },
});

jQuery.ajax({
    // ? url: "@Url.Action("Management_Controller_Marca_Producto_Listar", "Management")",
    url: "https://localhost:44381/Management/Management_Controller_Marca_Producto_Listar",
    type: "GET",
    dataType: "json",
    contentType: "application/json; charset=UTF-8",
    success: function (data) {
        $("<option>")
            .attr({ value: "0", disabled: "true", selected: "true" })
            .text("Seleccionar")
            .appendTo("#Marca_Producto");
        $.each(data.data, function (index, item) {
            $("<option>")
                .attr({ value: item.iD_Marca_Producto })
                .text(item.nombre_Marca_Producto)
                .appendTo("#Marca_Producto");
        });
    },
});

function Open_Form_Modal(data) {
    if (data == null) {
        $("#Categoria_Producto").removeClass("is-valid");
        $("#Categoria_Producto").removeClass("is-invalid");
        $("#Marca_Producto").removeClass("is-valid");
        $("#Marca_Producto").removeClass("is-invalid");
        $("#Nombre_Producto").removeClass("is-valid");
        $("#Nombre_Producto").removeClass("is-invalid");
        $("#Descripcion_Producto").removeClass("is-valid");
        $("#Descripcion_Producto").removeClass("is-invalid");
        $("#Precio_Producto").removeClass("is-valid");
        $("#Precio_Producto").removeClass("is-invalid");
        $("#Stock_Producto").removeClass("is-valid");
        $("#Stock_Producto").removeClass("is-invalid");
        $("#Estado_Producto").removeClass("is-valid");
        $("#Estado_Producto").removeClass("is-invalid");
        $("#Imagen_Producto_Input").removeClass("is-valid");
        $("#ID_Producto").val(0);
        $("#Categoria_Producto").val(0);
        $("#Marca_Producto").val(0);
        $("#Nombre_Producto").val("");
        $("#Descripcion_Producto").val("");
        $("#Precio_Producto").val("");
        $("#Stock_Producto").val("");
        $("#Estado_Producto").val(0);
        $("#Imagen_Producto_Input").val("");
        $("#Imagen_Producto").removeAttr("src");
    } else {
        if (data != null) {
            $("#Categoria_Producto").removeClass("is-valid");
            $("#Categoria_Producto").removeClass("is-invalid");
            $("#Marca_Producto").removeClass("is-valid");
            $("#Marca_Producto").removeClass("is-invalid");
            $("#Nombre_Producto").removeClass("is-valid");
            $("#Nombre_Producto").removeClass("is-invalid");
            $("#Descripcion_Producto").removeClass("is-valid");
            $("#Descripcion_Producto").removeClass("is-invalid");
            $("#Precio_Producto").removeClass("is-valid");
            $("#Precio_Producto").removeClass("is-invalid");
            $("#Stock_Producto").removeClass("is-valid");
            $("#Stock_Producto").removeClass("is-invalid");
            $("#Estado_Producto").removeClass("is-valid");
            $("#Estado_Producto").removeClass("is-invalid");
            $("#Imagen_Producto_Input").removeClass("is-valid");
            $("#ID_Producto").val(data.iD_Producto);
            $("#Categoria_Producto").val(
                data.object_ID_Categoria_Producto.iD_Categoria_Producto
            );
            $("#Marca_Producto").val(
                data.object_ID_Marca_Producto.iD_Marca_Producto
            );
            $("#Nombre_Producto").val(data.nombre_Producto);
            $("#Descripcion_Producto").val(data.descripcion_Producto);
            $("#Precio_Producto").val(data.precio_Producto);
            $("#Stock_Producto").val(data.stock_Producto);
            $("#Estado_Producto").val(
                data.estado_Producto == true ? "Available" : "Not_Available"
            );
            $("#Imagen_Producto_Input").val("");
            $("#Imagen_Producto").removeAttr("src");
            jQuery.ajax({
                // ? url: "@Url.Action("Management_Controller_Producto_Imagen", "Management")",
                url: "https://localhost:44381/Management/Management_Controller_Producto_Imagen",
                type: "GET",
                data: { iD_Producto: data.iD_Producto },
                success: function (data) {
                    $("#Imagen_Producto").LoadingOverlay("hide");
                    if (data.conversion) {
                        $("#Imagen_Producto").attr({
                            src:
                                "data:image/" +
                                data.extension_Imagen_Producto +
                                ";base64," +
                                data.base_64_Imagen_Producto,
                        });
                    }
                },
                error: function (error) {
                    alert(error);
                },
                beforeSend: function () {
                    $("#Imagen_Producto").LoadingOverlay("show", {
                        background: "rgba(0, 0, 0, 0.5)",
                        image: "../../img/clock-regular.svg",
                        imageAnimation: "1.5s fadein",
                        imageAutoResize: true,
                        imageResizeFactor: 1,
                    });
                },
            });
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

$("#Table_Producto").on("click", ".Edit_Button", function () {
    Selected_Row = Selected_Row_Function(this);
    var data = Table_Producto.row(Selected_Row).data();
    // console.log(data); // ? Good 'console.log'
    Open_Form_Modal(data);
});

$("#Table_Producto").on("click", ".Delete_Button", function () {
    Selected_Row = Selected_Row_Function(this);
    var data = Table_Producto.row(Selected_Row).data();
    Swal.fire({
        title: "Confirmaci\xf3n",
        text: "\xbfDesea Eliminar el Producto Seleccionado?",
        icon: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        cancelButtonColor: "#FF0000",
        confirmButtonText: "Eliminar",
        confirmButtonColor: "#3085D6",
    }).then((result) => {
        if (result.isConfirmed) {
            jQuery.ajax({
                // ? url: "@Url.Action("Management_Controller_Producto_Eliminar", "Management")",
                url: "https://localhost:44381/Management/Management_Controller_Producto_Eliminar",
                type: "DELETE",
                data: { iD_Producto: data.iD_Producto },
                success: function (data) {
                    // debugger; // TODO: Punto de Depuración

                    if (data.result) {
                        Swal.fire({
                            title: "Correcto",
                            text: "El Producto ha sido Eliminado",
                            icon: "success",
                        });
                        Table_Producto.row(Selected_Row).remove().draw();
                    } else {
                        Swal.fire({
                            title: "Error",
                            text: data.message,
                            icon: "error",
                        });
                    }
                },
                error: function (error) {
                    alert(error);
                },
            });
        }
    });
    // console.log(data); // ? Good 'console.log'
});

jQuery.validator.addMethod("Valid_Nombre_Producto", function (value, element) {
    return (
        this.optional(element) ||
        /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
    );
});

jQuery.validator.addMethod(
    "Valid_Descripcion_Producto",
    function (value, element) {
        return (
            this.optional(element) ||
            /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
        );
    }
);

$(document).ready(function () {
    $("#Form_Product").validate({
        rules: {
            Estado_Producto: {
                required: true,
            },
            Nombre_Producto: {
                required: true,
                Valid_Nombre_Producto: true,
            },
            Categoria_Producto: {
                required: true,
            },
            Marca_Producto: {
                required: true,
            },
            Precio_Producto: {
                required: true,
                number: true,
            },
            Stock_Producto: {
                required: true,
                number: true,
            },
            Descripcion_Producto: {
                required: true,
                Valid_Descripcion_Producto: true,
            },
        },
        messages: {
            Estado_Producto: {
                required: "Campo Requerido: Estado del Producto",
            },
            Nombre_Producto: {
                required: "Campo Requerido: Nombre de Producto",
                Valid_Nombre_Producto: "Campo Requerido: Nombre de Producto",
            },
            Categoria_Producto: {
                required: "Campo Requerido: Categor\xeda del Producto",
            },
            Marca_Producto: {
                required: "Campo Requerido: Marca del Insummo",
            },
            Precio_Producto: {
                required: "Campo Requerido: Precio del Producto",
                number: "Ingrese un Precio V\xe1lido",
            },
            Stock_Producto: {
                required: "Campo Requerido: Stock del Producto",
                number: "Ingrese un Stock V\xe1lido",
            },
            Descripcion_Producto: {
                required: "Campo Requerido: Descripci\xf3n del Producto",
                Valid_Descripcion_Producto: "Campo Requerido: Descripci\xf3n del Producto",
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

function Procesar() {
    if (!$("#Form_Product").valid()) {
        return;
    } else {
        var Imagen_Producto_Input = $("#Imagen_Producto_Input")[0].files[0];

        var Producto = {
            iD_Producto: $("#ID_Producto").val(),
            object_ID_Categoria_Producto: {
                iD_Categoria_Producto: $("#Categoria_Producto option:selected").val(),
                nombre_Categoria_Producto: $("#Categoria_Producto option:selected").text(),
            },
            object_ID_Marca_Producto: {
                iD_Marca_Producto: $("#Marca_Producto option:selected").val(),
                nombre_Marca_Producto: $("#Marca_Producto option:selected").text(),
            },
            nombre_Producto: $.trim($("#Nombre_Producto").val()),
            descripcion_Producto: $.trim($("#Descripcion_Producto").val()),
            precio_Producto: $("#Precio_Producto").val(),
            precio_Producto_String: $("#Precio_Producto").val(),
            stock_Producto: $("#Stock_Producto").val(),
            estado_Producto: $("#Estado_Producto").val() == "Available" ? true : false,
        };

        if ($("#ID_Producto").val() == 0) {
            var Request = new FormData();
            Request.append("Obj_Class_Entity_Producto", JSON.stringify(Producto));
            Request.append("Obj_IFormFile", Imagen_Producto_Input);

            jQuery.ajax({
                // ? url: "@Url.Action("Management_Controller_Producto_Registrar", "Management")",
                url: "https://localhost:44381/Management/Management_Controller_Producto_Registrar",
                type: "POST",
                data: Request,
                processData: false,
                contentType: false,
                success: function (data) {
                    // debugger; // TODO: Punto de Depuración

                    $(".modal-body").LoadingOverlay("hide");

                    if (data.iD_Auto_Generated != 0) {
                        Producto.iD_Producto = data.iD_Auto_Generated;
                        Table_Producto.row.add(Producto).draw(false);
                        $("#Form_Modal").modal("hide");
                        Table_Producto.ajax.reload();
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
                        toastr["success"]("El Producto ha sido Registrado", "\xc9xito:");
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
                error: function (error) {
                    $(".modal-body").LoadingOverlay("hide");
                    alert(error);
                },
                beforeSend: function () {
                    $(".modal-body").LoadingOverlay("show", {
                        background: "rgba(0, 0, 0, 0.5)",
                        image: "../../img/clock-regular.svg",
                        imageAnimation: "1.5s fadein",
                        imageAutoResize: true,
                        imageResizeFactor: 1,
                    });
                },
            });
        } else {
            if ($("#ID_Producto").val() != 0) {
                var Request = new FormData();
                Request.append("Obj_Class_Entity_Producto", JSON.stringify(Producto));
                Request.append("Obj_IFormFile", Imagen_Producto_Input);

                jQuery.ajax({
                    // ? url: "@Url.Action("Management_Controller_Producto_Editar", "Management")",
                    url: "https://localhost:44381/Management/Management_Controller_Producto_Editar",
                    type: "PUT",
                    data: Request,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        // debugger; // TODO: Punto de Depuración

                        $(".modal-body").LoadingOverlay("hide");

                        if (data.successful_operation) {
                            Table_Producto.row(Selected_Row).data(Producto).draw(false);
                            Selected_Row = null;
                            $("#Form_Modal").modal("hide");
                            Table_Producto.ajax.reload();
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
                            toastr["info"]("El Producto ha sido Modificado", "Informaci\xf3n:");
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
                    error: function (error) {
                        $(".modal-body").LoadingOverlay("hide");
                        alert(error);
                    },
                    beforeSend: function () {
                        $(".modal-body").LoadingOverlay("show", {
                            background: "rgba(0, 0, 0, 0.5)",
                            image: "../../img/clock-regular.svg",
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