using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BaseServicios
{
    //T es un nombre generico
    public class Serializacion<T>
    {
        //Transforma un JSON en un objeto del tipo que le he indicado
        public static T Deserializar(String obj)
        {
            //Para que funcione JavaScriptSerializer hay que añadir un using
            //O añadirlo a traves de las References y buscar en los ensamblados 'System Web Extensions'
            //JavaScriptSerializer lo hace desde JSON a un objeto
            //Para deserializar algo tiene que haber un constructor vacio o no haber constructor
            var ser = new JavaScriptSerializer();
            var data = ser.Deserialize<T>(obj);
            return data;
        }

        //Transforma un objeto a JSON
        public static String Serializar(T obj)
        {
            var ser = new JavaScriptSerializer();
            var data = ser.Serialize(obj);
            return data;
        }
    }
}
