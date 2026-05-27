using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class Login : Form
    {
        private const string STUDENT_EMAIL = "0025268@st.huce.edu.vn";
        private const string STUDENT_MSSV = "0025268";

        public Login()
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
                QuanLySinhVien f2 = new QuanLySinhVien();
                f2.FormClosed += (s, args) => { if (!this.Visible) this.Close(); };
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