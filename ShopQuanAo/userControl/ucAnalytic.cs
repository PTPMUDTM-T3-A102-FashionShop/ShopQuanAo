using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using DB;

using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Statistics.Filters;
using Accord.Math;
using System.Windows.Forms.DataVisualization.Charting;

namespace userControl
{
    public partial class ucAnalytic : UserControl
    {
        private DBConnection dbConnection;
        public ucAnalytic()
        {
            InitializeComponent();
            dbConnection = new DBConnection();

            // Đặt thuộc tính DropDownStyle cho ComboBox
            cbTenMau.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTenSize.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void ucAnalytic_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBoxes(); // Gọi phương thức để load dữ liệu cho combobox

            cbTenSize.SelectedIndex = -1;
            cbTenMau.SelectedIndex = -1;
            dgvSanPham.ReadOnly = true; // Ngăn người dùng chỉnh sửa dữ liệu trực tiếp trên DataGridView
        }

        private void LoadData()
        {
            try
            {
                dbConnection.OpenConnection();

                string query = @"
                    SELECT sp.MoTa, m.TenMau, s.TenSize, ctp.Gia, ctp.SoLuongTonKho
                    FROM SanPham sp
                    INNER JOIN ChiTietSanPham ctp ON sp.SanPhamID = ctp.SanPhamID
                    INNER JOIN Mau m ON ctp.MauID = m.MauID
                    INNER JOIN Size s ON ctp.SizeID = s.SizeID";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, dbConnection.conn);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Định dạng cột Giá
                dataTable.Columns["Gia"].ColumnMapping = MappingType.Element;
                foreach (DataRow row in dataTable.Rows)
                {
                    row["Gia"] = Convert.ToDecimal(row["Gia"]).ToString("N2");
                }

                dgvSanPham.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        private void LoadComboBoxes()
        {
            try
            {
                dbConnection.OpenConnection();

                // Lấy danh sách TenMau
                string queryMau = "SELECT MauID, TenMau FROM Mau";
                SqlDataAdapter mauAdapter = new SqlDataAdapter(queryMau, dbConnection.conn);
                DataTable mauTable = new DataTable();
                mauAdapter.Fill(mauTable);

                cbTenMau.DataSource = mauTable;
                cbTenMau.DisplayMember = "TenMau";
                cbTenMau.ValueMember = "MauID";

                // Lấy danh sách TenSize
                string querySize = "SELECT SizeID, TenSize FROM Size";
                SqlDataAdapter sizeAdapter = new SqlDataAdapter(querySize, dbConnection.conn);
                DataTable sizeTable = new DataTable();
                sizeAdapter.Fill(sizeTable);

                cbTenSize.DataSource = sizeTable;
                cbTenSize.DisplayMember = "TenSize";
                cbTenSize.ValueMember = "SizeID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading combobox data: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbTenMau_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbTenSize_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSoLuongTonKho_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDuDoan_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn dữ liệu hay chưa
            if (cbTenMau.SelectedIndex == -1 || cbTenSize.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtGia.Text) || string.IsNullOrWhiteSpace(txtSoLuongTonKho.Text))
            {
                MessageBox.Show("Vui lòng chọn đầy đủ dữ liệu trước khi dự đoán.");
                return;
            }

            // Kiểm tra giá nhập có phải là số dương hay không
            if (!decimal.TryParse(txtGia.Text, out decimal gia) || gia <= 0)
            {
                MessageBox.Show("Giá phải là một số dương.");
                return;
            }

            // Kiểm tra số lượng tồn kho có phải là số nguyên dương hay không
            if (!int.TryParse(txtSoLuongTonKho.Text, out int soLuongTonKho) || soLuongTonKho < 0)
            {
                MessageBox.Show("Số lượng tồn kho phải là một số nguyên không âm.");
                return;
            }

            bool isPredictionSuccessful = false;

            // Dự đoán sử dụng thuật toán ID3
            try
            {
                DataTable data = LoadDataForPrediction();
                Codification codebook = new Codification(data);
                DataTable symbols = codebook.Apply(data);

                int[][] inputs = symbols.ToArray<int>("TenMau", "TenSize", "Gia", "SoLuongTonKho");
                int[] outputs = symbols.ToArray<int>("MoTa");

                var teacher = new ID3Learning();
                DecisionTree tree = teacher.Learn(inputs, outputs);

                // Lấy dữ liệu từ người dùng nhập vào
                int tenMau = (int)cbTenMau.SelectedValue;
                int tenSize = (int)cbTenSize.SelectedValue;
                int giaNhap = (int)gia;
                int soLuong = soLuongTonKho;

                // Tạo mảng đầu vào cho dự đoán
                int[] input = new int[] { tenMau, tenSize, giaNhap, soLuong };

                // Dự đoán
                int predicted = tree.Decide(input);

                // Hiển thị kết quả dự đoán
                string moTa = codebook.Revert("MoTa", predicted);

                // Lấy câu đầu tiên từ mô tả dự đoán
                string moTaFirstSentence = moTa.Split(new[] { '.' }, 2)[0] + ".";

                // Hiển thị câu đầu tiên trong label
                lblDuDoan.Text = $"Dự đoán: {moTaFirstSentence}";
                isPredictionSuccessful = true;
            }
            catch
            {
                // Không làm gì ở đây, sẽ xử lý sau
            }

            // Truy vấn cơ sở dữ liệu để lấy 3 sản phẩm có SoLuongTonKho cao nhất
            try
            {
                dbConnection.OpenConnection();

                string query = @"
            SELECT TOP 3 sp.MoTa, ctp.SoLuongTonKho
            FROM SanPham sp
            INNER JOIN ChiTietSanPham ctp ON sp.SanPhamID = ctp.SanPhamID
            INNER JOIN Mau m ON ctp.MauID = m.MauID
            INNER JOIN Size s ON ctp.SizeID = s.SizeID
            WHERE m.MauID = @MauID AND s.SizeID = @SizeID
            ORDER BY ctp.SoLuongTonKho DESC";

                SqlCommand cmd = new SqlCommand(query, dbConnection.conn);
                cmd.Parameters.AddWithValue("@MauID", cbTenMau.SelectedValue);
                cmd.Parameters.AddWithValue("@SizeID", cbTenSize.SelectedValue);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Hiển thị dữ liệu lên biểu đồ
                chartTopProducts.Series.Clear();
                chartTopProducts.Titles.Clear();
                chartTopProducts.Titles.Add("Top 3 sản phẩm theo số lượng tồn kho");

                Series series = new Series
                {
                    Name = "Sản phẩm",
                    IsValueShownAsLabel = true,
                    ChartType = SeriesChartType.Bar
                };

                chartTopProducts.Series.Add(series);

                foreach (DataRow row in dataTable.Rows)
                {
                    series.Points.AddXY(row["MoTa"].ToString(), row["SoLuongTonKho"]);
                    chartTopProducts.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
                    chartTopProducts.ChartAreas[0].AxisX.LabelStyle.IsStaggered = false;
                }

                if (isPredictionSuccessful)
                {
                    MessageBox.Show("Dự đoán thành công");
                }
                else
                {
                    MessageBox.Show("Dự đoán không thành công, hãy tham khảo Top 3 sản phẩm khuyên dùng!");
                }
            }
            catch
            {
                MessageBox.Show("Dự đoán không thành công, hãy tham khảo Top 3 sản phẩm khuyên dùng!");
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }

        // Phương thức LoadDataForPrediction để tải dữ liệu từ cơ sở dữ liệu
        public DataTable LoadDataForPrediction()
        {
            string query = @"
                SELECT m.TenMau, s.TenSize, ctp.Gia, ctp.SoLuongTonKho, sp.MoTa
                FROM SanPham sp
                INNER JOIN ChiTietSanPham ctp ON sp.SanPhamID = ctp.SanPhamID
                INNER JOIN Mau m ON ctp.MauID = m.MauID
                INNER JOIN Size s ON ctp.SizeID = s.SizeID";

            SqlDataAdapter da = new SqlDataAdapter(query, dbConnection.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        private void lblDuDoan_Click(object sender, EventArgs e)
        {

        }

        private void txtMoTa_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl3MoTa_Click(object sender, EventArgs e)
        {

        }

        private void chartTopProducts_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
