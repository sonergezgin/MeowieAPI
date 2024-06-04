using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeowieAPI.Application.DTO.UserDTOs;

namespace MeowieAPI.Application.Features.Commands.UserCommands.GetUserFollowings
{
    public class GetUserFollowingsQueryResponse
    {
        public List<UserFollowDTO> Followings { get; set; }
    }
}
