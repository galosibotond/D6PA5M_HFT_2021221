using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace D6PA5M_HFT_2021221.Models
{
    [Table("Artists")]
    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ToString]
        public int Id { get; set; } 

        [Required]
        [MaxLength(128)]
        [ToString]
        public string Name { get; set; }

        [NotMapped]
        public virtual ICollection<Album> Albums { get; set; }

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        [NotMapped]
        public virtual Genre Genre { get; set; }

        [ToString]
        public string Country { get; set; }

        [ToString]
        public DateTime? FoundationDate { get; set; }

        public override string ToString()
        {
            string x = string.Empty;

            foreach (var item in this.GetType().GetProperties().Where(x => x.GetCustomAttribute<ToStringAttribute>() != null))
            {
                if (item.Name == "FoundationDate" && item.GetValue(this) == null)
                {
                    x += "   ";
                    x += item.Name + "\t=> ";
                    x += "N/A";
                    x += "\n";
                }
                else
                {
                    x += "   ";
                    x += item.Name + "\t=> ";
                    x += item.GetValue(this);
                    x += "\n";
                }
            }

            return x;
        }
    }
}
