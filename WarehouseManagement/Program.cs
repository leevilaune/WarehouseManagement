// See https://aka.ms/new-console-template for more information

using WarehouseManagement;

QueringProducts();
StartUI();


void StartUI()
{
    Console.WriteLine("Test");
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