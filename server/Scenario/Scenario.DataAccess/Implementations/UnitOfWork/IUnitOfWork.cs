using Scenario.Core.Repositories;

namespace Scenario.DataAccess.Implementations.UnitOfWork
{
    public interface IUnitOfWork
    {
        //public IMovieRepository MovieRepository { get; }
        public IAboutTestimonialRepository AboutTestimonialRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public ISettingsRepository SettingsRepository { get; }
        public IUserScenarioFavoriteRepository UserScenarioFavoriteRepository { get; }
        public ILikeDislikeRepository LikeDislikeRepository { get; }
        public void Commit();
    }
}
