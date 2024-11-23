using DATA___LAYER;
using ENTITY___LAYER;
using System.Data;

namespace BUSINESS___LAYER
{
    public class Class_Business_Movimiento_Inventario
    {
        private Class_Data_Movimiento_Inventario Obj_Class_Data_Movimiento_Inventario = new Class_Data_Movimiento_Inventario();

        public bool Class_Business_Venta_Registrar(Class_Entity_Movimiento_Inventario Obj_Class_Entity_Movimiento_Inventario, DataTable Obj_DataTable, out string Message)
        {
            return Obj_Class_Data_Movimiento_Inventario.Class_Data_Movimiento_Inventario_Registrar(Obj_Class_Entity_Movimiento_Inventario, Obj_DataTable, out Message);
        }
    }
}