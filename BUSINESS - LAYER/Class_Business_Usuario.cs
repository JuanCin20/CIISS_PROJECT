using System;
using System.Collections.Generic;
using DATA___LAYER;
using ENTITY___LAYER;

namespace BUSINESS___LAYER
{
    public class Class_Business_Usuario
    {
        private Class_Data_Usuario Obj_Class_Data_Usuario = new Class_Data_Usuario();

        public List<Class_Entity_Usuario> Class_Business_Usuario_Listar()
        {
            return Obj_Class_Data_Usuario.Class_Data_Usuario_Listar();
        }

        public List<Class_Entity_Usuario> Class_Business_Usuario_Listar_Alternative(bool Estado_Usuario)
        {
            return Obj_Class_Data_Usuario.Class_Data_Usuario_Listar_Alternative(Estado_Usuario);
        }

        public int Class_Business_Usuario_Registrar(Class_Entity_Usuario Obj_Class_Entity_Usuario, out string Message)
        {
            Message = string.Empty;
            if (string.IsNullOrEmpty(Obj_Class_Entity_Usuario.Nombre_Usuario) || string.IsNullOrWhiteSpace(Obj_Class_Entity_Usuario.Nombre_Usuario))
            {
                Message = "Error: Nombre_Usuario";
            }
            else
            {
                if (string.IsNullOrEmpty(Obj_Class_Entity_Usuario.Apellido_Usuario) || string.IsNullOrWhiteSpace(Obj_Class_Entity_Usuario.Apellido_Usuario))
                {
                    Message = "Error: Apellido_Usuario";
                }
                else
                {
                    if (string.IsNullOrEmpty(Obj_Class_Entity_Usuario.E_Mail_Usuario) || string.IsNullOrWhiteSpace(Obj_Class_Entity_Usuario.E_Mail_Usuario))
                    {
                        Message = "Error: E_Mail_Usuario";
                    }
                }
            }

            string Password_Usuario = Class_Business_Recurso.Generate_Password();

            Obj_Class_Entity_Usuario.Password_Usuario = Class_Business_Recurso.Convert_SHA_256(Password_Usuario);
            int ID_Auto_Generated = Obj_Class_Data_Usuario.Class_Data_Usuario_Registrar(Obj_Class_Entity_Usuario, out Message);

            if (ID_Auto_Generated == 0)
            {
                return 0;
            }
            else
            {
                if (string.IsNullOrEmpty(Message))
                {
                    string E_Mail_Topic = "Registro de Cuenta";
                    string Current_Year = DateTime.Now.Year.ToString();
                    string E_Mail_Message = "<div\r\n      style=\"\r\n        display: flex;\r\n        flex-direction: column;\r\n        align-items: center;\r\n        gap: 5px;\r\n        background-color: rgb(148, 148, 148);\r\n      \"\r\n      class=\"Container\"\r\n    >\r\n      <div\r\n        style=\"width: 100%; background-color: rgb(35, 10, 145)\"\r\n        class=\"Sub_Container_01\"\r\n      >\r\n        <h1\r\n          style=\"\r\n            text-align: center;\r\n            color: rgb(255, 255, 255);\r\n            font-family: 'Lucida Console', 'Courier New', monospace;\r\n          \"\r\n        >\r\n          Sistema de Inventario\r\n        </h1>\r\n      </div>\r\n      <div class=\"Sub_Container_02\">\r\n        <div style=\"text-align: center\" class=\"Image_Container\">\r\n          <img\r\n            style=\"width: 250px; height: 250px; margin: 20px\"\r\n            src=\"https://cdn-icons-png.flaticon.com/256/12201/12201509.png\"\r\n            alt=\"Image_Error\"\r\n          />\r\n        </div>\r\n      </div>\r\n      <div\r\n        style=\"width: 100%; background-color: rgb(0, 0, 0)\"\r\n        class=\"Sub_Container_03\"\r\n      >\r\n        <h2\r\n          style=\"\r\n            text-align: center;\r\n            color: rgb(255, 255, 255);\r\n            font-family: 'Lucida Console', 'Courier New', monospace;\r\n          \"\r\n        >\r\n          Su Cuenta ha sido Registrada Exitosamente\r\n        </h2>\r\n        <p\r\n          style=\"\r\n            text-align: center;\r\n            color: rgb(255, 0, 0);\r\n            font-family: 'Lucida Console', 'Courier New', monospace;\r\n          \"\r\n        >\r\n          Su Contraseña de Acceso es: !Password_Usuario!\r\n        </p>\r\n        <hr />\r\n        <h5\r\n          style=\"\r\n            text-align: center;\r\n            color: rgb(255, 255, 255);\r\n            font-family: 'Lucida Console', 'Courier New', monospace;\r\n          \"\r\n        >\r\n          © " + Current_Year + " Sistema de Inventario\r\n        </h5>\r\n      </div>\r\n    </div>";
                    E_Mail_Message = E_Mail_Message.Replace("!Password_Usuario!", Password_Usuario);

                    bool Result = Class_Business_Recurso.Send_E_Mail(Obj_Class_Entity_Usuario.E_Mail_Usuario, E_Mail_Topic, E_Mail_Message);

                    if (Result)
                    {
                        return ID_Auto_Generated;
                    }
                    else
                    {
                        Message = "Error: E_Mail_Usuario";
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool Class_Business_Usuario_Change_Password(int ID_Usuario, string New_Password_Usuario, out string Message)
        {
            return Obj_Class_Data_Usuario.Class_Data_Usuario_Change_Password(ID_Usuario, New_Password_Usuario, out Message);
        }

        public bool Class_Business_Usuario_Reset_Password(int ID_Usuario, string E_Mail_Usuario, out string Message)
        {
            Message = string.Empty;
            string Password_Usuario_Alternative = Class_Business_Recurso.Generate_Password();
            bool Result_01 = Obj_Class_Data_Usuario.Class_Data_Usuario_Reset_Password(ID_Usuario, Class_Business_Recurso.Convert_SHA_256(Password_Usuario_Alternative), out Message);

            if (Result_01)
            {
                string E_Mail_Topic = "Contraseña Reestablecida";
                string Current_Year = DateTime.Now.Year.ToString();
                string E_Mail_Message = "<div\r\n      style=\"\r\n        display: flex;\r\n        flex-direction: column;\r\n        align-items: center;\r\n        gap: 5px;\r\n        background-color: rgb(148, 148, 148);\r\n      \"\r\n      class=\"Container\"\r\n    >\r\n      <div\r\n        style=\"width: 100%; background-color: rgb(35, 10, 145)\"\r\n        class=\"Sub_Container_01\"\r\n      >\r\n        <h1\r\n          style=\"\r\n            text-align: center;\r\n            color: rgb(255, 255, 255);\r\n            font-family: 'Lucida Console', 'Courier New', monospace;\r\n          \"\r\n        >\r\n          Sistema de Inventario\r\n        </h1>\r\n      </div>\r\n      <div class=\"Sub_Container_02\">\r\n        <div style=\"text-align: center\" class=\"Image_Container\">\r\n          <img\r\n            style=\"width: 250px; height: 250px; margin: 20px\"\r\n            src=\"https://cdn-icons-png.flaticon.com/256/12201/12201509.png\"\r\n            alt=\"Image_Error\"\r\n          />\r\n        </div>\r\n      </div>\r\n      <div\r\n        style=\"width: 100%; background-color: rgb(0, 0, 0)\"\r\n        class=\"Sub_Container_03\"\r\n      >\r\n        <h2\r\n          style=\"\r\n            text-align: center;\r\n            color: rgb(255, 255, 255);\r\n            font-family: 'Lucida Console', 'Courier New', monospace;\r\n          \"\r\n        >\r\n          Su Cuenta ha sido Recuperada Exitosamente\r\n        </h2>\r\n        <p\r\n          style=\"\r\n            text-align: center;\r\n            color: rgb(255, 0, 0);\r\n            font-family: 'Lucida Console', 'Courier New', monospace;\r\n          \"\r\n        >\r\n          Su Contraseña de Acceso es: !Password_Usuario!\r\n        </p>\r\n        <hr />\r\n        <h5\r\n          style=\"\r\n            text-align: center;\r\n            color: rgb(255, 255, 255);\r\n            font-family: 'Lucida Console', 'Courier New', monospace;\r\n          \"\r\n        >\r\n          © " + Current_Year + " Sistema de Inventario\r\n        </h5>\r\n      </div>\r\n    </div>";
                E_Mail_Message = E_Mail_Message.Replace("!Password_Usuario!", Password_Usuario_Alternative);

                bool Result_02 = Class_Business_Recurso.Send_E_Mail(E_Mail_Usuario, E_Mail_Topic, E_Mail_Message);

                if (Result_02)
                {
                    return true;
                }
                else
                {
                    Message = "Error: E_Mail_Usuario";
                    return false;
                }
            }
            else
            {
                Message = "Error: Password_Usuario";
                return false;
            }
        }

        public bool Class_Business_Usuario_Editar(Class_Entity_Usuario Obj_Class_Entity_Usuario, out string Message)
        {
            Message = string.Empty;

            if (string.IsNullOrEmpty(Obj_Class_Entity_Usuario.E_Mail_Usuario) || string.IsNullOrWhiteSpace(Obj_Class_Entity_Usuario.E_Mail_Usuario))
            {
                Message = "Error: E_Mail_Usuario";
            }

            if (string.IsNullOrEmpty(Message))
            {
                return Obj_Class_Data_Usuario.Class_Data_Usuario_Editar(Obj_Class_Entity_Usuario, out Message);
            }
            else
            {
                return false;
            }
        }

        public bool Class_Business_Usuario_Save_Image(Class_Entity_Usuario Obj_Class_Entity_Usuario, out string Message)
        {
            return Obj_Class_Data_Usuario.Class_Data_Usuario_Save_Image(Obj_Class_Entity_Usuario, out Message);
        }

        public bool Class_Business_Usuario_Reset(int ID_Usuario, out string Message)
        {
            return Obj_Class_Data_Usuario.Class_Data_Usuario_Reset(ID_Usuario, out Message);
        }

        public bool Class_Business_Usuario_Eliminar(int ID_Usuario, out string Message)
        {
            return Obj_Class_Data_Usuario.Class_Data_Usuario_Eliminar(ID_Usuario, out Message);
        }
    }
}