using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace br.com.sagradacruz.Helpers
{
    public class Mail
    {
        public static void Send()
        {
            var mail = new Rebex.Mail.MailMessage
            {
                From = "pedidodeoracao@sagradacruz.com.br",
                To = "contato.sagradacruz@gmail.com",
                Subject = "Novo pedido de oração",
                BodyHtml = "<h1> Hello World</h1>"
            };
            
        }
    }
}
