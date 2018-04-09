using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace AContact.AContactClasses
{
    class ContactClass
    {
        // Getter and Setters properties 
        // Acts as a data carrier in this application 
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }   public string Status { get; set; }
       
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;


        // Selecting data from database 
        public DataTable Select()
        {
            // step 1: Database connection 
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // step 2: Writing sql query
                string sql = "SELECT * FROM tbl_contact";
                // Creating cmd using sql and conn 
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Creating sql DataAdappter using cmd 
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        // Inserting into database 
        public bool Insert(ContactClass c)
        {
            // Creating a default returntype and setting its value to false 
            bool isSuccess = false;

            // Step 1. Connect database 
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                // Step 2: Create a SQL query to insert data
          string sql = "INSERT INTO tbl_contact (FirstName, LastName, Phone, Email, Note, Status) VALUES(@FirstName, @LastName, @Phone, @Email, @Note, @Status)";
                // Creating SQL Command using sql and conn 
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Create Parameters to add data 
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@Phone", c.Phone);
                cmd.Parameters.AddWithValue("@Email", c.Email);
                cmd.Parameters.AddWithValue("@Note", c.Note);
                cmd.Parameters.AddWithValue("@Status", c.Status);

                // Connection Open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // If the query runs succesfully then the value is > 0, else the value will be < 0 
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
       
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        // Method to update data in database 
        public bool Update(ContactClass c)
        {
            // Create a default return type and set its default value to false 
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                // SQL to update data in hour database 
string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, Phone=@Phone, Email=@Email, Note=@Note, Status=@Status WHERE ContactID=@ContactID";

                // Creating SQL command
                SqlCommand cmd = new SqlCommand(sql, conn);
                // Create Parameters to add value
                cmd.Parameters.AddWithValue("@FirstName",   c.FirstName);
                cmd.Parameters.AddWithValue("@LastName",   c.LastName);
                cmd.Parameters.AddWithValue("@Phone",       c.Phone);
                cmd.Parameters.AddWithValue("@Email",       c.Email);
                cmd.Parameters.AddWithValue("@Note",        c.Note);
                cmd.Parameters.AddWithValue("@Status",      c.Status);
                cmd.Parameters.AddWithValue("@ContactID",   c.ContactID);

                // Open database connection 
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                // If the query runs succesfully then the value of rows > 0 else its value will be 0 
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }

            finally
            {
                conn.Close(); 
            }
            return isSuccess; 
        }

        // Method to delete data from database 
        public bool Delete(ContactClass c)
        {
            // create a default return value and set its value to false 
            bool isSuccess = false;
            // Create SQL Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //  Method to delete data 
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";

                // Creating SQL command   
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                // Open Connection 
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // if the query runs succesfully then the value of rows will be > 0 , else the value will be 0 

                if (rows > 0)

                 
                    {
                      isSuccess = true; 
                    } 
                
                else
                {
                        isSuccess = false; 

                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                // Close connection 
                conn.Close();
            }
            return isSuccess;
        }


    }

}

