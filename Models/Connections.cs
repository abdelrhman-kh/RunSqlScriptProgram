using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlScript.Models
{
    [Table("Connections", Schema = "dbo")]
    public class Connections
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Connections ID")]
        public int ConnectionsId { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        [Display(Name = "Connections Name")]
        public string ConnectionsName { get; set; }

        [Column(TypeName = "varchar(15)")]
        [Display(Name = "Connections Abbreviation")]
        public string ConnectionsAbbr { get; set; }


    }
}
