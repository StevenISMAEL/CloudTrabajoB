using CloudTrabajoBimestral.Consumer;
using CloudTrabajoBimestral.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudTrabajo.MVC.Controllers
{
    public class PonentesController : Controller
    {
        public ActionResult Index()
        {
            var data = Crud<Ponente>.GetAll().Result;
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var data = Crud<Ponente>.Get(id).Result;
            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.ListaPonentes = ListaPonentes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ponente ponente)
        {
            try
            {
                Crud<Ponente>.Create(ponente).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al crear el ponente");
                return View(ponente);
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ListaPonentes = ListaPonentes();
            var data = Crud<Ponente>.Get(id).Result;
            return View(data);
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
        public ActionResult Edit(int id, Ponente ponente)
        {
            try
            {
                Crud<Ponente>.Update(id, ponente).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al editar el ponente");
                return View(ponente);
            }
        }

        public ActionResult Delete(int id)
        {
            var data = Crud<Ponente>.Get(id).Result;
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Ponente ponente)
        {
            try
            {
                Crud<Ponente>.Delete(id).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar el ponente");
                return View(ponente);
            }
        }
    }
}
