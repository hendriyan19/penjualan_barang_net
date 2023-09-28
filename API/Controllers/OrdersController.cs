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
    public class OrdersController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;
        private readonly OrdersRepository ordersRepository;

        public OrdersController(IDbConnection dbConnection, OrdersRepository ordersRepository)
        {
            _dbConnection = dbConnection;
            this.ordersRepository = ordersRepository;

        }

        [HttpGet("GetOrderByOrderId/{id}")]
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

        [HttpGet("GetAllCustomer")]
        public ActionResult<List<orderVM>> GetAllCustomer()
        {
            try
            {
                var allCustomer = ordersRepository.GetAllCustomer();

                if (allCustomer != null && allCustomer.Any())
                {
                    return Ok(allCustomer); // Menggunakan kode status 200 OK untuk respons berhasil
                }
                else
                {
                    return NoContent(); // Menggunakan kode status 204 No Content jika data tidak ada
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Terjadi kesalahan server saat mengambil data pelanggan"); // Menggunakan kode status 500 untuk kesalahan server
            }
        }




        [HttpGet("GetAllDate")]
        public ActionResult<List<orderVM>> GetAllDate()
        {
            try
            {
                var allDate = ordersRepository.GetAllDate();

                if (allDate != null && allDate.Any())
                {
                    return Ok(allDate); // Menggunakan kode status 200 OK untuk respons berhasil
                }
                else
                {
                    return NoContent(); // Menggunakan kode status 204 No Content jika data tidak ada
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Terjadi kesalahan server saat mengambil data pelanggan"); // Menggunakan kode status 500 untuk kesalahan server
            }
        }


        [HttpGet("GetAllItem")]
        public ActionResult<List<orderVM>> GetAllItem()
        {
            try
            {
                var allItem = ordersRepository.GetAllItem();

                if (allItem != null && allItem.Any())
                {
                    return Ok(allItem); 
                }
                else
                {
                    return NoContent(); 
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Terjadi kesalahan server saat mengambil data pelanggan"); // Menggunakan kode status 500 untuk kesalahan server
            }
        }



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

                // Menjumlahkan pesanan untuk dikembalikan ke current page
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
        public ActionResult UpdateOrder(orderVM orderVM)
        {
            try
            {
                ordersRepository.UpdateOrder(orderVM);

                return Ok("Update Order Success");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Server Error");
            }
        }

        [HttpPost]
        public ActionResult AddOrder(orderVM newOrder)
        {
            try
            {
                ordersRepository.AddOrder(newOrder);

                return Ok(); // Return 200 OK if the order is successfully added
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Insert Server Error");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(long id)
        {
            try
            {
                ordersRepository.DeleteOrder(id);

                return Ok(); // Return 200 OK if the order is successfully deleted
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Server Error");
            }
        }

        private CustomResponse<orderVM> PaginateResponseOrderById(CustomResponse<orderVM> response, int page, int pageSize)
        {
            // Proses paginasi seperti yang Anda lakukan pada GetAllOrder
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
