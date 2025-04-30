using Scenario.Core.Repositories;
namespace Scenario.DataAccess.Implementations.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IPlotRepository PlotRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IScriptwriterRepository ScriptwriterRepository { get; }
        public IPlotRatingRepository PlotRatingRepository { get; }
        public IChapterRepository ChapterRepository { get; }
        public IPlotAppUserRepository PlotAppUserRepository { get; }
        public IPlotCategoryRepository PlotCategoryRepository { get; }
        public IContactUsRepository ContactUsRepository { get; }
        public IAdRepository AdRepository { get; }

        public IAboutTestimonialRepository AboutTestimonialRepository { get; }
        public ISettingsRepository SettingsRepository { get; }
        public IUserScenarioFavoriteRepository UserScenarioFavoriteRepository { get; }
        public ILikeDislikeRepository LikeDislikeRepository { get; }

        public void Commit();

    }
}
