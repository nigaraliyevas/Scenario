
using Scenario.Core.Repositories;

namespace Scenario.DataAccess.Implementations.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IPlotRepository PlotRepository { get; }
        public ICommentRepository CommentRepository { get; }


        public void Commit();
    }
}
