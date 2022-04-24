using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MongoDB.Bson;
using taskBoardApi.Models;
using taskBoardApi.Services;

namespace taskBoardApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly IProductService _productService;

    public ProductsController(IProductService products)
    {
        _productService = products;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> Get(int n = 20)
    {
        return await _productService.GetNAsync(n);
    }

    [HttpGet("{sku}", Name = "GetProduct")]
    public async Task<ActionResult<Product>> GetBySku(string sku)
    {
        var product =  await _productService.GetBySkuAsync(sku);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create(Product product)
    {
        var newProduct = product with { Id = ObjectId.GenerateNewId().ToString() };
        await _productService.CreateAsync(newProduct);

        return CreatedAtRoute("GetProduct", new { sku = product.Sku }, newProduct);
    }

    [HttpPut]
    public async Task<ActionResult<Product>> Update(Product update)
    {
        var product = await _productService.UpdateAsync(update);

        return CreatedAtRoute("GetProduct", new { sku = product.Sku }, product);
    }

    [HttpDelete("{sku}")]
    public async Task<IActionResult> Delete(string sku)
    {
        await _productService.DeleteAsync(sku);

        return Ok();
    }
}

