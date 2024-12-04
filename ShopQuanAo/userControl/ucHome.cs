using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace userControl
{
    public partial class ucHome : UserControl
    {
        public ucHome()
        {
            InitializeComponent();
        }

        private void ucHome_Load(object sender, EventArgs e)
        {
            // Cấu hình Timer
            timer1.Interval = 1000; // 1 giây
            timer1.Tick += Timer1_Tick; // Gán sự kiện Tick
            timer1.Start(); // Bắt đầu Timer

            // Hiển thị thời gian ngay khi UC được tải
            UpdateClock();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void UpdateClock()
        {
            // Lấy thời gian hiện tại
            DateTime now = DateTime.Now;

            // Cập nhật đồng hồ
            lbTime.Text = now.ToString("HH:mm:ss"); // Giờ:Phút:Giây

            // Cập nhật ngày tháng năm theo định dạng yêu cầu
            CultureInfo viCulture = new CultureInfo("vi-VN");
            lbDate.Text = now.ToString("dddd, 'ngày' dd 'tháng' MM 'năm' yyyy", viCulture);
        }
    }
}
