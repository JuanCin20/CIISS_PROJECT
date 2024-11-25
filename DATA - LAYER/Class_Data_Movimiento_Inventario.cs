using System.Data.SqlClient;
using System.Data;
using System;
using ENTITY___LAYER;

namespace DATA___LAYER
{
    public class Class_Data_Movimiento_Inventario
    {
        public bool Class_Data_Movimiento_Inventario_Registrar(Class_Entity_Movimiento_Inventario Obj_Class_Entity_Movimiento_Inventario, DataTable Obj_DataTable, out string Message)
        {
            bool Result = false;
            Message = string.Empty;
            try
            {
                using (SqlConnection Obj_SqlConnection = new SqlConnection(Class_Data_Connection.Connection_String))
                {
                    SqlCommand Obj_SqlCommand = new SqlCommand("SP_TRANSACTION_CREATE", Obj_SqlConnection);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Usuario", Obj_Class_Entity_Movimiento_Inventario.Obj_Class_Entity_Usuario.ID_Usuario);
                    Obj_SqlCommand.Parameters.AddWithValue("Tipo_Movimiento_Inventario", Obj_Class_Entity_Movimiento_Inventario.Tipo_Movimiento_Inventario);
                    Obj_SqlCommand.Parameters.AddWithValue("Cantidad_Insumo_Movimiento_Inventario", Obj_Class_Entity_Movimiento_Inventario.Cantidad_Insumo_Movimiento_Inventario);
                    Obj_SqlCommand.Parameters.AddWithValue("Monto_Total_Movimiento_Inventario", Obj_Class_Entity_Movimiento_Inventario.Monto_Total_Movimiento_Inventario);
                    Obj_SqlCommand.Parameters.AddWithValue("Restaurante_Movimiento_Inventario", Obj_Class_Entity_Movimiento_Inventario.Restaurante_Movimiento_Inventario);
                    Obj_SqlCommand.Parameters.AddWithValue("Telefono_Movimiento_Inventario", Obj_Class_Entity_Movimiento_Inventario.Telefono_Movimiento_Inventario);
                    Obj_SqlCommand.Parameters.AddWithValue("Direccion_Movimiento_Inventario", Obj_Class_Entity_Movimiento_Inventario.Direccion_Movimiento_Inventario);
                    Obj_SqlCommand.Parameters.AddWithValue("ID_Distrito", Obj_Class_Entity_Movimiento_Inventario.ID_Distrito);
                    Obj_SqlCommand.Parameters.AddWithValue("Tabla_Detalle_Movimiento_Inventario", Obj_DataTable);
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
    }
}