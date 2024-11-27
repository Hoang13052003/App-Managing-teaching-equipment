using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class Teaching_Schedule : Form
    {
        private ThoiKhoaBieuBUS tkbBUS = new ThoiKhoaBieuBUS();
        private List<ThoiKhoaBieuChiTietDTO> _listTKB = new List<ThoiKhoaBieuChiTietDTO>();
        private Dictionary<DayOfWeek, List<ThoiKhoaBieuChiTietDTO>> scheduleByDay = new Dictionary<DayOfWeek, List<ThoiKhoaBieuChiTietDTO>>();
        private DateTime currentDate;
        private MuonThietBiBUS mtbBUS = new MuonThietBiBUS();

        public Teaching_Schedule()
        {
            InitializeComponent();
            currentDate = DateTime.Now; // Khởi tạo ngày hiện tại
            Load_Visible_Pannel(false);
            Load_Clear_Pannel();
            AccountInfo.MaNguoiDung = "ND00000001";
        }

        private void Teaching_Schedule_Load(object sender, EventArgs e)
        {
            LoadSchedule(currentDate);
            DisplayDates(currentDate);
        }

        private void LoadSchedule(DateTime currentDate)
        {
            DateTime startOfWeek = currentDate.AddDays(-(int)(currentDate.DayOfWeek == DayOfWeek.Sunday ? 7 : currentDate.DayOfWeek - DayOfWeek.Monday));
            DateTime endOfWeek = startOfWeek.AddDays(6);
            
            _listTKB = tkbBUS.GetThoiKhoaBieuByUser("ND00000001", startOfWeek.Date, endOfWeek.Date);
            if (_listTKB.Count()>0)
            {
                scheduleByDay.Clear();
                foreach (var item in _listTKB)
                {
                    if (item.NgayHoc.HasValue)
                    {
                        DayOfWeek day = item.NgayHoc.Value.DayOfWeek;
                        if (!scheduleByDay.ContainsKey(day))
                        {
                            scheduleByDay[day] = new List<ThoiKhoaBieuChiTietDTO>();
                        }
                        scheduleByDay[day].Add(item);
                    }
                }

                DisplaySchedule();
                DisplayDates(currentDate);
            }
            else
            {
                MessageBox.Show("Tuần này bạn Chưa có lịch làm!", "Thông báo");
            }
        }

        private void DisplaySchedule()
        {
            //Duyệt qua dictionary để hiển thị thời khóa biểu theo từng ngày
            foreach (var daySchedule in scheduleByDay)
            {
                DayOfWeek day = daySchedule.Key;
                List<ThoiKhoaBieuChiTietDTO> scheduleList = daySchedule.Value;

                Allocating_Teaching_Schedules_For_The_Week(day, scheduleList);
            }
        }

        private void Allocating_Teaching_Schedules_For_The_Week(DayOfWeek day, List<ThoiKhoaBieuChiTietDTO> scheduleList)
        {
            switch (day)
            {
                case DayOfWeek.Monday:
                    {
                        foreach (var item in scheduleList)
                        {
                            Color subjectColor = GetColor(AccountInfo.MaNguoiDung, item.MaTKB);
                            var newForm = new Show_Subjects
                            {
                                _tkbDTO = item,
                                _color = subjectColor,
                            };
                            if (GetSession(item.GioHoc))
                            {
                                pannel_Lich_T2_Sang.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_T2_Sang, newForm);
                            }
                            else
                            {
                                pannel_Lich_T2_Chieu.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_T2_Chieu, newForm);
                            }
                        }
                    }
                    break;

                case DayOfWeek.Tuesday:
                    {
                        foreach (var item in scheduleList)
                        {
                            Color subjectColor = GetColor(AccountInfo.MaNguoiDung, item.MaTKB);
                            var newForm = new Show_Subjects
                            {
                                _tkbDTO = item,
                                _color = subjectColor,
                            };
                            if (GetSession(item.GioHoc))
                            {
                                pannel_Lich_T3_Sang.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_T3_Sang, newForm);
                            }
                            else
                            {
                                pannel_Lich_T3_Chieu.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_T3_Chieu, newForm);
                            }
                        }
                    }
                    break;

                case DayOfWeek.Wednesday:
                    {
                        foreach (var item in scheduleList)
                        {
                            Color subjectColor = GetColor(AccountInfo.MaNguoiDung, item.MaTKB);
                            var newForm = new Show_Subjects
                            {
                                _tkbDTO = item,
                                _color = subjectColor,
                            };
                            if (GetSession(item.GioHoc))
                            {
                                pannel_Lich_T4_Sang.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_T4_Sang, newForm);
                            }
                            else
                            {
                                pannel_Lich_T4_Chieu.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_T4_Chieu, newForm);
                            }
                        }
                    }
                    break;

                case DayOfWeek.Thursday:
                    {
                        foreach (var item in scheduleList)
                        {
                            Color subjectColor = GetColor(AccountInfo.MaNguoiDung, item.MaTKB);
                            var newForm = new Show_Subjects
                            {
                                _tkbDTO = item,
                                _color = subjectColor,
                            };
                            if (GetSession(item.GioHoc))
                            {
                                pannel_Lich_T5_Sang.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_T5_Sang, newForm);
                            }
                            else
                            {
                                pannel_Lich_T5_Chieu.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_T5_Chieu, newForm);
                            }
                        }
                    }
                    break;

                case DayOfWeek.Friday:
                    {
                        foreach (var item in scheduleList)
                        {
                            Color subjectColor = GetColor(AccountInfo.MaNguoiDung, item.MaTKB);
                            var newForm = new Show_Subjects
                            {
                                _tkbDTO = item,
                                _color = subjectColor,
                            };
                            if (GetSession(item.GioHoc))
                            {
                                pannel_Lich_T6_Sang.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_T6_Sang, newForm);
                            }
                            else
                            {
                                pannel_Lich_T6_Chieu.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_T6_Chieu, newForm);
                            }
                        }
                    }
                    break;

                case DayOfWeek.Saturday:
                    {
                        foreach (var item in scheduleList)
                        {
                            Color subjectColor = GetColor(AccountInfo.MaNguoiDung, item.MaTKB);
                            var newForm = new Show_Subjects
                            {
                                _tkbDTO = item,
                                _color = subjectColor,
                            };
                            if (GetSession(item.GioHoc))
                            {
                                pannel_Lich_T7_Sang.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_T7_Sang, newForm);
                            }
                            else
                            {
                                pannel_Lich_T7_Chieu.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_T7_Chieu, newForm);
                            }
                        }
                    }
                    break;

                case DayOfWeek.Sunday:
                    {
                        foreach (var item in scheduleList)
                        {
                            Color subjectColor = GetColor(AccountInfo.MaNguoiDung, item.MaTKB);
                            var newForm = new Show_Subjects
                            {
                                _tkbDTO = item,
                                _color = subjectColor,
                            };
                            if (GetSession(item.GioHoc))
                            {
                                pannel_Lich_TCN_Sang.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_TCN_Sang, newForm);
                            }
                            else
                            {
                                pannel_Lich_TCN_Chieu.Visible = true;
                                FormTask.OpenFormInPanel(pannel_Lich_TCN_Chieu, newForm);
                            }
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Ngày không hợp lệ.");
                    break;
            }
        }

        private Color GetColor(string maNguoiDung, int maTKB)
        {
            return mtbBUS.GetByMaND_MaTKB(maNguoiDung, maTKB) != null ? Color.FromArgb(239, 182, 200) : Color.FromArgb(192, 192, 255);

        }

        private bool GetSession(TimeSpan? gioHoc)
        {
            if (gioHoc.HasValue)
            {
                int hour = gioHoc.Value.Hours;
                if (hour >= 6 && hour < 12)
                {
                    return true;
                }
                else if (hour >= 12 && hour < 18)
                {
                    return false;
                }
            }
            return false;
        }


        //Load
        private void Load_Visible_Pannel(bool True_False)
        {
            pannel_Lich_T2_Chieu.Visible = True_False;
            pannel_Lich_T2_Sang.Visible = True_False;
            pannel_Lich_T3_Chieu.Visible = True_False;
            pannel_Lich_T3_Sang.Visible = True_False;
            pannel_Lich_T4_Chieu.Visible = True_False;
            pannel_Lich_T4_Sang.Visible = True_False;
            pannel_Lich_T5_Chieu.Visible = True_False;
            pannel_Lich_T5_Sang.Visible = True_False;
            pannel_Lich_T6_Chieu.Visible = True_False;
            pannel_Lich_T6_Sang.Visible = True_False;
            pannel_Lich_T7_Chieu.Visible = True_False;
            pannel_Lich_T7_Sang.Visible = True_False;
            pannel_Lich_TCN_Chieu.Visible = True_False;
            pannel_Lich_TCN_Sang.Visible = True_False;
        }
        private void Load_Clear_Pannel()
        {
            pannel_Lich_T2_Chieu.Controls.Clear();
            pannel_Lich_T2_Sang.Controls.Clear();
            pannel_Lich_T3_Sang.Controls.Clear();
            pannel_Lich_T4_Sang.Controls.Clear();
            pannel_Lich_T5_Sang.Controls.Clear();
            pannel_Lich_T6_Sang.Controls.Clear();
            pannel_Lich_T7_Sang.Controls.Clear();
            pannel_Lich_TCN_Sang.Controls.Clear();
            pannel_Lich_T3_Chieu.Controls.Clear();
            pannel_Lich_T4_Chieu.Controls.Clear();
            pannel_Lich_T5_Chieu.Controls.Clear();
            pannel_Lich_T6_Chieu.Controls.Clear();
            pannel_Lich_T7_Chieu.Controls.Clear();
            pannel_Lich_TCN_Chieu.Controls.Clear();
        }
        private void DisplayDates(DateTime currentDate)
        {
            DateTime startOfWeek = currentDate.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);

            lb_date_T2.Text = startOfWeek.ToString("dd/MM/yyyy"); // Thứ Hai
            lb_date_T3.Text = startOfWeek.AddDays(1).ToString("dd/MM/yyyy"); // Thứ Ba
            lb_date_T4.Text = startOfWeek.AddDays(2).ToString("dd/MM/yyyy"); // Thứ Tư
            lb_date_T5.Text = startOfWeek.AddDays(3).ToString("dd/MM/yyyy"); // Thứ Năm
            lb_date_T6.Text = startOfWeek.AddDays(4).ToString("dd/MM/yyyy"); // Thứ Sáu
            lb_date_T7.Text = startOfWeek.AddDays(5).ToString("dd/MM/yyyy"); // Thứ Bảy
            lb_date_TCN.Text = startOfWeek.AddDays(6).ToString("dd/MM/yyyy"); // Chủ Nhật
        }
        //Action
        private void btn_Hien_Tai_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_Tiep_Click(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(7);
            Load_Visible_Pannel(false);
            Load_Clear_Pannel();
            LoadSchedule(currentDate);
        }

        private void btn_Tro_Ve_Click(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(-7);
            Load_Visible_Pannel(false);
            Load_Clear_Pannel();
            LoadSchedule(currentDate);
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
