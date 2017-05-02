using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAssignment.Models;

namespace WebAssignment.Models
{
    public class MovieService
    {
        //Fethces single movie by id, with all comments attached to this id
        public static Movie getMovie(int id)
        {
            using (var dbconnection = DbService.getConnection())
            {
                var movie = dbconnection.Movie.Include("Comment").Where(x => x.Id == id).SingleOrDefault();
                return movie;
            }
        }

        //Update method for movie table
        public static Movie updateMovie(Movie movie)
        {
            using (var dbconnection = DbService.getConnection())
            {
                Movie updatedMovie = (from m in dbconnection.Movie
                                      where m.Id == movie.Id
                                      select m).SingleOrDefault();

                updatedMovie.Title = movie.Title;
                updatedMovie.Genre = movie.Genre;
                updatedMovie.Rating = movie.Rating;
                updatedMovie.ReleaseDate = movie.ReleaseDate;
                updatedMovie.Description = movie.Description;
                dbconnection.SaveChanges();
                return updatedMovie;
            }
        }

        //Delete method for movie table
        public static Movie deleteMovie(Movie movie)
        {
            using (var dbconnection = DbService.getConnection())
            {
                Movie DeleteMovie = (from x in dbconnection.Movie
                                     where x.Id == movie.Id
                                     select x).SingleOrDefault();

                dbconnection.Movie.Remove(DeleteMovie);
                dbconnection.SaveChanges();
                return null;
            }
        }

        //Create method for movie table, which also includes the possibility to upload image
        public static Movie createMovie(Movie movie, HttpPostedFileBase file)
        {
            if (file != null)
            {
                String filename = Path.GetFileName(file.FileName);

                String filepath = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Img"), filename);

                file.SaveAs(filepath);
                movie.Picture = filename;
            }
            using (var dbconnection = DbService.getConnection())
            {
                dbconnection.Movie.Add(movie);
                dbconnection.SaveChanges();
                return movie;
            }
        }
    }
}