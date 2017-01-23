using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyHome.Models
{
    public class DeviceViewModel
    {
        [Required(ErrorMessage = "Brak addressId (zły query string)")]
        public string AddressId { get; set; }

        public string AddressName { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [Display(Name = "Id urządzenia")]
        public string DeviceId { get; set; }

        [Display(Name = "Pracuje")]
        public bool IsOn { get; set; }

        [Display(Name = "Rodzaj")]
        public DataModel.SensorTypeEnum SensorType { get; set; }

        public string Error { get; set; }
    }
}