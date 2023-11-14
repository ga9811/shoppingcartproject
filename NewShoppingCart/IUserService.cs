namespace NewShoppingCart
{
    public interface IUserService
    {
        Task<(bool isValid, string role)> ValidateCredentialsAsync(string email, string password);
    }

}