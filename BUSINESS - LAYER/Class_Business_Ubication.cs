using System.Collections.Generic;
using DATA___LAYER;
using ENTITY___LAYER;

namespace BUSINESS___LAYER
{
    public class Class_Business_Ubication
    {
        private Class_Data_Ubication Obj_Class_Data_Ubication = new Class_Data_Ubication();

        public List<Class_Entity_Departamento> Class_Business_Ubication_Departamento_Listar()
        {
            return Obj_Class_Data_Ubication.Class_Data_Ubication_Departamento_Listar();
        }

        public List<Class_Entity_Provincia> Class_Business_Ubication_Provincia_Listar(int ID_Departamento)
        {
            return Obj_Class_Data_Ubication.Class_Data_Ubication_Provincia_Listar(ID_Departamento);
        }

        public List<Class_Entity_Distrito> Class_Business_Ubication_Distrito_Listar(int ID_Provincia, int ID_Departamento)
        {
            return Obj_Class_Data_Ubication.Class_Data_Ubication_Distrito_Listar(ID_Provincia, ID_Departamento);
        }
    }
}