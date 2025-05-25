using CloudTrabajoBimestral.Consumer;
using CloudTrabajoBimestral.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudTrabajo.MVC.Controllers
{
    public class EspaciosController : Controller
    {
        // GET: EspaciosController
        public ActionResult Index()
        {
            var data = Crud<Espacio>.GetAll().Result;
            return View(data);
        }

        // GET: EspaciosController/Details/5
        public ActionResult Details(int id)
        {

            var data = Crud<Espacio>.Get(id).Result;
            return View(data);
        }

        // GET: EspaciosController/Create
        public ActionResult Create()
        {
            ViewBag.ListaEspacios = ListaEspacios();
            return View();
        }

        // POST: EspaciosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Espacio espacio)
        {
            try
            {
                Crud<Espacio>.Create(espacio).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al crear el Espacio");
                return View(espacio);
            }
        }

        // GET: EspaciosController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.ListaEspacios = ListaEspacios();
            var data = Crud<Espacio>.Get(id).Result;
            return View(data);
        }

        private List<SelectListItem> ListaEspacios()
        {
            var espacios = Crud<Espacio>.GetAll().Result;
            var lista = espacios.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).ToList();
            return lista;
        }

        // POST: EspaciosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Espacio espacio)
        {
            try
            {
                Crud<Espacio>.Update(id, espacio).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al editar el espacio");
                return View(espacio);
            }
        }

        // GET: EspaciosController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = Crud<Espacio>.Get(id).Result;
            return View(data);
        }

        // POST: EspaciosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Espacio espacio)
        {

            try
            {
                Crud<Espacio>.Delete(id).Wait();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Error al eliminar el Espacio");
                return View(espacio);
            }
        }
        
    }
}
