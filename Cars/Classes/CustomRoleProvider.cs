using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;

namespace Cars.Classes
{
	public class CustomRoleProvider:RoleProvider
	{
        SqlConnection connect = new SqlConnection("Data Source=KIRA\\PRO; Initial Catalog=CARS; Integrated Security=SSPI;");
		
		/*get all roles for user*/
		public override string[] GetRolesForUser(string username)
		{
			int userId = ReadSqlServer.ReadIdUser(connect, username);

			string[] roles = ReadSqlServer.ReadRolesInUser(connect, userId);

			return roles;
		}

		/*creating new role*/
		public override void CreateRole(string roleName)
		{
			string sql = "insert into Roles(Name) values('" + roleName + "')";

			WriteSqlServer.UpdateDBonQuery(connect, sql);
		}

		/*user belongs to the specified role*/
		public override bool IsUserInRole(string username, string roleName)
		{
			int userId = ReadSqlServer.ReadIdUser(connect, username);

			string role = ReadSqlServer.ReadRoleInUser(connect, userId, roleName);
			if (role == roleName)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			int[] userId = new int[usernames.Count()];
			int[] roleId = new int[roleNames.Count()];
			string sql = "insert into UserRole(id_user, id_role) ";
			string sqlBef = "";
			int i = 0;
			foreach (string userName in usernames)
			{
				userId[i] = ReadSqlServer.GetIdItem(connect,
					"select id_user from Users where Nick_name = '" + userName + "'");
				i++;
			}
			i = 0;
			foreach (string roleName in roleNames)
			{
				roleId[i] = ReadSqlServer.GetIdItem(connect, "select id from Roles where Name = '" + roleName + "'");
				i++;
			}



			foreach (int id in userId)
			{
				foreach (int rId in roleId)
				{
					sqlBef = "values('" + id + "','" + rId + "')";
					WriteSqlServer.UpdateDBonQuery(connect, sql + sqlBef);
				}
			}

		}

		public override string ApplicationName { get; set; }

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new NotImplementedException();
		}

		public override string Description
		{
			get { return base.Description; }
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new NotImplementedException();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			connect.Open();

			string sql = "select distinct Name from [Roles] where Name = '" + roleName + "'";
			string result = "";
			SqlCommand cmd = new SqlCommand(sql, connect);
			SqlDataReader reader = cmd.ExecuteReader();

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					result = reader.GetString(0);
				}
				reader.NextResult();
			}

			reader.Close();
			connect.Close();

			if (result == "" || result != roleName)
			{
				return false;
			}
			else return true;
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			return ReadSqlServer.ReadAllRoles(connect);
		}
	}
}