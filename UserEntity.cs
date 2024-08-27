using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace App
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]

        public string Email { get; set; }
    }
}
