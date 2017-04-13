using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyHome.Models
{
    public class ReadoutViewModel
    {

        public const string ReadoutDateTimeFormat = "dd.MM.yyyy HH:mm";
        public const string ReadoutDateFormat = "dd.MM.yyyy";
        public int Id { get; set; }

        [Display(Name = "Czas")]
        [DisplayFormat(DataFormatString = "{0:" + ReadoutDateTimeFormat + "}")]
        public System.DateTime At { get; set; }

        [Display(Name = "Rodzaj")]
        public DataModel.SensorTypeEnum ReadoutType { get; set; }

        [Display(Name = "Wartość")]
        public decimal Value { get; set; }

        [Display(Name = "Pracuje")]
        public bool ActionOn { get; set; }

        [Display(Name = "Uwagi")]
        public string Comment { get; set; }

        [Display(Name = "Identyfikator urządzenia")]
        public string DeviceId { get; set; }

        [Display(Name = "Adres")]
        public string AddressName { get; set; }

        public string AddressId { get; set; }
    }

    public static class DateTimeExtensionFromReadout
    {
        public static bool MatchesReadoutDatePattern(this DateTime readoutTime, string pattern)
        {
            string readoutTimeString = readoutTime.ToString(ReadoutViewModel.ReadoutDateTimeFormat);

            if (readoutTimeString.Equals(pattern))
                return true;

            int range = Math.Min(readoutTimeString.Length, pattern.Length);
            for (int i = 0; i <= range-1; i++)
                if (!(pattern[i].EqualsInvariantAndCase(ReadoutViewModel.ReadoutDateTimeFormat[i]) || // pattern char is the same as format char = anything is accaptable 
                      readoutTimeString[i].EqualsInvariantAndCase(pattern[i])))
                    return false;

            return true;
        }

        public static bool EqualsInvariantAndCase(this char c1, char c2)
        {
            return char.ToUpperInvariant(c1) == char.ToUpperInvariant(c2);
        }
    }
}