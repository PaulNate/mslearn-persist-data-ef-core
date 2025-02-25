using Gameverse.Services;
using Gameverse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Gameverse.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    ProductsService _service;
    
    public ProductsController(ProductsService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = _service.GetById(id);

        if(product is not null)
        {
            return product;
        }
        else
        {
            return NotFound();
        }
    }
    [HttpGet("all")]
    public IEnumerable<Product> GetProducts()
    {
        var Products = _service.GetProducts();

        return Products;
    }
    [HttpGet]
    public IEnumerable<Product> GetByPriceMax(double priceMax)
    {
        var Products = _service.GetByPriceMax(priceMax);

        return Products;
    }
    [HttpGet ("category/{categoryId}")]
    public IEnumerable<Product> GetByCategory([FromRoute]int categoryId)
    {
        var Products = _service.GetByCategory(categoryId);

        return Products;
    }
    [HttpPost]
    public IActionResult Create([FromBody] ProductDto newProduct)
    {
        var product = _service.Create(newProduct);
        return CreatedAtAction(nameof(GetById), new { id = product!.ProductId }, product);
    }
    [HttpPut("{ProductId}/shopping-cart/{ShoppingCartId}")]
    public IActionResult AddToShoppingCart( int ProductId, int ShoppingCartId)
    {
        var productToUpdate = _service.GetById(ProductId);

        if(productToUpdate is not null)
        {
            _service.AddToShoppingCart(ProductId, ShoppingCartId);
            return NoContent();    
        }
        else
        {
            return NotFound();
        }
    }
    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var product = _service.GetById(id);

        if(product is not null)
        {
            _service.DeleteById(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public ActionResult<Product> UpdateProduct(int id, [FromBody]ProductDto newProduct)
    {
        var product = _service.UpdateProduct(id, newProduct);

        return product;
    }

    [HttpGet("grade/{productId}")]
    public double GetAverageReview(int ProductId)
    {
        var averageReview = _service.GetAverageReview(ProductId);
        return averageReview;
    }

    [HttpGet("sortedByReview")]
    public IEnumerable<Product> GetProductsByAverageReview()
    {
        var sorted_products = _service.GetProductsByAverageReview();
        return sorted_products;
    }

    [HttpGet("{id}/reviews")]
    public IEnumerable<Review> GetReviewsByProductId([FromRoute] int id)
    {
        var reviews = _service.GetReviewsByProductId(id);

        return reviews;
    }

    [HttpGet("randomGame")]
    public Product GetRandomGame()
    {
        var random_game = _service.GetRandomGame();
        return random_game;
    }

    [HttpGet("randomMedia")]
    public Product GetRandomMedia()
    {
        var random_media = _service.GetRandomMedia();
        return random_media;
    }

    [HttpGet("randomItem")]
    public Product GetRandomItem()
    {
        var random_item = _service.GetRandomItem();
        return random_item;
    }
}