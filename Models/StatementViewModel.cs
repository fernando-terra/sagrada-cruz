using System;

namespace br.com.sagradacruz.Models
{
    public class StatementViewModel
    {
        public int Id { get; set; }
        public string Author { get; set; }        
        public string City { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
    }
}
