
    namespace NewShoppingCart.Models
    {
        public enum Category
        {
            Electronics,
            Clothing,
            Groceries,

        }
        public class ItemModel
        {
            public int id { get; set; }

            public string name { get; set; }
           //public Category category { get; set; }
            public double price { get; set; }
            public int qty { get; set; }
        }

    }

