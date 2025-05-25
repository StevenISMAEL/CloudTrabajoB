using CloudTrabajoBimestral.Consumer;
using CloudTrabajoBimestral.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudTrabajo.MVC.Controllers
{
    public class InscripcionesController : Controller
    {
        public async Task<ActionResult> Index()
        {
            // Obtener inscripciones
            var inscripciones = await Crud<Inscripcion>.GetAll();
            var eventos = await Crud<Evento>.GetAll();

            // Combinar los datos manualmente (mapear Evento basado en EventoId)
            foreach (var inscripcion in inscripciones)
            {
                inscripcion.Evento = eventos.FirstOrDefault(e => e.Id == inscripcion.EventoId);
            }

            return View(inscripciones);
        }


        public ActionResult Details(int id)
        {
            var data = Crud<Inscripcion>.Get(id).Result;
            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.ListaEventos = ListaEventos();
            ViewBag.ListaParticipantes = ListaParticipantes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inscripcion inscripcion)
        {
            try
            {
                inscripcion.fechaInscripcion = DateTime.UtcNow; // 2025-05-24T23:39:00Z
                inscripcion.estado = true; // Valor por defecto (true para Confirmada)
                Crud<Inscripcion>.Create(inscripcion).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al crear la inscripción");
                ViewBag.ListaEventos = ListaEventos();
                ViewBag.ListaParticipantes = ListaParticipantes();
                return View(inscripcion);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ListaEventos = ListaEventos();
            ViewBag.ListaParticipantes = ListaParticipantes();
            var data = Crud<Inscripcion>.Get(id).Result;
            return View(data);
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

        private List<SelectListItem> ListaParticipantes()
        {
            var participantes = Crud<Participante>.GetAll().Result;
            var lista = participantes.Select(x => new SelectListItem
            {
                Value = x.Cedula,
                Text = $"{x.Name} {x.Lastname} (Cédula: {x.Cedula})"
            }).ToList();
            return lista;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Inscripcion inscripcion)
        {
            try
            {
                Crud<Inscripcion>.Update(id, inscripcion).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al editar la inscripción");
                ViewBag.ListaEventos = ListaEventos();
                ViewBag.ListaParticipantes = ListaParticipantes();
                return View(inscripcion);
            }
        }

        public ActionResult Delete(int id)
        {
            var data = Crud<Inscripcion>.Get(id).Result;
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Inscripcion inscripcion)
        {
            try
            {
                Crud<Inscripcion>.Delete(id).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar la inscripción");
                return View(inscripcion);
            }
        }
    }
}