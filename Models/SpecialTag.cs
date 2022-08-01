using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class SpecialTag
    {
        public int Id { get; set; }
        //[Required]
        //[Display(Name ="Tag Name")]
        public string Name { get; set; }
    }
}
