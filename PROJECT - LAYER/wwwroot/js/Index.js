var Table_Deadline_Report;
var Table_Transaction_Report;

$(document).ready(function () {
  var Close_Bootstrap_Button = $.fn.button.noConflict(); // Return "$.fn.button" to Previously Assigned Value
  $.fn.bootstrapBtn = Close_Bootstrap_Button; // Give "$().bootstrapBtn" the Bootstrap Functionality

  $("#Initial_Fecha_Movimiento_Inventario")
    .datepicker({ dateFormat: "dd/mm/yy" })
    .datepicker("setDate", new Date());
  $("#Final_Fecha_Movimiento_Inventario")
    .datepicker({ dateFormat: "dd/mm/yy" })
    .datepicker("setDate", new Date());

  jQuery.ajax({
    // ? url: "@Url.Action("Home_Controller_Dashboard_Tip_Report", "Home")",
    url: "https://localhost:44381/Home/Home_Controller_Dashboard_Tip_Report",
    type: "GET",
    dataType: "json",
    contentType: "application/json; charset=UTF-8",
    success: function (data) {
      var Object = data.obj_Class_Entity_Dashboard;
      $("#Tabla_Movimiento_Inventario").text(
        Object.tabla_Movimiento_Inventario
      );
      $("#Tabla_Categoria_Insumo").text(Object.tabla_Categoria_Insumo);
      $("#Tabla_Proveedor_Insumo").text(Object.tabla_Proveedor_Insumo);
      $("#Tabla_Insumo").text(Object.tabla_Insumo);
    },
  });

  jQuery.ajax({
    // ? url: "@Url.Action("Home_Controller_Dashboard_Chart_01", "Home")",
    url: "https://localhost:44381/Home/Home_Controller_Dashboard_Chart_01",
    type: "GET",
    dataType: "json",
    contentType: "application/json; charset=UTF-8",
    success: function (data) {
      var Array_01 = [];
      var Array_02 = [];

      for (var i = 0; i < data.obj_List_Class_Entity_Dashboard.length; i++) {
        Array_01.push(
          data.obj_List_Class_Entity_Dashboard[i].income_Month_Name
        );
        Array_02.push(data.obj_List_Class_Entity_Dashboard[i].income_Number);
      }

      var Chart_01 = document.getElementById("Chart_01");

      var Chart_01_Alternative = new Chart(Chart_01, {
        type: "bar",
        data: {
          labels: Array_01,
          datasets: [
            {
              label: "N\xb0 Transacciones de Entrada",
              backgroundColor: "rgb(75, 115, 225)",
              data: Array_02,
            },
          ],
        },
        options: {
          scales: {
            y: {
              beginAtZero: true,
            },
            y: {
              beginAtZero: true,
            },
          },
        },
      });
    },
    error: function (xhr, status, error) {
      alert(xhr.responseText);
    },
  });

  jQuery.ajax({
    // ? url: "@Url.Action("Home_Controller_Dashboard_Chart_02", "Home")",
    url: "https://localhost:44381/Home/Home_Controller_Dashboard_Chart_02",
    type: "GET",
    dataType: "json",
    contentType: "application/json; charset=UTF-8",
    success: function (data) {
      var Array_01 = [];
      var Array_02 = [];

      for (var i = 0; i < data.obj_List_Class_Entity_Dashboard.length; i++) {
        Array_01.push(data.obj_List_Class_Entity_Dashboard[i].exit_Month_Name);
        Array_02.push(data.obj_List_Class_Entity_Dashboard[i].exit_Number);
      }

      var Chart_02 = document.getElementById("Chart_02");

      var Chart_02_Alternative = new Chart(Chart_02, {
        type: "bar",
        data: {
          labels: Array_01,
          datasets: [
            {
              label: "N\xb0 Transacciones de Salida",
              backgroundColor: "rgb(230, 75, 60)",
              data: Array_02,
            },
          ],
        },
        options: {
          scales: {
            y: {
              beginAtZero: true,
            },
            y: {
              beginAtZero: true,
            },
          },
        },
      });
    },
    error: function (xhr, status, error) {
      alert(xhr.responseText);
    },
  });

  jQuery.ajax({
    // ? url: "@Url.Action("Home_Controller_Dashboard_Chart_03", "Home")",
    url: "https://localhost:44381/Home/Home_Controller_Dashboard_Chart_03",
    type: "GET",
    dataType: "json",
    contentType: "application/json; charset=UTF-8",
    success: function (data) {
      var Array_01 = [];
      var Array_02 = [];

      for (var i = 0; i < data.obj_List_Class_Entity_Dashboard.length; i++) {
        Array_01.push(
          data.obj_List_Class_Entity_Dashboard[i].nombre_Categoria_Insumo
        );
        Array_02.push(data.obj_List_Class_Entity_Dashboard[i].total_Stock);
      }

      var Chart_03 = document.getElementById("Chart_03");

      var Chart_03_Alternative = new Chart(Chart_03, {
        type: "pie",
        data: {
          labels: Array_01,
          datasets: [
            {
              data: Array_02,
              backgroundColor: [
                "rgb(255, 255, 205)",
                "rgb(255, 205, 155)",
                "rgb(255, 205, 205)",
                "rgb(255, 155, 205)",
                "rgb(255, 205, 255)",
                "rgb(205, 155, 255)",
                "rgb(205, 205, 255)",
                "rgb(155, 205, 255)",
                "rgb(205, 255, 255)",
                "rgb(155, 255, 205)",
                "rgb(205, 255, 205)",
                "rgb(205, 255, 155)",
              ],
            },
          ],
        },
      });
    },
    error: function (xhr, status, error) {
      alert(xhr.responseText);
    },
  });

  jQuery.ajax({
    // ? url: "@Url.Action("Home_Controller_Dashboard_Chart_04", "Home")",
    url: "https://localhost:44381/Home/Home_Controller_Dashboard_Chart_04",
    type: "GET",
    dataType: "json",
    contentType: "application/json; charset=UTF-8",
    success: function (data) {
      var Array_01 = [];
      var Array_02 = [];

      for (var i = 0; i < data.obj_List_Class_Entity_Dashboard.length; i++) {
        Array_01.push(
          data.obj_List_Class_Entity_Dashboard[i].nombre_Proveedor_01
        );
        Array_02.push(data.obj_List_Class_Entity_Dashboard[i].stock_01);
      }

      var Chart_04 = document.getElementById("Chart_04");

      var Chart_04_Alternative = new Chart(Chart_04, {
        type: "pie",
        data: {
          labels: Array_01,
          datasets: [
            {
              data: Array_02,
              backgroundColor: [
                "rgb(255, 255, 205)",
                "rgb(255, 205, 155)",
                "rgb(255, 205, 205)",
              ],
            },
          ],
        },
      });
    },
    error: function (xhr, status, error) {
      alert(xhr.responseText);
    },
  });

  jQuery.ajax({
    // ? url: "@Url.Action("Home_Controller_Dashboard_Chart_05", "Home")",
    url: "https://localhost:44381/Home/Home_Controller_Dashboard_Chart_05",
    type: "GET",
    dataType: "json",
    contentType: "application/json; charset=UTF-8",
    success: function (data) {
      var Array_01 = [];
      var Array_02 = [];

      for (var i = 0; i < data.obj_List_Class_Entity_Dashboard.length; i++) {
        Array_01.push(
          data.obj_List_Class_Entity_Dashboard[i].nombre_Proveedor_02
        );
        Array_02.push(data.obj_List_Class_Entity_Dashboard[i].stock_02);
      }

      var Chart_05 = document.getElementById("Chart_05");

      var Chart_05_Alternative = new Chart(Chart_05, {
        type: "pie",
        data: {
          labels: Array_01,
          datasets: [
            {
              data: Array_02,
              backgroundColor: ["rgb(255, 155, 205)", "rgb(255, 205, 255)"],
            },
          ],
        },
      });
    },
    error: function (xhr, status, error) {
      alert(xhr.responseText);
    },
  });

  jQuery.ajax({
    // ? url: "@Url.Action("Home_Controller_Dashboard_Chart_06", "Home")",
    url: "https://localhost:44381/Home/Home_Controller_Dashboard_Chart_06",
    type: "GET",
    dataType: "json",
    contentType: "application/json; charset=UTF-8",
    success: function (data) {
      var Array_01 = [];
      var Array_02 = [];

      for (var i = 0; i < data.obj_List_Class_Entity_Dashboard.length; i++) {
        Array_01.push(
          data.obj_List_Class_Entity_Dashboard[i].nombre_Proveedor_03
        );
        Array_02.push(data.obj_List_Class_Entity_Dashboard[i].stock_03);
      }

      var Chart_06 = document.getElementById("Chart_06");

      var Chart_06_Alternative = new Chart(Chart_06, {
        type: "pie",
        data: {
          labels: Array_01,
          datasets: [
            {
              data: Array_02,
              backgroundColor: [
                "rgb(205, 155, 255)",
                "rgb(205, 205, 255)",
                "rgb(155, 205, 255)",
              ],
            },
          ],
        },
      });
    },
    error: function (xhr, status, error) {
      alert(xhr.responseText);
    },
  });

  // ? var Url_01 = "@Url.Action("Home_Controller_Dashboard_Deadline_Report", "Home")";
  var Url_01 =
    "https://localhost:44381/Home/Home_Controller_Dashboard_Deadline_Report";

  Table_Deadline_Report = $("#Table_Deadline_Report").DataTable({
    responsive: {
      details: {
        display: DataTable.Responsive.display.modal({
          header: function (row) {
            var data = row.data();
            return "Insumo: " + data.nombre_Insumo_01;
          },
        }),
        renderer: DataTable.Responsive.renderer.tableAll(),
      },
    },
    ordering: true,
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
      { data: "nombre_Categoria_Insumo_01" },
      { data: "nombre_Proveedor_Insumo_01" },
      { data: "nombre_Insumo_01" },
      { data: "descripcion_Insumo_01" },
      { data: "unidad_Medida_Insumo" },
      { data: "precio_Insumo_01" },
      { data: "stock_Insumo" },
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
      { data: "fecha_Vencimiento_Insumo" },
      {
        data: null,
        render: function (data, type, row) {
          return (
            '<img style="width: 90px; height: 90px;" src="../Supply_Images/' +
            row.nombre_Imagen_Insumo +
            '" alt="Image_Error" class="border rounded img-fluid">'
          );
        },
      },
      { data: "deadline" },
      {
        defaultContent:
          '<button type="button" class="btn btn-danger btn-sm ms-2 Delete_Button"><i class="fa-solid fa-trash"></i></button>',
        orderable: false,
        searchable: false,
        width: "90px",
      },
    ],
  });

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

  $(document).on("click", ".Delete_Button", function () {
    Selected_Row = Selected_Row_Function(this);
    var data = Table_Deadline_Report.row(Selected_Row).data();
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
          // ? url: "@Url.Action("Home_Controller_Dashboard_Deadline_Report_Delete", "Home")",
          url: "https://localhost:44381/Home/Home_Controller_Dashboard_Deadline_Report_Delete",
          type: "POST",
          data: { ID_Insumo: data.iD_Insumo },
          success: function (data) {
            debugger; // TODO: Punto de Depuración

            if (data.result) {
              Swal.fire({
                title: "Correcto",
                text: "El Insumo ha sido Eliminado",
                icon: "success",
              });
              Table_Deadline_Report.row(Selected_Row).remove().draw();
              $(".ui-dialog-content").dialog("close");
            } else {
              Swal.fire({
                title: "Error",
                text: "Error",
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

  // ? var Url_02 = "@Url.Action("Home_Controller_Dashboard_Transaction_Report", "Home")",
  var Url_02 =
    "https://localhost:44381/Home/Home_Controller_Dashboard_Transaction_Report" +
    "?Initial_Fecha_Movimiento_Inventario=" +
    $("#Initial_Fecha_Movimiento_Inventario").val() +
    "&Final_Fecha_Movimiento_Inventario=" +
    $("#Final_Fecha_Movimiento_Inventario").val() +
    "&ID_Movimiento_Inventario=" +
    $("#ID_Movimiento_Inventario").val();

  Table_Transaction_Report = $("#Table_Transaction_Report").DataTable({
    searching: false,
    responsive: {
      details: {
        display: DataTable.Responsive.display.modal({
          header: function (row) {
            var data = row.data();
            return "Insumo: " + data.nombre_Insumo_02;
          },
        }),
        renderer: DataTable.Responsive.renderer.tableAll(),
      },
    },
    ordering: true,
    language: {
      url: "//cdn.datatables.net/plug-ins/2.1.8/i18n/es-MX.json",
    },
    ajax: {
      url: Url_02,
      type: "POST",
      dataType: "json",
    },
    columns: [
      { data: "iD_Movimiento_Inventario" },
      { data: "tipo_Movimiento_Inventario" },
      { data: "nombre_Categoria_Insumo_02" },
      { data: "nombre_Proveedor_Insumo_02" },
      { data: "nombre_Insumo_02" },
      { data: "descripcion_Insumo_02" },
      { data: "precio_Insumo_02" },
      { data: "cantidad_Movimiento_Inventario" },
      { data: "total_Transaction" },
      { data: "fecha_Movimiento_Inventario" },
      { data: "usuario_Transaction" },
    ],
  });

  $("#Search_Button").on("click", function () {
    // ? var New_Url_02 = "@Url.Action("Home_Controller_Dashboard_Transaction_Report", "Home")",
    var New_Url_02 =
      "https://localhost:44381/Home/Home_Controller_Dashboard_Transaction_Report" +
      "?Initial_Fecha_Movimiento_Inventario=" +
      $("#Initial_Fecha_Movimiento_Inventario").val() +
      "&Final_Fecha_Movimiento_Inventario=" +
      $("#Final_Fecha_Movimiento_Inventario").val() +
      "&ID_Movimiento_Inventario=" +
      $("#ID_Movimiento_Inventario").val();

    Table_Transaction_Report.ajax.url(New_Url_02).load();
  });
});