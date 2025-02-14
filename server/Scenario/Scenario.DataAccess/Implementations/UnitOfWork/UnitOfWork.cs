using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ScenarioAppDbContext _context;
        public IPlotRepository PlotRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IScriptwriterRepository ScriptwriterRepository { get; private set; }
        public IPlotRatingRepository PlotRatingRepository { get; private set; }

        public UnitOfWork(ScenarioAppDbContext context, IPlotRepository plotRepository, ICommentRepository commentRepository, ICategoryRepository categoryRepository, IScriptwriterRepository scriptwriterRepository, IPlotRatingRepository plotRatingRepository)
        {
            _context = context;
            PlotRepository = plotRepository;
            CommentRepository = commentRepository;
            CategoryRepository = categoryRepository;
            ScriptwriterRepository = scriptwriterRepository;
            PlotRatingRepository = plotRatingRepository;
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
