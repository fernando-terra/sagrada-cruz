using System;

namespace br.com.sagradacruz.Models
{
    public class Session
    {
        public string User { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public bool Status { get; set; }
    }
}
