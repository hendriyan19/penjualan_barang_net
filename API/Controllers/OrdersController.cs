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

        [HttpGet("GetOrderById/{id}")]
        public ActionResult GetOrderById(long id)
        {
            try
            {
                var dataMaster = ordersRepository.GetOrderById(id);
                return Ok(dataMaster);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  "Get Order by ID Server Error");
            }
        }





        [HttpGet("GetAllOrder")]
        public ActionResult GetAllOrder()
        {
            try
            {
                var dataMy = ordersRepository.GetAllOrder();
                return Ok(dataMy);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Get All Order Server Error");
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

    }
}

