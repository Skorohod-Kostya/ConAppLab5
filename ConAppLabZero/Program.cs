namespace ConAppLabZero;
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