namespace medievalworldweb.Dtos.Fight
{
    public class GetFightTallyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
        public string Username { get; set; }
    }
}