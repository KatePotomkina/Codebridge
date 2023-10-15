using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Codebridge.Models;

public class Dog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public int Id { get; set; }

    [StringLength(15, ErrorMessage = "Name cannot be longer than 15 characters.")]

    public string Name { get; set; }

    [StringLength(20, ErrorMessage = "Colour cannot be longer than 20 characters.")]

    public string Colour { get; set; }

    public int Tail_Lenght { get; set; }
    public int Weight { get; set; }
}