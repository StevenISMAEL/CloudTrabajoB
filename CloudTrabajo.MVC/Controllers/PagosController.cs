using CloudTrabajoBimestral.Consumer;
using CloudTrabajoBimestral.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudTrabajo.MVC.Controllers
{
    public class PagosController : Controller
    {
        public async Task<ActionResult> Index()
        {
            // Obtener pagos
            var pagos = await Crud<Pago>.GetAll();

            // Obtener inscripciones y participantes para enriquecer los datos
            var inscripciones = await Crud<Inscripcion>.GetAll();
            var participantes = await Crud<Participante>.GetAll();

            // Combinar los datos manualmente
            foreach (var pago in pagos)
            {
                pago.Inscripcion = inscripciones.FirstOrDefault(i => i.Id == pago.InscripcionID);
                if (pago.Inscripcion != null)
                {
                    pago.Inscripcion.Participante = participantes.FirstOrDefault(p => p.Cedula == pago.Inscripcion.Cedula);
                }
            }

            return View(pagos);
        }

        public ActionResult Details(int id)
        {
            var data = Crud<Pago>.Get(id).Result;
            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.ListaInscripciones = ListaInscripciones();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pago pago)
        {
            try
            {
                pago.fechaPago = DateTime.UtcNow; // 2025-05-24T23:50:00Z
                pago.estado = true; // Valor por defecto
                Crud<Pago>.Create(pago).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al crear el pago");
                ViewBag.ListaInscripciones = ListaInscripciones();
                return View(pago);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ListaInscripciones = ListaInscripciones();
            var data = Crud<Pago>.Get(id).Result;
            return View(data);
        }

        private List<SelectListItem> ListaPagos()
        {
            var pagos = Crud<Pago>.GetAll().Result;
            var lista = pagos.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.monto:C} - {x.fechaPago:dd/MM/yyyy}"
            }).ToList();
            return lista;
        }

        private List<SelectListItem> ListaInscripciones()
        {
            var inscripciones = Crud<Inscripcion>.GetAll().Result;
            var participantes = Crud<Participante>.GetAll().Result;

            var lista = inscripciones.Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.fechaInscripcion.ToString("dd/MM/yyyy"),
                Group = new SelectListGroup { Name = i.Cedula }, // Cédula
                Disabled = false,
                Selected = false
            }).ToList();

            // Enriquecer con nombres de participantes
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pago pago)
        {
            try
            {
                Crud<Pago>.Update(id, pago).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al editar el pago");
                ViewBag.ListaInscripciones = ListaInscripciones();
                return View(pago);
            }
        }

        public ActionResult Delete(int id)
        {
            var data = Crud<Pago>.Get(id).Result;
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Pago pago)
        {
            try
            {
                Crud<Pago>.Delete(id).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar el pago");
                return View(pago);
            }
        }
    }
}