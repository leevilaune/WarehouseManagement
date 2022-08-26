// See https://aka.ms/new-console-template for more information

using WarehouseManagement;

//QueringProducts();
StartUI();


void StartUI()
{
    while (true)
    {
        Console.WriteLine("\nPick an option: \n" +
                      "1 - Add new product\n" +
                      "2 - Delete product\n" +
                      "3 - Print product amounts\n" +
                      "4 - Edit product names\n" +
                      "0 - Close\n");
        Console.Write("> ");
        string option = Console.ReadLine();
        if (option.Equals("0"))
        {
            break;
        }
        else if (option.Equals("1"))
        {
            AddProductUI();
        }
        else if (option.Equals("2"))
        {
            RemoveProductUI();
        }
        else if (option.Equals("3"))
        {
            ListProducts();
        }
        else if (option.Equals("4"))
        {
            ChangeProductNameUI();
        }
    }
    Console.WriteLine("Closed");
}

static void AddProductUI()
{
    Console.Write("Product Name: ");
    string productName = Console.ReadLine();
    Console.Write("Product Price: ");
    string productPrice = Console.ReadLine();
    Console.Write("Product Amount: ");
    string productAmount = Console.ReadLine();

    if (AddProduct(productName,productPrice,productAmount))
    {
        Console.WriteLine("New product added");
    }
    else
    {
        Console.WriteLine("Saving product failed, try again");
    }
}
static void ChangeProductNameUI()
{
    Console.Write("Product Id: ");
    int productId = int.Parse(Console.ReadLine());
    Console.Write("New Product Name: ");
    string productName = Console.ReadLine();

    if (ChangeProductName(productId, productName))
    {
        Console.WriteLine("Changing name successful");
    }
    else
    {
        Console.WriteLine("Changing name failed");
    }
}

static void RemoveProductUI()
{
    Console.Write("Product Id: ");
    int productId = int.Parse(Console.ReadLine());

    DeleteProduct(productId);
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

static bool AddProduct(string productName, string productPrice, string productAmount)
{
    using(InventoryManagement db = new())
    {
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
            Console.WriteLine(product.Id + ". " + product.ProductName + ", " + product.ProductPrice + "$");
        }
    }
}
static bool ChangeProductName(int id, string newName)
{
    using (InventoryManagement db = new())
    {
        Product p = db.Products?.Find(id);

        if(p is null)
        {
            return false;
        }
        p.ProductName = newName;

        int affected = db.SaveChanges();
        return(affected == 1);
    }
}
static int DeleteProduct(int id)
{
    using(InventoryManagement db = new())
    {
        Product p = db.Products.Find(id);

        if(p is null)
        {
            Console.WriteLine("Not Found");
            return -1;
        }
        db.Remove(p);
        int affected = db.SaveChanges();
        return affected;
    }
}