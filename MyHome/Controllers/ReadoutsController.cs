using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;
using PagedList;

using MyHome.DataModel;
using MyHome.Models;

namespace MyHome.Controllers
{

    public class ReadoutsController : Controller
    {
        public static List<SelectListItem> FilterList = CreateFilterList();
        public static List<SelectListItem> PageSizeList = CreatePageSizeList();

        private DataModelContainer db = new DataModelContainer();

        // GET: Readouts
        // filterDropbox argument name must be the same, as the dropbox name in the view!!!
        public ActionResult Index(string filterDropbox, string filterString, string pageSizeDropbox, int? page, string sortOrder)
        {
            int pageSize = 30;
            try
            {
                int.Parse(pageSizeDropbox);
            }
            catch
            {
                ; // ignore
            }

            bool sortOrderAscending = HandleSortOrder(sortOrder);
            var dataList = GetFilteredReadouts(filterDropbox, filterString, sortOrderAscending);

            var vmList = dataList.Select(d => Mapper.Map<Models.ReadoutViewModel>(d));

            // restore filter dropbox state  
            foreach (var l in FilterList)
                if (l.Value.Equals(filterDropbox))
                    l.Selected = true;
            ViewData["FilterList"] = FilterList;

            // restore page size dropbox state  
            foreach (var l in PageSizeList)
                if (l.Value.Equals(pageSizeDropbox))
                    l.Selected = true;

            ViewData["PageSizeList"] = PageSizeList;



            int pageNumber = (page ?? 1);
            return View(vmList.ToPagedList(pageNumber, pageSize));
        }

        public const string SortAscending = "asc";
        public const string SortDescending = "desc";

        private bool HandleSortOrder(string sortOrder)
        {
            bool sortOrderAscending = false; // default order is DESCENDING!

            if (!String.IsNullOrEmpty(sortOrder) &&
                sortOrder.Equals(SortAscending))
            {
                ViewData["SortOrder"] = SortAscending;
                sortOrderAscending = true;
            }
            else
            {
                ViewData["SortOrder"] = SortDescending;
                sortOrderAscending = false;

            }
            ViewData["SortAscend"] = sortOrderAscending;
            ViewData["SortDescend"] = !sortOrderAscending;

            return sortOrderAscending;
        }



        // GET: Readouts/GotoAddress/GITAROWA15
        public ActionResult GotoAddress(string id)
        {
            // ^- action method argument MUST be called "id" - otherwise MVC cannot pass it correctly
            return RedirectToAction(AddressesController.DetailsAction, AddressesController.ClassName, new { id = id });

        }

        const string ByDevice = "1";
        const string ByDate = "2";
        const string ByType = "3";
        const string ByActive = "4";
        const string ByAddress = "5";

        private static List<SelectListItem> CreateFilterList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "Urządzenie", Value = ByDevice });
            list.Add(new SelectListItem() { Text = "Data i czas", Value = ByDate });
            list.Add(new SelectListItem() { Text = "Rodzaj", Value = ByType });
            list.Add(new SelectListItem() { Text = "Pracuje", Value = ByActive });
            list.Add(new SelectListItem() { Text = "Adres", Value = ByAddress });

            return list;
        }

        private static List<SelectListItem> CreatePageSizeList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "30", Value = "30" });
            list.Add(new SelectListItem() { Text = "50", Value = "50" });
            list.Add(new SelectListItem() { Text = "100", Value = "100" });
            return list;
        }

        private List<Readout> GetFilteredReadouts(string filterDropbox, string filterString, bool sortOrderAscending)
        {
            IQueryable<Readout> query;

            switch (filterDropbox)
            {
                case ByDevice:
                    {
                        query = from r in db.ReadoutSet where r.Device.DeviceId.Contains(filterString) select r;
                        break;
                    }
                //case ByDate
                case ByType:
                    {
                        query = from r in db.ReadoutSet where r.Device.SensorType.ToString().Contains(filterString) select r;
                        break;
                    }
                case ByActive:
                    {
                        bool b = BoolStringToBool(filterString);
                        query = from r in db.ReadoutSet where r.ActionOn == b select r;
                        break;
                    }
                // todo 
                case ByAddress:
                    {
                        query = from r in db.ReadoutSet where r.Device.DeviceAddress.FriendlyName.Contains(filterString) select r;
                        break;
                    }
                case ByDate:
                    {
                        // here we unfortunatelly can't select by db, so we get all and filter in the code
                        query = from r in db.ReadoutSet select r;
                        List<Readout> list;
                        if (sortOrderAscending)
                            list = query.OrderBy(r => r.At).ToList();
                        else
                            list = query.OrderByDescending(r => r.At).ToList();

                        return list.FindAll((r) => r.At.MatchesReadoutDatePattern(filterString));
                        
                    }
                default:
                    {
                        query = from r in db.ReadoutSet select r;
                        break;
                    }
            }
            if (sortOrderAscending)
                return query.OrderBy(r => r.At).ToList();
            else
                return query.OrderByDescending(r => r.At).ToList();
        }

        private int BoolStringToInt(string s)
        {
            s = s.ToLower(System.Globalization.CultureInfo.InvariantCulture);
            if (s.Equals("1") || s.Equals("true"))
                return 1;
            else
                return 0;
        }

        private bool BoolStringToBool(string s)
        {
            s = s.ToLower(System.Globalization.CultureInfo.InvariantCulture);
            if (s.Equals("1") || s.Equals("true"))
                return true;
            else
                return false;
        }

    }
}
