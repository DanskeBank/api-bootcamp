using System;
using System.Collections.Generic;
using Api.Accounts.Domain;
using static Api.Accounts.Domain.AccountStatus;

namespace Api.Accounts.Infrastructure
{
    // Repository written by a crazy developer.
    // Don't spend time on trying to understand it.
    public sealed class MemoryAccountRepository : IAccountRepository
    {
        // Some "interesting" storage.
        private readonly Dictionary<Guid, decimal> _activeAccountsBalance = new();
        private readonly Dictionary<Guid, decimal> _closedAccountsBalance = new();

        public Account? GetById(Guid id)
        {
            if (_activeAccountsBalance.TryGetValue(id, out var balance))
                return new Account(id, balance, Active);

            if (_closedAccountsBalance.TryGetValue(id, out balance))
                return new Account(id, balance, Closed);

            return null;
        }

        public void Add(Account account)
        {
            AddOrUpdate(account);
        }

        public void Update(Account account)
        {
            AddOrUpdate(account);
        }

        private void AddOrUpdate(Account account)
        {
            switch (account.Status)
            {
                case Active:
                    _closedAccountsBalance.Remove(account.Id);
                    _activeAccountsBalance[account.Id] = account.Balance;
                    break;

                case Closed:
                    _activeAccountsBalance.Remove(account.Id);
                    _closedAccountsBalance[account.Id] = account.Balance;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(account.Status));
            }
        }
    }
}
