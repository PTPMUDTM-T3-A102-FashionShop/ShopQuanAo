using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace userControl
{
    public partial class ucSizeColor : UserControl
    {
        MauBLL mauBLL = new MauBLL();
        SizeBLL sizeBLL = new SizeBLL();
        public ucSizeColor()
        {
            InitializeComponent();
        }

        private void ucSizeColor_Load(object sender, EventArgs e)
        {
            loadMau();
            loadSize();
        }

        void loadMau()
        {
            List<Mau> maus = mauBLL.getAllMau();
            dgvColor.DataSource = maus;
            dgvColor.Columns["MauID"].Visible = false;
        }

        void loadSize()
        {
            List<DTO.Size> sizes = sizeBLL.getAllSize();
            dgvSize.DataSource = sizes;
            dgvSize.Columns["SizeID"].Visible = false;
        }

        private void dgvSize_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dgvSize.Rows[e.RowIndex];
            txtSize.Text = selectedRow.Cells["TenSize"].Value.ToString();
            dgvSize.Columns["TenSize"].HeaderText = "Tên Size";
        }

        private void dgvColor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dgvColor.Rows[e.RowIndex];
            txtColor.Text = selectedRow.Cells["TenMau"].Value.ToString();
            dgvColor.Columns["TenMau"].HeaderText = "Tên màu";
        }

        private void btnAddSize_Click(object sender, EventArgs e)
        {
            string newTenSize = txtSize.Text.Trim();
            if (string.IsNullOrEmpty(newTenSize))
            {
                MessageBox.Show("Vui lòng nhập tên size!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<DTO.Size> sizes = sizeBLL.getAllSize();
            if (sizes.Any(s => s.TenSize == newTenSize))
            {
                MessageBox.Show("Size đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DTO.Size newSize = new DTO.Size { TenSize = newTenSize };
            sizeBLL.AddSize(newSize);
            MessageBox.Show("Thêm size thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadSize();
            txtSize.Text = string.Empty;
        }
        private void btnDeleteSize_Click(object sender, EventArgs e)
        {
            if (dgvSize.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn size để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedSizeId = Convert.ToInt32(dgvSize.SelectedRows[0].Cells["SizeID"].Value);
            sizeBLL.DeleteSize(selectedSizeId);
            MessageBox.Show("Xóa size thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadSize();
            txtSize.Text = string.Empty;

        }

        private void btnEditSize_Click(object sender, EventArgs e)
        {
            if (dgvSize.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn size để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedSizeId = Convert.ToInt32(dgvSize.SelectedRows[0].Cells["SizeID"].Value);
            string newTenSize = txtSize.Text.Trim();

            if (string.IsNullOrEmpty(newTenSize))
            {
                MessageBox.Show("Vui lòng nhập tên size mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DTO.Size updatedSize = new DTO.Size { SizeID = selectedSizeId, TenSize = newTenSize };
            sizeBLL.UpdateSize(updatedSize);
            MessageBox.Show("Cập nhật size thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadSize();
            txtSize.Text = string.Empty;

        }

        private void btnAddColor_Click(object sender, EventArgs e)
        {
            string newColorName = txtColor.Text.Trim();
            if (string.IsNullOrEmpty(newColorName))
            {
                MessageBox.Show("Vui lòng nhập tên màu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<Mau> colors = mauBLL.getAllMau();
            if (colors.Any(m => m.TenMau == newColorName))
            {
                MessageBox.Show("Màu đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Mau newMau = new Mau { TenMau = newColorName };
            mauBLL.AddMau(newMau);
            MessageBox.Show("Thêm màu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadMau();
            txtColor.Text = string.Empty;
        }


        private void btnDeleteColor_Click(object sender, EventArgs e)
        {
            if (dgvColor.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn màu để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedColorId = Convert.ToInt32(dgvColor.SelectedRows[0].Cells["MauID"].Value);
            mauBLL.DeleteMau(selectedColorId);
            MessageBox.Show("Xóa màu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadMau();
            txtColor.Text = string.Empty;

        }


        private void btnEditColor_Click(object sender, EventArgs e)
        {
            if (dgvColor.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn màu để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedColorId = Convert.ToInt32(dgvColor.SelectedRows[0].Cells["MauID"].Value);
            string newColorName = txtColor.Text.Trim();

            if (string.IsNullOrEmpty(newColorName))
            {
                MessageBox.Show("Vui lòng nhập tên màu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Mau updatedMau = new Mau { MauID = selectedColorId, TenMau = newColorName };
            mauBLL.UpdateMau(updatedMau);
            MessageBox.Show("Cập nhật màu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadMau();
            txtColor.Text = string.Empty;

        }

    }
}
