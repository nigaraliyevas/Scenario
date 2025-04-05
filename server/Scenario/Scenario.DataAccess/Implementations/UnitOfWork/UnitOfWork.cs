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
        public IChapterRepository ChapterRepository { get; private set; }
        public IPlotAppUserRepository PlotAppUserRepository { get; private set; }
        public IPlotCategoryRepository PlotCategoryRepository { get; private set; }
        public IContactUsRepository ContactUsRepository { get; private set; }

        public IAboutTestimonialRepository AboutTestimonialRepository { get; private set; }
        public ISettingsRepository SettingsRepository { get; private set; }
        public IUserScenarioFavoriteRepository UserScenarioFavoriteRepository { get; private set; }
        public ILikeDislikeRepository LikeDislikeRepository { get; private set; }

        public UnitOfWork(ScenarioAppDbContext context, IPlotRepository plotRepository, ICommentRepository commentRepository, ICategoryRepository categoryRepository, IScriptwriterRepository scriptwriterRepository, IPlotRatingRepository plotRatingRepository, IChapterRepository chapterRepository, IPlotAppUserRepository plotAppUserRepository = null, IPlotCategoryRepository plotCategoryRepository = null, IContactUsRepository contactUsRepository = null, IAboutTestimonialRepository aboutTestimonialRepository = null, ISettingsRepository settingsRepository = null, IUserScenarioFavoriteRepository userScenarioFavoriteRepository = null, ILikeDislikeRepository likeDislikeRepository = null)
        {
            _context = context;
            PlotRepository = plotRepository;
            CommentRepository = commentRepository;
            CategoryRepository = categoryRepository;
            ScriptwriterRepository = scriptwriterRepository;
            PlotRatingRepository = plotRatingRepository;
            ChapterRepository = chapterRepository;
            PlotAppUserRepository = plotAppUserRepository;
            PlotCategoryRepository = plotCategoryRepository;
            ContactUsRepository = contactUsRepository;

            AboutTestimonialRepository = aboutTestimonialRepository;
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
