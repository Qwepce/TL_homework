using System.Collections.Frozen;
using CarFactory.Models.Model;

namespace CarFactory.Models.Brand;

public class Mercedes : IBrand
{
    public string Name => "Mercedes";

    private readonly IReadOnlyDictionary<int, IModel> _models =
        new Dictionary<int, IModel>
        {
            { 1, new CarModel("GLE") },
            { 2, new CarModel("AMG GT") },
        }.ToFrozenDictionary();

    public IReadOnlyDictionary<int, IModel> GetBrandModels()
    {
        return _models;
    }
}
