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
using System.Windows.Forms.DataVisualization;

namespace GroupNo5
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            fillchartx();
        }
        private void fillchartx()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-R7519AN\SQLEXPRESS;Initial Catalog=IoTProject;Integrated Security=True");
            DataSet ds = new DataSet();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("Select Sno,Fname From StudentDetails", con);
            adapt.Fill(ds);
            chart1.DataSource = ds;
            chart1.Series["Attendance"].XValueMember = "Fname";
            chart1.Series["Attendance"].YValueMembers = "Sno";
            con.Close();
        }
    }
}