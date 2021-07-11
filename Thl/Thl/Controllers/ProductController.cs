using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thl.Contants;
using Thl.EFCore.Models;
using Thl.Extensions;
using Thl.Models;
using Thl.Repository.Contract.IRepository;

namespace Thl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductController(ILoggerFactory loggerFactory, IMapper mapper, IProductRepository productRepository)
        {
            _logger = loggerFactory.CreateLogger<ProductController>();
            _mapper = mapper;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Get specific product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(ValidationMessageConstant.NOT_FOUND_ID.ToDescription());

                var product = await _productRepository.GetProductByIdAsync(id);

                if (product == null)
                    return NotFound();

                var response = _mapper.Map<ProductDto>(product);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Find all products matching the specified name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> GetProductsByName([FromQuery] int page, [FromQuery] string name)
        {
            try
            {
                if (name == null || page <= 0)
                    return BadRequest(ValidationMessageConstant.INVALID_PARAM.ToDescription());

                var products = await _productRepository.GetProductsByNameAsync(page, name);

                if (products?.Any() == false)
                    return NotFound();

                var response = _mapper.Map<IEnumerable<ProductDto>>(products);

                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid || productDto == null)
                    return BadRequest(ValidationMessageConstant.INVALID_REQUEST.ToDescription());

                var product = _mapper.Map<Product>(productDto);

                await _productRepository.AddProductAsync(product);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            try
            {
                if (!ModelState.IsValid || productDto == null || id != productDto.Id)
                    return BadRequest(ValidationMessageConstant.INVALID_REQUEST.ToDescription());

                var product = _productRepository.GetProductByIdAsync(id);

                if (product == null)
                    return BadRequest(ValidationMessageConstant.NOT_FOUND_DATA.ToDescription());

                var productToUpdate = _mapper.Map<Product>(productDto);

                await _productRepository.UpdateProductAsync(productToUpdate);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}