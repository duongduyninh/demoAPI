using AutoMapper;
using demoAPI.Data;
using demoAPI.Models;

namespace demoAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Book,BookModel>().ReverseMap();
        }
    }
}
