namespace ENTITY___LAYER
{
    public class Class_Entity_Movimiento_Inventario
    {
        public int ID_Movimiento_Inventario { get; set; }
        public Class_Entity_Usuario Obj_Class_Entity_Usuario { get; set; }
        public string Tipo_Movimiento_Inventario { get; set; }
        public Class_Entity_Insumo Obj_Class_Entity_Insumo { get; set; }
        public int Cantidad_Movimiento_Inventario { get; set; }
        public string Fecha_Movimiento_Inventario { get; set; }
    }
}