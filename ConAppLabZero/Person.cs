namespace ConAppLabZero;

public class Person
{
    private string name;
    private string surname;
    private DateTime birthDate;

    public Person(string name, string surname, DateTime birthDate)
    {
        this.name = name;
        this.surname = surname;
        this.birthDate = birthDate;
    }

    public Person()
    {
        name = "John";
        surname = "Doe";
        birthDate = new DateTime(1990, 1, 1);
    }

    public string Name
    {
        get { return name; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                name = value;
            else
                throw new ArgumentOutOfRangeException("Ім'я не може бути порожнім");
        }
    }

    public string Surname
    {
        get { return surname; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                surname = value;
            else
                throw new ArgumentOutOfRangeException("Прізвище не може бути порожнім");
        }
    }

    public DateTime BirthDate
    {
        get { return birthDate; }
        set { birthDate = value; }
    }

    public int BirthYear
    {
        get { return birthDate.Year; }
        set
        {
            if (value >= 1900 && value <= DateTime.Now.Year)
                birthDate = new DateTime(value, birthDate.Month, birthDate.Day);
            else
                throw new ArgumentOutOfRangeException("Некоректний рік народження");
        }
    }

    public override string ToString()
    {
        return $"Ім'я: {name}, Прізвище: {surname}, Дата народження: {birthDate.ToShortDateString()}";
    }

    public string ToShortString()
    {
        return $"{name} {surname}";
    }
}
