using CloudTrabajoBimestral.Consumer;
using CloudTrabajoBimestral.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudTrabajo.MVC.Controllers
{
    public class AsistenciasController : Controller
    {
        public async Task<ActionResult> Index()
        {
            // Obtener asistencias
            var asistencias = await Crud<Asistencia>.GetAll();

            // Obtener inscripciones y sesiones para enriquecer los datos
            var inscripciones = await Crud<Inscripcion>.GetAll();
            var participantes = await Crud<Participante>.GetAll();
            var sesiones = await Crud<Sesion>.GetAll();

            // Combinar los datos manualmente
            foreach (var asistencia in asistencias)
            {
                asistencia.Inscripcion = inscripciones.FirstOrDefault(i => i.Id == asistencia.inscripcionId);
                if (asistencia.Inscripcion != null)
                {
                    asistencia.Inscripcion.Participante = participantes.FirstOrDefault(p => p.Cedula == asistencia.Inscripcion.Cedula);
                }
                asistencia.Sesion = sesiones.FirstOrDefault(s => s.Id == asistencia.sesionId);
            }

            return View(asistencias);
        }

        public ActionResult Details(int id)
        {
            var data = Crud<Asistencia>.Get(id).Result;
            return View(data);
        }

        public ActionResult Create()
        {
            // Asumimos que ya tienes métodos ListaInscripciones y ListaSesiones
            ViewBag.ListaInscripciones = ListaInscripciones();
            ViewBag.ListaSesiones = ListaSesiones();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Asistencia asistencia)
        {
            try
            {
                asistencia.Id = 0; // Autogenerado por la base de datos
                asistencia.fechaAsistencia = DateTime.UtcNow; // 2025-05-24T20:27:00Z
                asistencia.estado = true; // Valor por defecto
                Crud<Asistencia>.Create(asistencia).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al registrar la asistencia");
                ViewBag.ListaInscripciones = ListaInscripciones();
                ViewBag.ListaSesiones = ListaSesiones();
                return View(asistencia);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ListaInscripciones = ListaInscripciones();
            ViewBag.ListaSesiones = ListaSesiones();
            var data = Crud<Asistencia>.Get(id).Result;
            return View(data);
        }

        private List<SelectListItem> ListaInscripciones()
        {
            var inscripciones = Crud<Inscripcion>.GetAll().Result;
            var participantes = Crud<Participante>.GetAll().Result;
            var lista = inscripciones.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.fechaInscripcion.ToString("dd/MM/yyyy"),
                Group = new SelectListGroup { Name = i.Cedula },
                Disabled = false,
                Selected = false
            }).ToList();

            foreach (var item in lista)
            {
                var inscripcion = inscripciones.FirstOrDefault(i => i.Id.ToString() == item.Value);
                if (inscripcion != null)
                {
                    var participante = participantes.FirstOrDefault(p => p.Cedula == inscripcion.Cedula);
                    if (participante != null)
                    {
                        item.Text += $" - {participante.Name} {participante.Lastname} (Cédula: {participante.Cedula})";
                    }
                }
            }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Asistencia asistencia)
        {
            try
            {
                Crud<Asistencia>.Update(id, asistencia).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al editar la asistencia");
                ViewBag.ListaInscripciones = ListaInscripciones();
                ViewBag.ListaSesiones = ListaSesiones();
                return View(asistencia);
            }
        }

        public ActionResult Delete(int id)
        {
            var data = Crud<Asistencia>.Get(id).Result;
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Asistencia asistencia)
        {
            try
            {
                Crud<Asistencia>.Delete(id).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar la asistencia");
                return View(asistencia);
            }
        }
    }
}