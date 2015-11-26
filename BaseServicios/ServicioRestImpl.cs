using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServicios
{
    public class ServicioRestImpl<TModelo>:IServiciosRest<TModelo>
    {
        //La url donde esta el servicio
        private String url;
        //Auth es para saber si el servicio es autenticado o no
        private bool auth;
        //User es para, si el servicio es autenticado, saber cual es el nombre de usuario
        private String user;
        //Pass es para, si el servicio es autenticado, saber cual es el password de usuario
        private String pass;
                
        public ServicioRestImpl(String url, bool auth = false,String user = null,String pass = null)
        {
            this.url = url;
            this.auth = auth;
            this.user = user;
            this.pass = pass;
        } 

        public Task<TModelo> Add(TModelo model)
        {
            throw new NotImplementedException();
        }

        public Task Update(TModelo model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(TModelo model)
        {
            throw new NotImplementedException();
        }

        public List<TModelo> Get()
        {
            throw new NotImplementedException();
        }

        public List<TModelo> Get(Dictionary<string, string> param)
        {
            throw new NotImplementedException();
        }

        public TModelo Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
