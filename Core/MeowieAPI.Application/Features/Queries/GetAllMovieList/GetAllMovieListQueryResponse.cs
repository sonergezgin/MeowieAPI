using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeowieAPI.Application.DTO;
using MeowieAPI.Application.DTO.MovieListDTOs;

namespace MeowieAPI.Application.Features.Queries.GetAllMovieList
{
    public class GetAllMovieListQueryResponse
    {
        public List<ListMovieListDTO>? MovieLists { get; set; }
        public int TotalMovieListCount { get; set; } 
    }
}
