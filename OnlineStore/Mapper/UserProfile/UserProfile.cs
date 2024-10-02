using AutoMapper;
using OnlineStore.DTOs.User;
using OnlineStore.DTOs.User.UpdateUser;
using OnlineStore.Models;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Mapeamento de UpdateUserInput para User
        CreateMap<UpdateUserInput, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // O ID não será alterado durante a atualização
    }
}