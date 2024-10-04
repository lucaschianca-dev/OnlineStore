using AutoMapper;
using FirebaseAdmin.Auth;
using OnlineStore.DTOs.User;
using OnlineStore.DTOs.User.RegisterUserInput;
using OnlineStore.DTOs.User.UpdateUser;
using OnlineStore.Models;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Mapeamento de UpdateUserInput para User
        CreateMap<UpdateUserInput, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // O ID não será alterado durante a atualização

        CreateMap<UserRecord, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // O ID é gerado pelo Firebase
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        // Mapeamento de PendingUser para User
        CreateMap<PendingUser, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // O ID será definido mais tarde
    }
}