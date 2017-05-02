using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAssignment.Models;


namespace WebAssignment.Models
{
    public class CommentService
    {
        //Create method for comment, need to add syntax that connects the created comment on the selected movie id
        public static Comment createComment(Comment comment)
        {
            using (var dbconnection = DbService.getConnection())
            {
                dbconnection.Comment.Add(comment);
                dbconnection.SaveChanges();
                return comment;
            }
        }
    }
}