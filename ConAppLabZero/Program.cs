namespace ConAppLabZero;
enum ProductCategory
{
    OfficeEquipment,
    Software,
    OfficeSupplies
}

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
public class Product
{
    public Person Supplier { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(Person supplier, string name, decimal price)
    {
        Supplier = supplier;
        Name = name;
        Price = price;
    }

    public Product()
    {
        Supplier = new Person();
        Name = "Default Product";
        Price = 100.00m;
    }

    public override string ToString()
    {
        return $"Товар: {Name}, Постачальник: {Supplier.ToShortString()}, Ціна: {Price}";
    }
}
class Shop
{
    private string name;
    private ProductCategory category;
    private DateTime openingDate;
    private Product[] products;

    public Shop(string name, ProductCategory category, DateTime openingDate, Product[] products)
    {
        this.name = name;
        this.category = category;
        this.openingDate = openingDate;
        this.products = products;
    }

    public Shop()
    {
        name = "Default Shop";
        category = ProductCategory.OfficeSupplies;
        openingDate = new DateTime(2020, 1, 1);
        products = new Product[0];
    }

    public string Name
    {
        get { return name; }
        set
        {
            if (!string.IsNullOrEmpty(value))
                name = value;
            else
                throw new ArgumentOutOfRangeException("Назва магазину не може бути порожньою");
        }
    }

    public ProductCategory Category
    {
        get { return category; }
        set
        {
            if (Enum.IsDefined(typeof(ProductCategory), value))
                category = value;
            else
                throw new ArgumentOutOfRangeException("Некоректна категорія");
        }
    }

    public DateTime OpeningDate
    {
        get { return openingDate; }
        set
        {
            if (value <= DateTime.Now)
                openingDate = value;
            else
                throw new ArgumentOutOfRangeException("Дата відкриття не може бути в майбутньому");
        }
    }

    public double TotalCost
    {
        get
        {
            double sum = 0;
            foreach (Product p in products)
                sum += (double)p.Price;
            return sum;
        }
    }

    public bool this[ProductCategory cat]
    {
        get { return category == cat; }
    }

    public void AddProduct(params Product[] newProducts)
    {
        Product[] temp = new Product[products.Length + newProducts.Length];
        for (int i = 0; i < products.Length; i++)
            temp[i] = products[i];
        for (int i = 0; i < newProducts.Length; i++)
            temp[products.Length + i] = newProducts[i];
        products = temp;
    }

    public override string ToString()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine($"Назва магазину: {name}");
        sb.AppendLine($"Категорія: {category}");
        sb.AppendLine($"Дата відкриття: {openingDate.ToShortDateString()}");
        sb.AppendLine($"Сумарна вартість: {TotalCost}");
        sb.AppendLine("Товари:");
        foreach (Product p in products)
            sb.AppendLine(p.ToString());
        return sb.ToString();
    }

