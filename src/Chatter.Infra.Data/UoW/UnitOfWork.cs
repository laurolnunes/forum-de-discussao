using Chatter.Domain.Core.Commands;
using Chatter.Domain.Interfaces;
using Chatter.Infra.Data.Context;

namespace Chatter.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatterContext _context;

        public UnitOfWork(ChatterContext context)
        {
            _context = context;
        }

        public CommandResponse Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return new CommandResponse(rowsAffected > 0);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}