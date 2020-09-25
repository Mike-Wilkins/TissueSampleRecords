using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TissueSampleRecords.Models
{
    public class SampleModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Donor Count")]
        public int Donor_Count { get; set; }

        [Required]
        [DisplayName("Material Type")]
        public string Material_Type { get; set; }

        [DisplayName("Last Updated")]
        public string Last_Updated { get; set; }

        public string Collection_Title { get; set; }
    }
}
