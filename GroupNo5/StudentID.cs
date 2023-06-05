using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroupNo5
{
    public partial class StudentID : Form
    {
        public static StudentID instance;
        
        public StudentID()
        {
            InitializeComponent();
        }

        private void StudentID_Load(object sender, EventArgs e)
        {
            lblId.Text = " " + StudentRegistration.id.ToUpper();
            lblName.Text = " " + StudentRegistration.name.ToUpper();

            QRCoder.QRCodeGenerator QG = new QRCoder.QRCodeGenerator();
            var data = QG.CreateQrCode(lblId.Text, QRCoder.QRCodeGenerator.ECCLevel.H);
            var code = new QRCoder.QRCode(data);
            QRCode.Image = code.GetGraphic(5);
        }
    }
}
