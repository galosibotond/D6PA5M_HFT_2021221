using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D6PA5M_HFT_2021221.Models
{
    [Table("Albums")]
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }

        [ForeignKey(nameof(RecordCompany))]
        public int RecordCompanyId { get; set; }

        public int Stock { get; set; }

        public int Price { get; set; }

        [NotMapped]
        public virtual Artist Artist { get; set; }

        [NotMapped]
        public virtual RecordCompany RecordCompany { get; set; }

        public DateTime? ReleaseDate { get; set; }
    }
}
