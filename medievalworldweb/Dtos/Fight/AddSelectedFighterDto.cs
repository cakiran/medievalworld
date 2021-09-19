using System.Collections.Generic;

namespace medievalworldweb.Dtos.Fight
{
    public class AddSelectedFighterDto
    {
        ////{id:"1",userId:1,content:"Legolas",image:Legolas}
    public List<SelectedFighterDto> SelectedFighters { get; set; }
    }

    public class SelectedFighterDto{
        public int Id { get; set; } 
        public int UserId { get; set; } 
        public string Content { get; set; }
    }
}