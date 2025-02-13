﻿using MedHealth.DataAccess.Repository.IRepository;
using MedHealthyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MedHealthyWeb.Controllers
{
    [Area("Patient")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string text)
        {
            return RedirectToAction("BookAppointment", "Patient", new { area = "Patient", speciality = text });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}