﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AJAXAPP.Controllers
{
    public class CityController : Controller
    {

        private readonly AppDbContext _context;

        public CityController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<City> Cities;
            Cities = _context.Cities
                .Include(c => c.Country)
                .ToList();
            return View(Cities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            City City = new City();
            ViewBag.Countries = GetCountries();
            return View(City);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(City City)
        {

            _context.Add(City);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            City City = _context.Cities
               .Include(c => c.Country)
              .Where(c => c.Id == Id).FirstOrDefault();

            return View(City);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            City City = _context.Cities
               .Include(c => c.Country)
              .Where(c => c.Id == Id).FirstOrDefault();

            ViewBag.Countries = GetCountries(City.CountryId);

            return View(City);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(City City)
        {
            _context.Attach(City);
            _context.Entry(City).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            City City = _context.Cities
              .Where(c => c.Id == Id).FirstOrDefault();

            return View(City);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(City City)
        {
            _context.Attach(City);
            _context.Entry(City).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        private List<SelectListItem> GetCountries()
        {
            var lstCountries = new List<SelectListItem>();

            List<Country> Countries = _context.Countries.ToList();

            lstCountries = Countries.Select(ct => new SelectListItem()
            {
                Value = ct.Id.ToString(),
                Text = ct.Name
            }).ToList();

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Country----"
            };

            lstCountries.Insert(0, defItem);

            return lstCountries;
        }

        private List<SelectListItem> GetCountries(int? selectedId = null)
        {
            var countries = _context.Countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = (selectedId != null && c.Id == selectedId)
            }).ToList();

            countries.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "----Select Country----"
            });

            return countries;
        }

        [HttpGet]
        public IActionResult CreateModalForm(int countryId)
        {
            City city = new City();
            city.CountryId = countryId;
            city.CountryName = GetCountryName(countryId);
            return PartialView("_CreateModalForm", city);
        }

        [HttpPost]
        public IActionResult CreateModalForm(City city)
        {
            _context.Add(city);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        public JsonResult GetByCountry(int countryId)
        {
            var cities = _context.Cities
                .Where(c => c.CountryId == countryId)
                .Select(c => new { id = c.Id, name = c.Name })
                .ToList();

            return Json(cities);
        }
        private string GetCountryName(int countryId)
        {
            if (countryId == 0)
                return "";

            var strCountryName = _context.Countries
                .Where(ct => ct.Id == countryId)
                .Select(nm => nm.Name)
                .FirstOrDefault();

            return strCountryName ?? "";
        }


    }
}