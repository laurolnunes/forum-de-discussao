using Chatter.Domain.Log;
using Chatter.Infra.Data.Context;
using Chatter.Infra.Data.Repository.Base;

namespace Chatter.Infra.Data.Repository
{
    public class LogRepository : Repository<Log>, ILogRepository

    {
        public LogRepository(ChatterContext db) : base(db)
        {
        }
    }
}