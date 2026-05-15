using System;
using System.Drawing;
using System.Windows.Forms;

namespace LoginApp
{
    public partial class Form1 : Form
    {
        private const string STUDENT_EMAIL = "0025268@st.huce.edu.vn";
        private const string STUDENT_MSSV = "0025268";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (email == STUDENT_EMAIL && password == STUDENT_MSSV)
            {
                this.Hide();
                Form2 f2 = new Form2();
                f2.FormClosed += (s, args) => this.Close();
                f2.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại!\nSai email hoặc mật khẩu.", "Thất bại",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmail.Clear();
            txtPassword.Clear();
            txtEmail.Focus();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {
        }
    }
}