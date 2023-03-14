﻿using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Drive.v3;
using Google.Apis.Forms.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using register_app.Models;
using register_app.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace register_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFormService FormService { get; }
        public HomeController(IFormService formService, ILogger<HomeController> logger)
        {
            FormService = formService;
            _logger = logger;
        }

        [GoogleScopedAuthorize(DriveService.ScopeConstants.DriveReadonly)]
        public async Task<IActionResult> Index()
        {
            var files = await FormService.GetAllFormFiles();
            var names = files.Select(x => x.Name);
            ViewData["Filenames"] = names;
            return View();
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
