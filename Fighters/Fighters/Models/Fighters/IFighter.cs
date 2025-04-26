namespace Fighters.Models.Fighters;

public interface IFighter
{
    string GetName();

    int GetBaseDamage();

    int GetMaxArmor();

    int GetMaxHealth();

    int GetCurrentHealth();

    int GetCurrentArmor();

    int CalculateDamage();

    void TakeDamage( int damage );
}
