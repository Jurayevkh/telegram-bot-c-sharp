using Bot.Entity;

namespace Bot.Services;

public class UserService
{
    public async Task<User> GetUserAsync(long accountId){
        return new User() { LanguageCode="en-Us"};
    }
}