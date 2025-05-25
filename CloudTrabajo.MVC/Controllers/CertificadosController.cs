using CloudTrabajoBimestral.Consumer;
using CloudTrabajoBimestral.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudTrabajo.MVC.Controllers
{
    public class CertificadosController : Controller
    {
        // GET: CertificadosController
        public async Task<ActionResult> Index()
        {
            var certificados = await Crud<Certificado>.GetAll();
            var inscripciones = await Crud<Inscripcion>.GetAll();
            var participantes = await Crud<Participante>.GetAll();

            foreach (var certificado in certificados)
            {
                certificado.Inscripcion = inscripciones.FirstOrDefault(i => i.Id == certificado.InscripcionID);
                if (certificado.Inscripcion != null)
                {
                    certificado.Inscripcion.Participante = participantes.FirstOrDefault(p => p.Cedula == certificado.Inscripcion.Cedula);
                }
            }

            return View(certificados);
        }

        // GET: CertificadosController/Details/5
        public ActionResult Details(int id)
        {
            var data = Crud<Certificado>.Get(id).Result;
            return View(data);
        }

        // GET: CertificadosController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.ListaInscripciones = await ListaInscripciones();
            return View();
        }

        // POST: CertificadosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Certificado certificado)
        {
            if (ModelState.IsValid)
            {
                var inscripciones = await Crud<Inscripcion>.GetAll();
                var pagos = await Crud<Pago>.GetAll();
                var asistencias = await Crud<Asistencia>.GetAll();
                var sesiones = await Crud<Sesion>.GetAll();

                var inscripcion = inscripciones.FirstOrDefault(i => i.Id == certificado.InscripcionID);
                if (inscripcion == null)
                {
                    ModelState.AddModelError("", "Inscripción no encontrada.");
                    ViewBag.ListaInscripciones = await ListaInscripciones();
                    return View(certificado);
                }

                // Verificar requisitos
                var pagosInscripcion = pagos.Where(p => p.InscripcionID == inscripcion.Id && p.estado).ToList();
                var sesionesInscripcion = sesiones.Where(s => s.EventoID == inscripcion.EventoId).ToList();
                var asistenciasInscripcion = asistencias.Where(a => a.inscripcionId == inscripcion.Id).ToList();

                bool inscripcionValida = inscripcion.estado;
                bool pagoRealizado = pagosInscripcion.Any();
                bool asistenciaCompleta = sesionesInscripcion.Count > 0 && sesionesInscripcion.All(s =>
                    asistenciasInscripcion.Any(a => a.sesionId == s.Id && a.estado));

                if (!inscripcionValida)
                {
                    ModelState.AddModelError("", "La inscripción no está activa o confirmada.");
                }
                if (!pagoRealizado)
                {
                    ModelState.AddModelError("", "No se ha realizado el pago para esta inscripción.");
                }
                if (!asistenciaCompleta)
                {
                    ModelState.AddModelError("", "El participante no ha asistido a todas las sesiones.");
                }

                if (inscripcionValida && pagoRealizado && asistenciaCompleta)
                {
                    certificado.Id = 0; // Autogenerado por la base de datos
                    certificado.fechaEmision = DateTime.UtcNow; // 2025-05-24T20:54:00Z
                    certificado.UrlDescarga = $"https://tu-dominio.com/certificados/{inscripcion.Id}_{DateTime.UtcNow:yyyyMMddHHmmss}.pdf";
                    await Crud<Certificado>.Create(certificado);
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.ListaInscripciones = await ListaInscripciones();
            return View(certificado);
        }

        // GET: CertificadosController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ListaInscripciones = ListaInscripciones().Result;
            var data = Crud<Certificado>.Get(id).Result;
            return View(data);
        }

        private async Task<List<SelectListItem>> ListaCertificados()
        {
            var certificados = await Crud<Certificado>.GetAll();
            var lista = certificados.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.fechaEmision.ToString("dd/MM/yyyy"),
            }).ToList();
            return lista;
        }

        private async Task<List<SelectListItem>> ListaInscripciones()
        {
            var inscripciones = await Crud<Inscripcion>.GetAll();
            var participantes = await Crud<Participante>.GetAll();

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

        // POST: CertificadosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Certificado certificado)
        {
            try
            {
                Crud<Certificado>.Update(id, certificado).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al editar el certificado");
                ViewBag.ListaInscripciones = ListaInscripciones().Result;
                return View(certificado);
            }
        }

        // GET: CertificadosController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Certificado>.Get(id).Result;
            return View(data);
        }

        // POST: CertificadosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Certificado certificado)
        {
            try
            {
                Crud<Certificado>.Delete(id).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar el certificado");
                return View(certificado);
            }
        }
    }
}