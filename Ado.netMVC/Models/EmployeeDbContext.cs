using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Ado.netMVC.Models
{
    public class EmployeeDbContext
    {
        string conString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;


        //Method to get all employees using ado.net
        public List<EmployeeModel> GetEmployeesList()
        {
            List<EmployeeModel> employeesList = new List<EmployeeModel>();

            //making sqlconn
            SqlConnection con = new SqlConnection(conString);

            SqlCommand cmd = new SqlCommand("spGetEmployees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            //Read all data in dr
            while (dr.Read())
            {
                EmployeeModel emp = new EmployeeModel();
                emp.Id = Convert.ToInt32(dr["Id"].ToString());
                emp.EmpName = dr["EmpName"].ToString();
                emp.EmpEmail = dr["EmpEmail"].ToString();
                emp.EmpSalary = Convert.ToInt32(dr["EmpSalary"].ToString());
                employeesList.Add(emp);
            }

            con.Close();

            return employeesList;
        }


        //Method to add new employ using ado.net

        public bool addEmploy(EmployeeModel emp)
        {
            SqlConnection con = new SqlConnection(conString);

            SqlCommand cmd = new SqlCommand("spAddEmploy", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", emp.EmpName);
            cmd.Parameters.AddWithValue("@email", emp.EmpEmail);
            cmd.Parameters.AddWithValue("@salary", emp.EmpSalary);
            con.Open();

            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}