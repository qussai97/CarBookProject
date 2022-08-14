using CarBookProject.Classes;
using Newtonsoft.Json;

namespace Microsoft.AspNetCore.Mvc
{
    public static class Controllers
    {
        public static void Notification(this Controller controller, string txt)
        {
            Notifications Notification = new Notifications();
            Notification.Notification_txt = txt;


            controller.TempData.Remove("Notification");
            controller.TempData.Add("Notification", JsonConvert.SerializeObject(Notification));
        }

        public static void Notification(this Controller controller, string txt, string tile)
        {
            Notifications Notification = new Notifications();
            Notification.Notification_txt = txt;
            Notification.Notification_Title = tile;

            controller.TempData.Remove("Notification");
            controller.TempData.Add("Notification", JsonConvert.SerializeObject(Notification));
        }


        public static void Notification(this Controller controller, string txt, Noti_Type Notification_Type)
        {
            Notifications Notification = new Notifications();
            Notification.Notification_txt = txt;
            Notification.Notification_Type = Notification_Type;


            controller.TempData.Remove("Notification");
            controller.TempData.Add("Notification", JsonConvert.SerializeObject(Notification));
        }
        
        public static void Notification(this Controller controller, string txt, Noti_Type Notification_Type, Noti_PositionClass Position, int Time_out, string title)
        {
            Notifications Notification = new Notifications();
            Notification.Notification_txt = txt;
            Notification.Notification_Title = title;
            Notification.Notification_Type = Notification_Type;
            Notification.Notification_Title = title;
            Notification.PositionClass = Position;
            Notification.TimeOut = Time_out;

            controller.TempData.Remove("Notification");
            controller.TempData.Add("Notification", JsonConvert.SerializeObject(Notification));
        }
    }
}
