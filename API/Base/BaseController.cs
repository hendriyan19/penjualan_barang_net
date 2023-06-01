
using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public ActionResult<Entity> get()
        {
            try
            {
                var getData = repository.Get();
                return Ok(repository.Get());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Get all data Server Error");
            }
        }

        //show Data by ID
        [HttpGet("{Key}")]
        public ActionResult Get(Key key)
        {

            try
            {
                var getDataById = repository.Get(key);
                if (getDataById is null)
                {
                    return NotFound("DATA yang dicari Tidak ditemukan");

                }
                else
                {
                    return Ok(repository.Get(key));

                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "FIND DATA BY key Server Error");
            }

        }

        //insert data
        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            try
            {
                repository.Insert(entity);
                return Ok("Data berhasil ditambahkan");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "INSERT Server Error");
            }

        }

        ////update data
        [HttpPut]
        public ActionResult Update(Entity entity)
        {
            try
            {
                var updateData = repository.Update(entity);
                if (updateData != 0)
                {
                    return Ok("Data berhasil diperbaharui");
                }
                else
                {
                    return NotFound("Data tidak ditemukan");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "UPDATE Server Error");
            }
        }

        //delete by id
        [HttpDelete("{Key}")]
        public ActionResult Delete(Key key)
        {
            try
            {
                var deleteData = repository.Delete(key);
                if (deleteData != 0)
                {
                    return Ok("Data berhasil dihapus");
                }
                else
                {
                    return NotFound("Data tidak ditemukan");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "DELETE Server Error");
            }

        }


    }
}
