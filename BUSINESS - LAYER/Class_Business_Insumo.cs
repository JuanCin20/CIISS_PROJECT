﻿using System.Collections.Generic;
using DATA___LAYER;
using ENTITY___LAYER;

namespace BUSINESS___LAYER
{
    public class Class_Business_Insumo
    {
        private Class_Data_Insumo Obj_Class_Data_Insumo = new Class_Data_Insumo();

        public List<Class_Entity_Insumo> Class_Business_Insumo_Listar(bool Estado_Insumo)
        {
            return Obj_Class_Data_Insumo.Class_Data_Insumo_Listar(Estado_Insumo);
        }

        public List<Class_Entity_Categoria_Insumo> Class_Business_Insumo_Categoria_Insumo_Listar()
        {
            return Obj_Class_Data_Insumo.Class_Data_Insumo_Categoria_Insumo_Listar();
        }

        public List<Class_Entity_Proveedor_Insumo> Class_Business_Insumo_Proveedor_Insumo_Listar()
        {
            return Obj_Class_Data_Insumo.Class_Data_Insumo_Proveedor_Insumo_Listar();
        }

        public int Class_Business_Insumo_Registrar(Class_Entity_Insumo Obj_Class_Entity_Insumo, out string Message)
        {
            Message = string.Empty;
            if (string.IsNullOrEmpty(Obj_Class_Entity_Insumo.Nombre_Insumo) || string.IsNullOrWhiteSpace(Obj_Class_Entity_Insumo.Nombre_Insumo))
            {
                Message = "Error: Nombre_Insumo";
            }
            else
            {
                if (string.IsNullOrEmpty(Obj_Class_Entity_Insumo.Descripcion_Insumo) || string.IsNullOrWhiteSpace(Obj_Class_Entity_Insumo.Descripcion_Insumo))
                {
                    Message = "Error: Descripcion_Insumo";
                }
                else
                {
                    if (Obj_Class_Entity_Insumo.Precio_Insumo <= 0)
                    {
                        Message = "Error: Precio_Insumo";
                    }
                    else
                    {
                        if (Obj_Class_Entity_Insumo.Stock_Insumo < 0)
                        {
                            Message = "Error: Stock_Insumo";
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(Message))
            {
                return Obj_Class_Data_Insumo.Class_Data_Insumo_Registrar(Obj_Class_Entity_Insumo, out Message);
            }
            else
            {
                return 0;
            }
        }

        public bool Class_Business_Insumo_Editar(Class_Entity_Insumo Obj_Class_Entity_Insumo, out string Message)
        {
            Message = string.Empty;

            if (string.IsNullOrEmpty(Obj_Class_Entity_Insumo.Descripcion_Insumo) || string.IsNullOrWhiteSpace(Obj_Class_Entity_Insumo.Descripcion_Insumo))
            {
                Message = "Error: Descripcion_Insumo";
            }
            else
            {
                if (Obj_Class_Entity_Insumo.Precio_Insumo <= 0)
                {
                    Message = "Error: Precio_Insumo";
                }
                else
                {
                    if (Obj_Class_Entity_Insumo.Stock_Insumo < 0)
                    {
                        Message = "Error: Stock_Insumo";
                    }
                }
            }

            if (string.IsNullOrEmpty(Message))
            {
                return Obj_Class_Data_Insumo.Class_Data_Insumo_Editar(Obj_Class_Entity_Insumo, out Message);
            }
            else
            {
                return false;
            }
        }

        public bool Class_Business_Insumo_Save_Image(Class_Entity_Insumo Obj_Class_Entity_Insumo, out string Message)
        {
            return Obj_Class_Data_Insumo.Class_Data_Insumo_Save_Image(Obj_Class_Entity_Insumo, out Message);
        }

        public bool Class_Business_Insumo_Reset(int ID_Insumo, string Fecha_Vencimiento_Insumo, out string Message)
        {
            return Obj_Class_Data_Insumo.Class_Data_Insumo_Reset(ID_Insumo, Fecha_Vencimiento_Insumo, out Message);
        }

        public bool Class_Business_Insumo_Eliminar(int ID_Insumo, out string Message)
        {
            return Obj_Class_Data_Insumo.Class_Data_Insumo_Eliminar(ID_Insumo, out Message);
        }
    }
}