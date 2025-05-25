using CloudTrabajoBimestral.Consumer;
using CloudTrabajoBimestral.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudTrabajo.MVC.Controllers
{
    public class ParticipantesController : Controller
    {
        public ActionResult Index()
        {
            var data = Crud<Participante>.GetAll().Result;
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var data = Crud<Participante>.Get(id).Result;
            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.ListaParticipantes = ListaParticipantes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Participante participante)
        {
            try
            {
                Crud<Participante>.Create(participante).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al crear el participante");
                return View(participante);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ListaParticipantes = ListaParticipantes();
            var data = Crud<Participante>.Get(id).Result;
            return View(data);
        }

        private List<SelectListItem> ListaParticipantes()
        {
            var participantes = Crud<Participante>.GetAll().Result;
            var lista = participantes.Select(x => new SelectListItem
            {
                Value = x.Cedula,
                Text = $"{x.Name} {x.Lastname}"
            }).ToList();
            return lista;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Participante participante)
        {
            try
            {
                Crud<Participante>.Update(id, participante).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al editar el participante");
                return View(participante);
            }
        }

        public ActionResult Delete(int id)
        {
            var data = Crud<Participante>.Get(id).Result;
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Participante participante)
        {
            try
            {
                Crud<Participante>.Delete(id).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar el participante");
                return View(participante);
            }
        }
    }
}
