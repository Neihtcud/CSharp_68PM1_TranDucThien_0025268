using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LoginApp
{
    public partial class Form2 : Form
    {
        private List<SinhVien> danhSach = new List<SinhVien>();
        private int currentPage = 1;
        private int pageSize = 10;
        private List<SinhVien> filtered = new List<SinhVien>();

        public Form2()
        {
            InitializeComponent();
            LoadSampleData();
            LoadLop();
            RenderTable();
        }

        private void LoadSampleData()
        {
            danhSach.Add(new SinhVien { MaSV = "1", HoTen = "hieu", GioiTinh = "Nam", NgaySinh = new DateTime(2026, 3, 11), Lop = "68PM1" });
            danhSach.Add(new SinhVien { MaSV = "2", HoTen = "Nguyễn Văn B", GioiTinh = "Nam", NgaySinh = new DateTime(2026, 3, 11), Lop = "68PM2" });
            danhSach.Add(new SinhVien { MaSV = "3", HoTen = "Trần Văn C", GioiTinh = "Nam", NgaySinh = new DateTime(2026, 3, 21), Lop = "68PM2" });
        }

        private void LoadLop()
        {
            cboLop.Items.Clear();
            var lops = danhSach.Select(s => s.Lop).Distinct().OrderBy(l => l).ToList();
            foreach (var l in lops)
                cboLop.Items.Add(l + " – Lớp " + l);
            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
        }

        private void RenderTable()
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            filtered = string.IsNullOrEmpty(keyword)
                ? danhSach.ToList()
                : danhSach.Where(s =>
                    s.HoTen.ToLower().Contains(keyword) ||
                    s.MaSV.ToLower().Contains(keyword) ||
                    s.Lop.ToLower().Contains(keyword)).ToList();

            int total = filtered.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
            if (currentPage > totalPages) currentPage = totalPages;

            var pageData = filtered.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            dgvSinhVien.Rows.Clear();
            foreach (var sv in pageData)
                dgvSinhVien.Rows.Add(sv.MaSV, sv.HoTen, sv.GioiTinh, sv.NgaySinh.ToString("dd/MM/yyyy"), sv.Lop);

            lblPage.Text = $"Trang {currentPage}/{totalPages}  |  {total} bản ghi";
            btnFirst.Enabled = btnPrev.Enabled = currentPage > 1;
            btnNext.Enabled = btnLast.Enabled = currentPage < totalPages;
        }

        private void ClearForm()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            dtpNgaySinh.Value = DateTime.Today;
            cboGioiTinh.SelectedIndex = 0;
            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
            txtMaSV.Focus();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            RenderTable();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã SV và Họ tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (danhSach.Any(s => s.MaSV == txtMaSV.Text.Trim()))
            {
                MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string lopText = cboLop.Text.Contains("–") ? cboLop.Text.Split('–')[0].Trim() : cboLop.Text.Trim();
            danhSach.Add(new SinhVien
            {
                MaSV = txtMaSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                GioiTinh = cboGioiTinh.Text,
                NgaySinh = dtpNgaySinh.Value,
                Lop = lopText
            });
            LoadLop();
            RenderTable();
            ClearForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn sinh viên cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maSV = dgvSinhVien.SelectedRows[0].Cells[0].Value?.ToString();
            var sv = danhSach.FirstOrDefault(s => s.MaSV == maSV);
            if (sv == null) return;
            string lopText = cboLop.Text.Contains("–") ? cboLop.Text.Split('–')[0].Trim() : cboLop.Text.Trim();
            sv.HoTen = txtHoTen.Text.Trim();
            sv.GioiTinh = cboGioiTinh.Text;
            sv.NgaySinh = dtpNgaySinh.Value;
            sv.Lop = lopText;
            LoadLop();
            RenderTable();
            ClearForm();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn sinh viên cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maSV = dgvSinhVien.SelectedRows[0].Cells[0].Value?.ToString();
            if (MessageBox.Show("Xác nhận xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                danhSach.RemoveAll(s => s.MaSV == maSV);
                LoadLop();
                RenderTable();
                ClearForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dgvSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count == 0) return;
            var row = dgvSinhVien.SelectedRows[0];
            string maSV = row.Cells[0].Value?.ToString();
            var sv = danhSach.FirstOrDefault(s => s.MaSV == maSV);
            if (sv == null) return;
            txtMaSV.Text = sv.MaSV;
            txtHoTen.Text = sv.HoTen;
            dtpNgaySinh.Value = sv.NgaySinh;
            cboGioiTinh.Text = sv.GioiTinh;
            string target = sv.Lop + " – Lớp " + sv.Lop;
            int idx = cboLop.Items.IndexOf(target);
            if (idx >= 0) cboLop.SelectedIndex = idx;
        }

        private void btnFirst_Click(object sender, EventArgs e) { currentPage = 1; RenderTable(); }
        private void btnPrev_Click(object sender, EventArgs e) { if (currentPage > 1) { currentPage--; RenderTable(); } }
        private void btnNext_Click(object sender, EventArgs e) { currentPage++; RenderTable(); }
        private void btnLast_Click(object sender, EventArgs e)
        {
            currentPage = Math.Max(1, (int)Math.Ceiling(filtered.Count / (double)pageSize));
            RenderTable();
        }

        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
            new Form1().Show();
        }
    }

    public class SinhVien
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Lop { get; set; }
    }
}
