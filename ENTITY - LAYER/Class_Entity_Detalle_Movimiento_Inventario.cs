namespace ENTITY___LAYER
{
    public class Class_Entity_Detalle_Movimiento_Inventario
    {
        public int ID_Detalle_Movimiento_Inventario { get; set; }
        public Class_Entity_Movimiento_Inventario Obj_Class_Entity_Movimiento_Inventario { get; set; }
        public Class_Entity_Insumo Obj_Class_Entity_Insumo { get; set; }
        public int Cantidad_Insumo_Detalle_Movimiento_Inventario { get; set; }
        public decimal Monto_Total_Detalle_Movimiento_Inventario { get; set; }
    }
}