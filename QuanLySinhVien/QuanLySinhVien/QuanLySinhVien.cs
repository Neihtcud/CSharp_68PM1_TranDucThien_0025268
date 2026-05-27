using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLySinhVien
{
    public partial class QuanLySinhVien : Form
    {
        private int currentPage = 1;
        private int pageSize = 10;

        public QuanLySinhVien()
        {
            InitializeComponent();

            this.Click += (s, e) => ClearSelectionAndForm();
            grpThongTin.Click += (s, e) => ClearSelectionAndForm();
            dgvSinhVien.MouseClick += (s, e) =>
            {
                var hit = dgvSinhVien.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    ClearSelectionAndForm();
                }
            };

            dtpNgaySinh.ValueChanged += (s, e) =>
            {
                dtpNgaySinh.Format = DateTimePickerFormat.Short;
            };

            LoadLop();
            RenderTable();
            ClearForm();
        }

        private void ClearSelectionAndForm()
        {
            dgvSinhVien.ClearSelection();
            ClearForm();
        }

        private void LoadLop()
        {
            using (var db = new qlsvDataContext())
            {
                cboLop.Items.Clear();
                var lops = db.tbl_lophocs.Select(l => l.malop).OrderBy(l => l).ToList();
                foreach (var l in lops)
                {
                    cboLop.Items.Add(l + " – Lớp " + l);
                }
            }
        }

        private void RenderTable()
        {
            using (var db = new qlsvDataContext())
            {
                string keyword = txtSearch.Text.Trim().ToLower();
                var query = db.tbl_sinhviens.AsQueryable();

                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(s =>
                        s.hoten.ToLower().Contains(keyword) ||
                        s.id.ToString().ToLower().Contains(keyword) ||
                        s.malop.ToLower().Contains(keyword));
                }

                int total = query.Count();
                int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
                if (currentPage > totalPages) currentPage = totalPages;

                var pageData = query.OrderBy(s => s.id)
                                    .Skip((currentPage - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

                dgvSinhVien.Rows.Clear();
                foreach (var sv in pageData)
                {
                    string ngaySinh = sv.ngaysinh.HasValue ? sv.ngaysinh.Value.ToString("dd/MM/yyyy") : "";
                    dgvSinhVien.Rows.Add(sv.id.ToString(), sv.hoten, sv.gioitinh, ngaySinh, sv.malop);
                }

                lblPage.Text = $"Trang {currentPage}/{totalPages}  |  {total} bản ghi";
                btnFirst.Enabled = btnPrev.Enabled = currentPage > 1;
                btnNext.Enabled = btnLast.Enabled = currentPage < totalPages;
            }
        }

        private void ClearForm()
        {
            txtMaSV.Clear();
            txtHoTen.Clear();

            cboGioiTinh.SelectedIndex = -1;
            cboLop.SelectedIndex = -1;

            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.CustomFormat = " ";

            txtMaSV.Focus();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            RenderTable();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                cboGioiTinh.SelectedIndex == -1 ||
                cboLop.SelectedIndex == -1 ||
                dtpNgaySinh.CustomFormat == " ")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtMaSV.Text.Trim(), out int id))
            {
                MessageBox.Show("Mã sinh viên (Id) phải là số nguyên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var db = new qlsvDataContext())
            {
                if (db.tbl_sinhviens.Any(s => s.id == id))
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string lopText = cboLop.Text.Contains("–") ? cboLop.Text.Split('–')[0].Trim() : cboLop.Text.Trim();

                var sv = new tbl_sinhvien
                {
                    id = id,
                    hoten = txtHoTen.Text.Trim(),
                    gioitinh = cboGioiTinh.Text,
                    ngaysinh = dtpNgaySinh.Value,
                    malop = lopText
                };

                db.tbl_sinhviens.InsertOnSubmit(sv);
                db.SubmitChanges();
            }

            LoadLop();
            RenderTable();
            ClearSelectionAndForm();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn sinh viên cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaSV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                cboGioiTinh.SelectedIndex == -1 ||
                cboLop.SelectedIndex == -1 ||
                dtpNgaySinh.CustomFormat == " ")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(dgvSinhVien.SelectedRows[0].Cells[0].Value?.ToString(), out int id)) return;

            using (var db = new qlsvDataContext())
            {
                var sv = db.tbl_sinhviens.FirstOrDefault(s => s.id == id);
                if (sv == null) return;

                string lopText = cboLop.Text.Contains("–") ? cboLop.Text.Split('–')[0].Trim() : cboLop.Text.Trim();

                sv.hoten = txtHoTen.Text.Trim();
                sv.gioitinh = cboGioiTinh.Text;
                sv.ngaysinh = dtpNgaySinh.Value;
                sv.malop = lopText;

                db.SubmitChanges();
            }

            LoadLop();
            RenderTable();
            ClearSelectionAndForm();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn sinh viên cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(dgvSinhVien.SelectedRows[0].Cells[0].Value?.ToString(), out int id)) return;

            if (MessageBox.Show("Xác nhận xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var db = new qlsvDataContext())
                {
                    var sv = db.tbl_sinhviens.FirstOrDefault(s => s.id == id);
                    if (sv != null)
                    {
                        db.tbl_sinhviens.DeleteOnSubmit(sv);
                        db.SubmitChanges();
                    }
                }
                LoadLop();
                RenderTable();
                ClearSelectionAndForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearSelectionAndForm();
        }

        private void dgvSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSinhVien.SelectedRows.Count == 0) return;

            var row = dgvSinhVien.SelectedRows[0];
            txtMaSV.Text = row.Cells[0].Value?.ToString();
            txtHoTen.Text = row.Cells[1].Value?.ToString();
            cboGioiTinh.Text = row.Cells[2].Value?.ToString();

            if (DateTime.TryParseExact(row.Cells[3].Value?.ToString(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                dtpNgaySinh.Format = DateTimePickerFormat.Short;
                dtpNgaySinh.Value = date;
            }
            else
            {
                dtpNgaySinh.Format = DateTimePickerFormat.Custom;
                dtpNgaySinh.CustomFormat = " ";
            }

            string lop = row.Cells[4].Value?.ToString();
            string target = lop + " – Lớp " + lop;
            int idx = cboLop.Items.IndexOf(target);
            cboLop.SelectedIndex = idx;
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
                string keyword = txtSearch.Text.Trim().ToLower();
                var query = db.tbl_sinhviens.AsQueryable();
                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(s =>
                        s.hoten.ToLower().Contains(keyword) ||
                        s.id.ToString().ToLower().Contains(keyword) ||
                        s.malop.ToLower().Contains(keyword));
                }
                int total = query.Count();
                currentPage = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
            }
            RenderTable();
        }

        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            var f1 = Application.OpenForms.OfType<Login>().FirstOrDefault();
            if (f1 != null) f1.Show();
            this.Close();
        }

        private void menuQuanLySV_Click(object sender, EventArgs e)
        {
        }

        private void menuQuanLyLop_Click(object sender, EventArgs e)
        {
            this.Hide();
            QuanLyLopHoc f3 = new QuanLyLopHoc();
            f3.FormClosed += (s, args) => { if (!this.Visible) this.Close(); };
            f3.Show();
        }
    }
}