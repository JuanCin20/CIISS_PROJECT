var Table_Marca_Producto;
var Selected_Row;

/**
 * *jQuery.ajax({
 * *    url: "https://localhost:44381/Management/Management_Controller_Marca_Producto_Listar",
 * *    type: "GET",
 * *    dataType: "json",
 * *    contentType: "application/json; charset=UTF-8",
 * *    success: function (data) {
 * *        console.log(data); // ? Good 'console.log'
 * *    }
 * *});
 */
$(document).ready(function () {
    Table_Marca_Producto = $("#Table_Marca_Producto").DataTable({
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
            // ? url: "@Url.Action("Management_Controller_Marca_Producto_Listar", "Management")",
            url: "https://localhost:44381/Management/Management_Controller_Marca_Producto_Listar",
            type: "GET",
            dataType: "json",
        },
        columns: [
            { data: "iD_Marca_Producto" },
            {
                data: null,
                render: function (data, type, row) {
                    return (
                        '<a tabindex="' +
                        row.iD_Marca_Producto +
                        '" href="#/" class="Pop_Trigger" data-bs-html="true" data-bs-custom-class="custom-popover" data-bs-container="body" data-bs-toggle="popover" data-bs-title="Informaci\xf3n" data-bs-content="<p><b>Tel\xe9fono:</b> ' +
                        row.telefono_Marca_Producto +
                        "</p><p><b>E-Mail:</b> " +
                        row.e_Mail_Marca_Producto +
                        "</p><p><b>Direcci\xf3n:</b> " +
                        row.direccion_Marca_Producto +
                        '</p>">' +
                        row.nombre_Marca_Producto +
                        "</a>"
                    );
                },
            },
            {
                data: "estado_Marca_Producto",
                render: function (estado_Marca_Producto) {
                    if (estado_Marca_Producto) {
                        return '<span class="badge text-bg-success">Disponible</span>';
                    } else {
                        return '<span class="badge text-bg-danger">No Disponible</span>';
                    }
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

function Open_Form_Modal(data) {
    if (data == null) {
        $("#Nombre_Marca_Producto").removeClass("is-valid");
        $("#Nombre_Marca_Producto").removeClass("is-invalid");
        $("#Telefono_Marca_Producto").removeClass("is-valid");
        $("#Telefono_Marca_Producto").removeClass("is-invalid");
        $("#E_Mail_Marca_Producto").removeClass("is-valid");
        $("#E_Mail_Marca_Producto").removeClass("is-invalid");
        $("#Direccion_Marca_Producto").removeClass("is-valid");
        $("#Direccion_Marca_Producto").removeClass("is-invalid");
        $("#Estado_Marca_Producto").removeClass("is-valid");
        $("#Estado_Marca_Producto").removeClass("is-invalid");
        $("#ID_Marca_Producto").val(0);
        $("#Nombre_Marca_Producto").val("");
        $("#Telefono_Marca_Producto").val("");
        $("#E_Mail_Marca_Producto").val("");
        $("#Direccion_Marca_Producto").val("");
        $("#Estado_Marca_Producto").val(0);
    } else {
        if (data != null) {
            $("#Nombre_Marca_Producto").removeClass("is-valid");
            $("#Nombre_Marca_Producto").removeClass("is-invalid");
            $("#Telefono_Marca_Producto").removeClass("is-valid");
            $("#Telefono_Marca_Producto").removeClass("is-invalid");
            $("#E_Mail_Marca_Producto").removeClass("is-valid");
            $("#E_Mail_Marca_Producto").removeClass("is-invalid");
            $("#Direccion_Marca_Producto").removeClass("is-valid");
            $("#Direccion_Marca_Producto").removeClass("is-invalid");
            $("#Estado_Marca_Producto").removeClass("is-valid");
            $("#Estado_Marca_Producto").removeClass("is-invalid");
            $("#ID_Marca_Producto").val(data.iD_Marca_Producto);
            $("#Nombre_Marca_Producto").val(data.nombre_Marca_Producto);
            $("#Telefono_Marca_Producto").val(data.telefono_Marca_Producto);
            $("#E_Mail_Marca_Producto").val(data.e_Mail_Marca_Producto);
            $("#Direccion_Marca_Producto").val(data.direccion_Marca_Producto);
            $("#Estado_Marca_Producto").val(
                data.estado_Marca_Producto == true ? "Available" : "Not_Available"
            );
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

$("#Table_Marca_Producto").on("click", ".Edit_Button", function () {
    Selected_Row = Selected_Row_Function(this);
    var data = Table_Marca_Producto.row(Selected_Row).data();
    // console.log(data); // ? Good 'console.log'
    Open_Form_Modal(data);
});

$("#Table_Marca_Producto").on("click", ".Delete_Button", function () {
    Selected_Row = Selected_Row_Function(this);
    var data = Table_Marca_Producto.row(Selected_Row).data();
    Swal.fire({
        title: "Confirmaci\xf3n",
        text: "\xbfDesea Eliminar al Proveedor Seleccionado?",
        icon: "warning",
        showCancelButton: true,
        cancelButtonText: "Cancelar",
        cancelButtonColor: "#FF0000",
        confirmButtonText: "Eliminar",
        confirmButtonColor: "#3085D6",
    }).then((result) => {
        if (result.isConfirmed) {
            jQuery.ajax({
                // ? url: "@Url.Action("Management_Controller_Marca_Producto_Eliminar", "Management")",
                url: "https://localhost:44381/Management/Management_Controller_Marca_Producto_Eliminar",
                type: "DELETE",
                data: { iD_Marca_Producto: data.iD_Marca_Producto },
                success: function (data) {
                    // debugger; // TODO: Punto de Depuración

                    if (data.result) {
                        Swal.fire({
                            title: "Correcto",
                            text: "El Proveedor ha sido Eliminado",
                            icon: "success",
                        });
                        Table_Marca_Producto.row(Selected_Row).remove().draw();
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

jQuery.validator.addMethod(
    "Valid_Nombre_Marca_Producto",
    function (value, element) {
        return (
            this.optional(element) ||
            /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
        );
    }
);

jQuery.validator.addMethod(
    "Valid_Telefono_Marca_Producto",
    function (value, element) {
        return (
            this.optional(element) ||
            /^(?:\+1)?\s?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{3}$/.test(value)
        );
    }
);

jQuery.validator.addMethod(
    "Valid_E_Mail_Marca_Producto",
    function (value, element) {
        return (
            this.optional(element) || /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/.test(value)
        );
    }
);

jQuery.validator.addMethod(
    "Valid_Direccion_Marca_Producto",
    function (value, element) {
        return (
            this.optional(element) ||
            /([a-zA-Z',.-]+( [a-zA-Z',.-]+)*){2,30}/.test(value)
        );
    }
);

$(document).ready(function () {
    $("#Form_Brand").validate({
        rules: {
            Estado_Marca_Producto: {
                required: true,
            },
            Nombre_Marca_Producto: {
                required: true,
                Valid_Nombre_Marca_Producto: true,
            },
            Telefono_Marca_Producto: {
                required: true,
                number: true,
                Valid_Telefono_Marca_Producto: true,
            },
            E_Mail_Marca_Producto: {
                required: true,
                Valid_E_Mail_Marca_Producto: true,
            },
            Direccion_Marca_Producto: {
                required: true,
                Valid_Direccion_Marca_Producto: true,
            },
        },
        messages: {
            Estado_Marca_Producto: {
                required: "Campo Requerido: Estado del Proveedor del Insumo",
            },
            Nombre_Marca_Producto: {
                required: "Campo Requerido: Nombre del Proveedor del Insumo",
                Valid_Nombre_Marca_Producto:
                    "Campo Requerido: Nombre del Proveedor del Insumo",
            },
            Telefono_Marca_Producto: {
                required: "Campo Requerido: Tel\xe9fono del Proveedor del Insumo",
                number: "Ingrese un N\xfamero Tel\xe9fonico V\xe1lido",
                Valid_Telefono_Marca_Producto: "Ingrese un N\xfamero Tel\xe9fonico V\xe1lido",
            },
            E_Mail_Marca_Producto: {
                required:
                    "Campo Requerido: Correo Electr\xf3nico del Proveedor del Insumo",
                Valid_E_Mail_Marca_Producto: "Ingrese un Correo Electr\xf3nico V\xe1lido",
            },
            Direccion_Marca_Producto: {
                required: "Campo Requerido: Direcci\xf3n del Proveedor del Insumo",
                Valid_Direccion_Marca_Producto:
                    "Campo Requerido: Direcci\xf3n del Proveedor del Insumo",
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
    if (!$("#Form_Brand").valid()) {
        return;
    } else {
        var Proveedor = {
            iD_Marca_Producto: $("#ID_Marca_Producto").val(),
            nombre_Marca_Producto: $.trim($("#Nombre_Marca_Producto").val()),
            telefono_Marca_Producto: $("#Telefono_Marca_Producto").val(),
            e_Mail_Marca_Producto: $.trim($("#E_Mail_Marca_Producto").val()),
            direccion_Marca_Producto: $.trim(
                $("#Direccion_Marca_Producto").val()
            ),
            estado_Marca_Producto:
                $("#Estado_Marca_Producto").val() == "Available" ? true : false,
        };

        if ($("#ID_Marca_Producto").val() == 0) {
            jQuery.ajax({
                // ? url: "@Url.Action("Management_Controller_Marca_Producto_Registrar", "Management")",
                url: "https://localhost:44381/Management/Management_Controller_Marca_Producto_Registrar",
                type: "POST",
                data: { Obj_Class_Entity_Marca_Producto: Proveedor },
                success: function (data) {
                    // debugger; // TODO: Punto de Depuración

                    $(".modal-body").LoadingOverlay("hide");

                    if (data.result != 0) {
                        Proveedor.iD_Marca_Producto = data.result;
                        Table_Marca_Producto.row.add(Proveedor).draw(false);
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
            if ($("#ID_Marca_Producto").val() != 0) {
                jQuery.ajax({
                    // ? url: "@Url.Action("Management_Controller_Marca_Producto_Editar", "Management")",
                    url: "https://localhost:44381/Management/Management_Controller_Marca_Producto_Editar",
                    type: "PUT",
                    data: { Obj_Class_Entity_Marca_Producto: Proveedor },
                    success: function (data) {
                        debugger; // TODO: Punto de Depuración

                        $(".modal-body").LoadingOverlay("hide");

                        if (data.result) {
                            Table_Marca_Producto.row(Selected_Row)
                                .data(Proveedor)
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
                            toastr["info"]("El Proveedor ha sido Modificado", "Informaci\xf3n:");
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