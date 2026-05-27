using System.Drawing;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    partial class QuanLyLopHoc
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            menuQuanLySV = new ToolStripMenuItem();
            menuQuanLyLop = new ToolStripMenuItem();
            menuDangXuat = new ToolStripMenuItem();

            grpThongTin = new GroupBox();
            lblMaLop = new Label();
            txtMaLop = new TextBox();
            lblTenLop = new Label();
            txtTenLop = new TextBox();

            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();

            lblSearchTitle = new Label();
            txtSearch = new TextBox();
            btnTim = new Button();
            dgvLopHoc = new DataGridView();
            lblPage = new Label();
            btnFirst = new Button();
            btnPrev = new Button();
            btnNext = new Button();
            btnLast = new Button();

            menuStrip1.SuspendLayout();
            grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLopHoc).BeginInit();
            SuspendLayout();

            menuStrip1.Items.AddRange(new ToolStripItem[] { menuQuanLySV, menuQuanLyLop, menuDangXuat });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1100, 24);
            menuStrip1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            menuQuanLySV.Text = "Quản lý Sinh Viên";
            menuQuanLySV.Click += menuQuanLySV_Click;
            menuQuanLyLop.Text = "Quản lý Lớp Học";
            menuDangXuat.Text = "Đăng xuất";
            menuDangXuat.ForeColor = Color.FromArgb(220, 38, 38);
            menuDangXuat.Click += menuDangXuat_Click;

            grpThongTin.Text = "Thông tin lớp học";
            grpThongTin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpThongTin.Location = new Point(12, 30);
            grpThongTin.Size = new Size(430, 580);
            grpThongTin.Controls.AddRange(new Control[] { lblMaLop, txtMaLop, lblTenLop, txtTenLop });

            lblMaLop.Text = "Mã lớp:";
            lblMaLop.Location = new Point(15, 30);
            lblMaLop.AutoSize = true;
            lblMaLop.Font = new Font("Segoe UI", 9F);

            txtMaLop.Location = new Point(15, 52);
            txtMaLop.Size = new Size(395, 27);
            txtMaLop.BorderStyle = BorderStyle.FixedSingle;
            txtMaLop.Font = new Font("Segoe UI", 10F);
            txtMaLop.BackColor = Color.FromArgb(219, 234, 254);

            lblTenLop.Text = "Tên lớp:";
            lblTenLop.Location = new Point(15, 95);
            lblTenLop.AutoSize = true;
            lblTenLop.Font = new Font("Segoe UI", 9F);

            txtTenLop.Location = new Point(15, 117);
            txtTenLop.Size = new Size(395, 27);
            txtTenLop.BorderStyle = BorderStyle.FixedSingle;
            txtTenLop.Font = new Font("Segoe UI", 10F);

            btnThem.Text = "Thêm";
            btnThem.Location = new Point(12, 625);
            btnThem.Size = new Size(200, 50);
            btnThem.BackColor = Color.FromArgb(59, 130, 246);
            btnThem.ForeColor = Color.White;
            btnThem.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.FlatAppearance.BorderSize = 0;
            btnThem.Cursor = Cursors.Hand;
            btnThem.Click += btnThem_Click;

            btnSua.Text = "Sửa";
            btnSua.Location = new Point(222, 625);
            btnSua.Size = new Size(200, 50);
            btnSua.BackColor = Color.FromArgb(22, 163, 74);
            btnSua.ForeColor = Color.White;
            btnSua.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSua.FlatStyle = FlatStyle.Flat;
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.Cursor = Cursors.Hand;
            btnSua.Click += btnSua_Click;

            btnXoa.Text = "Xóa";
            btnXoa.Location = new Point(12, 685);
            btnXoa.Size = new Size(200, 50);
            btnXoa.BackColor = Color.FromArgb(220, 38, 38);
            btnXoa.ForeColor = Color.White;
            btnXoa.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnXoa.FlatStyle = FlatStyle.Flat;
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.Cursor = Cursors.Hand;
            btnXoa.Click += btnXoa_Click;

            btnLamMoi.Text = "Làm mới";
            btnLamMoi.Location = new Point(222, 685);
            btnLamMoi.Size = new Size(200, 50);
            btnLamMoi.BackColor = Color.FromArgb(107, 114, 128);
            btnLamMoi.ForeColor = Color.White;
            btnLamMoi.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.Cursor = Cursors.Hand;
            btnLamMoi.Click += btnLamMoi_Click;

            lblSearchTitle.Text = "Tìm kiếm (Mã lớp / Tên lớp):";
            lblSearchTitle.Location = new Point(460, 38);
            lblSearchTitle.AutoSize = true;
            lblSearchTitle.Font = new Font("Segoe UI", 9F);

            txtSearch.Location = new Point(460, 60);
            txtSearch.Size = new Size(480, 30);
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;

            btnTim.Text = "Tìm";
            btnTim.Location = new Point(950, 58);
            btnTim.Size = new Size(120, 34);
            btnTim.BackColor = Color.FromArgb(30, 58, 95);
            btnTim.ForeColor = Color.White;
            btnTim.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTim.FlatStyle = FlatStyle.Flat;
            btnTim.FlatAppearance.BorderSize = 0;
            btnTim.Cursor = Cursors.Hand;
            btnTim.Click += btnTim_Click;

            dgvLopHoc.Location = new Point(460, 105);
            dgvLopHoc.Size = new Size(620, 560);
            dgvLopHoc.AllowUserToAddRows = false;
            dgvLopHoc.AllowUserToDeleteRows = false;
            dgvLopHoc.ReadOnly = true;
            dgvLopHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLopHoc.MultiSelect = false;
            dgvLopHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLopHoc.BackgroundColor = Color.White;
            dgvLopHoc.BorderStyle = BorderStyle.Fixed3D;
            dgvLopHoc.Font = new Font("Segoe UI", 9F);
            dgvLopHoc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvLopHoc.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(243, 244, 246);
            dgvLopHoc.EnableHeadersVisualStyles = false;
            dgvLopHoc.RowTemplate.Height = 28;
            dgvLopHoc.GridColor = Color.FromArgb(229, 231, 235);
            dgvLopHoc.Columns.Add("MaLop", "Mã Lớp");
            dgvLopHoc.Columns.Add("TenLop", "Tên Lớp");
            dgvLopHoc.SelectionChanged += dgvLopHoc_SelectionChanged;

            btnFirst.Text = "<<";
            btnFirst.Location = new Point(460, 680);
            btnFirst.Size = new Size(50, 35);
            btnFirst.Font = new Font("Segoe UI", 9F);
            btnFirst.FlatStyle = FlatStyle.Flat;
            btnFirst.Cursor = Cursors.Hand;
            btnFirst.Click += btnFirst_Click;

            btnPrev.Text = "<";
            btnPrev.Location = new Point(515, 680);
            btnPrev.Size = new Size(50, 35);
            btnPrev.Font = new Font("Segoe UI", 9F);
            btnPrev.FlatStyle = FlatStyle.Flat;
            btnPrev.Cursor = Cursors.Hand;
            btnPrev.Click += btnPrev_Click;

            lblPage.Text = "Trang 1/1  |  0 bản ghi";
            lblPage.Location = new Point(575, 688);
            lblPage.AutoSize = true;
            lblPage.Font = new Font("Segoe UI", 9F);

            btnNext.Text = ">";
            btnNext.Location = new Point(940, 680);
            btnNext.Size = new Size(50, 35);
            btnNext.Font = new Font("Segoe UI", 9F);
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Cursor = Cursors.Hand;
            btnNext.Click += btnNext_Click;

            btnLast.Text = ">>";
            btnLast.Location = new Point(995, 680);
            btnLast.Size = new Size(50, 35);
            btnLast.Font = new Font("Segoe UI", 9F);
            btnLast.FlatStyle = FlatStyle.Flat;
            btnLast.Cursor = Cursors.Hand;
            btnLast.Click += btnLast_Click;

            BackColor = Color.FromArgb(249, 250, 251);
            ClientSize = new Size(1100, 750);
            MainMenuStrip = menuStrip1;
            Text = "Quản lý Lớp Học";
            StartPosition = FormStartPosition.CenterScreen;
            Controls.AddRange(new Control[] {
                menuStrip1, grpThongTin,
                btnThem, btnSua, btnXoa, btnLamMoi,
                lblSearchTitle, txtSearch, btnTim,
                dgvLopHoc,
                btnFirst, btnPrev, lblPage, btnNext, btnLast
            });

            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLopHoc).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuQuanLySV;
        private ToolStripMenuItem menuQuanLyLop;
        private ToolStripMenuItem menuDangXuat;
        private GroupBox grpThongTin;
        private Label lblMaLop;
        private TextBox txtMaLop;
        private Label lblTenLop;
        private TextBox txtTenLop;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Label lblSearchTitle;
        private TextBox txtSearch;
        private Button btnTim;
        private DataGridView dgvLopHoc;
        private Label lblPage;
        private Button btnFirst;
        private Button btnPrev;
        private Button btnNext;
        private Button btnLast;
    }
}