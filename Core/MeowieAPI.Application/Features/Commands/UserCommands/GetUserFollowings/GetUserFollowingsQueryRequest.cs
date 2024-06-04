using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MeowieAPI.Application.Features.Commands.UserCommands.GetUserFollowings
{
    public class GetUserFollowingsQueryRequest : IRequest<GetUserFollowingsQueryResponse>
    {
        public string Username { get; set; }
    }
}
