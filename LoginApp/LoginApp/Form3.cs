using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LoginApp
{
    public partial class Form3 : Form
    {
        public static List<LopHoc> DanhSachLop = new List<LopHoc>
        {
            new LopHoc { MaLop = "68PM1", TenLop = "Kỹ thuật phần mềm 1" },
            new LopHoc { MaLop = "68PM2", TenLop = "Kỹ thuật phần mềm 2" },
        };

        private int currentPage = 1;
        private int pageSize = 10;
        private List<LopHoc> filtered = new List<LopHoc>();

        public Form3()
        {
            InitializeComponent();
            RenderTable();
        }

        private void RenderTable()
        {
            string kw = txtSearch.Text.Trim().ToLower();
            filtered = string.IsNullOrEmpty(kw)
                ? DanhSachLop.ToList()
                : DanhSachLop.Where(l =>
                    l.MaLop.ToLower().Contains(kw) ||
                    l.TenLop.ToLower().Contains(kw)).ToList();

            int total = filtered.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
            if (currentPage > totalPages) currentPage = totalPages;

            var pageData = filtered.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            dgvLopHoc.Rows.Clear();
            foreach (var l in pageData)
                dgvLopHoc.Rows.Add(l.MaLop, l.TenLop);

            lblPage.Text = $"Trang {currentPage}/{totalPages}  |  {total} bản ghi";
            btnFirst.Enabled = btnPrev.Enabled = currentPage > 1;
            btnNext.Enabled = btnLast.Enabled = currentPage < totalPages;
        }

        private void ClearForm()
        {
            txtMaLop.Clear();
            txtTenLop.Clear();
            txtMaLop.Focus();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            RenderTable();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaLop.Text) || string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã lớp và Tên lớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (DanhSachLop.Any(l => l.MaLop == txtMaLop.Text.Trim()))
            {
                MessageBox.Show("Mã lớp đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DanhSachLop.Add(new LopHoc { MaLop = txtMaLop.Text.Trim(), TenLop = txtTenLop.Text.Trim() });
            RenderTable();
            ClearForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvLopHoc.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn lớp cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maLop = dgvLopHoc.SelectedRows[0].Cells[0].Value?.ToString();
            var lop = DanhSachLop.FirstOrDefault(l => l.MaLop == maLop);
            if (lop == null) return;
            lop.TenLop = txtTenLop.Text.Trim();
            RenderTable();
            ClearForm();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLopHoc.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn lớp cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maLop = dgvLopHoc.SelectedRows[0].Cells[0].Value?.ToString();
            if (MessageBox.Show("Xác nhận xóa lớp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DanhSachLop.RemoveAll(l => l.MaLop == maLop);
                RenderTable();
                ClearForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvLopHoc_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLopHoc.SelectedRows.Count == 0) return;
            var row = dgvLopHoc.SelectedRows[0];
            string maLop = row.Cells[0].Value?.ToString();
            var lop = DanhSachLop.FirstOrDefault(l => l.MaLop == maLop);
            if (lop == null) return;
            txtMaLop.Text = lop.MaLop;
            txtTenLop.Text = lop.TenLop;
        }

        private void btnFirst_Click(object sender, EventArgs e) { currentPage = 1; RenderTable(); }
        private void btnPrev_Click(object sender, EventArgs e) { if (currentPage > 1) { currentPage--; RenderTable(); } }
        private void btnNext_Click(object sender, EventArgs e) { currentPage++; RenderTable(); }
        private void btnLast_Click(object sender, EventArgs e)
        {
            currentPage = Math.Max(1, (int)Math.Ceiling(filtered.Count / (double)pageSize));
            RenderTable();
        }

        private void menuQuanLySV_Click(object sender, EventArgs e)
        {
            var f2 = Application.OpenForms.OfType<Form2>().FirstOrDefault();
            if (f2 != null) f2.Show();
            this.Close();
        }

        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            var f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (f1 != null) f1.Show();
            this.Close();
        }
    }

    public class LopHoc
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
    }
}