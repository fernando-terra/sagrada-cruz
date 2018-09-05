using System;

namespace br.com.sagradacruz.Models
{
    public class Member
    {
        public string Name { get; set; }
        public string Instagram { get; set; }
        public DateTime Birthday { get; set; }
        public string Function { get; set; }
        public string Pray { get; set; }
        public string Image { get; set; }

        public int Age
        { get
            {
                var bday = Birthday;
                var age = Convert.ToInt32(DateTime.Now.Year - Birthday.Year);

                if (DateTime.Now.Month < bday.Month || (DateTime.Now.Month == bday.Month && DateTime.Now.Day < bday.Day))
                {
                    age--;
                }                    

                return age;
            }
        }
    }
}
