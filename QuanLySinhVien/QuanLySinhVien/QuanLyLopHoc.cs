using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class QuanLyLopHoc : Form
    {
        private int currentPage = 1;
        private int pageSize = 10;

        public QuanLyLopHoc()
        {
            InitializeComponent();

            this.Click += (s, e) => ClearSelectionAndForm();
            grpThongTin.Click += (s, e) => ClearSelectionAndForm();
            dgvLopHoc.MouseClick += (s, e) =>
            {
                var hit = dgvLopHoc.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    ClearSelectionAndForm();
                }
            };

            RenderTable();
            ClearForm();
        }

        private void ClearSelectionAndForm()
        {
            dgvLopHoc.ClearSelection();
            ClearForm();
        }

        private void RenderTable()
        {
            using (var db = new qlsvDataContext())
            {
                string kw = txtSearch.Text.Trim().ToLower();
                var query = db.tbl_lophocs.AsQueryable();

                if (!string.IsNullOrEmpty(kw))
                {
                    query = query.Where(l =>
                        l.malop.ToLower().Contains(kw) ||
                        l.tenlop.ToLower().Contains(kw));
                }

                int total = query.Count();
                int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
                if (currentPage > totalPages) currentPage = totalPages;

                var pageData = query.OrderBy(l => l.id)
                                    .Skip((currentPage - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

                dgvLopHoc.Rows.Clear();
                foreach (var l in pageData)
                {
                    dgvLopHoc.Rows.Add(l.malop, l.tenlop);
                }

                lblPage.Text = $"Trang {currentPage}/{totalPages}  |  {total} bản ghi";
                btnFirst.Enabled = btnPrev.Enabled = currentPage > 1;
                btnNext.Enabled = btnLast.Enabled = currentPage < totalPages;
            }
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

            using (var db = new qlsvDataContext())
            {
                if (db.tbl_lophocs.Any(l => l.malop == txtMaLop.Text.Trim()))
                {
                    MessageBox.Show("Mã lớp đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var lophoc = new tbl_lophoc
                {
                    malop = txtMaLop.Text.Trim(),
                    tenlop = txtTenLop.Text.Trim()
                };

                db.tbl_lophocs.InsertOnSubmit(lophoc);
                db.SubmitChanges();
            }

            RenderTable();
            ClearSelectionAndForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvLopHoc.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn lớp cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaLop.Text) || string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã lớp và Tên lớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLop = dgvLopHoc.SelectedRows[0].Cells[0].Value?.ToString();

            using (var db = new qlsvDataContext())
            {
                var lop = db.tbl_lophocs.FirstOrDefault(l => l.malop == maLop);
                if (lop == null) return;

                if (maLop != txtMaLop.Text.Trim() && db.tbl_lophocs.Any(l => l.malop == txtMaLop.Text.Trim()))
                {
                    MessageBox.Show("Mã lớp mới đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lop.tenlop = txtTenLop.Text.Trim();
                lop.malop = txtMaLop.Text.Trim();
                db.SubmitChanges();
            }

            RenderTable();
            ClearSelectionAndForm();
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
                using (var db = new qlsvDataContext())
                {
                    var lop = db.tbl_lophocs.FirstOrDefault(l => l.malop == maLop);
                    if (lop != null)
                    {
                        db.tbl_lophocs.DeleteOnSubmit(lop);
                        db.SubmitChanges();
                    }
                }
                RenderTable();
                ClearSelectionAndForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearSelectionAndForm();
        }

        private void dgvLopHoc_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLopHoc.SelectedRows.Count == 0) return;
            var row = dgvLopHoc.SelectedRows[0];
            txtMaLop.Text = row.Cells[0].Value?.ToString();
            txtTenLop.Text = row.Cells[1].Value?.ToString();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            RenderTable();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                RenderTable();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            RenderTable();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            using (var db = new qlsvDataContext())
            {
                string kw = txtSearch.Text.Trim().ToLower();
                var query = db.tbl_lophocs.AsQueryable();
                if (!string.IsNullOrEmpty(kw))
                {
                    query = query.Where(l =>
                        l.malop.ToLower().Contains(kw) ||
                        l.tenlop.ToLower().Contains(kw));
                }
                int total = query.Count();
                currentPage = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
            }
            RenderTable();
        }

        private void menuQuanLySV_Click(object sender, EventArgs e)
        {
            var f2 = Application.OpenForms.OfType<QuanLySinhVien>().FirstOrDefault();
            if (f2 != null) f2.Show();
            this.Close();
        }

        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            var f1 = Application.OpenForms.OfType<Login>().FirstOrDefault();
            if (f1 != null) f1.Show();
            this.Close();
        }
    }
}