using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ScenarioAppDbContext _context;
        //public IMovieRepository MovieRepository { get; private set; }


        public UnitOfWork(ScenarioAppDbContext context /*,ICommentRepository commentRepository, ActorRepository actorRepository*/ )
        {
            _context = context;
            //CommentRepository = commentRepository;
            //ActorRepository = actorRepository;

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
