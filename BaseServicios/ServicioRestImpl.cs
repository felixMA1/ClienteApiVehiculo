using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BaseServicios
{
    //Lo que hace esta clase es una implementacion de llamadas a una api rest utilizando NET Framework
    public class ServicioRestImpl<TModelo> : IServiciosRest<TModelo>
    {
        //La url donde esta el servicio
        private String url;
        //Auth es para saber si el servicio es autenticado o no
        private bool auth;
        //User es para, si el servicio es autenticado, saber cual es el nombre de usuario
        private String user;
        //Pass es para, si el servicio es autenticado, saber cual es el password de usuario
        private String pass;

        public ServicioRestImpl(String url, bool auth = false, String user = null, String pass = null)
        {
            this.url = url;
            this.auth = auth;
            this.user = user;
            this.pass = pass;
        }

        public async Task<TModelo> Add(TModelo model)
        {
            var datos = Serializacion<TModelo>.Serializar(model);
            //HttpClientHandler se encarga de manejar las peticiones del HttpClient
            //cabeceras, autenticacion etc.
            using (var handler = new HttpClientHandler())
            {
                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }
                //HttpCliente crea un cliente nativo puro HTTP(como si abrieramos una especia de navegador web)
                using (var client = new HttpClient(handler))
                {
                    //StringContent le doy una cadena de texto plano y la prepara para enviarla en una peticion HTTP
                    var contenido = new StringContent(datos);
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var r = await client.PostAsync(new Uri(url), contenido);
                    if (!r.IsSuccessStatusCode)
                        throw new Exception("Fallo al insertar");
                    var objSerializado = await r.Content.ReadAsStringAsync();
                    return Serializacion<TModelo>.Deserializar(objSerializado);
                }
            }
        }
        //aync firma que ponemos en el metodo para forzar a que sea un metodo asincrono
        //await esperar a que termine una operacion para lanzar otra ?
        public async Task Update(TModelo model)
        {
            var datos = Serializacion<TModelo>.Serializar(model);
            //HttpClientHandler se encarga de manejar las peticiones del HttpClient
            //cabeceras, autenticacion etc.
            using (var handler = new HttpClientHandler())
            {
                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }
                //HttpCliente crea un cliente nativo puro HTTP(como si abrieramos una especia de navegador web)
                using (var client = new HttpClient(handler))
                {
                    //StringContent le doy una cadena de texto plano y la prepara para enviarla en una peticion HTTP
                    var contenido = new StringContent(datos);
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var r = await client.PutAsync(new Uri(url), contenido);
                    if(!r.IsSuccessStatusCode)
                        throw new Exception("Fallo al actualizar");
                }
            }
        }

        public async Task Delete(int id)
        {
            using (var handler = new HttpClientHandler())
            {
                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }
                using (var client = new HttpClient(handler))
                {
                    var r = await client.DeleteAsync(new Uri(url+"/"+id));
                    if (!r.IsSuccessStatusCode)
                        throw new Exception("Fallo al borrar");
                }
            }
        }

        public List<TModelo> Get(String paramUrl = null)
        {
            //Lista de objetos
            List<TModelo> lista;
            var urlDest = url;
            if (paramUrl != null)
                urlDest += paramUrl;
            //WebRequest hace peticiones solo por GET
            var request = WebRequest.Create(urlDest);
            //Si me tengo que autenticar sobre la WebApi...
            if (auth)
            {
                //NetWorkCredential crea unas credenciales de autenticacion
                //Genera una cabecera que pone basic y en base64 añade el user:pass
                //Una API Rest siempre tiene que ir cifrada
                request.Credentials = new NetworkCredential(user, pass);
            }
            request.Method = "GET";

            var response = request.GetResponse();
            //Cuando se necesita conectar con algun flujo de datos externo a la aplicacion se usa un stream
            //stream es un flujo de datos
            //Los stream abren un canal de comunicacion para que pueda leer o mandar datos hacia algo externo a nuestra aplicacion
            using (var stream = response.GetResponseStream())
            {
                //StreamReader crea una herramienta para poder leer ese flujo de datos (lo que le llega son bytes y los transforma en texto)
                using (var reader = new StreamReader(stream))
                {
                    var serializado = reader.ReadToEnd();
                    lista = Serializacion<List<TModelo>>.Deserializar(serializado);
                }
            }
            return lista;
        }

        //Diccionario par clave valor
        public List<TModelo> Get(Dictionary<string, string> param)
        {
            var paramsurl = "";
            var primero = true;
            foreach (var key in param.Keys)
            {
                if (primero)
                {
                    paramsurl += "?";
                    primero = false;
                }

                else
                    paramsurl += "&";

                paramsurl += key + "=" + param[key];
            }
            return Get(paramsurl);
        }

        public TModelo Get(int id)
        {

            TModelo objeto;
            var request = WebRequest.Create(url + "/" + id);

            if (auth)
            {
                request.Credentials = new NetworkCredential(user, pass);
            }
            request.Method = "GET";
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var serializado = reader.ReadToEnd();
                    objeto = Serializacion<TModelo>.Deserializar(serializado);
                }
            }
            return objeto;
        }
    }
}
