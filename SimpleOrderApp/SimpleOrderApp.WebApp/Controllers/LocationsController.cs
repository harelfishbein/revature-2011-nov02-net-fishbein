using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderApp.Domain;

namespace SimpleOrderApp.WebApp.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ILocationRepository _repository;

        public LocationsController(ILocationRepository repository)
        {
            _repository = repository;
        }

        // GET: Locations
        public ActionResult Index()
        {
            var locations = _repository.GetAll();
            return View(locations);
        }

        // GET: Locations/Details/asdf
        public ActionResult Details(string id)
        {
            var location = _repository.GetAll().First(x => x.Name == id);
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Locations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // whenever a form is involved, usually at least 2 action methods are involved.
        // 1. regular action method, returning the view that will render the form.
        // 2. [HttpPost] action method (limited to HTTP POST method that forms use by default)
        //         receives the submitted form data.

        // GET: Locations/Delete/asdf
        public ActionResult Delete(string id)
        {
            var location = _repository.GetAll().First(x => x.Name == id);
            return View(location);
        }

        // POST: Locations/Delete/asdf
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            // this action method and the other Delete one are distinguished to ASP.NET
            // by the [HttpPost] or lack thereof.
            // c# needs an extra unused parameter for one of them just so it can compile as a method
            try
            {
                _repository.Delete(id);

                //why a redirect instead of "return View("Index")"
                return RedirectToAction(nameof(Index));
                //var locations = _repository.GetAll();
                //return View("Index", locations);
            }
            catch
            {
                // (should definitely be logging that exception)
                // recover, put the user someplace that makes sense...
                //  in this case, just give him the same form again to try again.
                //   (consider putting an error message on the page for a good user experience)
                var location = _repository.GetAll().First(x => x.Name == id);
                return View(location);
            }
        }
    }
}
