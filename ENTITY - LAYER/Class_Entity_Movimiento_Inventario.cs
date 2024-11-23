namespace ENTITY___LAYER
{
    public class Class_Entity_Movimiento_Inventario
    {
        public int ID_Movimiento_Inventario { get; set; }
        public Class_Entity_Usuario Obj_Class_Entity_Usuario { get; set; }
        public string Tipo_Movimiento_Inventario { get; set; }
        public int Cantidad_Insumo_Movimiento_Inventario { get; set; }
        public decimal Monto_Total_Movimiento_Inventario { get; set; }
        public string Restaurante_Movimiento_Inventario { get; set; }
        public int Telefono_Movimiento_Inventario { get; set; }
        public string Direccion_Movimiento_Inventario { get; set; }
        public Class_Entity_Distrito Obj_Class_Entity_Distrito { get; set; }
        public string Fecha_Movimiento_Inventario { get; set; }
    }
}