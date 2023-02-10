using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace PPM.DAL;

public class RoleDal
{
    public void AddToRoleTable (int roleId, string roleName)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "INSERT INTO roles VALUES(" + roleId + ",'" + roleName + "')";
                MySqlCommand command = new MySqlCommand(commandString , connection);
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Added Succesfully...");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
    }

    public void ToViewRoleData ()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM roles;";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                connection.Open();
                command.ExecuteNonQuery();
                MySqlDataReader dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    Console.WriteLine("Role Id: " + dataReader[0] + "\n Role Name: " + dataReader[1]);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
    }

    public void ViewRoleDataById (int roleId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM roles WHERE roleId = " + roleId + ";";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["roleId"] };
                if (dataTable.Rows.Contains(roleId))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Console.WriteLine("Role Id: " + dataReader[0] + "\n Role Name: [" + dataReader[1] + "]");
                    }
                }
                else
                {
                    Console.WriteLine("No Role Found with this Id");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);  
        }
    }
    public Boolean ExistsInRoleTable (int roleId)
    {   
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM roles WHERE roleId = " + roleId + ";";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["roleId"] };
                if (dataTable.Rows.Contains(roleId))
                {
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
        return false;
    }

    public Boolean ExistsRoleInEmployeeTable (int roleId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM employees WHERE roleId = " + roleId + ";";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["roleId"] };
                if (dataTable.Rows.Contains(roleId))
                {
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
        return false;
    }

    public void DeleteRoleFromTable (int roleId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "DELETE FROM roles WHERE roleId = " + roleId + ";";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                if (!ExistsRoleInEmployeeTable(roleId))
                {
                  if (ExistsInRoleTable(roleId))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("Deleted Successfully...");
                    }
                    else
                    {
                        Console.WriteLine("Role Id does not Exists...");
                    }   
                }
            
                else
                {
                    Console.WriteLine("Looks like Employee consists this Role ID, Delete Employee with this Role Id First");
                }
            }
        }
        
            
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);  
        }
    }
}
    
