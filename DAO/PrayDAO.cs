using br.com.sagradacruz.Models;
using System;
using br.com.sagradacruz.Connection;
using System.Collections.Generic;
using System.Text;

namespace br.com.sagradacruz.DAO
{
    public class PrayDAO
    {
        Database _db = new Database();

        public List<Pray> GetPray()
        {
            var prays = new List<Pray>();
            var conn = _db.OpenConnection();
            try
            {
                var command = conn.CreateCommand();
                var cmdSql = new StringBuilder();

                cmdSql.AppendLine("SELECT id");
                cmdSql.AppendLine(",content");
                cmdSql.AppendLine(",author");
                cmdSql.AppendLine(",city");
                cmdSql.AppendLine(",creation_date");
                cmdSql.AppendLine(",prayed");
                cmdSql.AppendLine("FROM sagradacruz.pray");
                cmdSql.AppendLine("ORDER BY 5 DESC");
                
                command.CommandText = cmdSql.ToString();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    prays.Add(new Pray
                    {
                        Author = reader.GetString("author") ?? "ANÔNIMO",
                        City = reader.GetString("city") ?? "ANÔNIMO",
                        Content = reader.GetString("content") ?? "SEM CONTEÚDO"
                    });
                }

                return prays;
            }
            catch (Exception)
            {
                return new List<Pray>();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public bool CreatePray(Pray pray)
        {
            var conn = _db.OpenConnection();
            try
            {
                var command = conn.CreateCommand();
                var cmdSql = @"INSERT INTO sagradacruz.pray
                               (author, creation_date, city, content, prayed)
                               VALUES 
                               (@author, current_date(), @city, @content, NULL)";

                command.CommandText = cmdSql;

                _db.ClearParameters(ref command);
                _db.AddInParameter(ref command, "@author", pray.Author ?? "ANONIMO");
                _db.AddInParameter(ref command, "@city", pray.City ?? "NAO IDENTIFICADO");
                _db.AddInParameter(ref command, "@content", pray.Content ?? "VAZIO");

                var result = command.ExecuteNonQuery();

                return (result > 0);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public bool UpdatePray(int id)
        {
            var conn = _db.OpenConnection();
            try
            {
                var command = conn.CreateCommand();
                var cmdSql = @"UPDATE sagradacruz.pray
                               SET prayed = current_date()
                               WHERE id = @id";

                command.CommandText = cmdSql;

                _db.ClearParameters(ref command);
                _db.AddInParameter(ref command, "@id", id);

                var result = command.ExecuteNonQuery();

                return (result > 0);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
