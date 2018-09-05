using System;

namespace br.com.sagradacruz.Areas.Admin.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DesactivationDate { get; set; }
        public string Password { get; set; }
        public string Tip { get; set; }

        public string HashPass
        {            
            get { return Utils.Base64Encode(this.Password); }
        }
    }    
}