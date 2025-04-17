using CarFactory.Models.Model;

namespace CarFactory.Models.Brand;

public interface IBrand : IHaveName
{
    IReadOnlyDictionary<int, IModel> GetBrandModels();
}
