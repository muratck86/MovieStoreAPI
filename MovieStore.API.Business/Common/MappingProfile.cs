using AutoMapper;
using MovieStore.API.Business.Operations.CustomerOperations.Commands.CreateCustomer;
using MovieStore.API.Business.Operations.CustomerOperations.Commands.UpdateCustomer;
using MovieStore.API.Business.Operations.CustomerOperations.Queries.GetCustomerDetail;
using MovieStore.API.Business.Operations.CustomerOperations.Queries.GetCustomers;
using MovieStore.API.Business.Operations.MovieOperations.Commands.CreateMovie;
using MovieStore.API.Business.Operations.MovieOperations.Queries.GetMovies;
using MovieStore.API.Business.Operations.PersonOperations.Commands.CreatePerson;
using MovieStore.API.Business.Operations.PersonOperations.Commands.UpdatePerson;
using MovieStore.API.Business.Operations.PersonOperations.Queries.GetPeople;
using MovieStore.API.Domain.Entities;

namespace MovieStore.API.Business.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Customer
            CreateMap<CreateCustomerModel, Customer>();
            CreateMap<UpdateCustomerModel, Customer>();
            CreateMap<Customer, CustomerDetailModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.LastName}"))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Date.ToString("yyyy-MM-dd")));
            CreateMap<Customer, CustomersModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.LastName))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Date.ToString("yyyy-MM-dd")));

            
            //Person
            CreateMap<CreatePersonModel, Person>();
            CreateMap<UpdatePersonModel, Person>();
            CreateMap<Person, PeopleByRoleModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.LastName}"))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Date.ToString("yyyy-MM-dd")));
            CreateMap<Person, PersonModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Name} {src.LastName}"))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Date.ToString("yyyy-MM-dd")));
            
            //Movie
            CreateMap<CreateMovieModel, Movie>();
            CreateMap<Movie, MoviesModel>();
            //Genre
            CreateMap<GenreModel, Genre>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.GenreId));
            
            //MovieRoles
            CreateMap<MovieRole, string>();
                //.ForMember(dest => dest, opt => opt.MapFrom(src => src.Name));

            CreateMap<Genre, string>().ConvertUsing(g => g.Name);
            //var allRoles = from r in roles select r.Name //same with linq
        }
    }
}