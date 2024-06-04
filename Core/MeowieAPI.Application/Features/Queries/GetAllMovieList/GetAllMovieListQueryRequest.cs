using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MeowieAPI.Application.Features.Queries.GetAllMovieList
{
    public class GetAllMovieListQueryRequest : IRequest<GetAllMovieListQueryResponse>
    {
        public string? LoggedUsername { get; set; }
        public int Page { get; set; } = 0;
        public int Count { get; set; } = 5;
        public string? SearchKeyword { get; set; }
    }
}
