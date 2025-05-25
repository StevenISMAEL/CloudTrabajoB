using CloudTrabajoBimestral.Consumer;
using CloudTrabajoBimestral.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudTrabajo.MVC.Controllers
{
    public class SesionPonentesController : Controller
    {
        // GET: SesionPonentesController
        public async Task<ActionResult> Index()
        {
            // Obtener SesionPonente
            var sesionPonentes = await Crud<SesionPonente>.GetAll();

            // Obtener sesiones y ponentes para enriquecer los datos
            var sesiones = await Crud<Sesion>.GetAll();
            var ponentes = await Crud<Ponente>.GetAll();

            // Combinar los datos manualmente
            foreach (var sesionPonente in sesionPonentes)
            {
                sesionPonente.Sesion = sesiones.FirstOrDefault(s => s.Id == sesionPonente.SesionId);
                sesionPonente.Ponente = ponentes.FirstOrDefault(p => p.Id == sesionPonente.PonenteId);
            }

            return View(sesionPonentes);
        }

        // GET: SesionPonentesController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<SesionPonente>.Get(id).Result;
            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.ListaSesiones = ListaSesiones();
            ViewBag.ListaPonentes = ListaPonentes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SesionPonente sesionPonente)
        {
            try
            {
                sesionPonente.Id = 0; // Autogenerado por la base de datos
                Crud<SesionPonente>.Create(sesionPonente).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al Asignar el ponente a la sesión");
                ViewBag.ListaSesiones = ListaSesiones();
                ViewBag.ListaPonentes = ListaPonentes();
                return View(sesionPonente);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ListaSesiones = ListaSesiones();
            ViewBag.ListaPonentes = ListaPonentes();
            var data = Crud<SesionPonente>.Get(id).Result;
            return View(data);
        }

        private List<SelectListItem> ListaSesionesPonentes()
        {
            var sesiones = Crud<SesionPonente>.GetAll().Result;
            var lista = sesiones.Select(x => new SelectListItem
            {
                Value = x.Id.ToString()
            }).ToList();
            return lista;
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

        private List<SelectListItem> ListaPonentes()
        {
            var ponentes = Crud<Ponente>.GetAll().Result;
            var lista = ponentes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.Name} {x.Lastname} - {x.Especialidad}"
            }).ToList();
            return lista;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SesionPonente sesionPonente)
        {
            try
            {
                Crud<SesionPonente>.Update(id, sesionPonente).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al editar el ponente a la sesión");
                ViewBag.ListaSesiones = ListaSesiones();
                ViewBag.ListaPonentes = ListaPonentes();
                return View(sesionPonente);
            }
        }

        public ActionResult Delete(int id)
        {
            var data = Crud<SesionPonente>.Get(id).Result;
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SesionPonente sesionPonente)
        {
            try
            {
                Crud<SesionPonente>.Delete(id).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar el ponente a la sesión");
                return View(sesionPonente);
            }
        }
    }
}