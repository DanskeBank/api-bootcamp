using System;

namespace Api.Accounts.Domain
{
    public interface IAccountRepository
    {
        Account? GetById(Guid id);
        void Add(Account account);
        void Update(Account account);
    }
}