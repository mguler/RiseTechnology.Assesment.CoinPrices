using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.Abstract.UserManagement
{
    /// <summary>
    /// Abstraction for user management service
    /// </summary>
    public interface IUserManagementService
    {
        /// <summary>
        /// This method supplies functionality for user registration operation
        /// </summary>
        /// <param name="registerDto">An object that contains information about user</param>
        /// <returns>An object that contains information about the result of the operation</returns>
        ServiceResultDto Register(RegisterDto registerDto);
        /// <summary>
        /// This method supplies functionality for user login operation
        /// </summary>
        /// <param name="loginDto">An object that contains information about user login</param>
        /// <returns>An object that contains information about the result of the operation</returns>
        ServiceResultDto<LoginResultDto> Login(LoginDto loginDto);
        /// <summary>
        /// This method supplies functionality for user logout operation
        /// </summary>
        /// <returns>An object that contains information about the result of the operation</returns>
        ServiceResultDto Logout();
    }
}
