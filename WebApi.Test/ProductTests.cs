using Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApiTestDemo;


namespace WebApi.Test
{
    [TestFixture]
    public class ProductTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                   
                });
            });

            _client = _factory.CreateClient();
        }

        [Test]
        public async Task GetProducts_ReturnsOkResponse()
        {
            //Arrenge
            var response = await _client.GetAsync("/api/products?top=0");

            //Assert
            response.EnsureSuccessStatusCode();

            var products = await response.Content.ReadFromJsonAsync<List<Product>>();

            Assert.That(products != null);
        }

        [Test]
        public async Task GetProductsWithTop_ReturnsCorrectProduct()
        {
            //Arrenge  
            var count = 3;

            var response = await _client.GetAsync($"/api/products?top={count}");

            var products = await response.Content.ReadFromJsonAsync<List<Product>>();

            //Assert
            response.EnsureSuccessStatusCode();

            Assert.That(products != null);
            Assert.That(products?.Count <= count);
        }

        [Test]
        public async Task PostProduct_AddsNewProduct()
        {
            //Arrenge
            var newProduct = new Product
            {
                Name="New Product",
                Price=500
            };

            //Act

            var response = await _client.PostAsJsonAsync("/api/products", newProduct);

            //Assert
            response.EnsureSuccessStatusCode();

            var createdProduct = await response.Content.ReadFromJsonAsync<Product>();

            Assert.That(createdProduct != null);
            Assert.That(newProduct.Name, Is.EqualTo(createdProduct?.Name));
        }


        // GetById elave edin ProductsController
        // Then
        //Write Test for GetById , Delete , Update
    }
}
