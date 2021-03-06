﻿using System;

namespace CAIRO.ElasticEmail
{
    public class DeliveryStatusXml
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public int Recipients { get; set; }
        public int Failed { get; set; }
        public int Delivered { get; set; }
        public int Pending { get; set; }
        public int Clicked { get; set; }
        public int Opened { get; set; }
        public int Unsubscribed { get; set; }
    }
}