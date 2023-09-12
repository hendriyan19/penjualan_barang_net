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
    public class OrdersController : BaseController<orders_item, OrdersRepository, long>
    {
        private readonly OrdersRepository ordersRepository;

        public OrdersController(OrdersRepository ordersRepository) : base(ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        //[HttpGet("GetOrderById/{id}")]
        //public ActionResult GetOrderById(long id)
        //{
        //    try
        //    {
        //        var dataMaster = ordersRepository.GetOrderById(id);
        //        return Ok(dataMaster);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //          "Get Order by ID Server Error");
        //    }
        //}
        [HttpGet("GetOrderByOrderId/{id}")] //API/orders/GetOrderByOrderId/31?page=1&pagesize=10
        public ActionResult<CustomResponse<orderVM>> GetOrderById(long id, int page = 1, int pageSize = 10)
        {
            try
            {
                var orders = ordersRepository.GetOrderById(id);

                if (orders.Count == 0)
                {
                    return NotFound(); // Jika pesanan tidak ditemukan, kembalikan 404 Not Found.
                }

                // Membuat custom response object dengan informasi paginasi
                var response = new CustomResponse<orderVM>
                {
                    Meta = new Meta
                    {
                        Code = 200,
                        Message = "Success"
                    },
                    Data = orders
                };

                return PaginateResponseOrderById(response, page, pageSize);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Get Order by ID Server Error");
            }
        }

        //[HttpGet("GetAllOrder")]
        //public ActionResult GetAllOrder()
        //{
        //    try
        //    {
        //        var dataMy = ordersRepository.GetAllOrder();
        //        return Ok(dataMy);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Get All Order Server Error");
        //    }
        //}

        [HttpGet("GetAllOrder")]
public ActionResult<CustomResponse<orderVM>> GetAllOrder(int page = 1, int pageSize = 10)
{
    try
    {
        var allOrders = ordersRepository.GetAllOrder();

        // menjumlahkan total nomor dari orders dan nomor dari pages
        int totalOrders = allOrders.Count;
        int totalPages = (int)Math.Ceiling((double)totalOrders / pageSize);

        // validasi nomor page
        if (page < 1)
            page = 1;
        if (page > totalPages)
            page = totalPages;

        // Menjumlahkan orders untuk dikembalikan ke current page
        var ordersToReturn = allOrders
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // membuat custom response object dengan informasi pagination 
        var response = new CustomResponse<orderVM>
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
                    Count = ordersToReturn.Count
                }
            },
            Data = ordersToReturn
        };

        if (ordersToReturn.Count >= 15)
        {
            response.Meta.Pagination = null;
        }
        return response;
    }
    catch (Exception)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, "Get All Order Server Error");
    }
}


        [HttpPut("UpdateOrder")]
        public ActionResult UpdatePMCSS(orderVM orderVM)
        {
            try
            {
                ordersRepository.UpdateOrder(orderVM);
                return Ok("Update Order Success");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Insert Server Error");
            }
        }

        [HttpPost]
        public IActionResult AddOrder(orderVM newOrder)
        {
            try
            {
                ordersRepository.AddOrder(newOrder);
                return Ok(); // Return 200 OK if the order is successfully added
            }
            catch (Exception)
            {
                return StatusCode(500); // Return 500 Internal Server Error if an error occurs
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(long id)
        {
            try
            {
                    ordersRepository.DeleteOrder(id);
                    return Ok(); // Return 200 OK if the order is successfully deleted
            }
            catch (Exception)
            {
                return StatusCode(500); // Return 500 Internal Server Error if an error occurs
            }
        }


        private CustomResponse<orderVM> PaginateResponseOrderById(CustomResponse<orderVM> response, int page, int pageSize)
        {
            // Proses paginasi seperti yang Anda lakukan pada GetAllItem dan GetAllOrder
            var dataToPaginate = response.Data;
            int totalItems = dataToPaginate.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Validasi nomor page
            if (page < 1)
                page = 1;
            if (page > totalPages)
                page = totalPages;

            // Menjumlahkan data untuk dikembalikan ke current page
            var dataPage = dataToPaginate
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Mengisi informasi paginasi
            response.Meta.Pagination = new Pagination
            {
                Current = page.ToString(),
                Next = (page < totalPages) ? (page + 1).ToString() : null,
                Prev = (page > 1) ? (page - 1).ToString() : null,
                Count = dataPage.Count
            };

            // Mengganti data dengan data yang telah dipaginasi
            response.Data = dataPage;

            if (dataPage.Count >= 15)
            {
                response.Meta.Pagination = null;
            }

            return response;
        }

    }

}

