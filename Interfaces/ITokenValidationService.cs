using System;
namespace Chat_Service
{
	public interface ITokenValidationService
	{
        Task<bool> ValidateTokenAsync(string token);
    }
}

