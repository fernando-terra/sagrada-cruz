using System;

namespace br.com.sagradacruz.Models
{
    public class Pray
    {
        public string Author { get; set; }
        public string City { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public DateTime? Prayed { get; set; }
    }
}