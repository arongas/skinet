using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;

namespace Infrastracture.Data
{
    public class StoreContentSeed
    {
        public static async Task SeedAsync(StoreContext context){
            if (!context.ProductBrands.Any()){
                var brandsData=File.ReadAllText("../Infrastracture/Data/SeedData/brands.json");
                var brands=JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);
            }

            if (!context.ProductTypes.Any()){
                var typesData=File.ReadAllText("../Infrastracture/Data/SeedData/types.json");
                var types=JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }

            if (!context.Products.Any()){
                var productsData=File.ReadAllText("../Infrastracture/Data/SeedData/products.json");
                var products=JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }
            
            if (context.ChangeTracker.HasChanges()) {
                await context.SaveChangesAsync();
            }
        }
    }
}