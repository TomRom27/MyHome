using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyHome.Models
{
    public class ReadoutViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Czas")]
        public System.DateTime At { get; set; }

        [Display(Name = "Rodzaj")]
        public DataModel.SensorTypeEnum ReadoutType { get; set; }

        [Display(Name = "Wartość")]
        public decimal Value { get; set; }

        [Display(Name = "Pracuje")]
        public bool ActionOn { get; set; }

        [Display(Name = "Identyfikator urządzenia")]
        public string DeviceId { get; set; }

        [Display(Name = "Adres")]
        public string AddressName { get; set; }

        public string AddressId { get; set; }
    }
}