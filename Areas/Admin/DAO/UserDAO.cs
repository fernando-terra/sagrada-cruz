using br.com.sagradacruz.Areas.Admin.Models;
using br.com.sagradacruz.Connection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace br.com.sagradacruz.Areas.Admin.DAO
{
    public class UserDAO
    {
        Database _db = new Database();

        public int CreateUser(User user)
        {
            var conn = _db.OpenConnection();
            try
            {
                var newId = GetNewId();

                var command = conn.CreateCommand();
                var cmdSql = @" INSERT INTO sagradacruz.users
                                (
                                    id,
	                                name,
	                                username,
	                                email,
	                                creation_date,
	                                desactivation_date
                                )
                                VALUES
                                (
                                    @id,
	                                @name,
	                                @username,
	                                @email,
	                                current_date(),
	                                NULL
                                )";

                command.CommandText = cmdSql;

                _db.ClearParameters(ref command);
                _db.AddInParameter(ref command, "@id", newId);
                _db.AddInParameter(ref command, "@name", user.Name);
                _db.AddInParameter(ref command, "@username", user.Username);
                _db.AddInParameter(ref command, "@email", user.Email);

                var result = command.ExecuteNonQuery();              

                if (result > 0)
                {
                    // REGISTRY PASSWORD
                    cmdSql = @" INSERT INTO sagradacruz.passwords
                                (
	                                user_id,
	                                password,
	                                creation_date,
	                                desactivation_date,
	                                tip
                                )
                                VALUES
                                (
	                                @user_id,
	                                @password,
	                                current_date(),
	                                NULL,
	                                @tip
                                )";

                    command.CommandText = cmdSql;

                    _db.ClearParameters(ref command);
                    _db.AddInParameter(ref command, "@user_id", newId);
                    _db.AddInParameter(ref command, "@password", user.HashPass);
                    _db.AddInParameter(ref command, "@tip", user.Tip);

                    result = command.ExecuteNonQuery();

                    return newId;
                }
                else
                {
                    throw new Exception("Cannot insert new user in user's table. Method CreateUser at UserDAO.cs");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("The user cannot be created. Method CreateUser at UserDAO.cs: " + ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public bool RemoveUser(int id)
        {
            return true;
        }

        public User EditUser(User user)
        {
            return null;
        }

        private int GetNewId()
        {
            var id = 0;
            var conn = _db.OpenConnection();
            try
            {
                var cmdSql = @"SELECT ((COALESCE(MAX(id), 0)) + 1) NEWID FROM sagradacruz.users";

                var command = conn.CreateCommand();
                command.CommandText = cmdSql;

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt32("NEWID");
                }

                reader.Close();

                return id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
