using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using ENTITY___LAYER;

namespace DATA___LAYER
{
    public class Class_Data_Ubication
    {
        public List<Class_Entity_Departamento> Class_Data_Ubication_Departamento_Listar()
        {
            List<Class_Entity_Departamento> Obj_List_Class_Entity_Departamento = new List<Class_Entity_Departamento>();
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    string SQL_Server_Query_String = "SELECT * FROM Tabla_Departamento";

                    SqlCommand Obj_SqlCommand = new SqlCommand(SQL_Server_Query_String, Obj_SqlConnection);
                    Obj_SqlCommand.CommandType = CommandType.Text;

                    Obj_SqlConnection.Open();

                    using (SqlDataReader Obj_SqlDataReader = Obj_SqlCommand.ExecuteReader())
                    {
                        while (Obj_SqlDataReader.Read())
                        {
                            Obj_List_Class_Entity_Departamento.Add(new Class_Entity_Departamento()
                            {
                                ID_Departamento = Convert.ToInt32(Obj_SqlDataReader["ID_Departamento"]),
                                Nombre_Departamento = Obj_SqlDataReader["Nombre_Departamento"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Obj_List_Class_Entity_Departamento = new List<Class_Entity_Departamento>();
            }
            return Obj_List_Class_Entity_Departamento;
        }

        public List<Class_Entity_Provincia> Class_Data_Ubication_Provincia_Listar(int ID_Departamento)
        {
            List<Class_Entity_Provincia> Obj_List_Class_Entity_Provincia = new List<Class_Entity_Provincia>();
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    string SQL_Server_Query_String = "SELECT * FROM Tabla_Provincia WHERE ID_Departamento = @ID_Departamento";

                    SqlCommand Obj_SqlCommand = new SqlCommand(SQL_Server_Query_String, Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("@ID_Departamento", ID_Departamento);
                    Obj_SqlCommand.CommandType = CommandType.Text;

                    Obj_SqlConnection.Open();

                    using (SqlDataReader Obj_SqlDataReader = Obj_SqlCommand.ExecuteReader())
                    {
                        while (Obj_SqlDataReader.Read())
                        {
                            Obj_List_Class_Entity_Provincia.Add(new Class_Entity_Provincia()
                            {
                                ID_Provincia = Convert.ToInt32(Obj_SqlDataReader["ID_Provincia"]),
                                Nombre_Provincia = Obj_SqlDataReader["Nombre_Provincia"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Obj_List_Class_Entity_Provincia = new List<Class_Entity_Provincia>();
            }
            return Obj_List_Class_Entity_Provincia;
        }

        public List<Class_Entity_Distrito> Class_Data_Ubication_Distrito_Listar(int ID_Provincia, int ID_Departamento)
        {
            List<Class_Entity_Distrito> Obj_List_Class_Entity_Distrito = new List<Class_Entity_Distrito>();
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    string SQL_Server_Query_String = "SELECT * FROM Tabla_Distrito WHERE ID_Provincia = @ID_Provincia AND ID_Departamento = @ID_Departamento";

                    SqlCommand Obj_SqlCommand = new SqlCommand(SQL_Server_Query_String, Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("@ID_Provincia", ID_Provincia);
                    Obj_SqlCommand.Parameters.AddWithValue("@ID_Departamento", ID_Departamento);
                    Obj_SqlCommand.CommandType = CommandType.Text;

                    Obj_SqlConnection.Open();

                    using (SqlDataReader Obj_SqlDataReader = Obj_SqlCommand.ExecuteReader())
                    {
                        while (Obj_SqlDataReader.Read())
                        {
                            Obj_List_Class_Entity_Distrito.Add(new Class_Entity_Distrito()
                            {
                                ID_Distrito = Convert.ToInt32(Obj_SqlDataReader["ID_Distrito"]),
                                Nombre_Distrito = Obj_SqlDataReader["Nombre_Distrito"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Obj_List_Class_Entity_Distrito = new List<Class_Entity_Distrito>();
            }
            return Obj_List_Class_Entity_Distrito;
        }
    }
}