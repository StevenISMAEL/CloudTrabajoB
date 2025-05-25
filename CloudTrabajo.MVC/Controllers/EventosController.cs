using CloudTrabajoBimestral.Consumer;
using CloudTrabajoBimestral.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudTrabajo.MVC.Controllers
{
    public class EventosController : Controller
    {
        // GET: EventosController
        public ActionResult Index()
        {
            var data = Crud<Evento>.GetAll().Result;
            return View(data);
        }

        // GET: EventosController/Details/5
        public ActionResult Details(int id)
        {

            var data = Crud<Evento>.Get(id).Result;
            return View(data);
        }

        // GET: EventosController/Create
        public ActionResult Create()
        {
            ViewBag.ListaEventos = ListaEventos();
            return View();
        }

        // POST: EventosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Evento evento)
        {
            try
            {
                Crud<Evento>.Create(evento).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al crear el evento");
                return View(evento);
            }
        }

        // GET: EventosController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ListaEventos = ListaEventos();
            var data = Crud<Evento>.Get(id).Result;
            return View(data);
        }

        private List<SelectListItem> ListaEventos()
        {
            var eventos = Crud<Evento>.GetAll().Result;
            var lista = eventos.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).ToList();
            return lista;
        }

        // POST: EventosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Evento evento)
        {
            try
            {
                Crud<Evento>.Update(id, evento).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al editar el evento");
                return View(evento);
            }
        }

        // GET: EventosController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Evento>.Get(id).Result;
            return View(data);
        }

        // POST: EventosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Evento evento)
        {

            try
            {
                Crud<Evento>.Delete(id).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar el Evento");
                return View(evento);
            }
        }
    }
}
