using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Pgvector;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecomandationSystem.Application.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:vector", ",,");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    UserGender = table.Column<int>(type: "integer", nullable: false),
                    CombinedEmbedding1024 = table.Column<Vector>(type: "vector(1024)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Embedding1024 = table.Column<Vector>(type: "vector(1024)", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Brand", "Category", "Description", "Embedding1024", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Samsung", 0, "Latest flagship smartphone with AMOLED display", null, "Smartphone X10", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Sony", 0, "Noise-cancelling wireless earbuds with 30hr battery", null, "Wireless Earbuds Pro", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "LG", 0, "55-inch smart TV with HDR support", null, "4K Ultra HD TV", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "ASUS", 0, "High-performance laptop with RTX graphics", null, "Gaming Laptop", null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Apple", 0, "Fitness tracker with heart rate monitoring", null, "Smart Watch", null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "JBL", 0, "Portable waterproof speaker with 20W output", null, "Bluetooth Speaker", null },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Canon", 0, "24MP camera with 4K video recording", null, "DSLR Camera", null },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Microsoft", 0, "10-inch tablet with stylus support", null, "Tablet", null },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Bose", 0, "Over-ear headphones with 40hr battery life", null, "Noise Cancelling Headphones", null },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Amazon", 0, "Paper-like display with adjustable front light", null, "E-Reader", null },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "GoPro", 0, "Waterproof camera with 4K/60fps video", null, "Action Camera", null },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "SanDisk", 0, "1TB portable SSD with USB 3.2 interface", null, "External SSD", null },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "Belkin", 0, "15W fast charging pad for smartphones", null, "Wireless Charger", null },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "Google", 0, "Central control for all smart home devices", null, "Smart Home Hub", null },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "Meta", 0, "Immersive virtual reality experience", null, "VR Headset", null },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "Samsung", 1, "French door fridge with ice maker", null, "Refrigerator", null },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "LG", 1, "Front load washer with steam function", null, "Washing Machine", null },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "Panasonic", 1, "Convection microwave with grill", null, "Microwave Oven", null },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "Daikin", 1, "Inverter split AC with wifi control", null, "Air Conditioner", null },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "iRobot", 1, "Self-charging robot vacuum with mapping", null, "Robot Vacuum", null },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "Vitamix", 1, "High-speed blender with 6 preset programs", null, "Blender", null },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "Keurig", 1, "Programmable 12-cup coffee machine", null, "Coffee Maker", null },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "Cuisinart", 1, "12-cup capacity with multiple attachments", null, "Food Processor", null },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "KitchenAid", 1, "Professional grade kitchen mixer", null, "Stand Mixer", null },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "Ninja", 1, "Digital air fryer with 8 cooking presets", null, "Air Fryer", null },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "Nike", 2, "100% cotton crew neck t-shirt", null, "Men's T-Shirt", null },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "Levi's", 2, "Slim fit stretch denim jeans", null, "Women's Jeans", null },
                    { new Guid("00000000-0000-0000-0000-000000000028"), "The North Face", 2, "Waterproof down jacket for cold weather", null, "Winter Jacket", null },
                    { new Guid("00000000-0000-0000-0000-000000000029"), "Adidas", 2, "Lightweight shoes with cushioned soles", null, "Running Shoes", null },
                    { new Guid("00000000-0000-0000-0000-000000000030"), "Hugo Boss", 2, "Formal business shirt with slim fit", null, "Dress Shirt", null },
                    { new Guid("00000000-0000-0000-0000-000000000031"), "Lululemon", 2, "High-waisted leggings with moisture-wicking fabric", null, "Yoga Pants", null },
                    { new Guid("00000000-0000-0000-0000-000000000032"), "Zara", 2, "Floral print sundress with ruffled sleeves", null, "Summer Dress", null },
                    { new Guid("00000000-0000-0000-0000-000000000033"), "Gucci", 2, "Genuine leather belt with metal buckle", null, "Leather Belt", null },
                    { new Guid("00000000-0000-0000-0000-000000000034"), "Ralph Lauren", 2, "Cable knit sweater in various colors", null, "Wool Sweater", null },
                    { new Guid("00000000-0000-0000-0000-000000000035"), "Speedo", 2, "Quick-dry swim shorts with mesh lining", null, "Swim Trunks", null },
                    { new Guid("00000000-0000-0000-0000-000000000036"), "New Era", 2, "Adjustable cotton twill cap", null, "Baseball Cap", null },
                    { new Guid("00000000-0000-0000-0000-000000000037"), "Hermès", 2, "Luxury printed silk scarf", null, "Silk Scarf", null },
                    { new Guid("00000000-0000-0000-0000-000000000038"), "Canada Goose", 2, "Insulated touchscreen-compatible gloves", null, "Winter Gloves", null },
                    { new Guid("00000000-0000-0000-0000-000000000039"), "Under Armour", 2, "6-pack of cushioned athletic socks", null, "Socks Pack", null },
                    { new Guid("00000000-0000-0000-0000-000000000040"), "Wrangler", 2, "Classic blue denim jacket with pockets", null, "Denim Jacket", null },
                    { new Guid("00000000-0000-0000-0000-000000000041"), "IKEA", 3, "Decorative pillow with geometric pattern", null, "Throw Pillow", null },
                    { new Guid("00000000-0000-0000-0000-000000000042"), "Brooklinen", 3, "1000 thread count Egyptian cotton sheets", null, "Bed Sheet Set", null },
                    { new Guid("00000000-0000-0000-0000-000000000043"), "Corelle", 3, "16-piece porcelain dinner set", null, "Dinnerware Set", null },
                    { new Guid("00000000-0000-0000-0000-000000000044"), "Ruggable", 3, "5x7 ft wool rug with traditional design", null, "Area Rug", null },
                    { new Guid("00000000-0000-0000-0000-000000000045"), "West Elm", 3, "Modern LED lamp with dimmable light", null, "Table Lamp", null },
                    { new Guid("00000000-0000-0000-0000-000000000046"), "Etsy", 3, "Framed canvas print of abstract painting", null, "Wall Art", null },
                    { new Guid("00000000-0000-0000-0000-000000000047"), "All-Clad", 3, "10-piece non-stick cookware set", null, "Cookware Set", null },
                    { new Guid("00000000-0000-0000-0000-000000000048"), "Parachute", 3, "6-piece luxury cotton towel set", null, "Bath Towel Set", null },
                    { new Guid("00000000-0000-0000-0000-000000000049"), "Pottery Barn", 3, "Blackout curtains with thermal insulation", null, "Curtains", null },
                    { new Guid("00000000-0000-0000-0000-000000000050"), "The Container Store", 3, "Set of 3 woven storage baskets", null, "Storage Baskets", null },
                    { new Guid("00000000-0000-0000-0000-000000000051"), "CeraVe", 4, "24-hour hydration facial moisturizer", null, "Moisturizing Cream", null },
                    { new Guid("00000000-0000-0000-0000-000000000052"), "Olaplex", 4, "Sulfate-free shampoo for all hair types", null, "Shampoo", null },
                    { new Guid("00000000-0000-0000-0000-000000000053"), "MAC", 4, "Long-wear matte lipstick in 12 shades", null, "Lipstick", null },
                    { new Guid("00000000-0000-0000-0000-000000000054"), "Chanel", 4, "Eau de parfum with floral notes", null, "Perfume", null },
                    { new Guid("00000000-0000-0000-0000-000000000055"), "Oral-B", 4, "Sonic toothbrush with 3 cleaning modes", null, "Electric Toothbrush", null },
                    { new Guid("00000000-0000-0000-0000-000000000056"), "La Roche-Posay", 4, "Gentle foaming cleanser for sensitive skin", null, "Facial Cleanser", null },
                    { new Guid("00000000-0000-0000-0000-000000000057"), "Dyson", 4, "Professional ionic hair dryer", null, "Hair Dryer", null },
                    { new Guid("00000000-0000-0000-0000-000000000058"), "OPI", 4, "6-piece set of long-lasting nail colors", null, "Nail Polish Set", null },
                    { new Guid("00000000-0000-0000-0000-000000000059"), "Morphe", 4, "12-piece vegan makeup brush collection", null, "Makeup Brush Set", null },
                    { new Guid("00000000-0000-0000-0000-000000000060"), "Neutrogena", 4, "SPF 50 broad spectrum protection", null, "Sunscreen Lotion", null },
                    { new Guid("00000000-0000-0000-0000-000000000061"), "Manduka", 5, "Non-slip eco-friendly yoga mat", null, "Yoga Mat", null },
                    { new Guid("00000000-0000-0000-0000-000000000062"), "Bowflex", 5, "Adjustable dumbbells 5-25 lbs", null, "Dumbbell Set", null },
                    { new Guid("00000000-0000-0000-0000-000000000063"), "Garmin", 5, "GPS running watch with heart rate monitor", null, "Running Watch", null },
                    { new Guid("00000000-0000-0000-0000-000000000064"), "Spalding", 5, "Official size 7 basketball", null, "Basketball", null },
                    { new Guid("00000000-0000-0000-0000-000000000065"), "Wilson", 5, "Graphite racket with vibration dampening", null, "Tennis Racket", null },
                    { new Guid("00000000-0000-0000-0000-000000000066"), "Specialized", 5, "Lightweight aerodynamic bike helmet", null, "Cycling Helmet", null },
                    { new Guid("00000000-0000-0000-0000-000000000067"), "Fitbit", 5, "Water-resistant activity tracker", null, "Fitness Tracker", null },
                    { new Guid("00000000-0000-0000-0000-000000000068"), "TheraBand", 5, "Set of 5 latex resistance bands", null, "Resistance Bands", null },
                    { new Guid("00000000-0000-0000-0000-000000000069"), "Osprey", 5, "30L waterproof hiking backpack", null, "Hiking Backpack", null },
                    { new Guid("00000000-0000-0000-0000-000000000070"), "Arena", 5, "Anti-fog UV protection swim goggles", null, "Swim Goggles", null },
                    { new Guid("00000000-0000-0000-0000-000000000071"), "Penguin", 6, "Award-winning fiction book", null, "Bestselling Novel", null },
                    { new Guid("00000000-0000-0000-0000-000000000072"), "Williams Sonoma", 6, "Collection of 100 easy recipes", null, "Cookbook", null },
                    { new Guid("00000000-0000-0000-0000-000000000073"), "Simon & Schuster", 6, "Guide to personal development", null, "Self-Help Book", null },
                    { new Guid("00000000-0000-0000-0000-000000000074"), "Scholastic", 6, "Illustrated storybook for ages 4-8", null, "Children's Book", null },
                    { new Guid("00000000-0000-0000-0000-000000000075"), "Pearson", 6, "University level physics textbook", null, "Science Textbook", null },
                    { new Guid("00000000-0000-0000-0000-000000000076"), "Lonely Planet", 6, "Comprehensive city travel guide", null, "Travel Guide", null },
                    { new Guid("00000000-0000-0000-0000-000000000077"), "Random House", 6, "Life story of famous historical figure", null, "Biography", null },
                    { new Guid("00000000-0000-0000-0000-000000000078"), "Taschen", 6, "Collection of famous paintings", null, "Art Book", null },
                    { new Guid("00000000-0000-0000-0000-000000000079"), "Faber & Faber", 6, "Contemporary poetry anthology", null, "Poetry Collection", null },
                    { new Guid("00000000-0000-0000-0000-000000000080"), "Harvard Business Review", 6, "Guide to entrepreneurship", null, "Business Book", null },
                    { new Guid("00000000-0000-0000-0000-000000000081"), "LEGO", 7, "100-piece interlocking building set", null, "Building Blocks", null },
                    { new Guid("00000000-0000-0000-0000-000000000082"), "Barbie", 7, "Interactive doll with accessories", null, "Doll", null },
                    { new Guid("00000000-0000-0000-0000-000000000083"), "Traxxas", 7, "1:18 scale RC car with 2.4GHz remote", null, "Remote Control Car", null },
                    { new Guid("00000000-0000-0000-0000-000000000084"), "Hasbro", 7, "Family strategy board game", null, "Board Game", null },
                    { new Guid("00000000-0000-0000-0000-000000000085"), "Ravensburger", 7, "1000-piece jigsaw puzzle", null, "Puzzle", null },
                    { new Guid("00000000-0000-0000-0000-000000000086"), "Steiff", 7, "Plush teddy bear", null, "Stuffed Animal", null },
                    { new Guid("00000000-0000-0000-0000-000000000087"), "Thames & Kosmos", 7, "STEM learning kit for kids", null, "Science Kit", null },
                    { new Guid("00000000-0000-0000-0000-000000000088"), "Hot Toys", 7, "Collectible movie character figure", null, "Action Figure", null },
                    { new Guid("00000000-0000-0000-0000-000000000089"), "KidKraft", 7, "Miniature kitchen with accessories", null, "Play Kitchen", null },
                    { new Guid("00000000-0000-0000-0000-000000000090"), "Crayola", 7, "Deluxe art supplies for children", null, "Art Set", null },
                    { new Guid("00000000-0000-0000-0000-000000000091"), "Lavazza", 8, "Premium arabica whole bean coffee", null, "Organic Coffee", null },
                    { new Guid("00000000-0000-0000-0000-000000000092"), "Lindt", 8, "70% cocoa dark chocolate bar", null, "Dark Chocolate", null },
                    { new Guid("00000000-0000-0000-0000-000000000093"), "Colavita", 8, "Extra virgin cold-pressed olive oil", null, "Olive Oil", null },
                    { new Guid("00000000-0000-0000-0000-000000000094"), "Kind", 8, "Gluten-free almond honey granola", null, "Granola", null },
                    { new Guid("00000000-0000-0000-0000-000000000095"), "Twinings", 8, "Assorted premium tea collection", null, "Tea Selection", null },
                    { new Guid("00000000-0000-0000-0000-000000000096"), "iOttie", 9, "Dashboard smartphone holder", null, "Car Phone Mount", null },
                    { new Guid("00000000-0000-0000-0000-000000000097"), "NOCO", 9, "Portable car battery jump starter", null, "Jump Starter", null },
                    { new Guid("00000000-0000-0000-0000-000000000098"), "Black+Decker", 9, "Handheld cordless car vacuum cleaner", null, "Car Vacuum", null },
                    { new Guid("00000000-0000-0000-0000-000000000099"), "Accutire", 9, "Digital tire pressure monitor", null, "Tire Pressure Gauge", null },
                    { new Guid("00000000-0000-0000-0000-000000000100"), "Meguiar's", 9, "Premium liquid car wax", null, "Car Wax", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
