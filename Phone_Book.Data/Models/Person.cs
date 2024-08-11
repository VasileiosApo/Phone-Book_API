
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phone_Book.Data.Models;

[Table("Person")]
public class Person
{
    public int Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    public string? Type { get; set; }
    [Required]
    public string? PhoneNumber { get; set; }
}
