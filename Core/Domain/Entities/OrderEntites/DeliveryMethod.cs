﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntites
{
    public class DeliveryMethod:BaseEntity<int>
    {
        public DeliveryMethod() { } 
     

        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }

        public DeliveryMethod(string shortName, string description, string deliveryTime, decimal price)
        {
            ShortName = shortName;
            Description = description;
            DeliveryTime = deliveryTime;
            Price = price;
        }
    }
}
