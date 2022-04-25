namespace GamesApi.Models
{
    public class GameCharacter
    {
        public int Id { get; set; }

        public string CharacterName { get; set; } = string.Empty;
        public string AboutCharacter { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string GameName { get; set; } = string.Empty;
    }
}
