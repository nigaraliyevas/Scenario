using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ScenarioAppDbContext _context;

        public IPlotRepository PlotRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }

        public UnitOfWork(ScenarioAppDbContext context, IPlotRepository plotRepository, ICommentRepository commentRepository)
        {
            _context = context;
            PlotRepository = plotRepository;
            CommentRepository = commentRepository;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
