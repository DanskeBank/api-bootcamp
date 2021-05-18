using System;

namespace Api.Accounts.Dto
{
    public sealed record AccountDto(Guid Id, decimal Balance);

    public sealed record OpenAccountDto(decimal Balance);

    public sealed record DepositToAccountDto(decimal Amount);

    public sealed record WithdrawFromAccountDto(decimal Amount);
}
