﻿
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


        public void Commit();
    }
}
