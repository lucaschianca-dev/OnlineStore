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
             .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // Apenas mapeia campos que não são nulos

        CreateMap<UserRecord, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // O ID é gerado pelo Firebase
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        CreateMap<User, UserRecordArgs>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Uid, opt => opt.Ignore()) // O Uid será definido manualmente no serviço
            .ForMember(dest => dest.Password, opt => opt.Ignore()) // O Password não será atualizado aqui
            .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore()); // Ignora outros campos não relevantes

        // Mapeamento de PendingUser para User
        CreateMap<PendingUser, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // O ID será definido mais tarde
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate)); // Mapear a data de criação
    }
}