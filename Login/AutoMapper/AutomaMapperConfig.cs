using AutoMapper;
using Login.Models;
using Microsoft.Identity.Client;

namespace Login.AutoMapper
{
    public class AutomaMapperConfig: Profile
    {
      

  

        public AutomaMapperConfig()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
