using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebApplication.Models
{
    public class Comment
    {
        public int ID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int PostID { get; set; }
        public Post Post { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PostDate { get; set; }
    }
}