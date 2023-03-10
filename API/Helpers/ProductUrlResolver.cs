using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    //DTO to entity mapping resolver
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, String>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config=config;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl)){
                return _config["ApiUrl"]+source.PictureUrl;
            }else{
                return null;
            }
        }
    }
}