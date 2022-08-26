// See https://aka.ms/new-console-template for more information

using WarehouseManagement;

QueringProducts();
StartUI();


void StartUI()
{
    while (true)
    {
        Console.WriteLine("Pick an option: \n" +
                      "1 - Add new product\n" +
                      "2 - Delete product\n" +
                      "3 - Print product amounts\n" +
                      "4 - Edit product names\n" +
                      "0 - Close");
        string option = Console.ReadLine();
        if (option.Equals("0"))
        {
            break;
        }
        else if (option.Equals("1"))
        {
            if (AddProduct())
            {
                Console.WriteLine("New product added");
                continue;
            }
            Console.WriteLine("Saving product failed, try again");
        }
        else if (option.Equals("2"))
        {

        }
        else if (option.Equals("3"))
        {
            ListProducts();
        }
    }
    Console.WriteLine("Closed");
}

static void QueringProducts()
{
    using(InventoryManagement db = new())
    {
        IQueryable<Product>? products = db.Products;
        if(products is null)
        {
            Console.WriteLine("No products in the system");
            return;
        }
        foreach(Product product in products)
        {
            Console.WriteLine(product.ProductName);
        }
    }
}

static int FindHighestID()
{

    int highestID = 0;

    using (InventoryManagement db = new())
    {
        IQueryable<Product>? products = db.Products;
        if (products is null)
        {
            Console.WriteLine("No products in the system");
            return -1;
        }
        foreach (Product product in products)
        {
            if(product.Id > highestID)
            {
                highestID = product.Id;
            }
        }
    }
    return highestID;
}

static bool AddProduct()
{
    using(InventoryManagement db = new())
    {
        Console.Write("Product Name: ");
        string productName = Console.ReadLine();
        Console.Write("Product Price: ");
        string productPrice = Console.ReadLine();
        Console.Write("Product Amount: ");
        string productAmount = Console.ReadLine();
        Product p = new()
        {
            Id = FindHighestID()+1,
            ProductName = productName,
            ProductPrice = int.Parse(productPrice),
            ProductAmount = int.Parse(productAmount)
        };

        db.Products?.Add(p);

        int affected = db.SaveChanges();
        return (affected == 1);
    }
}
static void ListProducts()
{
    using (InventoryManagement db = new())
    {
        IQueryable<Product>? products = db.Products;
        if (products is null)
        {
            Console.WriteLine("No products in the system");
            return;
        }
        foreach (Product product in products)
        {
            Console.WriteLine(product.ProductName + ", " + product.ProductPrice + "$");
        }
    }
}