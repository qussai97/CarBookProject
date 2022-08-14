using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookProject.Classes
{
    public class Notifications
    {
        public string Notification_txt { get; set; }
        public string Notification_Title { get; set; }
        public Noti_Type Notification_Type { get; set; }
        public Noti_PositionClass PositionClass { get; set; }
        private int Time_Out { get; set; }
        public int TimeOut// set in seconds
        {
            set
            {
                if (value > 0) Time_Out = value;
            }
            get
            {
                if (Time_Out > 0) return Time_Out;
                else return 10;
            }
        }
    }
    public enum Noti_Type
    {
        warning,
        error,
        success,
        info
    }
    public enum Noti_PositionClass
    {
        toast_top_center,
        toast_top_right,
        toast_bottom_right,
        toast_bottom_left,
        toast_top_left,
        toast_top_full_width,
        toast_bottom_full_width,
        toast_bottom_center
    }

}

