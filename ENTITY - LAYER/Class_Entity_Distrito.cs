namespace ENTITY___LAYER
{
    public class Class_Entity_Distrito
    {
        public int ID_Distrito { get; set; }
        public string Nombre_Distrito { get; set; }
        public Class_Entity_Departamento Obj_Class_Entity_Departamento { get; set; }
        public Class_Entity_Provincia Obj_Class_Entity_Provincia { get; set; }
    }
}