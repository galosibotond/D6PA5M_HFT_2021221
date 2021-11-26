using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace D6PA5M_HFT_2021221.Models
{
    [Table("Artists")]
    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Album> Albums { get; set; }

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        [NotMapped]
        public virtual Genre Genre { get; set; }

        public string Country { get; set; }

        public DateTime? FoundationDate { get; set; }
    }
}
