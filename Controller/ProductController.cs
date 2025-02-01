using Microsoft.AspNetCore.Mvc;
using workingProject.EfDbContext.Repos;
using workingProject.Model;
using workingProject.WPAttribute;

namespace workingProject.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController: ControllerBase
    {
        private readonly IRepository<Product> productRepo;

        public ProductController(IRepository<Product> productRepo)
        {
            this.productRepo=productRepo;
        }
        [HttpGet]
        [DisableUnitOfWorkAttribute]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await productRepo.GetAllAsync();
            return products;
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            await productRepo.AddAsync(product);
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }
    }
}
