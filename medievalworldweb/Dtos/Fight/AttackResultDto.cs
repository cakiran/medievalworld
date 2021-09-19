namespace medievalworldweb.Dtos.Fight
{
    public class AttackResultDto
    {
        public string AttackerName { get; set; }    
        public string OpponentName { get; set; }
        public int AttackerHitPoints { get; set; }
        public int OpponentHitPoints { get; set; }
        public int Damage { get; set; }
    }
}