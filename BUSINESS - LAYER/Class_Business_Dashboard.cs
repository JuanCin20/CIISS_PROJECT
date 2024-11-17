using System.Collections.Generic;
using DATA___LAYER;
using ENTITY___LAYER;

namespace BUSINESS___LAYER
{
    public class Class_Business_Dashboard
    {
        private Class_Data_Dashboard Obj_Class_Data_Dashboard = new Class_Data_Dashboard();

        public Class_Entity_Dashboard Class_Business_Dashboard_Tip_Report()
        {
            return Obj_Class_Data_Dashboard.Class_Data_Dashboard_Tip_Report();
        }

        public List<Class_Entity_Dashboard> Class_Business_Dashboard_Chart_01()
        {
            return Obj_Class_Data_Dashboard.Class_Data_Dashboard_Chart_01();
        }

        public List<Class_Entity_Dashboard> Class_Business_Dashboard_Chart_02()
        {
            return Obj_Class_Data_Dashboard.Class_Data_Dashboard_Chart_02();
        }

        public List<Class_Entity_Dashboard> Class_Business_Dashboard_Chart_03()
        {
            return Obj_Class_Data_Dashboard.Class_Data_Dashboard_Chart_03();
        }

        public List<Class_Entity_Dashboard> Class_Business_Dashboard_Chart_04()
        {
            return Obj_Class_Data_Dashboard.Class_Data_Dashboard_Chart_04();
        }

        public List<Class_Entity_Dashboard> Class_Business_Dashboard_Chart_05()
        {
            return Obj_Class_Data_Dashboard.Class_Data_Dashboard_Chart_05();
        }

        public List<Class_Entity_Dashboard> Class_Business_Dashboard_Chart_06()
        {
            return Obj_Class_Data_Dashboard.Class_Data_Dashboard_Chart_06();
        }

        public List<Class_Entity_Dashboard> Class_Business_Dashboard_Deadline_Report()
        {
            return Obj_Class_Data_Dashboard.Class_Data_Dashboard_Deadline_Report();
        }

        public bool Class_Business_Dashboard_Deadline_Report_Delete(int ID_Insumo)
        {
            return Obj_Class_Data_Dashboard.Class_Data_Dashboard_Deadline_Report_Delete(ID_Insumo);
        }

        public List<Class_Entity_Dashboard> Class_Business_Dashboard_Transaction_Report(string Initial_Fecha_Movimiento_Inventario, string Final_Fecha_Movimiento_Inventario, int ID_Movimiento_Inventario)
        {
            return Obj_Class_Data_Dashboard.Class_Data_Dashboard_Transaction_Report(Initial_Fecha_Movimiento_Inventario, Final_Fecha_Movimiento_Inventario, ID_Movimiento_Inventario);
        }
    }
}