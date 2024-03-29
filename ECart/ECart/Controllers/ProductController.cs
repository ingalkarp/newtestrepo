﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ECart.Interfaces;
using ECart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ECart.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        readonly IWebHostEnvironment _hostingEnvironment;
        readonly IProductService _productService;
        readonly IConfiguration _config;
        readonly string coverImageFolderPath = string.Empty;

        public ProductController(IConfiguration config, IWebHostEnvironment hostingEnvironment, IProductService productService)
        {
            _config = config;
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
            coverImageFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "Upload");
            if (!Directory.Exists(coverImageFolderPath))
            {
                Directory.CreateDirectory(coverImageFolderPath);
            }
        }

        [HttpGet]
        public async Task<List<Product>> Get()
        {
            return await Task.FromResult(_productService.GetAllProducts()).ConfigureAwait(true);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = _productService.GetProductData(id);
            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

    
        [HttpGet]
        [Route("GetCategoriesList")]
        public async Task<IEnumerable<Categories>> CategoryDetails()
        {
            return await Task.FromResult(_productService.GetCategories()).ConfigureAwait(true);
        }

      
        [HttpGet]
        [Route("GetSimilarProducts/{itemId}")]
        public async Task<List<Product>> SimilarProducts(int itemId)
        {
            return await Task.FromResult(_productService.GetSimilarProducts(itemId)).ConfigureAwait(true);
        }

        [HttpPost, DisableRequestSizeLimit]
        [Authorize(Policy = UserRoles.Admin)]
        public int Post()
        {
            Product product = JsonConvert.DeserializeObject<Product>(Request.Form["productFormData"].ToString());

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(coverImageFolderPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    product.CoverFileName = fileName;
                }
            }
            else
            {
                product.CoverFileName = _config["DefaultCoverImageFile"];
            }
            return _productService.AddProduct(product);
        }

     
        [HttpPut]
        [Authorize(Policy = UserRoles.Admin)]
        public int Put()
        {
            Product product = JsonConvert.DeserializeObject<Product>(Request.Form["productFormData"].ToString());
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    string fileName = Guid.NewGuid() + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(coverImageFolderPath, fileName);
                    bool isFileExists = Directory.Exists(fullPath);

                    if (!isFileExists)
                    {
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        product.CoverFileName = fileName;
                    }
                }
            }
            return _productService.UpdateProduct(product);
        }

      
        [HttpDelete("{id}")]
        [Authorize(Policy = UserRoles.Admin)]
        public int Delete(int id)
        {
            string coverFileName = _productService.DeleteProduct(id);
            if (coverFileName != _config["DefaultCoverImageFile"])
            {
                string fullPath = Path.Combine(coverImageFolderPath, coverFileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            return 1;
        }
    }
}
