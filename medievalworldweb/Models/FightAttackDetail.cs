using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medievalworldweb.Models
{
    public class FightAttackDetailMap : IEntityTypeConfiguration<FightAttackDetail>
    {
        public void Configure(EntityTypeBuilder<FightAttackDetail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("FightAttackDetails");
        }
    }
    public class FightAttackDetail
    {
        public int Id { get; set; }
        public int OpponentId { get; set; }
        public string OpponentName { get; set; }
        public int OpponentHP { get; set; }
        public int AttckerHP { get; set; }
        public string LogText { get; set; }
        public string Winner { get; set; }
        public string Loser { get; set; }
        public int WinnerId { get; set; }
    }
}