using System.Collections.Generic;
using DATA___LAYER;
using ENTITY___LAYER;

namespace BUSINESS___LAYER
{
    public class Class_Business_Middle
    {
        private Class_Data_Middle Obj_Class_Data_Middle = new Class_Data_Middle();

        public bool Class_Business_Middle_Listar(int ID_Usuario, int ID_Insumo)
        {
            return Obj_Class_Data_Middle.Class_Data_Middle_Listar(ID_Usuario, ID_Insumo);
        }

        public bool Class_Business_Middle_Create_Update(int ID_Usuario, int ID_Insumo, bool Boolean_Operation, out string Message)
        {
            return Obj_Class_Data_Middle.Class_Data_Middle_Create_Update(ID_Usuario, ID_Insumo, Boolean_Operation, out Message);
        }

        public List<Class_Entity_Middle> Class_Business_Middle_Listar_Alternative(int ID_Usuario)
        {
            return Obj_Class_Data_Middle.Class_Data_Middle_Listar_Alternative(ID_Usuario);
        }

        public bool Class_Business_Middle_Delete(int ID_Usuario, int ID_Insumo)
        {
            return Obj_Class_Data_Middle.Class_Data_Middle_Delete(ID_Usuario, ID_Insumo);
        }

        public int Class_Business_Middle_Count(int ID_Usuario)
        {
            return Obj_Class_Data_Middle.Class_Data_Middle_Count(ID_Usuario);
        }
    }
}