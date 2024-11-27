namespace ENTITY___LAYER
{
    public class Class_Entity_Dashboard
    {
        /*SP_TIP_REPORT*/
        public int Tabla_Movimiento_Inventario { get; set; }
        public int Tabla_Categoria_Insumo { get; set; }
        public int Tabla_Proveedor_Insumo { get; set; }
        public int Tabla_Insumo { get; set; }

        /*SP_CHART_01*/
        public int Income_Year { get; set; }
        public int Income_Month { get; set; }
        public string Income_Month_Name { get; set; }
        public decimal Income_Sum { get; set; }
        public int Income_Number { get; set; }

        /*SP_CHART_02*/
        public int Exit_Year { get; set; }
        public int Exit_Month { get; set; }
        public string Exit_Month_Name { get; set; }
        public decimal Exit_Sum { get; set; }
        public int Exit_Number { get; set; }

        /*SP_CHART_03*/
        public string Nombre_Categoria_Insumo { get; set; }
        public int Total_Stock { get; set; }

        /*SP_CHART_04*/
        public string Nombre_Proveedor_01 { get; set; }
        public int Stock_01 { get; set; }

        /*SP_CHART_05*/
        public string Nombre_Proveedor_02 { get; set; }
        public int Stock_02 { get; set; }

        /*SP_CHART_06*/
        public string Nombre_Proveedor_03 { get; set; }
        public int Stock_03 { get; set; }

        /*SP_DEADLINE_REPORT*/
        public int ID_Insumo { get; set; }
        public string Nombre_Categoria_Insumo_01 { get; set; }
        public string Nombre_Proveedor_Insumo_01 { get; set; }
        public string Nombre_Insumo_01 { get; set; }
        public string Descripcion_Insumo_01 { get; set; }
        public string Unidad_Medida_Insumo { get; set; }
        public decimal Precio_Insumo_01 { get; set; }
        public int Stock_Insumo { get; set; }
        public bool Estado_Insumo { get; set; }
        public string Fecha_Vencimiento_Insumo { get; set; }
        public string Ruta_Imagen_Insumo { get; set; }
        public string Nombre_Imagen_Insumo { get; set; }
        public int Deadline { get; set; }

        /*SP_TRANSACTION_REPORT*/
        public int ID_Movimiento_Inventario { get; set; }
        public string Tipo_Movimiento_Inventario { get; set; }
        public string Nombre_Categoria_Insumo_02 { get; set; }
        public string Nombre_Proveedor_Insumo_02 { get; set; }
        public string Nombre_Insumo_02 { get; set; }
        public string Descripcion_Insumo_02 { get; set; }
        public decimal Precio_Insumo_02 { get; set; }
        public int Cantidad_Movimiento_Inventario { get; set; }
        public decimal Total_Transaction { get; set; }
        public string Fecha_Movimiento_Inventario { get; set; }
        public string Usuario_Transaction { get; set; }
    }
}