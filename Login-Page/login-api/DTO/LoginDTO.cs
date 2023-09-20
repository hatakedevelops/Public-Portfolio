using System.ComponentModel.DataAnnotations;

namespace login_api.DTO;

public class LoginDTO 
{
    [Required]
    [RegularExpression("^[a-zA-Z0-9]*$",ErrorMessage ="Only Alphanumeric allowed")]
    public string Username {get; set;}
    [Required]
    [DataType(DataType.Password)]
    public string Password {get;set;}
}