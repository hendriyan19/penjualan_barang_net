using API.Repositories;
using API.ViewModel;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;
        private readonly ItemsRepository itemsRepository;

        public ItemsController(IDbConnection dbConnection, ItemsRepository itemsRepository)
        {
            _dbConnection = dbConnection;
            this.itemsRepository = itemsRepository;
        }

        [HttpGet("GetAllItem")]
        public async Task<ActionResult<CustomResponse<itemVM>>> GetAllItem(int page = 1, int pageSize = 10)
        {
            try
            {
                var allItems = itemsRepository.GetAllItem();

                // Menghitung total item dan halaman
                int totalItems = allItems.Count();
                int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                // Validasi nomor halaman
                if (page < 1)
                    page = 1;
                if (page > totalPages)
                    page = totalPages;

                // Mengambil item untuk halaman saat ini
                var itemsToReturn = allItems
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Membuat custom response object dengan informasi pagination
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

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Get All Items Server Error");
            }
        }

        [HttpGet("GetItemById/{id}")]
        public async Task<ActionResult<itemVM>> GetItemById(long id)
        {
            try
            {
               
                var item = itemsRepository.GetItemById(id);

                if (item != null)
                {
                    return Ok(item);
                }
                else
                {
                    return NotFound("Item not found");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Get Item by ID Server Error");
            }
        }

        [HttpPost("AddItem")]
        public async Task<ActionResult> AddItem(itemVM newItem)
        {
            try
            {
                itemsRepository.AddItem(newItem);
                return Ok("Item added successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Insert Server Error");
            }
        }

        [HttpPut("UpdateItem")]
        public async Task<ActionResult> UpdateItem(itemVM updatedItem)
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
        public async Task<ActionResult> DeleteItem(long id)
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
