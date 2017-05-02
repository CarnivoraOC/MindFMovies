using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace WebAssignment.Models
{
    public class SuggestionService
    {
        //Fetches a list of all current suggestions in db
        public static List<Recommendation> getSuggestions()
        {
            List<Recommendation> suggestionlist = new List<Recommendation>();
            using (var dbconnection = DbService.getConnection())
            {
                suggestionlist = (from x in dbconnection.Recommendation
                                  select x).ToList();
            }
            return suggestionlist;
        }

        //Fetches a single suggestion in recommendation db
        public static Recommendation getSuggestion(int id)
        {
            using (var dbconnection = DbService.getConnection())
            {
                Recommendation suggestion = (from s in dbconnection.Recommendation
                               where s.Id == id
                               select s).SingleOrDefault();
                return suggestion;
            }
        }

        //Update method for recommendation table
        public static Recommendation updateSuggestion(Recommendation recommendation)
        {
            using (var dbconnection = DbService.getConnection())
            {
                Recommendation updatedSuggestion = (from s in dbconnection.Recommendation
                                      where s.Id == recommendation.Id
                                      select s).SingleOrDefault();

                updatedSuggestion.Title = recommendation.Title;
                updatedSuggestion.ReleaseDate = recommendation.ReleaseDate;
                updatedSuggestion.Review = recommendation.Review;
                dbconnection.SaveChanges();
                return updatedSuggestion;
            }
        }

        //Delete method for recommendation table
        public static Recommendation deleteSuggestion(Recommendation recommendation)
        {
            using (var dbconnection = DbService.getConnection())
            {
                Recommendation DeleteSuggestion = (from x in dbconnection.Recommendation
                                     where x.Id == recommendation.Id
                                     select x).SingleOrDefault();

                dbconnection.Recommendation.Remove(DeleteSuggestion);
                dbconnection.SaveChanges();
                return null;
            }
        }

        //Create method for recommendation table
        public static Recommendation createSuggestion(Recommendation recommendation)
        {
            using (var dbconnection = DbService.getConnection())
            {
                dbconnection.Recommendation.Add(recommendation);
                dbconnection.SaveChanges();
                return recommendation;
            }
        }
    }
}