using System;
using Api.Accounts.Domain;
using Api.Accounts.Dto;
using Microsoft.AspNetCore.Mvc;
using static Api.Accounts.Domain.AccountStatus;

namespace Api.Accounts.Controllers
{
    [ApiController]
    [Route("api/v1/accounts")]
    public sealed class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repository;

        public AccountsController(IAccountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(Guid id)
        {
            var account = _repository.GetById(id);
            if (account is null || account.Status is Closed)
                return NotFound();

            var accountDto = new AccountDto(account.Id, account.Balance);
            return Ok(accountDto);
        }

        [HttpPost]
        public IActionResult Open(OpenAccountDto requestDto)
        {
            var account = Account.Open(Guid.NewGuid(), requestDto.Balance);
            _repository.Add(account);

            return CreatedAtRoute("GetById", new { account.Id }, account.Id);
        }

        [HttpPost("{id}/deposit")]
        public IActionResult Deposit(Guid id, DepositToAccountDto requestDto)
        {
            var account = _repository.GetById(id);
            if (account is null)
                return NotFound();

            account.Deposit(requestDto.Amount);
            _repository.Update(account);

            return Ok();
        }

        [HttpPost("{id}/withdraw")]
        public IActionResult Withdraw(Guid id, WithdrawFromAccountDto requestDto)
        {
            var account = _repository.GetById(id);
            if (account is null)
                return NotFound();

            account.Withdraw(requestDto.Amount);
            _repository.Update(account);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Close(Guid id)
        {
            var account = _repository.GetById(id);
            if (account is null)
                return NotFound();

            account.Close();
            _repository.Update(account);

            return Ok();
        }
    }
}
