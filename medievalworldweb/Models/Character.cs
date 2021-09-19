using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace medievalworldweb.Models
{
    public class CharacterMap : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Characters");
        }
    }
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int HitPoints { get; set; } 

        public int Strength { get; set; } 
        public int Defense { get; set; }

        public int Intelligence { get; set; } 

        public RpgClass Class { get; set; } 

        public virtual User User { get; set; }
        
        public virtual Weapon Weapon { get; set; }

        public virtual ICollection<CharacterSkill> CharacterSkills { get; set; }

        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }

    }
}