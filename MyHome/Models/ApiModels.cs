using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHome.Models
{
    public class ReadoutRequestModel
    {
        public string Secret;
        public int? ReadoutId; 
        public DateTime? At;
        public string DeviceId;
        public decimal Value;
    }

}