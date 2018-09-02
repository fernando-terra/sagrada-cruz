using br.com.sagradacruz.Connection;
using br.com.sagradacruz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace br.com.sagradacruz.DAO
{
    public class StatementDAO
    {
        Database _db = new Database();

        public List<StatementViewModel> GetStatements()
        {
            var statements = new List<StatementViewModel>();
            var conn = _db.OpenConnection();
            try
            {
                var command = conn.CreateCommand();
                var cmdSql = @"SELECT creation_date
                                     ,author
                                     ,city
                                     ,content
                                 FROM sagradacruz.statement
                                ORDER BY 1 DESC";

                command.CommandText = cmdSql;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    statements.Add(new StatementViewModel {
                        Author = reader.GetString("author") ?? "ANÔNIMO",
                        City = reader.GetString("city") ?? "ANÔNIMO",
                        Content = reader.GetString("content") ?? "SEM CONTEÚDO",
                        CreationDate = reader.GetDateTime("creation_date")
                    });
                }

                return statements;
            }
            catch (Exception)
            {
                throw new Exception("Cannot execute query. Method GetStatements at StatementDAO.cs");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
