﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CyScada.Model;

namespace CyScada.Web.Controllers
{
    public class ParameterSettingController : Controller
    {
        //
        // GET: /ParameterSetting/

        public ActionResult Index()
        {
            var user = Session["User"] as UserModel;
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Id = user.Id;
            ViewBag.Name = user.Name;
            return View();
        }

        //
        // GET: /ParameterSetting/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /ParameterSetting/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ParameterSetting/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ParameterSetting/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ParameterSetting/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ParameterSetting/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ParameterSetting/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
