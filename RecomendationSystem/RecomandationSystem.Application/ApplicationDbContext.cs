using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetTopologySuite.Index.HPRtree;
using RecomandationSystem.Application.Enums;
using RecomandationSystem.Application.Models;
using System.Reflection.Emit;

namespace RecomandationSystem.Application
{
    public sealed class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IConfiguration configuration) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(ApplicationDbContext)), o => o.UseVector());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("vector");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(GetSeedProducts());
        }

        public static List<Product> GetSeedProducts()
        {
            return new List<Product>
            {
                // Electronics (15 items)
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Smartphone X10", Description = "Latest flagship smartphone with AMOLED display", Brand = "Samsung", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Wireless Earbuds Pro", Description = "Noise-cancelling wireless earbuds with 30hr battery", Brand = "Sony", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "4K Ultra HD TV", Description = "55-inch smart TV with HDR support", Brand = "LG", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Gaming Laptop", Description = "High-performance laptop with RTX graphics", Brand = "ASUS", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Smart Watch", Description = "Fitness tracker with heart rate monitoring", Brand = "Apple", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "Bluetooth Speaker", Description = "Portable waterproof speaker with 20W output", Brand = "JBL", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "DSLR Camera", Description = "24MP camera with 4K video recording", Brand = "Canon", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = "Tablet", Description = "10-inch tablet with stylus support", Brand = "Microsoft", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = "Noise Cancelling Headphones", Description = "Over-ear headphones with 40hr battery life", Brand = "Bose", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = "E-Reader", Description = "Paper-like display with adjustable front light", Brand = "Amazon", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000011"), Name = "Action Camera", Description = "Waterproof camera with 4K/60fps video", Brand = "GoPro", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000012"), Name = "External SSD", Description = "1TB portable SSD with USB 3.2 interface", Brand = "SanDisk", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000013"), Name = "Wireless Charger", Description = "15W fast charging pad for smartphones", Brand = "Belkin", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000014"), Name = "Smart Home Hub", Description = "Central control for all smart home devices", Brand = "Google", Category = Categories.Electronics },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000015"), Name = "VR Headset", Description = "Immersive virtual reality experience", Brand = "Meta", Category = Categories.Electronics },

                // Appliances (10 items)
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000016"), Name = "Refrigerator", Description = "French door fridge with ice maker", Brand = "Samsung", Category = Categories.Appliances },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000017"), Name = "Washing Machine", Description = "Front load washer with steam function", Brand = "LG", Category = Categories.Appliances },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000018"), Name = "Microwave Oven", Description = "Convection microwave with grill", Brand = "Panasonic", Category = Categories.Appliances },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000019"), Name = "Air Conditioner", Description = "Inverter split AC with wifi control", Brand = "Daikin", Category = Categories.Appliances },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000020"), Name = "Robot Vacuum", Description = "Self-charging robot vacuum with mapping", Brand = "iRobot", Category = Categories.Appliances },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000021"), Name = "Blender", Description = "High-speed blender with 6 preset programs", Brand = "Vitamix", Category = Categories.Appliances },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000022"), Name = "Coffee Maker", Description = "Programmable 12-cup coffee machine", Brand = "Keurig", Category = Categories.Appliances },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000023"), Name = "Food Processor", Description = "12-cup capacity with multiple attachments", Brand = "Cuisinart", Category = Categories.Appliances },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000024"), Name = "Stand Mixer", Description = "Professional grade kitchen mixer", Brand = "KitchenAid", Category = Categories.Appliances },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000025"), Name = "Air Fryer", Description = "Digital air fryer with 8 cooking presets", Brand = "Ninja", Category = Categories.Appliances },

                // Clothing (15 items)
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000026"), Name = "Men's T-Shirt", Description = "100% cotton crew neck t-shirt", Brand = "Nike", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000027"), Name = "Women's Jeans", Description = "Slim fit stretch denim jeans", Brand = "Levi's", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000028"), Name = "Winter Jacket", Description = "Waterproof down jacket for cold weather", Brand = "The North Face", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000029"), Name = "Running Shoes", Description = "Lightweight shoes with cushioned soles", Brand = "Adidas", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000030"), Name = "Dress Shirt", Description = "Formal business shirt with slim fit", Brand = "Hugo Boss", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000031"), Name = "Yoga Pants", Description = "High-waisted leggings with moisture-wicking fabric", Brand = "Lululemon", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000032"), Name = "Summer Dress", Description = "Floral print sundress with ruffled sleeves", Brand = "Zara", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000033"), Name = "Leather Belt", Description = "Genuine leather belt with metal buckle", Brand = "Gucci", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000034"), Name = "Wool Sweater", Description = "Cable knit sweater in various colors", Brand = "Ralph Lauren", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000035"), Name = "Swim Trunks", Description = "Quick-dry swim shorts with mesh lining", Brand = "Speedo", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000036"), Name = "Baseball Cap", Description = "Adjustable cotton twill cap", Brand = "New Era", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000037"), Name = "Silk Scarf", Description = "Luxury printed silk scarf", Brand = "Hermès", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000038"), Name = "Winter Gloves", Description = "Insulated touchscreen-compatible gloves", Brand = "Canada Goose", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000039"), Name = "Socks Pack", Description = "6-pack of cushioned athletic socks", Brand = "Under Armour", Category = Categories.Clothing },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000040"), Name = "Denim Jacket", Description = "Classic blue denim jacket with pockets", Brand = "Wrangler", Category = Categories.Clothing },

                // HomeGoods (10 items)
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000041"), Name = "Throw Pillow", Description = "Decorative pillow with geometric pattern", Brand = "IKEA", Category = Categories.HomeGoods },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000042"), Name = "Bed Sheet Set", Description = "1000 thread count Egyptian cotton sheets", Brand = "Brooklinen", Category = Categories.HomeGoods },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000043"), Name = "Dinnerware Set", Description = "16-piece porcelain dinner set", Brand = "Corelle", Category = Categories.HomeGoods },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000044"), Name = "Area Rug", Description = "5x7 ft wool rug with traditional design", Brand = "Ruggable", Category = Categories.HomeGoods },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000045"), Name = "Table Lamp", Description = "Modern LED lamp with dimmable light", Brand = "West Elm", Category = Categories.HomeGoods },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000046"), Name = "Wall Art", Description = "Framed canvas print of abstract painting", Brand = "Etsy", Category = Categories.HomeGoods },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000047"), Name = "Cookware Set", Description = "10-piece non-stick cookware set", Brand = "All-Clad", Category = Categories.HomeGoods },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000048"), Name = "Bath Towel Set", Description = "6-piece luxury cotton towel set", Brand = "Parachute", Category = Categories.HomeGoods },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000049"), Name = "Curtains", Description = "Blackout curtains with thermal insulation", Brand = "Pottery Barn", Category = Categories.HomeGoods },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000050"), Name = "Storage Baskets", Description = "Set of 3 woven storage baskets", Brand = "The Container Store", Category = Categories.HomeGoods },

                // Beauty (10 items)
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000051"), Name = "Moisturizing Cream", Description = "24-hour hydration facial moisturizer", Brand = "CeraVe", Category = Categories.Beauty },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000052"), Name = "Shampoo", Description = "Sulfate-free shampoo for all hair types", Brand = "Olaplex", Category = Categories.Beauty },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000053"), Name = "Lipstick", Description = "Long-wear matte lipstick in 12 shades", Brand = "MAC", Category = Categories.Beauty },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000054"), Name = "Perfume", Description = "Eau de parfum with floral notes", Brand = "Chanel", Category = Categories.Beauty },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000055"), Name = "Electric Toothbrush", Description = "Sonic toothbrush with 3 cleaning modes", Brand = "Oral-B", Category = Categories.Beauty },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000056"), Name = "Facial Cleanser", Description = "Gentle foaming cleanser for sensitive skin", Brand = "La Roche-Posay", Category = Categories.Beauty },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000057"), Name = "Hair Dryer", Description = "Professional ionic hair dryer", Brand = "Dyson", Category = Categories.Beauty },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000058"), Name = "Nail Polish Set", Description = "6-piece set of long-lasting nail colors", Brand = "OPI", Category = Categories.Beauty },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000059"), Name = "Makeup Brush Set", Description = "12-piece vegan makeup brush collection", Brand = "Morphe", Category = Categories.Beauty },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000060"), Name = "Sunscreen Lotion", Description = "SPF 50 broad spectrum protection", Brand = "Neutrogena", Category = Categories.Beauty },

                // Sports (10 items)
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000061"), Name = "Yoga Mat", Description = "Non-slip eco-friendly yoga mat", Brand = "Manduka", Category = Categories.Sports },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000062"), Name = "Dumbbell Set", Description = "Adjustable dumbbells 5-25 lbs", Brand = "Bowflex", Category = Categories.Sports },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000063"), Name = "Running Watch", Description = "GPS running watch with heart rate monitor", Brand = "Garmin", Category = Categories.Sports },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000064"), Name = "Basketball", Description = "Official size 7 basketball", Brand = "Spalding", Category = Categories.Sports },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000065"), Name = "Tennis Racket", Description = "Graphite racket with vibration dampening", Brand = "Wilson", Category = Categories.Sports },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000066"), Name = "Cycling Helmet", Description = "Lightweight aerodynamic bike helmet", Brand = "Specialized", Category = Categories.Sports },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000067"), Name = "Fitness Tracker", Description = "Water-resistant activity tracker", Brand = "Fitbit", Category = Categories.Sports },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000068"), Name = "Resistance Bands", Description = "Set of 5 latex resistance bands", Brand = "TheraBand", Category = Categories.Sports },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000069"), Name = "Hiking Backpack", Description = "30L waterproof hiking backpack", Brand = "Osprey", Category = Categories.Sports },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000070"), Name = "Swim Goggles", Description = "Anti-fog UV protection swim goggles", Brand = "Arena", Category = Categories.Sports },

                // Books (10 items)
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000071"), Name = "Bestselling Novel", Description = "Award-winning fiction book", Brand = "Penguin", Category = Categories.Books },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000072"), Name = "Cookbook", Description = "Collection of 100 easy recipes", Brand = "Williams Sonoma", Category = Categories.Books },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000073"), Name = "Self-Help Book", Description = "Guide to personal development", Brand = "Simon & Schuster", Category = Categories.Books },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000074"), Name = "Children's Book", Description = "Illustrated storybook for ages 4-8", Brand = "Scholastic", Category = Categories.Books },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000075"), Name = "Science Textbook", Description = "University level physics textbook", Brand = "Pearson", Category = Categories.Books },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000076"), Name = "Travel Guide", Description = "Comprehensive city travel guide", Brand = "Lonely Planet", Category = Categories.Books },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000077"), Name = "Biography", Description = "Life story of famous historical figure", Brand = "Random House", Category = Categories.Books },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000078"), Name = "Art Book", Description = "Collection of famous paintings", Brand = "Taschen", Category = Categories.Books },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000079"), Name = "Poetry Collection", Description = "Contemporary poetry anthology", Brand = "Faber & Faber", Category = Categories.Books },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000080"), Name = "Business Book", Description = "Guide to entrepreneurship", Brand = "Harvard Business Review", Category = Categories.Books },

                // Toys (10 items)
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000081"), Name = "Building Blocks", Description = "100-piece interlocking building set", Brand = "LEGO", Category = Categories.Toys },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000082"), Name = "Doll", Description = "Interactive doll with accessories", Brand = "Barbie", Category = Categories.Toys },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000083"), Name = "Remote Control Car", Description = "1:18 scale RC car with 2.4GHz remote", Brand = "Traxxas", Category = Categories.Toys },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000084"), Name = "Board Game", Description = "Family strategy board game", Brand = "Hasbro", Category = Categories.Toys },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000085"), Name = "Puzzle", Description = "1000-piece jigsaw puzzle", Brand = "Ravensburger", Category = Categories.Toys },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000086"), Name = "Stuffed Animal", Description = "Plush teddy bear", Brand = "Steiff", Category = Categories.Toys },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000087"), Name = "Science Kit", Description = "STEM learning kit for kids", Brand = "Thames & Kosmos", Category = Categories.Toys },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000088"), Name = "Action Figure", Description = "Collectible movie character figure", Brand = "Hot Toys", Category = Categories.Toys },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000089"), Name = "Play Kitchen", Description = "Miniature kitchen with accessories", Brand = "KidKraft", Category = Categories.Toys },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000090"), Name = "Art Set", Description = "Deluxe art supplies for children", Brand = "Crayola", Category = Categories.Toys },

                // Food (5 items)
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000091"), Name = "Organic Coffee", Description = "Premium arabica whole bean coffee", Brand = "Lavazza", Category = Categories.Food },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000092"), Name = "Dark Chocolate", Description = "70% cocoa dark chocolate bar", Brand = "Lindt", Category = Categories.Food },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000093"), Name = "Olive Oil", Description = "Extra virgin cold-pressed olive oil", Brand = "Colavita", Category = Categories.Food },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000094"), Name = "Granola", Description = "Gluten-free almond honey granola", Brand = "Kind", Category = Categories.Food },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000095"), Name = "Tea Selection", Description = "Assorted premium tea collection", Brand = "Twinings", Category = Categories.Food },

                // Automotive (5 items)
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000096"), Name = "Car Phone Mount", Description = "Dashboard smartphone holder", Brand = "iOttie", Category = Categories.Automotive },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000097"), Name = "Jump Starter", Description = "Portable car battery jump starter", Brand = "NOCO", Category = Categories.Automotive },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000098"), Name = "Car Vacuum", Description = "Handheld cordless car vacuum cleaner", Brand = "Black+Decker", Category = Categories.Automotive },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000099"), Name = "Tire Pressure Gauge", Description = "Digital tire pressure monitor", Brand = "Accutire", Category = Categories.Automotive },
                new Product { Id = Guid.Parse("00000000-0000-0000-0000-000000000100"), Name = "Car Wax", Description = "Premium liquid car wax", Brand = "Meguiar's", Category = Categories.Automotive }
            };
        }
    }
}
