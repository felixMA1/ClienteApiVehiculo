using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServicios
{
    public interface IServiciosRest<TModelo>
    {
        //Sincrono: Pido algo y estoy esperando a que llegue el resultado
        //Asincrono: Yo pido algo se queda haciendolo puedo hacer cosas mientras y cuando ya tiene los resultados los manda
        //Task : Tarea asincrona
        //Un task vacio no tiene resultado, es un void si se quieren obtener los detalles agregamos <Tmodelo>

        Task<TModelo> Add(TModelo model);
        Task Update(TModelo model);
        Task Delete(TModelo model);

        //Las llamadas a Get (operaciones de consulta) pueden ser sincronas por eso no usamos Task
        List<TModelo> Get();
        List<TModelo> Get(Dictionary<String, String> param);
        TModelo Get(int id);
    }
}
