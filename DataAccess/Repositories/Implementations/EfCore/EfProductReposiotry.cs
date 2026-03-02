using DataAccess.Repositories.Interfaces;
namespace DataAccess.Repositories.Implementations.EfCore;

public class EfProductReposiotry : EfBaseRepository<Product, AppDbContext>, IProductRepository
{
    public EfProductReposiotry(AppDbContext context) : base(context)
    {
    }
}
