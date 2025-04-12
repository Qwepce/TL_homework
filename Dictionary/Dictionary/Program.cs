using Dictionary.Implementations;
using Dictionary.Interfaces;
using Dictionary.Utils;

namespace Dictionary;

public class Program
{
    private const string FilePath = "translations.txt";

    public static void Main()
    {
        ICustomValidator validator = new CustomValidator();
        ITranslatorService translatorService = new TranslatorService( FilePath );

        Console.WriteLine( Messages.GreetingMessage );
        Console.WriteLine( Messages.MenuMessage );

        TranslatorManager.Run( translatorService, validator );

        Console.WriteLine( Messages.FarewellMessage );
    }
}