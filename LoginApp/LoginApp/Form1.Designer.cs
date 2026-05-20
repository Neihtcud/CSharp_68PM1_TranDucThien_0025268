namespace LoginApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnClear = new Button();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(37, 99, 235);
            lblTitle.Location = new Point(40, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(333, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "🔐 ĐĂNG NHẬP HỆ THỐNG";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Click += lblTitle_Click;

            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmail.ForeColor = Color.FromArgb(55, 65, 81);
            lblEmail.Location = new Point(40, 90);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(134, 23);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "Email sinh viên:";

            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Font = new Font("Segoe UI", 11F);
            txtEmail.Location = new Point(40, 115);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "example@student.edu.vn";
            txtEmail.Size = new Size(320, 32);
            txtEmail.TabIndex = 2;

            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPassword.ForeColor = Color.FromArgb(55, 65, 81);
            lblPassword.Location = new Point(40, 165);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(155, 23);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Mật khẩu (MSSV):";

            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 11F);
            txtPassword.Location = new Point(40, 190);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.PlaceholderText = "Nhập MSSV của bạn";
            txtPassword.Size = new Size(320, 32);
            txtPassword.TabIndex = 4;

            btnLogin.BackColor = Color.FromArgb(37, 99, 235);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(40, 260);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(150, 45);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;

            btnClear.BackColor = Color.FromArgb(243, 244, 246);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 11F);
            btnClear.ForeColor = Color.FromArgb(55, 65, 81);
            btnClear.Location = new Point(210, 260);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(150, 45);
            btnClear.TabIndex = 6;
            btnClear.Text = "Xóa";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;

            panel1.BackColor = Color.White;
            panel1.Controls.Add(lblTitle);
            panel1.Controls.Add(lblEmail);
            panel1.Controls.Add(txtEmail);
            panel1.Controls.Add(lblPassword);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(btnLogin);
            panel1.Controls.Add(btnClear);
            panel1.Location = new Point(40, 30);
            panel1.Name = "panel1";
            panel1.Size = new Size(400, 380);
            panel1.TabIndex = 0;

            BackColor = Color.FromArgb(239, 246, 255);
            ClientSize = new Size(480, 440);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng Nhập - LoginApp";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panel1;
    }
}