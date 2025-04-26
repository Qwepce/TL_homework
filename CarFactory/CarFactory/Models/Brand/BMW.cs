using System.Collections.Frozen;
using CarFactory.Models.Model;

namespace CarFactory.Models.Brand;

public class BMW : IBrand
{
    public string Name => "BMW";

    private readonly IReadOnlyDictionary<int, IModel> _models =
        new Dictionary<int, IModel>
        {
            { 1, new CarModel("M5") },
            { 2, new CarModel("X3") },
        }.ToFrozenDictionary();

    public IReadOnlyDictionary<int, IModel> GetBrandModels()
    {
        return _models;
    }
}
