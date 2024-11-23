namespace ENTITY___LAYER
{
    public class Class_Entity_Provincia
    {
        public int ID_Provincia { get; set; }
        public string Nombre_Provincia { get; set; }
        public Class_Entity_Departamento Obj_Class_Entity_Departamento { get; set; }
    }
}