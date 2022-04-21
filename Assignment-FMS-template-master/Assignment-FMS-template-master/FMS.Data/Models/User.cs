using System;
namespace FMS.Data.Models {
    
    public enum Role { admin, manager, guest }

    public class User {
        public int Id { get; set; }
        //user ID
        public string Name { get; set; }
        //user name
        public string Email { get; set; }
        //user email
        public string Password { get; set; }
        //user password
        public Role Role { get; set; }
        //user role

        
    }
}
