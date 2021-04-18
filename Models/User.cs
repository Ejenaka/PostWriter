using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TestWebApplication.Models
{
    public class User
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
        public int BlogsCount { get; set; }

        public ICollection<Post> LikedPosts { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public User()
        {
            LikedPosts = new List<Post>();
            Comments = new List<Comment>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
                return false;

            User other = obj as User;

            return ID == other.ID && Name == other.Name;
        }

        public override int GetHashCode()
        {
            int hashCode = 1479869798;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}