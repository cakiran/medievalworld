using medievalworldweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace medievalworldweb.Dtos.Fight
{
    public class CharacterToFightDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HitPoints { get; set; }

        public int Strength { get; set; }
        public int Defense { get; set; }

        public int Intelligence { get; set; }

        public Skill[] Skills{ get; set; }
        public  Weapon Weapon { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
        public RpgClass Class { get; set; }
        public User User { get; set; }

    }
}
