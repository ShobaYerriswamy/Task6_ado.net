using System;
using System.Data;
using System.Diagnostics; 
using System.Configuration;
using MySql.Data.MySqlClient;

namespace PPM.DAL;
public class EmployeeDal
{
    public void AddToEmployeeTable (int employeeId, string firstName, string lastName, string email, string mobileNumber, string address, int roleId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = @"INSERT INTO employees VALUES(" + employeeId + ",'" + firstName + "','" + lastName + "','" + email + "','" + mobileNumber + "','" + address + "'," + roleId + ");";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Added Succesfully...");
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
    }

    public void ToViewEmployeeData ()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = @"
                    SELECT employees.employeeId,employees.firstName,employees.lastName,employees.email,employees.mobileNumber,employees.roleId,roles.roleName
                    FROM employees
                    INNER JOIN roles
                    ON employees.RoleId = roles.RoleId;";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["employeeID"] };
                if (dataTable.Rows.Count != 0)
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Console.WriteLine("Employee Id: " + dataReader[0] + "\n Employee FirstName: " + dataReader[1] + "\n Employee LastName: " + dataReader[2] + "\n Employee Email: " + dataReader[3] + "\n Employee MobileNumber: " + dataReader[4] + "\n Employee Address: " + dataReader[5] + "\n RoleId: " + dataReader[6]);
                    }
                }
                else
                {
                    Console.WriteLine("No Emplooyee Found with this Id");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
    }

    public void ViewEmployeeDataById (int employeeId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = @"
                    SELECT employees.employeeId,employees.firstName,employees.lastName,employees.email,employees.mobileNumber,employees.roleId,roles.roleName
                    FROM employees
                    INNER JOIN roles
                    ON employees.RoleId = roles.RoleId WHERE employeeId = " + employeeId + ";";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                if (ExistsInEmployeeTable(employeeId))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Console.WriteLine("Employee Id: " + dataReader[0] + "\n Employee FirstName: " + dataReader[1] + "\n Employee LastName: " + dataReader[2] + "\n Employee Email: " + dataReader[3] + "\n Employee MobileNumber: " + dataReader[4] + "\n Employee Address: " + dataReader[5] + "\n RoleId: " + dataReader[6] );
                    }
                }
                else
                {
                    Console.WriteLine("No Emplooyee Found with this Id");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("OOPs, something went wrong.\n" + e);
        }
    }

    public Boolean ExistsInEmployeeTable (int employeeId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "SELECT * FROM employees WHERE employeeId = " + employeeId + "";
                MySqlCommand command = new MySqlCommand(commandString, connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["employeeId"] };
                if (dataTable.Rows.Contains(employeeId))
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

    public void DeleteEmployeeFromTable (int employeeId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString.ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string commandString = "DELETE FROM employees WHERE employeeId = " + employeeId + ";";
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
}
