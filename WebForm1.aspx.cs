using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ques14
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Validate the form
            if (Page.IsValid)
            {
                // Insert user data into the database
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=abc;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Users (Username, Email, Password, MobileNumber) VALUES (@Username, @Email, @Password, @MobileNumber)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@MobileNumber", txtMobileNumber.Text);

                        cmd.ExecuteNonQuery();
                    }
                }

                // Registration successful, redirect or display a success message
                Response.Redirect("WebForm1.aspx"); // Redirect to a success page
            }
        }

        protected void ValidateMobileNumber(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            // Custom validation for mobile number (e.g., unique and 10 digits)
            args.IsValid = IsMobileNumberValid(txtMobileNumber.Text);
        }

        protected void ValidateName(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            // Custom validation for name (e.g., should not contain any number)
            args.IsValid = IsNameValid(txtUsername.Text);
        }

        protected void ValidateEmail(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            // Custom validation for email
            args.IsValid = IsEmailValid(txtEmail.Text);
        }

        private bool IsMobileNumberValid(string mobileNumber)
        {
            // Implement your validation logic here
            // Example: Check if the mobile number is unique and has 10 digits
            // You may need to query the database to check for uniqueness

            // For simplicity, let's assume the mobile number is valid
            return mobileNumber.Length == 10;
        }

        private bool IsNameValid(string name)
        {
            // Implement your validation logic here
            // Example: Check if the name contains any number
            return !Regex.IsMatch(name, @"\d");
        }

        private bool IsEmailValid(string email)
        {
            // Implement your validation logic here
            // Example: Check if the email is valid using a regular expression
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        }
    }
}