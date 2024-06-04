using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MeowieAPI.Application.Abstractions.Services;
using MeowieAPI.Application.DTO.UserDTOs;

namespace MeowieAPI.Application.Features.Commands.UserCommands.GetUserFollowings
{
    public class GetUserFollowingsQueryHandler : IRequestHandler<GetUserFollowingsQueryRequest, GetUserFollowingsQueryResponse>
    {
        private readonly IUserService _userService;

        public GetUserFollowingsQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetUserFollowingsQueryResponse> Handle(GetUserFollowingsQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByUsername(request.Username);
            if (user == null) return null;

            var followings = user.Follows.Select(f => new UserFollowDTO() { UserName = f.UserName, ProfileImage = f.ProfileImage }).ToList();

            return new GetUserFollowingsQueryResponse() { Followings = followings };
        }
    }
}
