using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlScript.Models
{
    [Table("ConnectionString", Schema = "dbo")]
    public class ConnectionString
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Connection String ID")]
        public int ConnectionStringID { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [MaxLength(100)]
        [Display(Name = "Connection String Name")]
        public string ConnectionStringName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [MaxLength(100)]
        [Display(Name = "Connection String Data Source")]
        public string ConnectionStringDataSource { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [MaxLength(100)]
        [Display(Name = "Connection String UserID")]
        public string ConnectionStringUserID { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [MaxLength(100)]
        [Display(Name = "Connection String Password")]
        public string ConnectionStringPassword { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [MaxLength(100)]
        [Display(Name = "Connection String Initial Catalog")]
        public string ConnectionStringInitialCatalog { get; set; }

        [Required]
        [ForeignKey("Connections")]
        public int ConnectionsId { get; set; }

        [Display(Name = "Connections")]
        [NotMapped]
        public string ConnectionsName { get; set; }

        public virtual Connections Connections { get; set; }


    }
}
