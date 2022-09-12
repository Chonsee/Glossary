using Glossary.Domain;
using Glossary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Glossary.Controllers
{
    /// <summary>
    /// This is the home controller, where CRUD operations are preformed relating to basic Glossary functionality. 
    /// </summary>
    public class HomeController : Controller
    {

        private readonly TermService _termService;

        public HomeController(TermService termService)
        {
            _termService = termService;
        }

        /// <summary>
        /// This is the default route for this application. 
        /// </summary>
        /// <returns>The starting page of the application.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This page is a GUI for browsing through all added terms.
        /// </summary>
        /// <returns>A GUI for browsing through all added terms.</returns>
        public IActionResult Browse()
        {
            var terms = _termService.GetAllTerms(); //TODO: Ideally pagination would be implemented here, this would not scale well. 
            var model = new BrowseModel();
            model.TermModels = terms;
            return View(model);
        }

        /// <summary>
        /// This endpoint removes an existing term from the db. 
        /// </summary>
        /// <param name="id">The Id of the existing term.</param>
        /// <returns>Directs the user to the browse page where they can see there entry, and many more on successful submission. Error page otherwise.</returns>
        [HttpPost]
        public IActionResult Delete(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction(nameof(this.Error));

            var term = _termService.GetTermById(id);

            if (term == null)
                return RedirectToAction(nameof(this.Error));

            _termService.Delete(term);

            return RedirectToAction(nameof(this.Browse));
        }

        /// <summary>
        /// This page is a GUI for editing an existing term. 
        /// </summary>
        /// <param name="id">The Id of the existing term.</param>
        /// <returns>A GUI for editing an existing term.</returns>
        public IActionResult Edit(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction(nameof(this.Error));

            var term = _termService.GetTermById(id);

            if (term == null)
                return RedirectToAction(nameof(this.Error));

            var model = new TermModel()
            {
                Id = term.Id,
                TermNameOrPhrase = term.TermNameOrPhrase,
                Definition = term.Definition
            };

            return View(model);
        }

        /// <summary>
        /// This is the endpoint used for submitting an update request on a term. 
        /// </summary>
        /// <param name="model">Representation of the term to be recorded in the DB.</param>
        /// <returns>Directs the user to the browse page where they can see there entry, and many more on successful submission. Error page otherwise.</returns>
        [HttpPost]
        public IActionResult EditSubmit(TermModel model)
        {
            if(model.IsValid())
            {
                //get the term to update, 
                var term = _termService.GetTermById(model.Id);

                if (term == null) //If not found, something went wrong. Oops 404 not found is also acceptable. 
                    return RedirectToAction(nameof(this.Error));

                term.TermNameOrPhrase = model.TermNameOrPhrase;
                term.Definition = model.Definition;

                _termService.Update(term);

                return RedirectToAction(nameof(this.Browse)); //success
            }

            //If here, error
            return RedirectToAction(nameof(this.Error));
        }

        /// <summary>
        /// This is the endpoint for adding in a new term with definition. 
        /// </summary>
        /// <param name="model">Representation of the term to be recorded in the DB.</param>
        /// <returns>Directs the user to the browse page where they can see there entry, and many more on successful submission. Error page otherwise.</returns>
        [HttpPost]
        public IActionResult Add(TermModel model)
        {
            if (model.IsValid())
            {
                //Add in the term to the db
                var term = new Term
                {
                    TermNameOrPhrase = model.TermNameOrPhrase,
                    Definition = model.Definition
                };
                _termService.Add(term);

                return RedirectToAction(nameof(this.Browse)); //Success
            }

            //If here, error
            return RedirectToAction(nameof(this.Error));
        }

        /// <summary>
        /// This is the intended catch all error page for routes associated with this controller. 
        /// </summary>
        /// <returns>The application error page</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.Message = "Oops, something went wrong!";
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
