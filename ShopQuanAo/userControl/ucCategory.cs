﻿using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;

namespace userControl
{
    public partial class ucCategory : UserControl
    {
        DanhMucBLL danhMucBLL = new DanhMucBLL();
        private int selectedCategoryId = -1;
        public ucCategory()
        {
            InitializeComponent();
        }

        private void ucCategory_Load(object sender, EventArgs e)
        {
            List<DanhMuc> danhMucList = danhMucBLL.getAllDanhMucBLL();

            dgvCate.DataSource = danhMucList;

            //dgvCate.Columns["DanhMucID"].HeaderText = "Mã danh mục";
            dgvCate.Columns["DanhMucID"].Visible = false;
            dgvCate.Columns["TenDanhMuc"].HeaderText = "Tên danh mục";
        }
        private void LoadDanhMuc()
        {
            DanhMucBLL danhMucBLL = new DanhMucBLL();
            dgvCate.DataSource = danhMucBLL.getAllDanhMucBLL();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCate.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục.");
                return;
            }

            string tenDanhMuc = txtCate.Text.Trim();

            DanhMuc newCategory = new DanhMuc
            {
                TenDanhMuc = tenDanhMuc
            };

            DanhMucBLL danhMucBLL = new DanhMucBLL();
            bool isAdded = danhMucBLL.AddDanhMuc(newCategory);

            if (isAdded)
            {
                MessageBox.Show("Danh mục đã được thêm thành công.");
                LoadDanhMuc();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm danh mục.");
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCategoryId == -1)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa.");
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa danh mục này?", "Xác nhận", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                DanhMucBLL danhMucBLL = new DanhMucBLL();
                bool isDeleted = danhMucBLL.DeleteDanhMuc(selectedCategoryId);

                if (isDeleted)
                {
                    MessageBox.Show("Danh mục đã được xóa.");
                    LoadDanhMuc();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi xóa danh mục.");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedCategoryId == -1)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần chỉnh sửa.");
                return;
            }
            if (string.IsNullOrEmpty(txtCate.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục.");
                return;
            }
            string newTenDanhMuc = txtCate.Text.Trim();

            DanhMucBLL danhMucBLL = new DanhMucBLL();
            bool isUpdated = danhMucBLL.UpdateDanhMuc(selectedCategoryId, newTenDanhMuc);

            if (isUpdated)
            {
                MessageBox.Show("Danh mục đã được cập nhật.");
                LoadDanhMuc();
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật danh mục.");
            }
        }

        

        private void dgvCate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvCate.Rows[e.RowIndex];

                string tenDanhMuc = selectedRow.Cells["TenDanhMuc"].Value.ToString();
                txtCate.Text = tenDanhMuc;

                selectedCategoryId = int.Parse(selectedRow.Cells["DanhMucID"].Value.ToString());
            }
        }
    }
}
