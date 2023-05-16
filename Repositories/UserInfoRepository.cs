using Core.Abstractions.Repository;
using Core.Models;
using Repository;
using Serilog;

namespace Repositories
{
    public class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfoRepository(ILogger logger, IGamblingContext context) : base(logger, context)
        {
        }
    }
}
