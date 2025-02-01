namespace workingProject.EfDbContext.DBUnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        void Rollback();
    }
}
