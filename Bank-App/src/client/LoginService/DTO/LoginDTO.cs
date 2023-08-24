using System.ComponentModel.DataAnnotations;

namespace LoginService.DTO;

public class LoginDTO 
{
    [Required]
    public string Username {get; set;}
    [Required]
    public string Password {get; set;}
    [Required]
    public string JwtIssuer {get; set;}
    [Required]
    public string JwtAudience {get; set;}
    [Required]
    public string JwtSubject {get; set;}   
}