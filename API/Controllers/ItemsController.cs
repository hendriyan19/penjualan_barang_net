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
    public class ItemsController : BaseController<Item, ItemsRepository, long>
    {
        private readonly ItemsRepository itemsRepository;

        public ItemsController(ItemsRepository itemsRepository) : base(itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet("GetAllItem")]
        public ActionResult<CustomResponse<itemVM>> GetAllItem(int page = 1, int pageSize = 10)
        {
            try
            {
                var allItems = itemsRepository.GetAllItem();

                // menjumlahkantotal nomor dari items dan nomor dari pages
                int totalItems = allItems.Count;
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                // validasi nomor page
                if (page < 1)
                    page = 1;
                if (page > totalPages)
                    page = totalPages;

                // Menjumlahkan items untuk dikembalikan ke current page
                var itemsToReturn = allItems
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // membuat custom response object dengan informasi pagination 
                var response = new CustomResponse<itemVM>
                {
                    Meta = new Meta
                    {
                        Code = 200,
                        Message = "Success",
                        Pagination = new Pagination
                        {
                            Current = page.ToString(),
                            Next = (page < totalPages) ? (page + 1).ToString() : null,
                            Prev = (page > 1) ? (page - 1).ToString() : null,
                            Count = itemsToReturn.Count
                        }
                    },
                    Data = itemsToReturn
                };

                if (itemsToReturn.Count >= 15)
                {
                    response.Meta.Pagination = null;
                }
                return response;
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
    public class CustomResponse<T>
    {
        public Meta Meta { get; set; }
        public List<T> Data { get; set; }
    }

    public class Meta
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public string Current { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
        public int Count { get; set; }
    }

}

