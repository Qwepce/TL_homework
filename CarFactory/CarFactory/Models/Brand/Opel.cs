using System.Collections.Frozen;
using CarFactory.Models.Model;

namespace CarFactory.Models.Brand;

public class Opel : IBrand
{
    public string Name => "Opel";

    private readonly IReadOnlyDictionary<int, IModel> _models =
        new Dictionary<int, IModel>
        {
            { 1, new CarModel("Astra") },
            { 2, new CarModel("Insignia") },
        }.ToFrozenDictionary();

    public IReadOnlyDictionary<int, IModel> GetBrandModels()
    {
        return _models;
    }
}
