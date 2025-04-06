namespace Dictionary.Interfaces;

public interface ITranslatorService
{
    void GetAllTranslations();

    void GetTranslation( string word, bool isEnglishToRussian );

    void AddNewTranslation( string word, string translation );
}
