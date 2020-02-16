using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Kehyeedra3.Services.Models;
using Discord;
using System.Timers;

namespace Kehyeedra3.Services
{
    public class DatabaseService
    {/*
        private readonly string ConnectionString;
        private MySqlConnection Connection;
        public DatabaseService(string host, ushort port, string user, string password, string database)
        {
            ConnectionString = $"Server={host};Port={port};Database={database};Uid={user};Pwd={password}";
        }        
        private async Task ConnectOrCreateAsync()
        {
            if (Connection == null)
            {
                Connection = new MySqlConnection(ConnectionString);
            }
            if (Connection.State == System.Data.ConnectionState.Open)
                return;
            else
            {
                await Connection.OpenAsync();
                return;
            }
        }
        public async Task<bool> CreateUserAsync(IUser user)
        {
            await ConnectOrCreateAsync();

            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (UserID, Avatar, UName) VALUES (@uid, @avatar, @uname);"); //userinfo

            command.Parameters.AddWithValue("@uid", user.Id);
            command.Parameters.AddWithValue("@avatar", user.GetAvatarUrl());
            command.Parameters.AddWithValue("@uname", user.Username);

            command.Connection = Connection;
            try
            {
                await command.ExecuteScalarAsync();
                command.Dispose();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                command.Dispose();
            }
            return false;
        }

        public async Task<bool> DoesUserExistAsync(IUser user)
        {
            await ConnectOrCreateAsync();

            var command = new MySqlCommand("SELECT 1 FROM `users` WHERE UserID = @userId;");

            command.Connection = Connection;

            command.Parameters.AddWithValue("@userId", user.Id);

            return Convert.ToBoolean(await command.ExecuteScalarAsync());
        }
        public async Task<DatabaseUser> GetUserAsync(ulong userID)
        {
            var command = new MySqlCommand("SELECT * FROM `users` WHERE UserID = @userId;");

            command.Connection = Connection;
            command.Parameters.AddWithValue("@userId", userID);

            var result = await command.ExecuteReaderAsync();
            if (result.HasRows)
            {
                while(await result.ReadAsync())
                {
                    return new DatabaseUser //userinfo
                    {
                        UserID = ulong.Parse(result["UserId"].ToString()),
                        Avatar = result["Avatar"].ToString(),
                        UName = result["UName"].ToString(),
                    };
                }
            }
            return null;
        }
        public async Task<bool> CreateReminderAsync(ulong rUserID, string rMessage, ulong rSend)
        {
            await ConnectOrCreateAsync();

            MySqlCommand command = new MySqlCommand("INSERT INTO `reminders` (UserID, RMessage, RSend) VALUES (@uid, @rmsg, @rsend);"); //reminderinfo

            command.Parameters.AddWithValue("@uid", rUserID);
            command.Parameters.AddWithValue("@rmsg", rMessage);
            command.Parameters.AddWithValue("@rsend", rSend);
            command.Connection = Connection;
            try
            {
                await command.ExecuteScalarAsync();
                command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                command.Dispose();
            }
            return false;
        }
        public async Task<bool> DoesReminderExistAsync(IUser user)
        {
            await ConnectOrCreateAsync();

            var command = new MySqlCommand("SELECT 1 FROM `reminders` WHERE UserID = @userId;");

            command.Connection = Connection;

            command.Parameters.AddWithValue("@userId", user.Id);

            return Convert.ToBoolean(await command.ExecuteScalarAsync());
        }
        public async Task<DatabaseReminder> GetReminderAsync(ulong userID)
        {
            var command = new MySqlCommand("SELECT * FROM `reminders` WHERE UserID = @userId;");

            command.Connection = Connection;
            command.Parameters.AddWithValue("@userId", userID);

            var result = await command.ExecuteReaderAsync();
            if (result.HasRows)
            {
                while (await result.ReadAsync())
                {
                    return new DatabaseReminder //reminderinfo
                    {
                        rUserID = ulong.Parse(result["rUserID"].ToString()),
                        rMessage = result["rMessage"].ToString(),
                        rSend = ulong.Parse(result["rSend"].ToString())
                    };
                }
            }
            return null;
        }
        public async Task<List<DatabaseReminder>> GetAllReminderAsync()
        {
            var command = new MySqlCommand("SELECT * FROM `reminders`;");

            command.Connection = Connection;

            var Reminders = new List<DatabaseReminder>();
            var result = await command.ExecuteReaderAsync();
            if (result.HasRows)
            {
                while (await result.ReadAsync())
                {
                    Reminders.Add (new DatabaseReminder //reminderinfo
                    {
                        rUserID = ulong.Parse(result["rUserID"].ToString()),
                        rMessage = result["rMessage"].ToString(),
                        rSend = ulong.Parse(result["rSend"].ToString())
                    });
                }
                return Reminders;
            }
            return null;
        }
        */
    }
}
