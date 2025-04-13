using Fighters.Models.Fighters;

namespace Fighters.Utils;

public class Messages
{
    public const string GreetingMessage = "Добро пожаловать на безжалостную арену гладиаторов!";

    public const string AskUserInputNumberOfFighters = "Введите количество бойцов: ";

    public const string AskFighterNameMessage = "Введите имя бойца: ";
    public const string SelectRaceMessage = "Выберите расу бойца:";
    public const string SelectWeaponMessage = "Выберите оружие для бойца:";
    public const string SelectArmorMessage = "Выберите броню для бойца:";
    public const string SelectFighterClassMessage = "Выберите класс бойца:";

    public const string AskUserSelectOption = "Выберите опцию: ";

    public const string UnknownOptionSelected = "Выбрана неизвестная опция. Повторите ввод: ";
    public const string InvalidUserNumberInput = "Ввод должен быть в формате целого числа положительного числа. Повторите ввод: ";
    public const string InvalidUserInput = "Введены некорректные данные, строка не может быть пустой. Повторите ввод.";
    public const string InvalidNumberOfFighters = "Для начала боёв количество бойцов должно быть больше или равно 2. Повторите ввод: ";

    public const string RaceOptionsMessage = """
        1. Человек
        2. Гоблин
        3. Гном
        """;

    public const string WeaponOptionsMessage = """
        1. Без оружия
        2. Кинжал
        3. Меч
        4. Булава
        5. Посох
        6. Лук
        """;

    public const string ArmorOptionsMessage = """
        1. Без брони
        2. Кольчуга
        3. Шлем
        4. Щит
        """;

    public const string FighterClassesOptionsMessage = """
        1. Рыцарь
        2. Наемник
        3. Лучник
        4. Маг
        """;

    public static void PrintSuccessfullFighterCreation( int fighterIndex, IFighter fighter )
    {
        Console.WriteLine( $"""
            Боец #{fighterIndex} по имени {fighter.GetName()} успешно добавлен!
            Характеристики бойца:
            Здоровье = {fighter.GetMaxHealth()}
            Броня = {fighter.GetMaxArmor()}
            Урон = {fighter.GetBaseDamage()}
            """ );
    }

    public static void PrintFightersNameAndRoundNumber( int roundNumber, IFighter firstFighter, IFighter secondFighter )
    {
        Console.WriteLine( $"""
            ============================================
            Раунд #{roundNumber}.
            Бой между {firstFighter.GetName()} и {secondFighter.GetName()}
            ============================================
            """ );
    }

    public static void PrintInfoAboutFighterAfterRound( IFighter fighter, int takenDamage )
    {
        Console.WriteLine( $"""
            Боец {fighter.GetName()} получает {takenDamage} урона.
            Состояние бойца {fighter.GetName()} после раунда: 
            Здоровье = {fighter.GetCurrentHealth()}.
            Броня = {fighter.GetCurrentArmor()}

            """ );
    }

    public static void PrintWinner( IFighter fighter )
    {
        Console.WriteLine( $"""

            Битва окончена!
            Победителем из битвы вышел боец по имени {fighter.GetName()}
            Текущее здоровье = {fighter.GetCurrentHealth()}
            Текущая броня = {fighter.GetCurrentArmor()}

            """ );
    }

    public static void PrintMessageAboutFightersDeath( IFighter fighter )
    {
        Console.WriteLine( $"Боец {fighter.GetName()} погибает" );
    }
}
