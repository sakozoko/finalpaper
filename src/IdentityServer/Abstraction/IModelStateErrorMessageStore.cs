namespace IdentityServer.Abstraction;

public interface IModelStateErrorMessageStore
{
    string GetErrorMessage(string key);
}