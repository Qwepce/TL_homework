namespace Dictionary.Utils;

public class Messages
{
    public const string GreetingMessage = "Добро пожаловать!";
    public const string FarewellMessage = "До свидания!";

    public const string AskUserChoiceMessage = "Выберите команду: ";
    public const string AskUserInputMessage = "Введите слово, для которого нужно найти перевод: ";
    public const string TranslatedWordMessage = "Перевод введённого слова:";
    public const string NewTranslationAddedMessage = "Новое слово и его перевод успешно добавлены!";

    public const string AskUserNewRussianWord = "Введите новое слово на русском: ";
    public const string AskUserNewEnglishWord = "Введите новое слово на английском: ";
    public const string AskUserNewTranslation = "Введите перевод для нового слова: ";

    public const string UnknownSelectedCommand = "Не удалось распознать команду, попробуйте повторить ввод";
    public const string InvalidCommandInput = "Команда должна быть в виде числа, попробуйте повторить ввод: ";
    public const string TranslationAlreadyExists = "Ошибка, такое слово уже есть!";
    public const string InvalidUserInput = "Неверный ввод, попробуйте ещё раз.";
    public const string TranslationNotFound = "Не удалось найти перевод для данного слова.";

    public const string MenuMessage = """
        1. Узнать перевод русского слова (на английский).
        2. Узнать перевод английского слова (на русский).
        3. Добавить перевод русского слова.
        4. Добавить перевод английского слова.
        5. Посмотреть все переводы.
        6. Выйти
        """;
}
