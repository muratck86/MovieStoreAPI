using AutoMapper;
using MovieStore.API.Business.Operations.CustomerOperations.Commands.CreateCustomer;
using MovieStore.API.Business.Operations.CustomerOperations.Commands.UpdateCustomer;
using MovieStore.API.Business.Operations.CustomerOperations.Queries.GetCustomerDetail;
using MovieStore.API.Business.Operations.CustomerOperations.Queries.GetCustomers;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Customer
            CreateMap<CreateCustomerModel, Customer>()
                .ForMember(dest => dest.CustomerGenres, opt => opt.MapFrom(src => src.FavouriteGenreIds));
            CreateMap<UpdateCustomerModel, Customer>();
            CreateMap<Customer, CustomerDetailModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.LastName}"))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.FavouriteGenres, opt => opt.MapFrom(src => src.CustomerGenres));
            CreateMap<Customer, CustomersModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.LastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Date.ToString("yyyy-MM-dd")));

            // CreateMap<Customer, CustomerGenres>()
            //     .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
            //     .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.CustomerGenres));

            
            //Person
            // CreateMap<CreatePersonModel, Person>();
            // CreateMap<UpdatePersonModel, Person>();
            // CreateMap<Person, PeopleByRoleModel>()
            //     .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.LastName}"))
            //     .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Date.ToString("yyyy-MM-dd")));
            // CreateMap<Person, PersonModel>()
            //     .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.LastName}"))
            //     .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Date.ToString("yyyy-MM-dd")));
            
            //Movie
            // CreateMap<CreateMovieModel, Movie>();
            // CreateMap<Movie, MoviesModel>();
            //Genre
            // CreateMap<GenreModel, Genre>()
            //     .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GenreId));
            
            //MovieRoles
            //CreateMap<MovieRole, string>();
                //.ForMember(dest => dest, opt => opt.MapFrom(src => src.Name));

            CreateMap<Genre, string>().ConvertUsing(g => g.Name);
            CreateMap<int, Genre>().ConstructUsing(g => new Genre{Id = g});
            CreateMap<int, CustomerGenre>().ConstructUsing(g => new CustomerGenre{GenreId = g});
            CreateMap<CustomerGenre, string>().ConvertUsing(c => c.Genre.Name);

            //var allRoles = from r in roles select r.Name //same with linq
        }
    }
}