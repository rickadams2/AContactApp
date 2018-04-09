using AContact.AContactClasses;
using System;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Net.Mail;

namespace AContact
{
    public partial class AContact : Form
    {
        public AContact()
        {
            InitializeComponent();
        }


        ContactClass c = new ContactClass();

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Get the value from the input fields 
            c.FirstName = tbFirstName.Text;
            c.LastName = tbLastName.Text;
            c.Phone = maskedTextBox1.Text;
            c.Email = tbEmail.Text;
            c.Note = tbNote.Text;
            c.Status = cmbStatus.Text;

            // Inserting datat into database
            bool success = c.Insert(c);
            if (success == true)
            {
                // succesfully Inserted 
                MessageBox.Show("New contact succesfully inserted");
                // Call the Clear method here 
                eEmail.Clear();
                Clear();
            }
            else
            {
                // Faild to add contact 
                MessageBox.Show("Failed to add new contact. Try again.");
            }

            // load data on Gridview 
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;

        }

        private void AContact_Load(object sender, EventArgs e)
        {
            // load data on Gridview 
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        // Method to clear fields 
        public void Clear()
        {
            tbFirstName.Text = "";
            tbLastName.Text = "";
            maskedTextBox1.Text = "";
            tbEmail.Text = "";
            tbNote.Text = "";
            cmbStatus.Text = "";

        }


        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Get the data from textboxes 
            c.ContactID = int.Parse(tbContactID.Text);
            c.FirstName = tbFirstName.Text;
            c.LastName = tbLastName.Text;
            c.Phone = maskedTextBox1.Text;
            c.Email = tbEmail.Text;
            c.Note = tbNote.Text;
            c.Status = cmbStatus.Text;
            // Update data in database
            bool success = c.Update(c);
            if (success == true)
            {
                // Updated succesfully
                MessageBox.Show("Contact has been succesfully Updated");
                // Load data on Data Gridview 
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                // Call Clear Method 

                Clear();
            }
            else
            {
                // Failed to Update 
                MessageBox.Show("Failed to update. Try again.");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(tbContactID.Text);
            bool success = c.Delete(c);
            if (success == true)
            {
                // Succesfully deleted 
                MessageBox.Show("Contact succesfully deleted.");
                // Refresh data GridView
                // Load Data on Data GridView
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                // Call the  Clear methode here 

                Clear();
            }
            else
            {
                // Failed to delete 
                MessageBox.Show("Failed to delete contact. Try again.");
            }

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            // Call Clear method here 

            Clear();
        }

        private void DgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get data from Data Grid View and load it to the textboxes respectively 
            // Identify the row on witch mouse is clicked 
            int rowIndex = e.RowIndex;
            tbContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            tbFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            tbLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            maskedTextBox1.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            tbEmail.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            tbNote.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
            cmbStatus.Text = dgvContactList.Rows[rowIndex].Cells[6].Value.ToString();
        }

        // FirstName text-input validation 
        private void FirstName(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // LastName text-input validation
        private void LastName(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Phone input-validation 
        private void Phone(object sender, KeyPressEventArgs e)
        {
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        // Email input-validation 

        private void tbEmail_TextChanged(object sender, EventArgs e)
        {

            string RegexEmail = @"(?<email>\w+@\w+\.[a-z]{0,3})";

            if (Regex.IsMatch(tbEmail.Text, RegexEmail))
            {

                eEmail.Clear();
            }
            else
            {
                eEmail.SetError(this.tbEmail, "Invalid e-mail; firstname.lastname@company.com");
                return;
            }

        }

        // Note text-input validation 
        private void tbNote_TextChanged(object sender, EventArgs e)
        {

            string regexAlfabet = "^([a-zA-Z]*$)";

            if (Regex.IsMatch(tbNote.Text, regexAlfabet))
            {

                eNote.Clear();
            }
            else
            {
                eNote.SetError(this.tbNote, "Invalid note input; only a-Z, 0-9, @");
                return;
            }

        }


        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void Search_TextChanged(object sender, EventArgs e)
        {
            // get the value from Searchbox 
            string q = tbSearch.Text;

            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + q + "%' OR LastName LIKE '%" + q + "%' OR Phone LIKE '%" + q + "%' OR Email LIKE '%" + q + "%' OR Note LIKE '%" + q + "%' OR Status LIKE '%" + q + "%' ", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
        }
    }
} 

    

        

        // 
    
