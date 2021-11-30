using API.Repository.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]

    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            if(result.Count() > 0)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(new ResultVM { Status = HttpStatusCode.NotFound.ToString(), Pesan = "Data tidak ada !!!" });
            }
        }

        [HttpGet("{Key}")]
        public ActionResult<Entity> Get(Key key)
        {
            var result = repository.Get(key);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(new ResultVM { Status = HttpStatusCode.NotFound.ToString(), Pesan = "Data Tidak ada !!!" });
            }
        }

        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            try
            {
                var cek = repository.Insert(entity);
                return Ok(new ResultVM { Status = HttpStatusCode.OK.ToString(), Pesan = $"Berhasil Tambah data" });
            }
            catch(Exception e)
            {
                return NotFound(new ResultVM { Status = HttpStatusCode.NotFound.ToString(), Pesan = e.ToString() });
            }
        }

        [HttpPut("{Key}")]
        public ActionResult Update(Entity entity, Key key)
        {
            try
            {
                var result = repository.Update(entity, key);
                return Ok(new ResultVM { Status = HttpStatusCode.OK.ToString(), Pesan = $"Berhasil Update Key : {key} "});
            }
            catch (Exception)
            {
                return NotFound(new ResultVM { Status = HttpStatusCode.NotFound.ToString(), Pesan = "Gagal Data Tidak Ditemukan" });
            }
        }

        [HttpDelete("{Key}")]
        public ActionResult Delete(Key key)
        {
            try
            {
                repository.Delete(key);
                return Ok(new ResultVM { Status = HttpStatusCode.OK.ToString(), Pesan = $"Berhasil Menghapus Key : {key} " });
            }
            catch (ArgumentNullException)
            {
                return NotFound(new ResultVM { Status = HttpStatusCode.NotFound.ToString(), Pesan = $"Gagal Menghapus Key : {key} data tidak ditemukan" });
            }
        }
    }
}
