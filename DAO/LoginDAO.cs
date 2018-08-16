using br.com.sagradacruz.Models;
using br.com.sagradacruz.Connection;
using System.Data;
using System;

namespace br.com.sagradacruz.DAO
{
    public class LoginDAO
    {
        Database _db = new Database();

        public bool Login(LoginViewModel user)
        {
            var conn = _db.OpenConnection();
            try
            {                
                var command = conn.CreateCommand();
                var cmdSql = @"SELECT usr.username, psw.password 
                                 FROM sagradacruz.users usr
                           INNER JOIN sagradacruz.passwords psw ON psw.user_id = usr.id
                                WHERE usr.username = @username
                                  AND psw.password = @password";

                command.CommandText = cmdSql;

                _db.AddInParameter(ref command, "@username", user.User);
                _db.AddInParameter(ref command, "@password", user.Password);

                var result = command.ExecuteScalar();

                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                throw new Exception("Cannot execute query. Method Login at LoginDAO.cs");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
