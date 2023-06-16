using AutoMapper;
using Data.Entities;
using Data.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MappingConfig
{
	public class ProfileConfig : Profile
	{
		public ProfileConfig()
		{
			CreateMap<Account, AccountViewModel>();
		}
	}
}
