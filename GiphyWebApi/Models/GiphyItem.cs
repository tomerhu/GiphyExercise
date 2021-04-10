using System.ComponentModel.DataAnnotations;

namespace GiphyApp.Models
{
    public class GiphyItem
    {
        //Primary Key
        [Key]
        public string SearchTerm { get; set; }
        public string Url { get; set; }
    }
}