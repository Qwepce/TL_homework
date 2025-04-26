namespace CarFactory.Utils;

public class Messages
{
    public const string GreetingMessage = "Добро пожаловать на фабрику машин!\nСоздать свою машину - легко! Приступим?";
    public const string FarewellMessage = "До свидания! Будем ждать вас снова!";

    public const string AskUserChooseCarBrand = "Выберите марку автомобиля:";
    public const string AskUserChooseCarModel = "Выберите модель автомобиля:";
    public const string AskUserChooseEngine = "Выберите двигатель:";
    public const string AskUserChooseTransmission = "Выберите тип коробки передач:";
    public const string AskUserChoiceSteeringWheelPosition = "Выберите положение руля:";
    public const string AskUserChooseBodyShape = "Выберите тип кузова:";
    public const string AskUserChooseCarColor = "Выберите цвет автомобиля:";

    public const string AskUserSelectOption = "Введите ваш ответ: ";

    public const string NewCarAddedSuccessfully = "Новая машина успешно добавлена!";

    public const string InvalidOptionSelectInput = "Ошибка! Ввод должен быть в формате целого числа больше нуля. Повторите ввод: ";
    public const string UnknownOption = "Выбрана неизвестная опция. Повторите ввод: ";
    public const string UnknownCommandSelected = "Выбрана неизвестная команда. Повторите ввод.";
    public const string CarsListIsEmpty = "Вы пока не создали ни одной машины...";

    public const string AvailableCommandsMessage = """
        Доступный список команд:
        add - добавить новую конфигурацию
        list - показать все созданные конфигурации
        clear - очистить консоль
        exit - выйти
        """;
}
