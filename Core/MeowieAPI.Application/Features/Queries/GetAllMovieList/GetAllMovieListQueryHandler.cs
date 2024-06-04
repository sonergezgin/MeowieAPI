using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MeowieAPI.Application.Abstractions.Services;
using MeowieAPI.Application.DTO.MovieListDTOs;
using MeowieAPI.Application.Repositories.CommentRepository;
using MeowieAPI.Application.Repositories.MovieListRepository;
using MeowieAPI.Application.Repositories.MovieRepository;
using MeowieAPI.Domain.Entities;

namespace MeowieAPI.Application.Features.Queries.GetAllMovieList
{
    public class GetAllMovieListQueryHandler : IRequestHandler<GetAllMovieListQueryRequest, GetAllMovieListQueryResponse>
    {
        readonly IMovieListReadRepository _movieListReadRepository;
        readonly IUserService _userService;

        public GetAllMovieListQueryHandler(IMovieListReadRepository movieListReadRepository, IUserService userService)
        {
            _movieListReadRepository = movieListReadRepository;
            _userService = userService;
        }

        public async Task<GetAllMovieListQueryResponse> Handle(GetAllMovieListQueryRequest request, CancellationToken cancellationToken)
        {
            int totalMovieListCount = _movieListReadRepository.GetAll().Count();
            User? user = null;
            List<ListMovieListDTO>? movieLists = null;

            if(request.LoggedUsername != null)
            {
                user = await _userService.GetUserByUsername(request.LoggedUsername);
            }

            IQueryable<MovieList> allMovieListQueryable = _movieListReadRepository.GetAll();

            if(request.SearchKeyword != null)
            {
                allMovieListQueryable = allMovieListQueryable.Where(ml => ml.Title.ToLower().Contains(request.SearchKeyword.ToLower()));
            }

            if(user == null)
            {
                movieLists = allMovieListQueryable.Skip(request.Page * request.Count).Take(request.Count).Select(ml => new ListMovieListDTO()
                {
                    Id = ml.Id,
                    Title = ml.Title,
                    LikesCount = ml.Likes.Count,
                    MovieCount = ml.Movies.Count,
                    Username = ml.User.UserName,
                    ProfileImage = ml.User.ProfileImage,
                    IsLiked = false
                }).ToList();
            }
            else
            {
                var allMovieList = allMovieListQueryable.Skip(request.Page * request.Count).Take(request.Count);

                movieLists = allMovieList.Select(ml => 
                new ListMovieListDTO()
                {
                    Id = ml.Id,
                    Title= ml.Title,
                    LikesCount= ml.Likes.Count(),
                    MovieCount= ml.Movies.Count(),
                    Username = ml.User.UserName,
                    ProfileImage= ml.User.ProfileImage,
                    IsLiked = ml.Likes.Any(likes => likes.UserId == user.Id)
                }).ToList();
            }

            return new() { MovieLists = movieLists, TotalMovieListCount = totalMovieListCount };


        }
    }
}
