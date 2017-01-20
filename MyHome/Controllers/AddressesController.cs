﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyHome.DataModel;
using MyHome.Models;

namespace MyHome.Controllers
{
    public class AddressesController : Controller
    {
        public const string ClassName = "Addresses";

        private DataModelContainer db = new DataModelContainer();

        // GET: Addresses
        public ActionResult Index()
        {
            var list = db.AddressSet.ToList().Select((a) => AutoMapper.Mapper.Map<AddressViewModel>(a)).ToList();
            return View(list);
        }

        public const string DetailsAction = "Details";
        // GET: Addresses/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.AddressSet.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }

            return View(AutoMapper.Mapper.Map<AddressViewModel>(address));
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressId,FriendlyName")] AddressViewModel address)
        {
            if (ModelState.IsValid)
            {
                var dbAddress = AutoMapper.Mapper.Map<Address>(address);

                var defaultOwner = db.OwnerGroupSet.Find(Properties.Settings.Default.OwnerGroupIdDefault);
                dbAddress.Owner = defaultOwner;

                db.AddressSet.Add(dbAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.AddressSet.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AddressId,FriendlyName")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.AddressSet.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Address address = db.AddressSet.Find(id);
            db.AddressSet.Remove(address);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Device related
        public ActionResult CreateDevice(string addressId, string friendlyName)
        {
            var device = new DeviceViewModel() { AddressId = addressId, AddressName = friendlyName };
            ViewBag.AddressName = friendlyName;
            ViewBag.Error = "";
            return View(device);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDevice(DeviceViewModel device)
        {
            if (ModelState.IsValid)
            {
                // check uniqueness
                bool deviceExists = db.DeviceSet.Find(device.DeviceId) != null;
                if (deviceExists)
                {
                    ModelState.AddModelError(nameof(device.DeviceId), "Takie urządzenie już istnieje, zmień Id");
                    return View(device);
                }

                // verify is address correct
                // todo - check if user has access to address
                var dbDevice = AutoMapper.Mapper.Map<Device>(device);

                var dbAddress = db.AddressSet.Find(device.AddressId);
                if (dbAddress == null)
                    return HttpNotFound("Address not found by Id");

                dbAddress.Devices.Add(dbDevice);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = dbAddress.AddressId });
            }
            else
            {
                string errorMsg;
                var errorState = ModelState.FirstOrDefault(ms => ms.Value.Errors.Count > 0);

                if (!string.IsNullOrEmpty(errorState.Key))
                    errorMsg = errorState.Key + ": " + errorState.Value.Errors[0].ErrorMessage;
                else
                    errorMsg = errorState.Value.Errors[0].ErrorMessage;

                ModelState.AddModelError("", errorMsg);
                return View(device);
            }
        }


        #endregion
    }
}
