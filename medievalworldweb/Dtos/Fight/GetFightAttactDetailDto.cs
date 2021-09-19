namespace medievalworldweb.Dtos.Fight
{
    public class GetFightAttactDetailDto
    {
        public int OpponentId { get; set; }
        public string OpponentName { get; set; }
        public string LogText { get; set; }
        public string Winner { get; set; }
        public string Loser { get; set; }
        public int OpponentHP { get; set; }
        public int AttackerHP { get; set; }
        public int WinnerId { get; set; }
    }
}