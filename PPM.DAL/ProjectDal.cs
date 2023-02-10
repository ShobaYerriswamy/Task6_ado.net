using System;
using System.Data;
using System.Diagnostics; 
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace PPM.DAL;

public class ProjectDal
{
    public void AddToProjectTable (int projectId, string projectName, string startDate, string endDate)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "INSERT INTO projects VALUES(" + projectId + ",'" + projectName + "','" + startDate + "','" + endDate + "')";
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

    public void ToViewProjectData ()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM projects;";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["projectId"] };
                if (dataTable.Rows.Count != 0)
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while(dataReader.Read())
                    {
                        Console.WriteLine("Project Id: " + dataReader[0] + "\n Project Name: " + dataReader[1] + "\n StartDate: " + dataReader[2] + "\n EndDate: " + dataReader[3]);
                    }
                }
                else
                {
                    Console.WriteLine(("No Project Available"));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
    }

    public void ViewProjectDataById (int projectId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM projects WHERE projectId = " + projectId + ";";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                if (ExistsInProjectTable(projectId))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Console.WriteLine("Project Id: " + dataReader[0] + "\n Project Name: " + dataReader[1] + "\n StartDate: " + dataReader[2] + "\n EndDate: " + dataReader[3]);
                    }
                }
                else
                {
                    Console.WriteLine("No Project Found with this Id");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);  
        }
    }

    public Boolean ExistsInProjectTable (int projectId)
    {
       try
       {

           string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM projects WHERE projectId = " + projectId + ";";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["projectId"] };
                if (dataTable.Rows.Contains(projectId))
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

    public void DeleteProjectFromTable (int projectId)
    { 
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "DELETE FROM projects WHERE projectId = " + projectId + ";";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Successfully Deleted");
            }
        }
        catch(Exception e) 
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
    }

    public void AddEmployeeToProject (int projectId, int employeeId)
    {
        try 
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "INSERT INTO projectswithemployees VALUES (" + projectId + "," + employeeId + ")";
                MySqlCommand command = new MySqlCommand(commandString, connection);               
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

    public void DeleteEmployeeFromProject (int projectId, int employeeId)
    {
        try 
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "DELETE FROM projectswithemployees WHERE projectId = " + projectId + " AND employeeId = " + employeeId + "";
                MySqlCommand command = new MySqlCommand(commandString, connection);               
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Successfully Deleted");
            }   
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
    }


    // ProjectswithEmployee Dal...
    public Boolean ExistsInProjectsWithEmployees (int projectId, int employeeId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM projectswithemployees WHERE projectId = " + projectId + " AND employeeId = " + employeeId + "";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["projectId"] };
                if (dataTable.Rows.Contains(projectId))
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

    public Boolean ExistsInProjectsWithEmployeesTable (int projectId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM projectswithemployees WHERE projectId = " + projectId +"";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    if (Convert.ToInt32(row["projectId"]) == projectId )
                    {
                        return true;
                    }
                }
            } 
        }
       catch (Exception e)
       {
            Console.WriteLine("OOPs, something went wrong.\n" + e);  
       }
       return false;
    }

    public void DeleteProjectFromProjectWithEmployees (int projectId)
    {
        try 
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "DELETE FROM projectswithemployees WHERE projectId = " + projectId + ";";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                if(ExistsInProjectsWithEmployeesTable(projectId))
                {             
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                else
                {
                    Console.WriteLine("With this ID no Project Exists");
                }
            }   
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
    }
}   
            

