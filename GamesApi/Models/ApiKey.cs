using System.Text.Json.Serialization;

namespace GamesApi.Models;

public class ApiKey
{
    public int Id { get; set; }
    public string Owner { get; set; } = "";
    public string Email { get; set; } = "";
    public Guid Key { get; set; }
    public DateTime ValidTill { get; set; }
    public bool Blocked { get; set; } = false;
}