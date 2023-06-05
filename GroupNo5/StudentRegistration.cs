using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GroupNo5
{
    public partial class StudentRegistration : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-R7519AN\SQLEXPRESS;Initial Catalog=IoTProject;Integrated Security=True");
        public static StudentRegistration instance;
        public TextBox tb1;

        public StudentRegistration()
        {
            InitializeComponent();
            instance = this;
            tb1 = txtStdID;
        }

        public void StdId()
        {
            string StdId;
            string qry = "Select StdId from StudentDetails order by StdId";
            con.Open();
            SqlCommand cmd = new SqlCommand(qry,con);
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                int i = int.Parse(dr[0].ToString())+1;
                StdId = i.ToString("0000");
            }
            else if(Convert.IsDBNull(dr))
            {
                StdId = ("0000");
            }
            else
            {
                StdId = ("0001");
            }
            con.Close();
            txtStdID.Text = StdId.ToString();
        }

        public void displayData()
        {
            string StdId = txtStdID.Text;
            string qry = "Select * from StudentDetails where StdId = '"+StdId+"'";
            con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataReader dr = cmd.ExecuteReader();
        }

        private void StudentRegistration_Load(object sender, EventArgs e)
        {
            StdId();
        }

        public static string id, name;
        private void button1_Click(object sender, EventArgs e)
        {
            id = txtStdID.Text;
            name = txtFullName.Text;
            StudentID f1 = new StudentID();
            f1.Show();
            this.Hide();
        }

        string Gender;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Gender = "Female";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string namewithInitials = txtInitialName.Text;
            string fullName = txtFullName.Text;
            string surname = txtSurname.Text;
            string address = txtAddress.Text;
            string parent = txtParentName.Text;
            string occupation = txtOccupation.Text;
            string phoneNo = txtPhoneNum.Text;


            string qry = "insert into StudentDetails values ('" + namewithInitials + "','" + fullName + "','" + surname + "','" + this.txtBdate.Value.ToString() + "','" + Gender + "','" + address + "','" + parent + "','" + occupation + "','" + phoneNo + "')";
            SqlCommand cmd = new SqlCommand(qry,con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Successfully Inserted");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception occured. "+ex);
            }
            finally
            {
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string id = txtStdID.Text;
            string namewithInitials = txtInitialName.Text;
            string fullName = txtFullName.Text;
            string surname = txtSurname.Text;
            string address = txtAddress.Text;
            string parent = txtParentName.Text;
            string occupation = txtOccupation.Text;
            string phoneNo = txtPhoneNum.Text;

            string updateqry = "UPDATE StudentDetails SET InitialName='" + namewithInitials + "',FullName='" + fullName + "',Surname='"+surname+"',BirthDate='" + this.txtBdate.Value.ToString() + "',Gender='" + Gender + "',Address='" + address + "',ParentName='" + parent + "',Occupation='" + occupation + "',PhoneNo='" + phoneNo + "' WHERE StdID='" + id + "'";
            SqlCommand cmd = new SqlCommand(updateqry, con);

            try
            {
                con.Open();

                if (MessageBox.Show("Do you want to update this Row?", "Update Row", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Successfully Updated!");
                }
                else
                {
                    MessageBox.Show("Row Not Updated", "Update row", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException se)
            {
                MessageBox.Show(" " + se);
            }
            finally
            {
                con.Close();
            }

        }

        private void txtStdID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string displaydata = "select InitialName,FullName,Surname,BirthDate,Gender,Address,ParentName,Occupation,PhoneNo from StudentDetails where StdId=@StdId";
                con.Open();
                SqlCommand cmd = new SqlCommand(displaydata, con);
                cmd.Parameters.AddWithValue("@StdId", int.Parse(txtStdID.Text));
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    txtInitialName.Text = da.GetValue(1).ToString();
                    txtFullName.Text = da.GetValue(2).ToString();
                    txtSurname.Text = da.GetValue(3).ToString();
                    txtBdate.Text = da.GetValue(4).ToString();
                    Gender = da.GetValue(5).ToString();
                    txtAddress.Text = da.GetValue(6).ToString();
                    txtParentName.Text = da.GetValue(7).ToString();
                    txtOccupation.Text = da.GetValue(8).ToString();
                    txtPhoneNum.Text = da.GetValue(9).ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception Occured! " + ex);
            }
            finally
            {
                con.Close();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string stdid = txtStdID.Text;

            string deletequery = "DELETE FROM StudentDetails where StdId='" + stdid + "'";
            SqlCommand cmd = new SqlCommand(deletequery, con);
            try
            {
                con.Open();

                if (MessageBox.Show("Do you want to remove this Row?", "Remove Row", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Successfully Deleted!");
                }
                else
                {
                    MessageBox.Show("Row Not Removed", "Remove row", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException se)
            {
                MessageBox.Show(" " + se);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
