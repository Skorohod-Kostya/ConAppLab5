namespace ConAppLabZero;

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
