﻿using AutoMapper;

namespace Scenario.Application.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            ////Comment
            //CreateMap<CommentCreateDto, Comment>()
            //    .ForMember(dest => dest.CommentText, opt => opt.MapFrom(src => src.Text));
            //CreateMap<CommentUpdateDto, Comment>()
            //    .ForMember(dest => dest.CommentText, opt => opt.MapFrom(src => src.Text));
            //CreateMap<Comment, CommentDto>()
            //    .ForMember(dest => dest.CommentText, opt => opt.MapFrom(src => src.CommentText))
            //    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            //    .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUser.UserName))
            //    .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUser.Id))
            //    .ForMember(dest => dest.UserImg, opt => opt.MapFrom(src => src.AppUser.UserImg));
            ////Actor
            //CreateMap<ActorCreateDto, Actor>();
            //CreateMap<ActorUpdateDto, Actor>();

            ////Genre
            //CreateMap<GenreCreateDto, Genre>();
            //CreateMap<GenreUpdateDto, Genre>();

            ////Country
            //CreateMap<CountryCreateDto, Country>();
            //CreateMap<CountryUpdateDto, Country>();

            ////Tag
            //CreateMap<TagCreateDto, Tag>();
            //CreateMap<TagUpdateDto, Tag>();

            ////OriginalLanguage
            //CreateMap<OriginalLanguageCreateDto, OriginalLanguage>();
            //CreateMap<OriginalLanguageUpdateDto, OriginalLanguage>();


            //CreateMap<Movie, MovieReturnDto>()
            //    .ForMember(dest => dest.ThumbBgImgURL, opt => opt.MapFrom(src => src.ThumbBgImg))
            //    .ForMember(dest => dest.ThumbImgURL, opt => opt.MapFrom(src => src.ThumbImg))
            //    .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.MovieActors.Select(ma => new ActorDto { Id = ma.Actor.Id, Name = ma.Actor.FullName })))
            //   .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.MovieCountries.Select(mc => new CountryDto { Id = mc.Country.Id, Name = mc.Country.Name })))
            //   .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.MovieGenres.Select(mg => new GenreDto { Id = mg.Genre.Id, Name = mg.Genre.Name })))
            //   .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.MovieTags.Select(mt => new TagDto { Id = mt.Tag.Id, Name = mt.Tag.Name })))
            //       .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.MovieTags.Select(mt => new TagDto { Id = mt.Tag.Id, Name = mt.Tag.Name })))
            //       .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Select(c => new CommentDto
            //       {
            //           CommentText = c.CommentText,
            //           AppUser = c.AppUser.UserName,
            //           AppUserId = c.AppUserId,
            //           CreatedAt = c.CreatedAt,
            //           Id = c.Id,
            //           UserImg = c.AppUser.UserImg
            //       })));


            //CreateMap<OriginalLanguageDto, OriginalLanguage>();
            //CreateMap<MovieCreateDto, Movie>()
            //    .ForMember(dest => dest.OriginalLanguage, opt => opt.MapFrom(src => src.OriginalLanguage))
            //    .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.ActorIds.Select(id => new MovieActor { ActorId = id }).ToList()))
            //    .ForMember(dest => dest.MovieTags, opt => opt.MapFrom(src => src.TagIds.Select(id => new MovieTag { TagId = id }).ToList()))
            //    .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => src.GenreIds.Select(id => new MovieGenre { GenreId = id }).ToList()))
            //    .ForMember(dest => dest.MovieCountries, opt => opt.MapFrom(src => src.CountryIds.Select(id => new MovieCountry { CountryId = id }).ToList()))
            //    .AfterMap((src, dest) =>
            //    {
            //        foreach (var movieActor in dest.MovieActors)
            //        {
            //            movieActor.MovieId = dest.Id;
            //        }
            //        foreach (var movieTag in dest.MovieTags)
            //        {
            //            movieTag.MovieId = dest.Id;
            //        }
            //        foreach (var movieGenre in dest.MovieGenres)
            //        {
            //            movieGenre.MovieId = dest.Id;
            //        }
            //        foreach (var movieCountry in dest.MovieCountries)
            //        {
            //            movieCountry.MovieId = dest.Id;
            //        }
            //    })
            //    .ReverseMap();


            //CreateMap<MovieUpdateDto, Movie>()
            // .ForMember(dest => dest.OriginalLanguageId, opt => opt.MapFrom(src => src.OriginalLanguage.Id)) // Set the OriginalLanguageId directly
            // .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.ActorIds.Select(id => new MovieActor { ActorId = id }).ToList()))
            // .ForMember(dest => dest.MovieTags, opt => opt.MapFrom(src => src.TagIds.Select(id => new MovieTag { TagId = id }).ToList()))
            // .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => src.GenreIds.Select(id => new MovieGenre { GenreId = id }).ToList()))
            // .ForMember(dest => dest.MovieCountries, opt => opt.MapFrom(src => src.CountryIds.Select(id => new MovieCountry { CountryId = id }).ToList()))
            // .AfterMap((src, dest) =>
            // {
            //     foreach (var movieActor in dest.MovieActors)
            //     {
            //         movieActor.MovieId = dest.Id;
            //     }
            //     foreach (var movieTag in dest.MovieTags)
            //     {
            //         movieTag.MovieId = dest.Id;
            //     }
            //     foreach (var movieGenre in dest.MovieGenres)
            //     {
            //         movieGenre.MovieId = dest.Id;
            //     }
            //     foreach (var movieCountry in dest.MovieCountries)
            //     {
            //         movieCountry.MovieId = dest.Id;
            //     }
            // })
            // .ReverseMap();

            //CreateMap<Genre, GenreReturnDto>()
            //    .ForMember(dest => dest.CountMovies, opt => opt.MapFrom(src => src.MovieGenres.Count));


            //CreateMap<PlanRoleNameUpdateDto, PlanRoleName>();

            //CreateMap<PlanRoleNameCreateDto, PlanRoleName>();

            //CreateMap<AppUser, UserGetDto>();
            //CreateMap<UserRegisterDto, AppUser>();

            //CreateMap<SubscriptionPlanCreateDto, SubscriptionPlan>();
            //CreateMap<SubscriptionPlanUpdateDto, SubscriptionPlan>();

            //CreateMap<SubscriptionPlan, SubscriptionPlanCreateDto>();
            //CreateMap<SubscriptionPlan, SubscriptionPlanUpdateDto>();

            //CreateMap<MovieSliderCreateDto, MovieSlider>();
            //CreateMap<MovieSliderUpdateDto, MovieSlider>();

            //CreateMap<MovieSlider, MovieSliderReturnDto>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Movie.Name))
            //    .ForMember(dest => dest.ViewCount, opt => opt.MapFrom(src => src.Movie.ViewCount))
            //    .ForMember(dest => dest.IsFree, opt => opt.MapFrom(src => src.Movie.IsFree))
            //    .ForMember(dest => dest.ReleasedDate, opt => opt.MapFrom(src => src.Movie.ReleasedDate))
            //    .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Movie.Duration))
            //    .ForMember(dest => dest.IMDBRate, opt => opt.MapFrom(src => src.Movie.IMDBRate))
            //    .ForMember(dest => dest.MovieURL, opt => opt.MapFrom(src => src.Movie.MovieURL))
            //    .ForMember(dest => dest.MovieTrailerURL, opt => opt.MapFrom(src => src.Movie.MovieTrailerURL))
            //    .ForMember(dest => dest.ThumbBgImgURL, opt => opt.MapFrom(src => src.Movie.ThumbBgImg))
            //    .ForMember(dest => dest.ThumbImgURL, opt => opt.MapFrom(src => src.Movie.ThumbImg))
            //    .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Movie.Summary))
            //    .ForMember(dest => dest.OriginalLanguageName, opt => opt.MapFrom(src => src.Movie.OriginalLanguage.Name))
            //    .ForMember(dest => dest.OriginalLanguageId, opt => opt.MapFrom(src => src.Movie.OriginalLanguage.Id))
            //    .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Movie.MovieActors.Select(ma => new ActorDto { Id = ma.Actor.Id, Name = ma.Actor.FullName })))
            //   .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.Movie.MovieCountries.Select(mc => new CountryDto { Id = mc.Country.Id, Name = mc.Country.Name })))
            //   .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Movie.MovieGenres.Select(mg => new GenreDto { Id = mg.Genre.Id, Name = mg.Genre.Name })))
            //   .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Movie.MovieTags.Select(mt => new TagDto { Id = mt.Tag.Id, Name = mt.Tag.Name })))
            //   .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Movie.Comments.Select(c => c.CommentText)));// Mapping the Movie


        }
    }

}