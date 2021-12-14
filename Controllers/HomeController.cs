using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MitraSolusiTelematika.Models;
using MitraSolusiTelematika.Repositories;
using MitraSolusiTelematika.Services;
using MitraSolusiTelematika.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MitraSolusiTelematika.Controllers
{
    public class HomeController : Controller
    {
      
        private readonly IKodePosService _IKodePosService;
        public HomeController(IKodePosService kodePosService)
        {
           _IKodePosService = kodePosService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.Mode = "Normal";
            return View(await _IKodePosService.GetListAsync(1,10));
        }

        [Route("/{currentPageNumber}/{pageSize}")]
        [Route("IndexAsync")]
        public async Task<IActionResult> IndexAsync(int? currentPageNumber,int? pageSize)
        {
            if (!currentPageNumber.HasValue)
                currentPageNumber = 1;

            if (!pageSize.HasValue)
                currentPageNumber = 10;

            ViewBag.Mode = "Normal";
            return View(await _IKodePosService.GetListAsync(currentPageNumber.Value,pageSize.Value));
        }

        //[HttpGet]
        [Route("/home/SearchAsync")]
        public async Task<IActionResult> SearchAsync(string strSearh, int currentPageNumber, int pageSize)
        {
            ViewBag.Mode = "SearchMode";
            ViewBag.strSearch = strSearh;

            if (currentPageNumber == 0)
                currentPageNumber = 1;

            if (pageSize == 0)
                pageSize = 10;

            return View("Index",await _IKodePosService.SearchAsync(strSearh, currentPageNumber, pageSize));
        }
        [Route("/Home/AddNew")]
        public IActionResult AddNew()
        {
            ViewBag.PropinsiList = _IKodePosService.GetPropinsiList();
            return View();
        }

        [Route("/Home/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            return View();
        }

        //[HttpPost]
        [Route("/Home/GetKabupatenByNamaPropinsi")]
        public JsonResult GetKabupatenByNamaPropinsi(string nama)
        {
            return Json(_IKodePosService.GetKabupatenByNamaPropinsi(nama));
        }

        public JsonResult GetKecamatanByNamaKabupaten(string nama)
        {
            return Json(_IKodePosService.GetKecamatanByNamaKabupaten(nama));
        }

        public JsonResult GetDesaByNamaKecamatan(string nama)
        {
            return Json(_IKodePosService.GetDesaByNamaKecamatan(nama));
        }


    }
}
