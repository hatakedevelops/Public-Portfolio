using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using LoginService.DTO;
using LoginService.Data;
using LoginService.Data.Entities;

namespace LoginService.Business;

public class Logic : ILogic
{
    private IDbManager _data;

    public Configuration.Configuration _config;

    public Logic(IDbManager data, IOptions<Configuration.Configuration> opitons)
    {
        _data = data;
        _config = opitons.Value;
    }

    public User GetUser(LoginDTO dto)
    {
        string saltUsed = _data.GetSaltUsed(dto.Username);

        if (!saltUsed.Any())
        {
            return new User();
        }

        var passHash = GeneratedSaltedHash(Encoding.ASCII.GetBytes(dto.Password),
        Encoding.ASCII.GetBytes(saltUsed));

        UserCred cred = new UserCred();
        cred.UserName = dto.Username;
        cred.PassHash = Encoding.ASCII.GetString(passHash);

        return _data.GetUser(cred);
    }

    private static byte[] GeneratedSaltedHash(byte[] plainText, byte[] salt)
    {
        HashAlgorithm hash = SHA256.Create();

        byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

        for(int i = 0; i < plainText.Length; i++)
        {
            plainTextWithSaltBytes[i] = plainText [i];
        }

        for(int i = 0; i < salt.Length; i++)
        {
            plainTextWithSaltBytes[plainText.Length + i] = salt[i];
        }

        return hash.ComputeHash(plainTextWithSaltBytes);
    }

    public JwtSecurityToken GetJwtSecurityToken(LoginDTO dto)
    {
        if (!dto.JwtAudience.Equals(_config.Jwt.Audience) ||
            !dto.JwtIssuer.Equals(_config.Jwt.Issuer) ||
            !dto.JwtSubject.Equals(_config.Jwt.Subject))
        {
            throw new Exception("Invalid Credentials");
        }

        var user = GetUser(dto);

        if (string.IsNullOrEmpty(user.FName))
        {
            throw new Exception("Invalid Credentials");
        }

        var claims = new[] 
        {
            new Claim(JwtRegisteredClaimNames.Sub, _config.Jwt.Subject),    
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("UserId", user.UserId.ToString()),
            new Claim("First Name", user.FName),
            new Claim("Last Name", user.LName),
            new Claim("Email", user.Email),
            new Claim("Phone", user.PhoneNum.ToString()
        )};

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Jwt.Key));
        var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _config.Jwt.Issuer,
            _config.Jwt.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: signin
        );

        return token;
    }
}