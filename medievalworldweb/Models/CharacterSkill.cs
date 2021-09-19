using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace medievalworldweb.Models
{
    public class CharacterSkillMap : IEntityTypeConfiguration<CharacterSkill>
    {
        public void Configure(EntityTypeBuilder<CharacterSkill> builder)
        {
            builder.HasKey(x => x.CharacterId);
            builder.ToTable("CharacterSkills");
            builder.HasOne(x => x.Character).WithMany(x => x.CharacterSkills).HasForeignKey(x => x.CharacterId);
            builder.HasOne(x => x.Skill).WithMany(x => x.CharacterSkills).HasForeignKey(x => x.SkillId);
        }
    }
    public class CharacterSkill
    {
        public int CharacterId { get; set; }
        public virtual Character Character { get; set; }
        public int SkillId { get; set; }
        public virtual Skill Skill { get; set; }
    }
}