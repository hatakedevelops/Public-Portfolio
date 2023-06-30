using System.ComponentModel.DataAnnotations;

namespace OrchestratorService.DTO;

    public class RegisterDTO
    {

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{2,50}$",ErrorMessage ="Letters Only")]
       public string FName {get;set;}=null!; 

       [Required]
       [RegularExpression(@"^[a-zA-Z''-'\s]{2,50}$",ErrorMessage ="Letters Only")]
       public string LName {get;set;}=null!;

       [Required]
       [RegularExpression("^[a-zA-Z0-9' ']*$",ErrorMessage ="Only Alphanumeric and Space allowed")]
       public string UserAddress {get;set;}=null!;

       [Required]
       [RegularExpression(@"^[0-9][0-9]*$",ErrorMessage ="Numbers Only")]
       public string PhoneNum {get;set;}=null!;

       [Required]
       [EmailAddress]
       public string Email {get;set;}=null!;

       [Required]
       [RegularExpression("^[a-zA-Z0-9]*$",ErrorMessage ="Only Alphanumeric allowed")]
       public string? UserName {get;set;}

       [Required]
       [DataType(DataType.Password)]
       public string Password {get;set;}=null!; 
    }