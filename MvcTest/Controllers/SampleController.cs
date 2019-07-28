using MvcTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MvcTest.Controllers
{
    public class SampleController : Controller
    {
		IList<Sample> mockData = new List<Sample>();

		public SampleController()
		{
			ViewBag.InstalledByList = new List<SelectListItem>() {
				new SelectListItem() { Value = "1", Text = "Item 1" },
				new SelectListItem() { Value = "2", Text = "Item 2" },
				new SelectListItem() { Value = "3", Text = "Item 3" },
			};

			mockData.Add(new Sample()
			{
				Id = 1,
				Brand = "Marca",
				EquipmentId = "YGIY7Y7867",
				InstalledBy = (long)2,
				Quantity = 10,
				MensalValueStr = "23.45",
				Progress = 50,
				MSISDN = "123456789",
				PhoneNumber = "999999999",
				Observations = "Nada a informar 1!"
			});
			mockData.Add(new Sample()
			{
				Id = 2,
				Brand = "Marca 2",
				EquipmentId = "YUHVBKUT9Y",
				InstalledBy = (long)3,
				Quantity = 1,
				MensalValueStr = "87.99",
				Progress = 80,
				MSISDN = "987654321",
				PhoneNumber = "988888888",
				Observations = "Nada a informar 2!"
			});
			mockData.Add(new Sample()
			{
				Id = 3,
				Brand = "Marca 3",
				EquipmentId = "7GH78GO78I",
				InstalledBy = (long)2,
				Quantity = 2,
				MensalValueStr = "29.99",
				Progress = 20,
				MSISDN = "12345",
				PhoneNumber = "988888889",
				Observations = "Nada a informar 3!"
			});
		}

		private SampleVM Clone(Sample sample)
		{
			SampleVM vm = new SampleVM();

			foreach (PropertyInfo sourcePropertyInfo in sample.GetType().GetProperties())
			{
				PropertyInfo destPropertyInfo = vm.GetType().GetProperty(sourcePropertyInfo.Name);
				destPropertyInfo.SetValue(vm, sourcePropertyInfo.GetValue(sample, null), null);
			}

			return vm;
		}

		// GET: Sample
		public ActionResult Index()
        {
            return View();
        }

        // GET: Sample/Details/5
        public ActionResult Details(int id)
		{
			SampleVM sampleData = Clone(mockData.Where(w => w.Id == id).FirstOrDefault());
			return View(sampleData);
		}

        // GET: Sample/Create
        public ActionResult Create()
        {
			var form = new SampleVM();

			return View(form);
        }

        // POST: Sample/Create
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

        // GET: Sample/Edit/5
        public ActionResult Edit(int id)
		{
			SampleVM sampleData = Clone(mockData.Where(w => w.Id == id).FirstOrDefault());
			return View(sampleData);
		}

        // POST: Sample/Edit/5
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

        // GET: Sample/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sample/Delete/5
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
