using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAssignment.Models;
using System.IO;

namespace WebAssignment.Controllers
{
    public class MovieController : Controller
    {
        //Shows all current movies in db
        public ActionResult Index(string search, string sort)
        {
            MindFMoviesDBEntities2 db = null;
            db = DbService.getConnection();
            ViewBag.SortByTitle = string.IsNullOrEmpty(sort) ? "Descending Title" : "";
            ViewBag.SortByGenre = sort == "Genre" ? "Descending Genre" : "Genre";
            ViewBag.SortByReleaseDate = sort == "ReleaseDate" ? "Descending ReleaseDate" : "ReleaseDate";
            ViewBag.SortByRating = sort == "Rating" ? "Descending Rating" : "Rating";
            
            //Search function
            var records = db.Movie.AsQueryable();
            records = records.Where(x => x.Title == search || search == null);

            //Sorting function
            switch (sort)
            {
                case "Descending Title":
                    records = records.OrderByDescending(x => x.Title);
                    break;
                case "Descending Genre":
                    records = records.OrderByDescending(x => x.Genre);
                    break;
                case "Descending ReleaseDate":
                    records = records.OrderByDescending(x => x.ReleaseDate);
                    break;
                case "Descending Rating":
                    records = records.OrderByDescending(x => x.Rating);
                    break;
                case "Genre":
                    records = records.OrderBy(x => x.Genre);
                    break;
                case "ReleaseDate":
                    records = records.OrderBy(x => x.ReleaseDate);
                    break;
                case "Rating":
                    records = records.OrderBy(x => x.Rating);
                    break;
                default:
                    records = records.OrderBy(x => x.Title);
                    break;
            }
            db = null;
            return View(records);
        }

//Controller is used to delegate methods from class MovieService
        public ActionResult SelectMovie(int id)
        {
            Movie movie = MovieService.getMovie(id);
            return View(movie);
        }

        [HttpGet]
        public ActionResult EditMovie(int id)
        {
            Movie movie = MovieService.getMovie(id);
            return View(movie);
        }

        [HttpPost]
        public ActionResult EditMovie(Movie movie)
        {
            if (ModelState.IsValid)
            { 
                MovieService.updateMovie(movie);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DeleteMovie(int id)
        {
            Movie movieToDelete = MovieService.getMovie(id);
            return View(movieToDelete);
        }

        [HttpPost]
        public ActionResult DeleteMovie(Movie movie)
        {
            MovieService.deleteMovie(movie);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(Movie movie, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            { 
                MovieService.createMovie(movie, file);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}
