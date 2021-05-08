using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestWebApplication.Models
{
    public class Post
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public int AuthorID { get; set; }
        public User User { get; set; }

        [Required]
        public string Title { get; set; }

        public string Themes { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string FrontContent { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        [AllowHtml]
        public string Content { get; set; }

        public int Likes { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublicationDate { get; set; }

        public ICollection<User> LikedUsers { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Post()
        {
            LikedUsers = new List<User>();
            Comments = new List<Comment>();
        }
    }
}