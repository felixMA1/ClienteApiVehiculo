using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaseServicios;
using ClienteApiVehiculos.Models;
using Microsoft.Practices.Unity;

namespace ClienteApiVehiculos.Controllers
{
    public class TipoController : Controller
    {
        [Dependency]
        public IServiciosRest<Tipo> Servicio { get; set; }
         
        // GET: Tipo
        public ActionResult Index()
        {
            var data = Servicio.Get();
            return View(data);
        }
    }
}