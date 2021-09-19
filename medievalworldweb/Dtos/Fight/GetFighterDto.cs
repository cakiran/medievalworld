using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace medievalworldweb.Dtos.Fight
{
    public class GetFighterDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
