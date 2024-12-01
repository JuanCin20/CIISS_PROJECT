using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using ENTITY___LAYER;
using System.Globalization;

namespace DATA___LAYER
{
    public class Class_Data_Insumo
    {
        public List<Class_Entity_Insumo> Class_Data_Insumo_Listar(bool Estado_Insumo)
        {
            List<Class_Entity_Insumo> Obj_List_Class_Entity_Insumo = new List<Class_Entity_Insumo>();
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_SUPPLY_LIST", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("Estado_Insumo", Estado_Insumo);
                    Obj_SqlCommand.CommandType = CommandType.StoredProcedure;

                    Obj_SqlConnection.Open();

                    using (SqlDataReader Obj_SqlDataReader = Obj_SqlCommand.ExecuteReader())
                    {
                        while (Obj_SqlDataReader.Read())
                        {
                            Obj_List_Class_Entity_Insumo.Add(new Class_Entity_Insumo()
                            {
                                ID_Insumo = Convert.ToInt32(Obj_SqlDataReader["ID_Insumo"]),
                                Obj_Class_Entity_Categoria_Insumo = new Class_Entity_Categoria_Insumo()
                                {
                                    ID_Categoria_Insumo = Convert.ToInt32(Obj_SqlDataReader["ID_Categoria_Insumo"]),
                                    Nombre_Categoria_Insumo = Obj_SqlDataReader["Nombre_Categoria_Insumo"].ToString()

                                },
                                Obj_Class_Entity_Proveedor_Insumo = new Class_Entity_Proveedor_Insumo()
                                {
                                    ID_Proveedor_Insumo = Convert.ToInt32(Obj_SqlDataReader["ID_Proveedor_Insumo"]),
                                    Nombre_Proveedor_Insumo = Obj_SqlDataReader["Nombre_Proveedor_Insumo"].ToString()

                                },
                                Nombre_Insumo = Obj_SqlDataReader["Nombre_Insumo"].ToString(),
                                Descripcion_Insumo = Obj_SqlDataReader["Descripcion_Insumo"].ToString(),
                                Unidad_Medida_Insumo = Obj_SqlDataReader["Unidad_Medida_Insumo"].ToString(),
                                Precio_Insumo = Convert.ToDecimal(Obj_SqlDataReader["Precio_Insumo"], new CultureInfo("es-PE")),
                                Stock_Insumo = Convert.ToInt32(Obj_SqlDataReader["Stock_Insumo"]),
                                Estado_Insumo = Convert.ToBoolean(Obj_SqlDataReader["Estado_Insumo"]),
                                Fecha_Registro_Insumo = Obj_SqlDataReader["Fecha_Registro_Insumo"].ToString(),
                                Fecha_Vencimiento_Insumo = Obj_SqlDataReader["Fecha_Vencimiento_Insumo"].ToString(),
                                Ruta_Imagen_Insumo = Obj_SqlDataReader["Ruta_Imagen_Insumo"].ToString(),
                                Nombre_Imagen_Insumo = Obj_SqlDataReader["Nombre_Imagen_Insumo"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Obj_List_Class_Entity_Insumo = new List<Class_Entity_Insumo>();
            }
            return Obj_List_Class_Entity_Insumo;
        }

        public List<Class_Entity_Categoria_Insumo> Class_Data_Insumo_Categoria_Insumo_Listar()
        {
            List<Class_Entity_Categoria_Insumo> Obj_List_Class_Entity_Categoria_Insumo = new List<Class_Entity_Categoria_Insumo>();
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    string SQL_Server_Query_String = "SELECT * FROM Tabla_Categoria_Insumo;";

                    SqlCommand Obj_SqlCommand = new SqlCommand(SQL_Server_Query_String, Obj_SqlConnection);
                    Obj_SqlCommand.CommandType = CommandType.Text;

                    Obj_SqlConnection.Open();

                    using (SqlDataReader Obj_SqlDataReader = Obj_SqlCommand.ExecuteReader())
                    {
                        while (Obj_SqlDataReader.Read())
                        {
                            Obj_List_Class_Entity_Categoria_Insumo.Add(new Class_Entity_Categoria_Insumo()
                            {
                                ID_Categoria_Insumo = Convert.ToInt32(Obj_SqlDataReader["ID_Categoria_Insumo"]),
                                Nombre_Categoria_Insumo = Obj_SqlDataReader["Nombre_Categoria_Insumo"].ToString(),
                                Descripcion_Categoria_Insumo = Obj_SqlDataReader["Descripcion_Categoria_Insumo"].ToString(),
                                Estado_Categoria_Insumo = Convert.ToBoolean(Obj_SqlDataReader["Estado_Categoria_Insumo"]),
                                Fecha_Registro_Categoria_Insumo = Obj_SqlDataReader["Fecha_Registro_Categoria_Insumo"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Obj_List_Class_Entity_Categoria_Insumo = new List<Class_Entity_Categoria_Insumo>();
            }
            return Obj_List_Class_Entity_Categoria_Insumo;
        }

        public List<Class_Entity_Proveedor_Insumo> Class_Data_Insumo_Proveedor_Insumo_Listar()
        {
            List<Class_Entity_Proveedor_Insumo> Obj_List_Class_Entity_Proveedor_Insumo = new List<Class_Entity_Proveedor_Insumo>();
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    string SQL_Server_Query_String = "SELECT * FROM Tabla_Proveedor_Insumo;";

                    SqlCommand Obj_SqlCommand = new SqlCommand(SQL_Server_Query_String, Obj_SqlConnection);
                    Obj_SqlCommand.CommandType = CommandType.Text;

                    Obj_SqlConnection.Open();

                    using (SqlDataReader Obj_SqlDataReader = Obj_SqlCommand.ExecuteReader())
                    {
                        while (Obj_SqlDataReader.Read())
                        {
                            Obj_List_Class_Entity_Proveedor_Insumo.Add(new Class_Entity_Proveedor_Insumo()
                            {
                                ID_Proveedor_Insumo = Convert.ToInt32(Obj_SqlDataReader["ID_Proveedor_Insumo"]),
                                Nombre_Proveedor_Insumo = Obj_SqlDataReader["Nombre_Proveedor_Insumo"].ToString(),
                                Telefono_Proveedor_Insumo = Convert.ToInt32(Obj_SqlDataReader["Telefono_Proveedor_Insumo"]),
                                E_Mail_Proveedor_Insumo = Obj_SqlDataReader["E_Mail_Proveedor_Insumo"].ToString(),
                                Direccion_Proveedor_Insumo = Obj_SqlDataReader["Direccion_Proveedor_Insumo"].ToString(),
                                Estado_Proveedor_Insumo = Convert.ToBoolean(Obj_SqlDataReader["Estado_Proveedor_Insumo"]),
                                Fecha_Registro_Proveedor_Insumo = Obj_SqlDataReader["Fecha_Registro_Proveedor_Insumo"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Obj_List_Class_Entity_Proveedor_Insumo = new List<Class_Entity_Proveedor_Insumo>();
            }
            return Obj_List_Class_Entity_Proveedor_Insumo;
        }

        public int Class_Data_Insumo_Registrar(Class_Entity_Insumo Obj_Class_Entity_Insumo, out string Message)
        {
            int ID_Auto_Generated = 0;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_SUPPLY_CREATE", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Categoria_Insumo", Obj_Class_Entity_Insumo.Obj_Class_Entity_Categoria_Insumo.ID_Categoria_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Proveedor_Insumo", Obj_Class_Entity_Insumo.Obj_Class_Entity_Proveedor_Insumo.ID_Proveedor_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("Nombre_Insumo", Obj_Class_Entity_Insumo.Nombre_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("Descripcion_Insumo", Obj_Class_Entity_Insumo.Descripcion_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("Unidad_Medida_Insumo", Obj_Class_Entity_Insumo.Unidad_Medida_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("Precio_Insumo", Obj_Class_Entity_Insumo.Precio_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("Stock_Insumo", Obj_Class_Entity_Insumo.Stock_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("Fecha_Vencimiento_Insumo", Obj_Class_Entity_Insumo.Fecha_Vencimiento_Insumo);
                    Obj_SqlCommand.Parameters.Add("Message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Obj_SqlCommand.Parameters.Add("Result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    Obj_SqlCommand.CommandType = CommandType.StoredProcedure;

                    Obj_SqlConnection.Open();

                    Obj_SqlCommand.ExecuteNonQuery();

                    Message = Obj_SqlCommand.Parameters["Message"].Value.ToString();
                    ID_Auto_Generated = Convert.ToInt32(Obj_SqlCommand.Parameters["Result"].Value);
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Message = Error.Message;
                ID_Auto_Generated = 0;
            }
            return ID_Auto_Generated;
        }

        public bool Class_Data_Insumo_Editar(Class_Entity_Insumo Obj_Class_Entity_Insumo, out string Message)
        {
            bool Result = false;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_SUPPLY_UPDATE", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Insumo", Obj_Class_Entity_Insumo.ID_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("Descripcion_Insumo", Obj_Class_Entity_Insumo.Descripcion_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("Precio_Insumo", Obj_Class_Entity_Insumo.Precio_Insumo);
                    Obj_SqlCommand.Parameters.Add("Message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Obj_SqlCommand.Parameters.Add("Result", SqlDbType.Int).Direction = ParameterDirection.Output;
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

        public bool Class_Data_Insumo_Save_Image(Class_Entity_Insumo Obj_Class_Entity_Insumo, out string Message)
        {
            bool Result = false;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    string SQL_Server_Query_String = "UPDATE Tabla_Insumo SET Ruta_Imagen_Insumo = @Ruta_Imagen_Insumo, Nombre_Imagen_Insumo = @Nombre_Imagen_Insumo WHERE ID_Insumo = @ID_Insumo;";

                    SqlCommand Obj_SqlCommand = new SqlCommand(SQL_Server_Query_String, Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("@Ruta_Imagen_Insumo", Obj_Class_Entity_Insumo.Ruta_Imagen_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("@Nombre_Imagen_Insumo", Obj_Class_Entity_Insumo.Nombre_Imagen_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("@ID_Insumo", Obj_Class_Entity_Insumo.ID_Insumo);
                    Obj_SqlCommand.CommandType = CommandType.Text;

                    Obj_SqlConnection.Open();

                    Result = Obj_SqlCommand.ExecuteNonQuery() > 0 ? true : false;
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

        public bool Class_Data_Insumo_Reset(int ID_Insumo, string Fecha_Vencimiento_Insumo, out string Message)
        {
            bool Result = false;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    string SQL_Server_Query_String = "UPDATE Tabla_Insumo SET Estado_Insumo = 1, Fecha_Vencimiento_Insumo = @Fecha_Vencimiento_Insumo WHERE ID_Insumo = @ID_Insumo;";

                    SqlCommand Obj_SqlCommand = new SqlCommand(SQL_Server_Query_String, Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("@Fecha_Vencimiento_Insumo", Fecha_Vencimiento_Insumo);
                    Obj_SqlCommand.Parameters.AddWithValue("@ID_Insumo", ID_Insumo);
                    Obj_SqlCommand.CommandType = CommandType.Text;

                    Obj_SqlConnection.Open();

                    Result = Obj_SqlCommand.ExecuteNonQuery() > 0 ? true : false;
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

        public bool Class_Data_Insumo_Eliminar(int ID_Insumo, out string Message)
        {
            bool Result = false;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_SUPPLY_DELETE", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Insumo", ID_Insumo);
                    Obj_SqlCommand.Parameters.Add("Message", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    Obj_SqlCommand.Parameters.Add("Result", SqlDbType.Int).Direction = ParameterDirection.Output;
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
    }
}