using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thl.Controllers;
using Thl.EFCore.Models;
using Thl.Models;
using Thl.Repository.Contract.IRepository;

namespace Thl.Test.ControllerTest
{
    [TestClass]
    public class ProductControllerTest
    {
        private int aProductId;
        private string productName;

        private Product aProduct;
        private Product bProduct;

        private IEnumerable<Product> productsByName;

        private Product productToCreate;
        private Product productToUpdate;

        private ProductDto productToCreateDto;
        private ProductDto productToUpdateDto;

        private int productToCreateId;

        private Mock<ILoggerFactory> _logger;
        private Mock<IMapper> _mapper;
        private Mock<IProductRepository> _productRespository;
        private ProductController _productController;

        [TestInitialize]
        public void SetUp()
        {
            _logger = new Mock<ILoggerFactory>();
            _mapper = new Mock<IMapper>();
            _productRespository = new Mock<IProductRepository>();
            _productController = new ProductController(_logger.Object, _mapper.Object, _productRespository.Object);

            SetUpData();

            _productRespository
                .Setup(p => p.GetByIdAsync(aProductId))
                .Returns(Task.FromResult(aProduct));

            _productRespository
                .Setup(p => p.GetProductsByNameAsync(1, productName))
                .Returns(Task.FromResult(productsByName.AsEnumerable()));

            _productRespository
                .Setup(p => p.AddProductAsync(productToCreate))
                .Returns(Task.FromResult(productToCreate));

            _productRespository
                .Setup(p => p.AddProductAsync(productToUpdate))
                .Returns(Task.FromResult(productToUpdate));
        }

        private void SetUpData()
        {
            aProductId = 1;
            productName = "Product";
            productToCreateId = 3;

            aProduct = new Product
            {
                Id = aProductId,
                Name = productName,
                Price = (decimal)1.1
            };

            bProduct = new Product
            {
                Id = 2,
                Name = productName,
                Price = (decimal)2.2
            };

            productsByName = new List<Product> { aProduct, bProduct };

            productToCreate = new Product
            {
                Id = productToCreateId,
                Name = "New_" + productName,
                Price = (decimal)3.3
            };

            productToUpdate = new Product
            {
                Id = aProductId,
                Name = "Update_" + productName,
                Price = (decimal)4.4
            };

            productToCreateDto = _mapper.Object.Map<ProductDto>(productToCreate);
            productToUpdateDto = _mapper.Object.Map<ProductDto>(productToUpdate);
        }

        [TestMethod]
        public async Task When_GetProductById_Should_Return_BadRequest_InvalidId()
        {
            //Arrange
            //Act
            var response = await _productController.GetProductById(6) as NotFoundResult;

            //Assert
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task When_GetProductById_Should_Return_Product_ValidId()
        {
            //Arrange
            //Act
            var response = await _productController.GetProductById(aProductId) as OkObjectResult;

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public async Task When_GetProductsByName_Should_Return_BadRequest_InvalidName()
        {
            //Arrange
            //Act
            var response = await _productController.GetProductsByName(1, null) as BadRequestObjectResult;

            //Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task When_GetProductsByName_Should_Return_Products_ValidName()
        {
            //Arrange
            //Act
            var response = await _productController.GetProductsByName(1, productName) as OkObjectResult;

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public async Task When_CreateProduct_Should_Return_BadRequest_InvalidProduct()
        {
            //Arrange
            //Act
            var response = await _productController.CreateProduct(null) as BadRequestObjectResult;

            //Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task When_CreateProduct_Should_Return_Ok_ValidProduct()
        {
            //Arrange
            //Act
            var response = await _productController.CreateProduct(productToCreateDto) as OkObjectResult;

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public async Task When_UpdateProduct_Should_Return_BadRequest_InvalidProduct()
        {
            //Arrange
            //Act
            var response = await _productController.UpdateProduct(productToCreateId, null) as BadRequestObjectResult;

            //Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task When_UpdateProduct_Should_Return_Ok_ValidProduct()
        {
            //Arrange
            //Act
            var response = await _productController.UpdateProduct(aProductId, productToUpdateDto) as OkObjectResult;

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }
    }
}
