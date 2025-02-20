using Scenario.Core.Repositories;
using Scenario.DataAccess.Data;

namespace Scenario.DataAccess.Implementations.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ScenarioAppDbContext _context;
        public IAboutTestimonialRepository AboutTestimonialRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }
        public ISettingsRepository SettingsRepository { get; private set; }
        public IUserScenarioFavoriteRepository UserScenarioFavoriteRepository { get; private set; }
        public ILikeDislikeRepository LikeDislikeRepository { get; private set; }


        public UnitOfWork(ScenarioAppDbContext context, IAboutTestimonialRepository aboutTestimonialRepository, ICommentRepository commentRepository, ISettingsRepository settingsRepository, IUserScenarioFavoriteRepository userScenarioFavoriteRepository, ILikeDislikeRepository likeDislikeRepository)
        {
            _context = context;
            AboutTestimonialRepository = aboutTestimonialRepository;
            CommentRepository = commentRepository;
            SettingsRepository = settingsRepository;
            UserScenarioFavoriteRepository = userScenarioFavoriteRepository;
            LikeDislikeRepository = likeDislikeRepository;
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
