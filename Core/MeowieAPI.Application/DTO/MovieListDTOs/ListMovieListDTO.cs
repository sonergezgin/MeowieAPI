using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeowieAPI.Application.DTO.MovieListDTOs
{
    public class ListMovieListDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public int LikesCount { get; set; }
        public int MovieCount { get; set; }
        public bool IsLiked { get; set; } = false;
        public string? Username { get; set; }
        public string? ProfileImage { get; set; }

    }
}
