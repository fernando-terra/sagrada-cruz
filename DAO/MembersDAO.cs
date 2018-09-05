using br.com.sagradacruz.Connection;
using br.com.sagradacruz.Models;
using System;
using System.Collections.Generic;

namespace br.com.sagradacruz.DAO
{
    public class MembersDAO
    {
        Database _db = new Database();

        public List<Member> GetMembers()
        {
            var members = new List<Member>();
            var conn = _db.OpenConnection();
            try
            {
                var command = conn.CreateCommand();
                var cmdSql = @"SELECT name
	                                 ,birthday
                                     ,instagram
                                     ,pray
                                     ,function
                                     ,image
                                FROM sagradacruz.members 
                                ORDER BY birthday ASC";

                command.CommandText = cmdSql;

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    members.Add(new Member
                    {
                        Name = reader.GetString("name") ?? "ANONIMO",
                        Function = reader.GetString("function") ?? string.Empty,
                        Instagram = reader.GetString("instagram") ?? string.Empty,
                        Birthday = reader.GetDateTime("birthday"),
                        Pray = reader.GetString("pray") ?? string.Empty,
                        Image = reader.GetString("image") ?? string.Empty
                    });
                }

                return members;
            }
            catch (Exception)
            {
                throw new Exception("Cannot execute query. Method GetMembers at MembersDAO.cs");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
