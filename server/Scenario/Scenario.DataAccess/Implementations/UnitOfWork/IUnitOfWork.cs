namespace Scenario.DataAccess.Implementations.UnitOfWork
{
    public interface IUnitOfWork
    {
        //public IMovieRepository MovieRepository { get; }


        public void Commit();
    }
}
