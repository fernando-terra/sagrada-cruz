using br.com.sagradacruz.Connection;
using br.com.sagradacruz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace br.com.sagradacruz.DAO
{
    public class StatementDAO
    {
        Database _db = new Database();

        public List<StatementViewModel> GetStatements(bool onlyPublished=false)
        {
            var statements = new List<StatementViewModel>();
            var conn = _db.OpenConnection();
            try
            {
                var command = conn.CreateCommand();
                var cmdSql = new StringBuilder();

                cmdSql.AppendLine("SELECT creation_date");
                cmdSql.AppendLine(",author");
                cmdSql.AppendLine(",city");
                cmdSql.AppendLine(",content");
                cmdSql.AppendLine(",state");

                if (onlyPublished)
                {
                    cmdSql.AppendLine("WHERE state = 'P'");
                }

                cmdSql.AppendLine("FROM sagradacruz.statement");
                cmdSql.AppendLine("ORDER BY 1 DESC");

                command.CommandText = cmdSql.ToString();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    statements.Add(new StatementViewModel {
                        Author = reader.GetString("author") ?? "ANÔNIMO",
                        City = reader.GetString("city") ?? "ANÔNIMO",
                        Content = reader.GetString("content") ?? "SEM CONTEÚDO",
                        CreationDate = reader.GetDateTime("creation_date"),
                        Status = reader.GetString("state") ?? "R"
                    });
                }

                return statements;
            }
            catch (Exception)
            {
                return new List<StatementViewModel>();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
