using System;
using ProjectWeb.ViewModels;
using ProjectWeb.DAL.Models;

namespace ProjectWeb.ViewMapper
{
	public class AuthMapper
	{
		public static UserModel MapRegisterViewModelToUserModel(RegisterViewModel model)
		{
            return new UserModel()
            {
                Email = model.Email!,
                Password = model.Password!
            };
        }
    }
}