    public string ToShortString()
    {
        return $"Назва магазину: {name}, Категорія: {category}, Дата відкриття: {openingDate.ToShortDateString()}, Сумарна вартість: {TotalCost}";
    }
}
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Створення першого магазину:");
        Shop shop = CreateShopFromInput();
        Console.WriteLine("\nСкорочена інформація про магазин:");
        Console.WriteLine(shop.ToShortString());
        Console.WriteLine("\nЗначення індексатора:");
        Console.WriteLine("OfficeEquipment: {0}", shop[ProductCategory.OfficeEquipment]);
        Console.WriteLine("Software: {0}", shop[ProductCategory.Software]);
        Console.WriteLine("OfficeSupplies: {0}", shop[ProductCategory.OfficeSupplies]);
        try
        {
            Console.WriteLine("\nВведіть нові дані для магазину:");
            Console.Write("Назва магазину: ");
            shop.Name = Console.ReadLine();
            Console.Write("Категорія (0=OfficeEquipment, 1=Software, 2=OfficeSupplies): ");
            shop.Category = (ProductCategory)int.Parse(Console.ReadLine());
            Console.Write("Дата відкриття (рррр-мм-дд): ");
            shop.OpeningDate = DateTime.Parse(Console.ReadLine());
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine("Помилка: {0}", ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка введення: {0}", ex.Message);
        }

        Console.WriteLine("\nПовна інформація про магазин:");
        Console.WriteLine(shop.ToString());
        Console.WriteLine("\nВведіть кількість товарів для додавання: ");
        int productCount = 0;
        bool flag;
        do
        {
            flag = true;
            try
            {
                productCount = int.Parse(Console.ReadLine());
                if (productCount < 0)
                    throw new ArgumentOutOfRangeException("Кількість не може бути від’ємною");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: {0}. Введіть знову!", ex.Message);
                flag = false;
            }
        } while (!flag);

        Product[] newProducts = new Product[productCount];
        for (int i = 0; i < productCount; i++)
        {
            Console.WriteLine($"Введіть дані для товару {i + 1}:");
            newProducts[i] = CreateProductFromInput();
        }
        shop.AddProduct(newProducts);

        Console.WriteLine("\nОновлений магазин:");
        Console.WriteLine(shop.ToString());
        Console.WriteLine("\nСтворення масиву магазинів:");
        Shop[] shops = new Shop[3];
        shops[0] = shop;
        for (int i = 1; i < shops.Length; i++)
        {
            Console.WriteLine($"\nСтворення магазину {i + 1}:");
            shops[i] = CreateShopFromInput();
            Console.Write($"Введіть кількість товарів для магазину {i + 1}: ");
            do
            {
                flag = true;
                try
                {
                    productCount = int.Parse(Console.ReadLine());
                    if (productCount < 0)
                        throw new ArgumentOutOfRangeException("Кількість не може бути від’ємною");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}. Введіть знову!", ex.Message);
                    flag = false;
                }
            } while (!flag);

            newProducts = new Product[productCount];
            for (int j = 0; j < productCount; j++)
            {
                Console.WriteLine($"Введіть дані для товару {j + 1}:");
                newProducts[j] = CreateProductFromInput();
            }
            shops[i].AddProduct(newProducts);
        }
        double maxCost = shops[0].TotalCost, minCost = shops[0].TotalCost;
        Shop maxShop = shops[0], minShop = shops[0];

        for (int i = 1; i < shops.Length; i++)
        {
            if (shops[i].TotalCost > maxCost)
            {
                maxCost = shops[i].TotalCost;
                maxShop = shops[i];
            }
            if (shops[i].TotalCost < minCost)
            {
                minCost = shops[i].TotalCost;
                minShop = shops[i];
            }
        }
        Console.WriteLine("\nМагазин із найбільшою вартістю:");
        Console.WriteLine(maxShop.ToShortString());
        Console.WriteLine("\nМагазин із найменшою вартістю:");
        Console.WriteLine(minShop.ToShortString());
        Console.ReadKey();
    }

    static Shop CreateShopFromInput()
    {
        try
        {
            Console.Write("Назва магазину: ");
            string name = Console.ReadLine();
            Console.Write("Категорія (0=OfficeEquipment, 1=Software, 2=OfficeSupplies): ");
            ProductCategory category = (ProductCategory)int.Parse(Console.ReadLine());
            Console.Write("Дата відкриття (рррр-мм-дд): ");
            DateTime openingDate = DateTime.Parse(Console.ReadLine());
            return new Shop(name, category, openingDate, new Product[0]);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка введення: {0}. Використано значення за замовчуванням.", ex.Message);
            return new Shop();
        }
    }

    static Product CreateProductFromInput()
    {
        try
        {
            Console.Write("Ім'я постачальника: ");
            string name = Console.ReadLine();
            Console.Write("Прізвище постачальника: ");
            string surname = Console.ReadLine();
            Console.Write("Дата народження постачальника (рррр-мм-дд): ");
            DateTime birthDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Назва товару: ");
            string productName = Console.ReadLine();
            Console.Write("Ціна товару: ");
            decimal price = decimal.Parse(Console.ReadLine());
            if (price < 0)
                throw new ArgumentOutOfRangeException("Ціна не може бути від’ємною");
            Person supplier = new Person(name, surname, birthDate);
            return new Product(supplier, productName, price);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine("Помилка: {0}. Використано значення за замовчуванням.", ex.Message);
            return new Product();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка введення: {0}. Використано значення за замовчуванням.", ex.Message);
            return new Product();
        }
    }
}