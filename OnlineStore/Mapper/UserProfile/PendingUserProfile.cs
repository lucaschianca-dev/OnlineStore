using AutoMapper;
using OnlineStore.DTOs.User.RegisterUserInput;
using OnlineStore.Models;

namespace OnlineStore.Mapper.UserProfile;

public class PendingUserProfile : Profile
{
    public PendingUserProfile() 
    {
        // Mapeamento de RegisterUserInput para PendingUser
        CreateMap<RegisterUserInput, PendingUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignorar o campo Id, pois ele será gerado automaticamente
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.UtcNow)); // Definir a data de criação
    }
}
