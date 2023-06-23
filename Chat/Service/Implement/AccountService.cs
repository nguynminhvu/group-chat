using AutoMapper;
using Data.Entities;
using Data.ViewModel.Account;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Service.Implement
{
	public class AccountService : IAccountService
	{
		private static string KEY = "askbdAS key size of at least '128' bitsDuhDuhDuhDuhASD";
		private IMapper _mapper;
		private IUnitOfWork _uow;

		public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_mapper = mapper;
			_uow = unitOfWork;
		}
		private string GenerateToken(Account account)
		{
			var key = System.Text.Encoding.UTF8.GetBytes(KEY);
			var tokenDescription = new SecurityTokenDescriptor
			{
				Subject = new System.Security.Claims.ClaimsIdentity(new[] {
					new Claim("id", account.Id)
				}),
				Expires = DateTime.Now.AddDays(5),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var tokenHandle = new JwtSecurityTokenHandler();
			var token = tokenHandle.CreateToken(tokenDescription);
			return tokenHandle.WriteToken(token);
		}
	
		public async Task<Account> CheckToken(string token)
		{
			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = System.Text.Encoding.UTF8.GetBytes(KEY);
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateAudience = false,
					ValidateIssuer = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ClockSkew = TimeSpan.Zero,
					ValidateLifetime = true,
				}, out SecurityToken validated);
				var tokenValidated = (JwtSecurityToken)validated;
				var id = tokenValidated.Claims.FirstOrDefault(x => x.Type.Equals("id"))!.Value;
				return await GetAccount(id);
			}
			catch (Exception ex)
			{
				return null!;
			}
		}

		public async Task<bool> CheckEmailExist(string email)
		{
			return await _uow.AccountRepository.FirstOrDefaultAsync(x => x.Email.Equals(email)) == null ? true : false;
		}

		public async Task<string> Login(AccountLoginModel alm)
		{
			var account = await _uow.AccountRepository.FirstOrDefaultAsync(x => x.Email.Equals(alm.Email) && x.Password.Equals(alm.Password) && x.IsActive);
			if (account != null)
			{
				return GenerateToken(account);
			}
			return null!;
		}

		public async Task<AccountViewModel> RegisterAccount(AccountCreateModel acm)
		{
			var account = await _uow.AccountRepository.FirstOrDefaultAsync(x => x.Email.Equals(acm.AccId!));
			if (account == null)
			{
				Account acc = new Account
				{
					Id = Guid.NewGuid().ToString(),
					Email = acm.Email!,
					Password = acm.Password!,
					IsActive = true
				};
				await _uow.AccountRepository.AddAsync(acc);
				return await _uow.SaveChanges() > 0 ? _mapper.Map<AccountViewModel>(acc) : null!;
			}
			return null!;
		}

		public async Task<Account> GetAccount(string id)
		{
			return await _uow.AccountRepository.FirstOrDefaultAsync(x => x.Id.Equals(id));
		}
	}
}
