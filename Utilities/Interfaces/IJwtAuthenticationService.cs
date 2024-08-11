namespace Utilities.Interfaces
{
    public interface IJwtAuthenticationService
    {
        string Authenticate(string user, string password);

        string EncryptMD5(string password);
    }
}
