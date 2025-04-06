namespace Dictionary.Utils;

public class Messages
{
    public const string GREETING_MESSAGE = "Добро пожаловать!";
    public const string FAREWELL_MESSAGE = "До свидания!";

    public const string ASK_USER_CHOICE_MESSAGE = "Выберите команду: ";
    public const string ASK_USER_INPUT_MESSAGE = "Введите слово, для которого нужно найти перевод: ";
    public const string TRANSLATED_WORD_MESSAGE = "Перевод введённого слова:";
    public const string NEW_TRANSLATION_ADDED_MESSAGE = "Новое слово и его перевод успешно добавлены!";

    public const string ASK_USER_NEW_RUSSIAN_WORD = "Введите новое слово на русском: ";
    public const string ASK_USER_NEW_ENGLISH_WORD = "Введите новое слово на английском: ";
    public const string ASK_USER_NEW_TRANSLATION = "Введите перевод для нового слова: ";

    public const string UNKNOWN_SELECTED_COMMAND_MESSAGE = "Не удалось распознать команду, попробуйте повторить ввод";
    public const string INVALID_COMMAND_INPUT_MESSAGE = "Команда должна быть в виде числа, попробуйте повторить ввод: ";
    public const string TRANSLATION_ALREADY_EXISTS_MESSAGE = "Ошибка, такое слово уже есть!";
    public const string INVALID_USER_INPUT_MESSAGE = "Неверный ввод, попробуйте ещё раз.";
    public const string TRANSLATION_NOT_FOUND_MESSAGE = "Не удалось найти перевод для данного слова.";

    public const string MENU_MESSAGE = """
        1. Узнать перевод русского слова (на английский).
        2. Узнать перевод английского слова (на русский).
        3. Добавить перевод русского слова.
        4. Добавить перевод английского слова.
        5. Посмотреть все переводы.
        6. Выйти
        """;
}
