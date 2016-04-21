using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAIRO.ElasticEmail
{

    public class DeliveryStatusJson
    {
        public DeliveryStatusJsonInner data { get; set; }
    }

    public class DeliveryStatusJsonInner
    {
        public int DeliveredCount { get; set; }
        public object Delivered { get; set; }
        public object Failed { get; set; }
        public int FailedCount { get; set; }
        public Guid Id { get; set; }
        public object Pending { get; set; }
        public int PendingCount { get; set; }
        public object Recipients { get; set; }
        public int RecipientsCount { get; set; }
        public string Status { get; set; }
        public object Clicked { get; set; }
        public int ClickedCount { get; set; }
        public object Opened { get; set; }
        public int OpenedCount { get; set; }
        public object Unsuscribed { get; set; }
        public int UnsuscribedCount { get; set; }
        public object AbuseReports { get; set; }
        public int AbuseReportsCount { get; set; }
    }



}
