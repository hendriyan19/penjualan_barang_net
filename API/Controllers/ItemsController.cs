using API.Base;
using API.Models;
using API.Repositories;
using API.Repository.Interface;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : BaseController<items, ItemsRepository, long>
    {
        private readonly ItemsRepository itemsRepository;

        public ItemsController(ItemsRepository itemsRepository) : base(itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet("GetAllItem")]
        public ActionResult<List<items>> GetAllItem()
        {
            try
            {
                var items = itemsRepository.GetAllItem();
                return Ok(items);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Get All Items Server Error");
            }
        }

        [HttpGet("GetItemById/{id}")]
        public ActionResult<List<itemVM>> GetItemById(long id)
        {
            try
            {
                var item = itemsRepository.GetItemById(id);
                return Ok(item);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Get Item by ID Server Error");
            }
        }

        [HttpPost("AddItem")]
        public ActionResult AddItem(itemVM newItem)
        {
            try
            {
                itemsRepository.AddItem(newItem);
                return Ok(); // Return 200 OK if the item is successfully added
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Insert Server Error");
            }
        }

        [HttpPut("UpdateItem")]
        public ActionResult UpdateItem(itemVM updatedItem)
        {
            try
            {
                itemsRepository.UpdateItem(updatedItem);
                return Ok("Update Item Success");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Server Error");
            }
        }

        [HttpDelete("DeleteItem/{id}")]
        public ActionResult DeleteItem(long id)
        {
            try
            {
                itemsRepository.DeleteItem(id);
                return Ok("Delete Item Success");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Server Error");
            }
        }

    }
}

