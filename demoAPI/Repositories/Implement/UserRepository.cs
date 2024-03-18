using AutoMapper;
using demoAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace demoAPI.Repositories.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly BookStoreContext _DBcontext;
        private readonly IMapper _mapper;


        public UserRepository(BookStoreContext DBcontext, IMapper mapper)
        {
            _DBcontext = DBcontext;
            _mapper = mapper;

        }

    }
}
