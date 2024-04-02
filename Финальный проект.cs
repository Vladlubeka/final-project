using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp42
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public Product(int id, string name, decimal price, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }
    }

    public class ProductCatalog
    {
        private readonly List<Product> _products = new List<Product>();

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void RemoveProduct(int id)
        {
            var productToRemove = _products.Find(p => p.Id == id);
            if (productToRemove != null)
            {
                _products.Remove(productToRemove);
            }
        }

        public void EditProduct(int id, string name, decimal price, string category)
        {
            var productToEdit = _products.Find(p => p.Id == id);
            if (productToEdit != null)
            {
                productToEdit.Name = name;
                productToEdit.Price = price;
                productToEdit.Category = category;
            }
        }

        public List<Product> GetProducts()
        {
            return _products;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var productCatalog = new ProductCatalog();

            productCatalog.AddProduct(new Product(1, "Товар 1", 100.0m, "Категорія 1"));
            productCatalog.AddProduct(new Product(2, "Товар 2", 200.0m, "Категорія 2"));

            productCatalog.RemoveProduct(1);

            productCatalog.EditProduct(2, "Нова назва", 300.0m, "Нова категорія");

            var products = productCatalog.GetProducts();

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id} - {product.Name} - {product.Price} - {product.Category}");
            }
        }
    }
}
