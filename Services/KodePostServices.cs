using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MitraSolusiTelematika.Models;
using MitraSolusiTelematika.Repositories;
using MitraSolusiTelematika.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MitraSolusiTelematika.Services
{
    public interface IKodePosService
    {
        Task<PagingResponseModel<List<KodePos>>> GetListAsync(int currentPageNumber, int pageSize);
        Task<PagingResponseModel<List<KodePos>>> SearchAsync(string strSearch, int currentPageNumber, int pageSize);
        List<SelectListItem> GetPropinsiList();
        IEnumerable<string> GetKabupatenByNamaPropinsi(string nama);
        IEnumerable<SelectListItem> GetKabupatenByNamaPropinsiAsSelectListItem(string nama);
        IEnumerable<string> GetKecamatanByNamaKabupaten(string nama);
        IEnumerable<SelectListItem> GetKecamatanByNamaKabupatenAsSelectListItem(string nama);
        IEnumerable<string> GetDesaByNamaKecamatan(string nama);
        IEnumerable<SelectListItem> GetDesaByNamaKecamatanAsSelectListItem(string nama);
        Task Save(KodePos model);
        Task Update(KodePos model);
        KodePos GetById(int id);
        Task HapusAsync(int id);
    }
    public class KodePosServices : IKodePosService
    {
        private readonly MstDbContext _Context;

        public KodePosServices(MstDbContext context)
        {
            _Context = context;
        }

        public IEnumerable<string> GetDesaByNamaKecamatan(string nama)
        {
            var query = _Context.KodePos
                        .Where(x => x.Kecamatan == nama)
                        .OrderBy(x => x.Kelurahan)
                        .Select(t => t.Kelurahan)
                        .Distinct()
                        .ToList();

            return query;
        }

        public IEnumerable<string> GetKabupatenByNamaPropinsi(string nama)
        {
            var query = _Context.KodePos
                        .Where(x=>x.Propinsi==nama)
                        .OrderBy(x=>x.Kabupaten)
                        .Select(t => t.Kabupaten)
                        .Distinct()
                        .ToList();

            return query;
        }

        public IEnumerable<SelectListItem> GetDesaByNamaKecamatanAsSelectListItem(string nama)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            var query = _Context.KodePos
                        .Where(x => x.Kecamatan == nama)
                        .OrderBy(x => x.Kelurahan)
                        .Select(t => t.Kelurahan)
                        .Distinct()
                        .ToList();

            foreach (var p in query)
            {
                result.Add(new SelectListItem { Text = p, Value = p });
            }

            return result;
        }

        public IEnumerable<SelectListItem> GetKabupatenByNamaPropinsiAsSelectListItem(string nama)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            var query = _Context.KodePos
                        .Where(x => x.Propinsi == nama)
                        .OrderBy(x => x.Kabupaten)
                        .Select(t => t.Kabupaten)
                        .Distinct()
                        .ToList();

            foreach (var p in query)
            {
                result.Add(new SelectListItem { Text = p, Value = p });
            }

            return result;
        }

        public IEnumerable<SelectListItem> GetKecamatanByNamaKabupatenAsSelectListItem(string nama)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            var query = _Context.KodePos
                           .Where(x => x.Kabupaten == nama)
                           .OrderBy(x => x.Kecamatan)
                           .Select(t => t.Kecamatan)
                           .Distinct()
                           .ToList();

            foreach (var p in query)
            {
                result.Add(new SelectListItem { Text = p, Value = p });
            }

            return result;


        }


        public IEnumerable<string> GetKecamatanByNamaKabupaten(string nama)
        {
            var query = _Context.KodePos
                           .Where(x => x.Kabupaten == nama)
                           .OrderBy(x => x.Kecamatan)
                           .Select(t => t.Kecamatan)
                           .Distinct()
                           .ToList();

            return query;

           
        }

        public async Task<PagingResponseModel<List<KodePos>>> GetListAsync(int currentPageNumber, int pageSize) 
        {

                var query =  _Context.KodePos;
                var total = query.ToList().Count;
                var data = await query
                            .Skip((currentPageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

                return new PagingResponseModel<List<KodePos>>(data, total, currentPageNumber, pageSize);


        }

        public List<SelectListItem> GetPropinsiList()
        {
            List<SelectListItem> result=new List<SelectListItem>();

            var query = _Context.KodePos.Select(t => t.Propinsi)
                       .Distinct().ToList();

            foreach(var p in query)
            {
                result.Add(new SelectListItem { Text = p, Value=p });
            }

            return result;
        }

        public async Task HapusAsync(int id)
        {
            var obj = _Context.KodePos.Where(x => x.Id == id).SingleOrDefault();
             _Context.KodePos.Remove(obj);
            await _Context.SaveChangesAsync();
        }

        public async Task Save(KodePos model)
        {
            _Context.KodePos.Add(model);
            await _Context.SaveChangesAsync();
        }

        public async Task Update(KodePos model)
        {
            _Context.KodePos.Update(model);
            await _Context.SaveChangesAsync();
        }

        public async Task<PagingResponseModel<List<KodePos>>> SearchAsync(string strSearch, int currentPageNumber, int pageSize)
        {

            var query = _Context.KodePos
                                .Where(s => s.NoKodePos.Contains(strSearch)
                                    || s.Kelurahan.Contains(strSearch)
                                    || s.Kecamatan.Contains(strSearch)
                                    || s.Kabupaten.Contains(strSearch)
                                    || s.Propinsi.Contains(strSearch)); 


            var total = query.ToList().Count;
            var data = await query
                        .Skip((currentPageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

            return new PagingResponseModel<List<KodePos>>(data, total, currentPageNumber, pageSize);


        }

        public KodePos GetById(int id)
        {
            return _Context.KodePos.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
