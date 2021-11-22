using API.Repository.Interface;
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
                return NotFound(new { status = HttpStatusCode.NotFound, Messege = "Data tidak ada !!!" });
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
                return NotFound(new { Status = HttpStatusCode.NotFound, messege = "Data Tidak ada !!!" });
            }
        }

        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            try
            {
                var cek = repository.Insert(entity);
                return Ok(new { status = HttpStatusCode.OK, messege = $"Berhasil Tambah data" });
            }
            catch(Exception)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, messege = $"Gagal Tambah data" });
            }
        }

        [HttpPut("{Key}")]
        public ActionResult Update(Entity entity, Key key)
        {
            try
            {
                var result = repository.Update(entity, key);
                return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil Update Key : {key} "});
            }
            catch (Exception)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, messege = "Gagal Data Tidak Ditemukan" });
            }
        }

        [HttpDelete("{Key}")]
        public ActionResult Delete(Key key)
        {
            try
            {
                repository.Delete(key);
                return Ok(new { status = HttpStatusCode.OK, message = $"Berhasil Menghapus Key : {key} " });
            }
            catch (ArgumentNullException)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = $"Gagal Menghapus Key : {key} data tidak ditemukan" });
            }
        }
    }
}
