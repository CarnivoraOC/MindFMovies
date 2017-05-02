using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAssignment.Models
{
    //Method for calling db connection
    public abstract class DbService
    {
        public static MindFMoviesDBEntities2 getConnection()
        {
            try
            { 
                return new MindFMoviesDBEntities2();
            }
            catch
            {
                return null;
            }
        }
    }
}