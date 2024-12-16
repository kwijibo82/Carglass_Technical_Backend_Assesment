using System.ComponentModel.DataAnnotations;

public class ClientPutDto
{
    [Required]
    public string DocType { get; set; }
    [Required]
    public string DocNum { get; set; }
    [Required]
    public string GivenName { get; set; }
    [Required]
    public string FamilyName1 { get; set; }
}

////NOTA: Dado que el verbo HTTP Put se utiliza para actualizar un recurso ya existente o crear uno nuevo si no existe
///(dependiendo de la implementación), se crea un DTO a parte para delegarle solo la responsabilidad
///de actualizar los campos indicados
//Es decir, es idempotente, esto es que si se envía la misma solicitud varias veces, el resultado en el servidor no cambia.
//Con ello se busca preservar el principio de responsabilidad única sin modificar el DTO principal ni incurrir en DRY
//(Don't Repeat Yoursefl)