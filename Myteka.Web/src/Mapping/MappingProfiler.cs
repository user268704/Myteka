using AutoMapper;
using Myteka.Models.ExternalModels;
using Myteka.Models.ExternalModels.RegisterModels;
using Myteka.Models.InternalModels;

namespace Myteka.Web.Mapping;

public class MappingProfiler : Profile
{
    public MappingProfiler()
    {
        CreateMap<Author, AuthorExternal>();
        CreateMap<Book, BookExternal>();
        CreateMap<User, UserExternal>();
        CreateMap<Audio, AudioExternal>();
        CreateMap<Content, ContentExternal>();

        // reverse mappings
        CreateMap<AuthorExternal, Author>();
        CreateMap<BookExternal, Book>();
        CreateMap<UserExternal, User>();
        CreateMap<AudioExternal, Audio>();
        CreateMap<ContentExternal, Content>();

        // collection mapping
        CreateMap<ICollection<Author>, ICollection<AuthorExternal>>();
        CreateMap<ICollection<Book>, ICollection<BookExternal>>();
        CreateMap<ICollection<User>, ICollection<UserExternal>>();
        CreateMap<ICollection<Audio>, ICollection<AudioExternal>>();
        
        // collection reverse mapping
        CreateMap<ICollection<AuthorExternal>, ICollection<Author>>();
        CreateMap<ICollection<BookExternal>, ICollection<Book>>();
        CreateMap<ICollection<UserExternal>, ICollection<User>>();
        CreateMap<ICollection<AudioExternal>, ICollection<Audio>>();
        
        // Register models mapping
        CreateMap<Book, BookRegisterModel>();
        CreateMap<Author, AuthorRegisterModel>();
        CreateMap<Content, ContentRegisterModel>();

        CreateMap<BookRegisterModel, Book>();
        CreateMap<AuthorRegisterModel, Author>();
        CreateMap<ContentRegisterModel, Content>();
    }
    
    
}