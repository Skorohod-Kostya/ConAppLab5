namespace ConAppLabZero;

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
