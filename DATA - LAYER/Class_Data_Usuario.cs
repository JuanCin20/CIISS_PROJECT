﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using ENTITY___LAYER;

namespace DATA___LAYER
{
    public class Class_Data_Usuario
    {
        public List<Class_Entity_Usuario> Class_Data_Usuario_Listar()
        {
            List<Class_Entity_Usuario> Obj_List_Class_Entity_Usuario = new List<Class_Entity_Usuario>();
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_USER", Obj_SqlConnection);
                    Obj_SqlCommand.CommandType = CommandType.StoredProcedure;

                    Obj_SqlConnection.Open();

                    using (SqlDataReader Obj_SqlDataReader = Obj_SqlCommand.ExecuteReader())
                    {
                        while (Obj_SqlDataReader.Read())
                        {
                            Obj_List_Class_Entity_Usuario.Add(new Class_Entity_Usuario()
                            {
                                ID_Usuario = Convert.ToInt32(Obj_SqlDataReader["ID_Usuario"]),
                                Obj_Class_Entity_Tipo_Usuario = new Class_Entity_Tipo_Usuario()
                                {
                                    ID_Tipo_Usuario = Convert.ToInt32(Obj_SqlDataReader["ID_Tipo_Usuario"]),
                                    Nombre_Tipo_Usuario = Obj_SqlDataReader["Nombre_Tipo_Usuario"].ToString()
                                },
                                Nombre_Usuario = Obj_SqlDataReader["Nombre_Usuario"].ToString(),
                                Apellido_Usuario = Obj_SqlDataReader["Apellido_Usuario"].ToString(),
                                E_Mail_Usuario = Obj_SqlDataReader["E_Mail_Usuario"].ToString(),
                                Password_Usuario = Obj_SqlDataReader["Password_Usuario"].ToString(),
                                Reestablecer_Password_Usuario = Convert.ToBoolean(Obj_SqlDataReader["Reestablecer_Password_Usuario"]),
                                Estado_Usuario = Convert.ToBoolean(Obj_SqlDataReader["Estado_Usuario"]),
                                Fecha_Registro_Usuario = Obj_SqlDataReader["Fecha_Registro_Usuario"].ToString(),
                                Ruta_Imagen_Usuario = Obj_SqlDataReader["Ruta_Imagen_Usuario"].ToString(),
                                Nombre_Imagen_Usuario = Obj_SqlDataReader["Nombre_Imagen_Usuario"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Obj_List_Class_Entity_Usuario = new List<Class_Entity_Usuario>();
            }
            return Obj_List_Class_Entity_Usuario;
        }

        public List<Class_Entity_Usuario> Class_Data_Usuario_Listar_Alternative(bool Estado_Usuario)
        {
            List<Class_Entity_Usuario> Obj_List_Class_Entity_Usuario = new List<Class_Entity_Usuario>();
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_USER_LIST", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("Estado_Usuario", Estado_Usuario);
                    Obj_SqlCommand.CommandType = CommandType.StoredProcedure;

                    Obj_SqlConnection.Open();

                    using (SqlDataReader Obj_SqlDataReader = Obj_SqlCommand.ExecuteReader())
                    {
                        while (Obj_SqlDataReader.Read())
                        {
                            Obj_List_Class_Entity_Usuario.Add(new Class_Entity_Usuario()
                            {
                                ID_Usuario = Convert.ToInt32(Obj_SqlDataReader["ID_Usuario"]),
                                Obj_Class_Entity_Tipo_Usuario = new Class_Entity_Tipo_Usuario()
                                {
                                    ID_Tipo_Usuario = Convert.ToInt32(Obj_SqlDataReader["ID_Tipo_Usuario"]),
                                    Nombre_Tipo_Usuario = Obj_SqlDataReader["Nombre_Tipo_Usuario"].ToString()
                                },
                                Nombre_Usuario = Obj_SqlDataReader["Nombre_Usuario"].ToString(),
                                Apellido_Usuario = Obj_SqlDataReader["Apellido_Usuario"].ToString(),
                                E_Mail_Usuario = Obj_SqlDataReader["E_Mail_Usuario"].ToString(),
                                Password_Usuario = Obj_SqlDataReader["Password_Usuario"].ToString(),
                                Reestablecer_Password_Usuario = Convert.ToBoolean(Obj_SqlDataReader["Reestablecer_Password_Usuario"]),
                                Estado_Usuario = Convert.ToBoolean(Obj_SqlDataReader["Estado_Usuario"]),
                                Fecha_Registro_Usuario = Obj_SqlDataReader["Fecha_Registro_Usuario"].ToString(),
                                Ruta_Imagen_Usuario = Obj_SqlDataReader["Ruta_Imagen_Usuario"].ToString(),
                                Nombre_Imagen_Usuario = Obj_SqlDataReader["Nombre_Imagen_Usuario"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error.Message);
                Obj_List_Class_Entity_Usuario = new List<Class_Entity_Usuario>();
            }
            return Obj_List_Class_Entity_Usuario;
        }

        public int Class_Data_Usuario_Registrar(Class_Entity_Usuario Obj_Class_Entity_Usuario, out string Message)
        {
            int ID_Auto_Generated = 0;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_USER_CREATE", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Tipo_Usuario", Obj_Class_Entity_Usuario.Obj_Class_Entity_Tipo_Usuario.ID_Tipo_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("Nombre_Usuario", Obj_Class_Entity_Usuario.Nombre_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("Apellido_Usuario", Obj_Class_Entity_Usuario.Apellido_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("E_Mail_Usuario", Obj_Class_Entity_Usuario.E_Mail_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("Password_Usuario", Obj_Class_Entity_Usuario.Password_Usuario);
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

        public bool Class_Data_Usuario_Change_Password(int ID_Usuario, string New_Password_Usuario, out string Message)
        {
            bool Result = false;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("UPDATE Tabla_Usuario SET Password_Usuario = @New_Password_Usuario, Reestablecer_Password_Usuario = 0 WHERE ID_Usuario = @ID_Usuario", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("@New_Password_Usuario", New_Password_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
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

        public bool Class_Data_Usuario_Reset_Password(int ID_Usuario, string Password_Usuario, out string Message)
        {
            bool Result = false;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("UPDATE Tabla_Usuario SET Password_Usuario = @Password_Usuario, Reestablecer_Password_Usuario = 1 WHERE ID_Usuario = @ID_Usuario", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("@Password_Usuario", Password_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
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

        public bool Class_Data_Usuario_Editar(Class_Entity_Usuario Obj_Class_Entity_Usuario, out string Message)
        {
            bool Result = false;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_USER_UPDATE", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Usuario", Obj_Class_Entity_Usuario.ID_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("E_Mail_Usuario", Obj_Class_Entity_Usuario.E_Mail_Usuario);
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

        public bool Class_Data_Usuario_Save_Image(Class_Entity_Usuario Obj_Class_Entity_Usuario, out string Message)
        {
            bool Result = false;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    string SQL_Server_Query_String = "UPDATE Tabla_Usuario SET Ruta_Imagen_Usuario = @Ruta_Imagen_Usuario, Nombre_Imagen_Usuario = @Nombre_Imagen_Usuario WHERE ID_Usuario = @ID_Usuario;";

                    SqlCommand Obj_SqlCommand = new SqlCommand(SQL_Server_Query_String, Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("@Ruta_Imagen_Usuario", Obj_Class_Entity_Usuario.Ruta_Imagen_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("@Nombre_Imagen_Usuario", Obj_Class_Entity_Usuario.Nombre_Imagen_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("@ID_Usuario", Obj_Class_Entity_Usuario.ID_Usuario);
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

        public bool Class_Data_Usuario_Reset(int ID_Usuario, out string Message)
        {
            bool Result = false;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    string SQL_Server_Query_String = "UPDATE Tabla_Usuario SET Estado_Usuario = 1 WHERE ID_Usuario = @ID_Usuario;";

                    SqlCommand Obj_SqlCommand = new SqlCommand(SQL_Server_Query_String, Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
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

        public bool Class_Data_Usuario_Eliminar(int ID_Usuario, out string Message)
        {
            bool Result = false;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_USER_DELETE", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Usuario", ID_Usuario);
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