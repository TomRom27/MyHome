using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyHome.Models
{
    public class AddressViewModel
    {

        [Display(Name = "Id")]
        public string AddressId { get; set; }

        [Display(Name = "Nazwa")]
        public string FriendlyName { get; set; }

        public virtual ICollection<DeviceViewModel> Devices { get; set; }
    }

}