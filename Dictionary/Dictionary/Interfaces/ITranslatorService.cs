namespace Dictionary.Interfaces;

public interface ITranslatorService
{
    void PrintAllTranslations();

    void PrintTranslation( string word, bool isEnglishToRussian );

    void AddNewTranslation( string word, string translation );
}
