using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Carglass.TechnicalAssessment.Backend.Dtos;

public class ClientDto
{

    [Required]
    public int Id { get; set; }
    [Required]
    public string DocType { get; set; }
    [Required]
    public string DocNum { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string GivenName { get; set; }
    [Required]
    public string FamilyName1 { get; set; }
    [Required]
    public string Phone { get; set; }
}
