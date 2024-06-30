using E_commerceSystem.Api.Data;
using E_commerceSystem.Api.Models;
using E_commerceSystem.Api.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace E_commerceSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public ProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; 
        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
          var allProducts =   dbContext.Products.ToList();

            return Ok(allProducts);
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetProductById(Guid id)
        {
            var product = dbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        [HttpPost]
        public IActionResult AddProduct(AddProductDto addProductDto)
        {
            var productEntity = new Products()
            {
                Name = addProductDto.Name,
                Price = addProductDto.Price,
            };
            dbContext.Products.Add(productEntity);
            dbContext.SaveChanges();

            return Ok(productEntity);
        }
        [HttpPut]
        public IActionResult UpdateProduct(Guid id, UpdateProductDto updateProductDto)
        {
            var product = dbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            product.Name = updateProductDto.Name;
            product.Price = updateProductDto.Price;
            dbContext.SaveChanges();
            return Ok(product);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(Guid id) 
        {
            var product = dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
            return Ok(product);
        }

        // Order management endpoints
        [HttpGet("orders")]
        public IActionResult GetAllOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = dbContext.Orders.Where(o => o.UserId == Guid.Parse(userId)).ToList();
            return Ok(orders);
        }

        [HttpGet("orders/{id:guid}")]
        public IActionResult GetOrderById(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = dbContext.Orders.FirstOrDefault(o => o.Id == id && o.UserId == Guid.Parse(userId));
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }


        [HttpPost("orders")]
        public IActionResult CreateOrder(CreateOrderDto createOrderDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(userId),
                OrderDate = DateTime.UtcNow,
                TotalAmount = createOrderDto.TotalAmount,
                OrderItems = createOrderDto.OrderItems.Select(i => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };

            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
            return Ok(order);
        }

        [HttpPut("orders/{id:guid}")]
        public IActionResult UpdateOrder(Guid id, UpdateOrderDto updateOrderDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = dbContext.Orders.FirstOrDefault(o => o.Id == id && o.UserId == Guid.Parse(userId));
            if (order == null)
            {
                return NotFound();
            }
            // Update order details
            order.TotalAmount = updateOrderDto.TotalAmount;
            // Update order items
            dbContext.OrderItems.RemoveRange(order.OrderItems);
            order.OrderItems = updateOrderDto.OrderItems.Select(i => new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList();
            dbContext.SaveChanges();
            return Ok(order);
        }

        [HttpDelete("orders/{id:guid}")]
        public IActionResult DeleteOrder(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = dbContext.Orders.FirstOrDefault(o => o.Id == id && o.UserId == Guid.Parse(userId));
            if (order == null)
            {
                return NotFound();
            }
            dbContext.Orders.Remove(order);
            dbContext.SaveChanges();
            return Ok(order);
        }
    }
}
