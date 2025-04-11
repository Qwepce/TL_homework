using Dictionary.Interfaces;
using Dictionary.Utils;

namespace Dictionary.Implementations;

public class TranslatorService : ITranslatorService
{
    private Dictionary<string, string> _dictionary = new Dictionary<string, string>();

    private readonly string _filePath;

    public TranslatorService( string filePath )
    {
        _filePath = filePath;
        _dictionary = LoadData( filePath );
    }

    private static Dictionary<string, string> LoadData( string filePath )
    {
        Dictionary<string, string> translations = new Dictionary<string, string>();

        try
        {
            using ( StreamReader reader = new StreamReader( filePath ) )
            {
                string line;

                while ( ( line = reader.ReadLine() ) != null )
                {
                    string[] splittedLine = line
                        .Split( "-" )
                        .Select( word => word.Trim().ToLower() )
                        .ToArray();

                    if ( splittedLine.Length == 2 )
                    {
                        translations.Add( splittedLine[ 0 ], splittedLine[ 1 ] );
                    }
                }
            }
        }
        catch ( FileNotFoundException )
        {
            using ( File.CreateText( filePath ) )
            {

            }
        }

        return translations;
    }

    public void GetAllTranslations()
    {
        foreach ( KeyValuePair<string, string> pair in _dictionary )
        {
            Console.WriteLine( $"{pair.Key} - {pair.Value}" );
        }
    }

    public void GetTranslation( string word, bool isEnglishToRussian )
    {
        string? translation = isEnglishToRussian
            ? _dictionary.GetValueOrDefault( word )
            : _dictionary.FirstOrDefault( record => record.Value == word ).Key;

        if ( translation != null )
        {
            Console.WriteLine( $"{Messages.TranslatedWordMessage} {translation}" );
        }
        else
        {
            Console.WriteLine( Messages.TranslationNotFound );
        }
    }

    public void AddNewTranslation( string word, string translation )
    {
        if ( _dictionary.ContainsKey( translation ) || _dictionary.ContainsValue( word ) )
        {
            Console.WriteLine( Messages.TranslationAlreadyExists );
        }
        else
        {
            string newFileLineRecord = $"{translation} - {word}";

            using ( StreamWriter writer = new StreamWriter( _filePath, append: true ) )
            {
                writer.WriteLine( newFileLineRecord );
            }

            _dictionary[ translation ] = word;

            Console.WriteLine( Messages.NewTranslationAddedMessage );
        }
    }

}
