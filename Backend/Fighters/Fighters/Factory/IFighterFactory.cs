using Fighters.Models.Fighters;

namespace Fighters.Factory;

public interface IFighterFactory
{
    IFighter CreateFighter();
}
