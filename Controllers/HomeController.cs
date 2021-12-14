using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MitraSolusiTelematika.Helpers;
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
            return View(await _IKodePosService.GetListAsync(1, 10));
        }

        [Route("/{currentPageNumber}/{pageSize}")]
        [Route("IndexAsync")]
        public async Task<IActionResult> IndexAsync(int? currentPageNumber, int? pageSize)
        {
            if (!currentPageNumber.HasValue)
                currentPageNumber = 1;

            if (!pageSize.HasValue)
                currentPageNumber = 10;

            ViewBag.Mode = "Normal";
            return View(await _IKodePosService.GetListAsync(currentPageNumber.Value, pageSize.Value));
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

            return View("Index", await _IKodePosService.SearchAsync(strSearh, currentPageNumber, pageSize));
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
            var model = _IKodePosService.GetById(id);

            ViewBag.PropinsiList = _IKodePosService.GetPropinsiList();
            ViewBag.KabupatenList = _IKodePosService.GetKabupatenByNamaPropinsiAsSelectListItem(model.Propinsi);
            ViewBag.KecamatanList = _IKodePosService.GetKecamatanByNamaKabupatenAsSelectListItem(model.Kabupaten);
            ViewBag.KeluarahanList = _IKodePosService.GetDesaByNamaKecamatanAsSelectListItem(model.Kecamatan);

            return View(model);
        }

        //[HttpPost]
        [Route("/Home/GetKabupatenByNamaPropinsi")]
        public JsonResult GetKabupatenByNamaPropinsi(string nama)
        {
            return Json(_IKodePosService.GetKabupatenByNamaPropinsi(nama));
        }
        [Route("/Home/GetKecamatanByNamaKabupaten")]
        public JsonResult GetKecamatanByNamaKabupaten(string nama)
        {
            return Json(_IKodePosService.GetKecamatanByNamaKabupaten(nama));
        }
        [Route("/Home/GetDesaByNamaKecamatan")]
        public JsonResult GetDesaByNamaKecamatan(string nama)
        {
            return Json(_IKodePosService.GetDesaByNamaKecamatan(nama));
        }

        [HttpPost("Save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(KodePos model)
        {
            var ajaxRs = new AjaxFormResponseJson { success = false, title = "Error!", message = "Error" };

            if (model.NoKodePos.Length > 5)
            {
                ajaxRs.success = false;
                ajaxRs.title = "Cek Kodepos";
                ajaxRs.message = "Kodepos maksimal adalah 5";
                return Json(ajaxRs);
            }

            try
            {
                if (ModelState.IsValid)
                {

                    ajaxRs.success = true;
                    ajaxRs.title = "OK";
                    ajaxRs.message = "Data berhasil ditambahkan";
                    ajaxRs.url = Url.Action("Index", "Home");
                    ajaxRs.enforce = true;

                    await _IKodePosService.Save(model);

                    return Json(ajaxRs);
                }
            }
            catch(Exception ex)
            {
                ajaxRs.success = false;
                ajaxRs.title = "Error";
                ajaxRs.message = ex.Message;
            }

            return Json(ajaxRs);


        }

        [HttpPost("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(KodePos model)
        {
            var ajaxRs = new AjaxFormResponseJson { success = false, title = "Error!", message = "Error" };

            if(model.NoKodePos.Length >5)
            {
                ajaxRs.success = false;
                ajaxRs.title = "Cek Kodepos";
                ajaxRs.message = "Kodepos maksimal adalah 5";
                return Json(ajaxRs);
            }

            try
            {
                if (ModelState.IsValid)
                {

                    ajaxRs.success = true;
                    ajaxRs.title = "OK";
                    ajaxRs.message = "Data berhasil diupdate";
                    ajaxRs.url = Url.Action("Index", "Home");
                    ajaxRs.enforce = true;

                    await _IKodePosService.Update(model);

                    return Json(ajaxRs);
                }
            }catch(Exception ex)
            {
                ajaxRs.success = false;
                ajaxRs.title = "Error";
                ajaxRs.message = ex.Message;
            }

            return Json(ajaxRs);


        }

        [Route("/Home/HapusData/{id}")]
        public async Task<IActionResult> HapusData(int id)
        {
            await _IKodePosService.HapusAsync(id);
            return Json(id);
        }
    }
}
