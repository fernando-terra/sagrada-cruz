using br.com.sagradacruz.Areas.Admin.Models;
using br.com.sagradacruz.Connection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    _db.AddInParameter(ref command, "@tip", (user.Tip ?? string.Empty));

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
            var conn = _db.OpenConnection();
            try
            {
                var command = conn.CreateCommand();
                var cmdSql = @"DELETE FROM sagradacruz.users where id = @id";

                command.CommandText = cmdSql;

                _db.ClearParameters(ref command);
                _db.AddInParameter(ref command, "@id", id);

                var result = command.ExecuteNonQuery();

               return (result > 0 ? true : false);
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

        public bool EditUser(User user)
        {
            var conn = _db.OpenConnection();
            try
            {
                var command = conn.CreateCommand();
                var cmdSql = new StringBuilder();

                cmdSql.AppendLine("UPDATE sagradacruz.users SET");
                cmdSql.AppendLine(" name = @name");
                cmdSql.AppendLine(",username = @username");
                cmdSql.AppendLine(",email = @email");
                cmdSql.AppendLine("WHERE id = @id");
                
                command.CommandText = cmdSql.ToString();

                _db.ClearParameters(ref command);
                _db.AddInParameter(ref command, "@name", user.Name);
                _db.AddInParameter(ref command, "@username", user.Username);
                _db.AddInParameter(ref command, "@email", user.Email);
                _db.AddInParameter(ref command, "@id", user.Id);

                var result = command.ExecuteNonQuery();

                /*******************************************************************/

                if(result > 0)
                {
                    cmdSql = new StringBuilder();
                    cmdSql.AppendLine("UPDATE sagradacruz.passwords SET");
                    cmdSql.AppendLine(" desactivation_date = current_date()");
                    cmdSql.AppendLine("WHERE user_id = @id");

                    command.CommandText = cmdSql.ToString();

                    _db.ClearParameters(ref command);                    
                    _db.AddInParameter(ref command, "@id", user.Id);

                    result = command.ExecuteNonQuery();

                    /*******************************************************************/

                    if (result > 0)
                    {
                        cmdSql = new StringBuilder();
                        cmdSql.AppendLine("INSERT INTO sagradacruz.passwords (");
                        cmdSql.AppendLine("user_id, password, creation_date, desactivation_date, tip) ");
                        cmdSql.AppendLine("VALUES (");
                        cmdSql.AppendLine("@user_id, @password, current_date(), NULL, @tip)");

                        command.CommandText = cmdSql.ToString();

                        _db.ClearParameters(ref command);
                        _db.AddInParameter(ref command, "@user_id", user.Id);
                        _db.AddInParameter(ref command, "@password", user.HashPass);
                        _db.AddInParameter(ref command, "@tip", (user.Tip ?? string.Empty));

                        result = command.ExecuteNonQuery();
                    }
                }

                return (result > 0 ? true : false);
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

        public List<User> GetUser(int id = 0)
        {
            var conn = _db.OpenConnection();
            var user = new List<User>();
            var cmdSql = new StringBuilder();

            try
            {
                cmdSql.AppendLine("SELECT usr.id, usr.username, psw.password, usr.name, psw.tip, usr.email");
                cmdSql.AppendLine("FROM sagradacruz.users usr");
                cmdSql.AppendLine("INNER JOIN sagradacruz.passwords psw ON psw.user_id = usr.id");
                cmdSql.AppendLine("WHERE usr.desactivation_date IS NULL");
                cmdSql.AppendLine("AND psw.desactivation_date IS NULL");

                if (id > 0)
                {
                    cmdSql.AppendLine("AND usr.id = @id");
                }
                
                var command = conn.CreateCommand();
                command.CommandText = cmdSql.ToString();

                if(id > 0)
                {
                    _db.AddInParameter(ref command, "@id", id);
                }

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    user.Add(new User
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("name"),
                        Username = reader.GetString("username"),
                        Email = reader.GetString("email"),
                        Password = Utils.Base64Decode(reader.GetString("password")),
                        Tip = reader.GetString("tip") ?? string.Empty
                    });                    
                }

                reader.Close();

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
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
