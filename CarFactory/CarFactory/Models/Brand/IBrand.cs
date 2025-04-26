using CarFactory.Models.Model;

namespace CarFactory.Models.Brand;

public interface IBrand : IHasName
{
    IReadOnlyDictionary<int, IModel> GetBrandModels();
}
