using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using ENTITY___LAYER;
using System.Globalization;

namespace DATA___LAYER
{
    public class Class_Data_Middle
    {
        public bool Class_Data_Middle_Listar(int ID_Usuario, int ID_Insumo)
        {
            bool Result = true;

            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_MIDDLE_LIST", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Usuario", ID_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Insumo", ID_Insumo);
                    Obj_SqlCommand.Parameters.Add("Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    Obj_SqlCommand.CommandType = CommandType.StoredProcedure;

                    Obj_SqlConnection.Open();

                    Obj_SqlCommand.ExecuteNonQuery();

                    Result = Convert.ToBoolean(Obj_SqlCommand.Parameters["Result"].Value);
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Result = false;
            }
            return Result;
        }

        public bool Class_Data_Middle_Create_Update(int ID_Usuario, int ID_Insumo, bool Boolean_Operation, out string Message)
        {
            bool Result = true;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_MIDDLE_CREATE_UPDATE", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Usuario", ID_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Insumo", ID_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("Boolean_Operation", Boolean_Operation);
                    Obj_SqlCommand.Parameters.Add("Message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Obj_SqlCommand.Parameters.Add("Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    Obj_SqlCommand.CommandType = CommandType.StoredProcedure;

                    Obj_SqlConnection.Open();

                    Obj_SqlCommand.ExecuteNonQuery();

                    Message = Obj_SqlCommand.Parameters["Message"].Value.ToString();
                    Result = Convert.ToBoolean(Obj_SqlCommand.Parameters["Result"].Value);
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Message = Error.Message;
                Result = false;
            }
            return Result;
        }

        public bool Class_Data_Middle_Create_Update_Alternative(int ID_Usuario, int ID_Insumo, bool Boolean_Operation, out string Message)
        {
            bool Result = true;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_MIDDLE_CREATE_UPDATE_ALTERNATIVE", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Usuario", ID_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Insumo", ID_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("Boolean_Operation", Boolean_Operation);
                    Obj_SqlCommand.Parameters.Add("Message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Obj_SqlCommand.Parameters.Add("Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    Obj_SqlCommand.CommandType = CommandType.StoredProcedure;

                    Obj_SqlConnection.Open();

                    Obj_SqlCommand.ExecuteNonQuery();

                    Message = Obj_SqlCommand.Parameters["Message"].Value.ToString();
                    Result = Convert.ToBoolean(Obj_SqlCommand.Parameters["Result"].Value);
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Message = Error.Message;
                Result = false;
            }
            return Result;
        }

        public List<Class_Entity_Middle> Class_Data_Middle_Listar_Alternative(int ID_Usuario)
        {
            List<Class_Entity_Middle> Obj_List_Class_Entity_Middle = new List<Class_Entity_Middle>();
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    string SQL_Server_Query_String = "SELECT * FROM FN_MIDDLE_LIST(@ID_Usuario)";

                    SqlCommand Obj_SqlCommand = new SqlCommand(SQL_Server_Query_String, Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                    Obj_SqlCommand.CommandType = CommandType.Text;

                    Obj_SqlConnection.Open();

                    using (SqlDataReader Obj_SqlDataReader = Obj_SqlCommand.ExecuteReader())
                    {
                        while (Obj_SqlDataReader.Read())
                        {
                            Obj_List_Class_Entity_Middle.Add(new Class_Entity_Middle()
                            {
                                Obj_Class_Entity_Insumo = new Class_Entity_Insumo()
                                {
                                    ID_Insumo = Convert.ToInt32(Obj_SqlDataReader["ID_Insumo"]),
                                    Obj_Class_Entity_Proveedor_Insumo = new Class_Entity_Proveedor_Insumo()
                                    {
                                        Nombre_Proveedor_Insumo = Obj_SqlDataReader["Nombre_Proveedor_Insumo"].ToString()
                                    },
                                    Nombre_Insumo = Obj_SqlDataReader["Nombre_Insumo"].ToString(),
                                    Precio_Insumo = Convert.ToDecimal(Obj_SqlDataReader["Precio_Insumo"], new CultureInfo("es-PE")),
                                    Ruta_Imagen_Insumo = Obj_SqlDataReader["Ruta_Imagen_Insumo"].ToString(),
                                    Nombre_Imagen_Insumo = Obj_SqlDataReader["Nombre_Imagen_Insumo"].ToString()
                                },
                                Cantidad_Insumo_Middle = Convert.ToInt32(Obj_SqlDataReader["Cantidad_Insumo_Middle"])
                            });
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Obj_List_Class_Entity_Middle = new List<Class_Entity_Middle>();
            }
            return Obj_List_Class_Entity_Middle;
        }

        public bool Class_Data_Middle_Delete(int ID_Usuario, int ID_Insumo)
        {
            bool Result = true;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_MIDDLE_DELETE", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Usuario", ID_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Insumo", ID_Insumo);
                    Obj_SqlCommand.Parameters.Add("Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    Obj_SqlCommand.CommandType = CommandType.StoredProcedure;

                    Obj_SqlConnection.Open();

                    Obj_SqlCommand.ExecuteNonQuery();

                    Result = Convert.ToBoolean(Obj_SqlCommand.Parameters["Result"].Value);
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Result = false;
            }
            return Result;
        }

        public bool Class_Data_Middle_Delete_Alternative(int ID_Usuario, int ID_Insumo)
        {
            bool Result = true;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_MIDDLE_DELETE_ALTERNATIVE", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Usuario", ID_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Insumo", ID_Insumo);
                    Obj_SqlCommand.Parameters.Add("Result", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    Obj_SqlCommand.CommandType = CommandType.StoredProcedure;

                    Obj_SqlConnection.Open();

                    Obj_SqlCommand.ExecuteNonQuery();

                    Result = Convert.ToBoolean(Obj_SqlCommand.Parameters["Result"].Value);
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Result = false;
            }
            return Result;
        }

        public int Class_Data_Middle_Count(int ID_Usuario)
        {
            int Result = 0;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SELECT COUNT(*) FROM Tabla_Middle WHERE ID_Usuario = @ID_Usuario", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                    Obj_SqlCommand.CommandType = CommandType.Text;

                    Obj_SqlConnection.Open();

                    Result = Convert.ToInt32(Obj_SqlCommand.ExecuteScalar());
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Result = 0;
            }
            return Result;
        }
    }
}