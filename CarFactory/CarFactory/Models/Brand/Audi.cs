using System.Collections.Frozen;
using CarFactory.Models.Model;

namespace CarFactory.Models.Brand;

public class Audi : IBrand
{
    public string Name => "AUDI";

    private readonly IReadOnlyDictionary<int, IModel> _models =
        new Dictionary<int, IModel>
        {
            { 1, new CarModel("RS6") },
            { 2, new CarModel("A6") },
        }.ToFrozenDictionary();

    public IReadOnlyDictionary<int, IModel> GetBrandModels()
    {
        return _models;
    }
}
