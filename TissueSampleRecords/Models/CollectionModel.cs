using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TissueSampleRecords.Models
{
    public class CollectionModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Collection Id")]
        public int Collection_Id { get; set; }

        [Required]
        [DisplayName("Disease Term")]
        public string Disease_Term { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
