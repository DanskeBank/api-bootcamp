using System;
using static Api.Accounts.Domain.AccountStatus;

namespace Api.Accounts.Domain
{
    public sealed class Account
    {
        public Guid Id { get; }
        public decimal Balance { get; private set; }
        public AccountStatus Status { get; private set; }

        public Account(Guid id, decimal balance, AccountStatus status)
        {
            Id = id;
            Balance = balance;
            Status = status;
        }

        public static Account Open(Guid id, decimal balance)
        {
            if (balance < 0)
                throw new ArgumentException("Balance can't be negative.");

            return new Account(id, balance, Active);
        }

        public void Deposit(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount can't be negative.");

            if (Status is Closed)
                throw new InvalidOperationException("Account is closed.");

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentException("Amount can't be negative.");

            if (Status is Closed)
                throw new InvalidOperationException("Account is closed.");

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient balance.");

            Balance -= amount;
        }

        public void Close()
        {
            if (Status is Closed)
                return;

            Status = Closed;
        }
    }
}
