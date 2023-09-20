using login_api.data;
using login_api.data.Entities;
using login_api.DTO;
using Microsoft.Extensions.Options;

namespace login_api.logic;

public class Logic : ILogic
{
    private IDbManager _data;

    public Configuration _config;
    public Logic(IDbManager data, IOptions<Configuration> options)
    {
        _data = data;
        _config = options.Value;
    }
    private User GetUser(LoginDTO dto)
    {
        User user = new User();
            user.Username = dto.Username;
            user.Password = dto.Password;

        return _data.GetUser(user);
    }

    public User ConfirmUserCred(LoginDTO dto)
    {
        var user = GetUser(dto);

        if (string.IsNullOrEmpty(user.Username) | string.IsNullOrEmpty(user.Password))
        {
            throw new Exception("Invalid Credentials");
        }

        return user;    
    }

}