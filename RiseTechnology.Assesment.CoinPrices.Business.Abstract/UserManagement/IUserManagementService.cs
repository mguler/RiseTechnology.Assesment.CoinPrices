using RiseTechnology.Assesment.CoinPrices.Data.Dto;
using RiseTechnology.Assesment.CoinPrices.Dto.UserManagement;

namespace RiseTechnology.Assesment.CoinPrices.Business.Abstract.UserManagement
{
    public interface IUserManagementService
    {
        ServiceResultDto Register(RegisterDto registerDto);
        ServiceResultDto<LoginResultDto> Login(LoginDto loginDto);
        ServiceResultDto Logout();
    }
}
