using AutoMapper;
using PizzaDinner.Backend.WebApi.Models;
using PizzaDinner.Models;

namespace PizzaDinner.Backend.WebApi.Mappings
{
    public class PizzaProfile : Profile
    {
        public PizzaProfile()
        {
            CreateMap<CreatePizzaModel, Pizza>()  // Mapeo CreatePizzaDto > Pizza (y viceversa con '.ReverseMap()')
                .ReverseMap();

            CreateMap<Pizza, PizzaResponseModel>();   // Mapeo Pizza > PizzaResponseDto

            CreateMap<UpdatePizzaModel, Pizza>();     // Mapeo UpdatePizzaDto > Pizza
        }
    }
}
