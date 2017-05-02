using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAssignment.Models;

namespace WebAssignment.Controllers
{
    public class SuggestionController : Controller
    {

//Controller is used to delegate methods from class SuggestionService
        public ActionResult ShowSuggestions()
        {
            return View(SuggestionService.getSuggestions());
        }

        public ActionResult SelectSuggestion(int id)
        {
            Recommendation recommendation = SuggestionService.getSuggestion(id);
            return View(recommendation);
        }

        [HttpGet]
        public ActionResult EditSuggestion(int id)
        {
            Recommendation recommendation = SuggestionService.getSuggestion(id);
            return View(recommendation);
        }

        [HttpPost]
        public ActionResult EditSuggestion(Recommendation recommendation)
        {
            if (ModelState.IsValid)
            { 
                SuggestionService.updateSuggestion(recommendation);
                return RedirectToAction("ShowSuggestions");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteSuggestion(int id)
        {
            Recommendation recommendationToDelete = SuggestionService.getSuggestion(id);
            return View(recommendationToDelete);
        }

        [HttpPost]
        public ActionResult DeleteSuggestion(Recommendation recommendation)
        {
            SuggestionService.deleteSuggestion(recommendation);
            return RedirectToAction("ShowSuggestions");
        }

        [HttpGet]
        public ActionResult AddSuggestion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSuggestion(Recommendation recommendation)
        {
            if(ModelState.IsValid)
            { 
                SuggestionService.createSuggestion(recommendation);
                return RedirectToAction("ShowSuggestions");
            }
            else
            {
                return View();
            }
        }
    }
}