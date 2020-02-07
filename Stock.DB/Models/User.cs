using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stock.DB.Models
{
    public class User
    {
        [Key]
        public Int32 Id { get; set; }
        public String Name { get; set; }
        [Required]
        public String Login { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
        public Role Role { get; set; }
        public IEnumerable<Order> Orders { get; set; }

        public User()
        {
            this.Orders = new List<Order>();
        }
    }

    public enum Role
    {
        User,
        Admin
    }
}
