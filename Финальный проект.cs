using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp44
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public Product() { } 

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

        public void SaveToXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, _products);
            }

            Console.WriteLine("Данные успешно записаны в XML файл.");
        }

        public void LoadFromXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                _products.Clear();
                _products.AddRange((List<Product>)serializer.Deserialize(stream));
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var productCatalog = new ProductCatalog();

            Console.WriteLine("Введите ID товара:");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите название товара:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите цену товара:");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Введите категорию товара:");
            string category = Console.ReadLine();

            productCatalog.AddProduct(new Product(id, name, price, category));

            productCatalog.SaveToXml("products.xml");

            productCatalog.RemoveProduct(1);
            productCatalog.EditProduct(2, "Нова назва", 300.0m, "Нова категорія");

            productCatalog.LoadFromXml("products.xml");

            var products = productCatalog.GetProducts();

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id} - {product.Name} - {product.Price} - {product.Category}");
            }
        }
    }
}
