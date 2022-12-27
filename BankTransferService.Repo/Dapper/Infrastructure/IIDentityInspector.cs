namespace BankTransferService.Repo.Dapper.Infrastructure
{
    public interface IIDentityInspector<TEntity> where TEntity : class
    {
        string GetColumnsIdentityForType();
    }
}
