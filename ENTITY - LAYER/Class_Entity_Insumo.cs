namespace ENTITY___LAYER
{
    public class Class_Entity_Insumo
    {
        public int ID_Insumo { get; set; }
        public Class_Entity_Categoria_Insumo Obj_Class_Entity_Categoria_Insumo { get; set; }
        public Class_Entity_Proveedor_Insumo Obj_Class_Entity_Proveedor_Insumo { get; set; }
        public string Nombre_Insumo { get; set; }
        public string Descripcion_Insumo { get; set; }
        public string Unidad_Medida_Insumo { get; set; }
        public decimal Precio_Insumo { get; set; }
        public string Precio_Insumo_String { get; set; } // ?
        public int Stock_Insumo { get; set; }
        public bool Estado_Insumo { get; set; }
        public string Fecha_Registro_Insumo { get; set; }
        public string Fecha_Vencimiento_Insumo { get; set; }
        public string Ruta_Imagen_Insumo { get; set; }
        public string Nombre_Imagen_Insumo { get; set; }
        public string Base_64_Imagen_Insumo { get; set; } // ?
        public string Extension_Imagen_Insumo { get; set; } // ?
        public int Deadline { get; set; } // ?
    }
}