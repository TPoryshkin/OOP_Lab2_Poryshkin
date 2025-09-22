public class Plant
{
    private string _name;
    private int _age;
    private double _height;
    private PlantType _type;
    private DateTime _plantingDate;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва не може бути порожньою.");
            if (value.Length < 2 || value.Length > 50)
                throw new ArgumentException("Назва повинна містити від 2 до 50 символів.");
            if (!value.All(c => char.IsLetter(c) || c == ' '))
                throw new ArgumentException("Назва може містити лише літери та пробіли.");
            _name = value;
        }
    }

    public PlantType Type
    {
        get => _type;
        set
        {
            if (!Enum.IsDefined(typeof(PlantType), value))
                throw new ArgumentException("Невірний тип рослини.");
            _type = value;
        }
    }

    public int Age
    {
        get => _age;
        set
        {
            if (value < 0 || value > 5000)
                throw new ArgumentException("Вік повинен бути в діапазоні від 0 до 5000 років.");
            _age = value;
        }
    }

    public double Height
    {
        get => _height;
        set
        {
            if (value <= 0 || value > 115.7)
                throw new ArgumentException("Висота повинна бути в діапазоні від 0 до 115.7 м (рекорд Hyperion).");
            _height = value;
        }
    }

    public DateTime PlantingDate
    {
        get => _plantingDate;
        set
        {
            ValidatePlantingDate(value);
            _plantingDate = value;
        }
    }

    public bool IsFlowering { get; set; } = true;

    public string AgeCategory
    {
        get
        {
            if (Age < 2) return "Молода";
            if (Age < 10) return "Доросла";
            return "Стара";
        }
    }

    public string LastWatered { get; private set; } = "Ніколи";

    public Plant(string name, PlantType type, int age, double height, DateTime plantingDate)
    {
        Name = name;
        Type = type;
        Age = age;
        Height = height;
        PlantingDate = plantingDate;
    }

    private void ValidatePlantingDate(DateTime date)
    {
        if (date.Year < 1900)
            throw new ArgumentException("Дата посадки не може бути раніше 1900 року.");
        if (date > DateTime.Now)
            throw new ArgumentException("Дата посадки не може бути у майбутньому.");
    }

    private string FormatWateringTime(DateTime time)
    {
        return time.ToString("dd.MM.yyyy HH:mm");
    }

    public void WaterPlant()
    {
        DateTime wateringTime = DateTime.Now;
        LastWatered = FormatWateringTime(wateringTime);
        Console.WriteLine($"{Name} було полито. Час останнього поливу: {LastWatered}");
    }

    private string GetFormattedDescription()
    {
        return $"{Name} ({Type}) - {Age} років, {Height} м";
    }

    public string GetDescription()
    {
        return GetFormattedDescription();
    }

    public void Grow(double growth)
    {
        if (growth <= 0)
            throw new ArgumentException("Ріст повинен бути більше 0.");
        Height += growth;
        Console.WriteLine($"{Name} виріс на {growth}м. Нова висота: {Height}м");
    }

    public string GetPlantingInfo()
    {
        return $"{Name} було висаджено {PlantingDate:dd.MM.yyyy}.";
    }

    public bool IsMature()
    {
        return Age > 5;
    }
}