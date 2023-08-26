using FiapStore.Entity;

namespace FiapStore.Services
{
    public interface ITokenService
    {
        string GetToken(Usuario usuario);
    }
}
