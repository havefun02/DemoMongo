using System.ComponentModel.DataAnnotations;

namespace App
{
    public class UserDto
    {
        public string name {get; set;}
        [EmailAddress]
        public string email {get; set;} 
    }
}
