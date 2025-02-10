using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ScenarioAppDbContext _context;
        //public IMovieRepository MovieRepository { get; private set; }
        public IAboutTestimonialRepository AboutTestimonialRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }
        public ISettingsRepository SettingsRepository { get; private set; }
        public IUserScenarioFavoriteRepository UserScenarioFavoriteRepository { get; private set; }

        public UnitOfWork(ScenarioAppDbContext context, IAboutTestimonialRepository aboutTestimonialRepository, ICommentRepository commentRepository, ISettingsRepository settingsRepository, IUserScenarioFavoriteRepository userScenarioFavoriteRepository /*,ICommentRepository commentRepository, ActorRepository actorRepository*/ )
        {
            _context = context;
            AboutTestimonialRepository = aboutTestimonialRepository;
            CommentRepository = commentRepository;
            SettingsRepository = settingsRepository;
            UserScenarioFavoriteRepository = userScenarioFavoriteRepository;
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
