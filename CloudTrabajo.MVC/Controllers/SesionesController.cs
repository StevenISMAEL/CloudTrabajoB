using CloudTrabajoBimestral.Consumer;
using CloudTrabajoBimestral.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudTrabajo.MVC.Controllers
{
    public class SesionesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            // Obtener sesiones
            var sesiones = await Crud<Sesion>.GetAll();

            // Obtener espacios y eventos para enriquecer los datos
            var espacios = await Crud<Espacio>.GetAll();
            var eventos = await Crud<Evento>.GetAll();

            // Combinar los datos manualmente
            foreach (var sesion in sesiones)
            {
                sesion.Espacio = espacios.FirstOrDefault(e => e.Id == sesion.EspacioID);
                sesion.Evento = eventos.FirstOrDefault(e => e.Id == sesion.EventoID);
            }

            return View(sesiones);
        }

        public ActionResult Details(int id)
        {
            var data = Crud<Sesion>.Get(id).Result;
            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.ListaEspacios = ListaEspacios();
            ViewBag.ListaEventos = ListaEventos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sesion sesion)
        {
            try
            {
                sesion.Id = 0; // Autogenerado por la base de datos
                Crud<Sesion>.Create(sesion).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al crear la sesión");
                ViewBag.ListaEspacios = ListaEspacios();
                ViewBag.ListaEventos = ListaEventos();
                return View(sesion);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ListaEspacios = ListaEspacios();
            ViewBag.ListaEventos = ListaEventos();
            var data = Crud<Sesion>.Get(id).Result;
            return View(data);
        }

        private List<SelectListItem> ListaSesiones()
        {
            var sesiones = Crud<Sesion>.GetAll().Result;
            var lista = sesiones.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.Name} - {x.horaInicio:dd/MM/yyyy HH:mm}"
            }).ToList();
            return lista;
        }

        private List<SelectListItem> ListaEspacios()
        {
            var espacios = Crud<Espacio>.GetAll().Result;
            var lista = espacios.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.Name} (Capacidad: {x.Capacity})"
            }).ToList();
            return lista;
        }

        private List<SelectListItem> ListaEventos()
        {
            var eventos = Crud<Evento>.GetAll().Result;
            var lista = eventos.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.Name} ({x.fechaInicio:dd/MM/yyyy})"
            }).ToList();
            return lista;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Sesion sesion)
        {
            try
            {
                Crud<Sesion>.Update(id, sesion).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al editar la sesión");
                ViewBag.ListaEspacios = ListaEspacios();
                ViewBag.ListaEventos = ListaEventos();
                return View(sesion);
            }
        }

        public ActionResult Delete(int id)
        {
            var data = Crud<Sesion>.Get(id).Result;
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Sesion sesion)
        {
            try
            {
                Crud<Sesion>.Delete(id).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar la sesión");
                return View(sesion);
            }
        }
    }
}