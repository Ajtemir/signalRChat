using FSHomeWork.Models.Dto;
using FSHomeWork.Models.ViewModels;

namespace FSHomeWork.Services.TokenService
{
    public interface ITokenService
    {
        TokenDto GetToken(UserAuthViewModel userAuthViewModel);
    }
}