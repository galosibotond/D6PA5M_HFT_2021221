using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace D6PA5M_HFT_2021221.Models
{
    [Table("Albums")]
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ToString]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        [ToString]
        public string Title { get; set; }

        [ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }

        [ForeignKey(nameof(RecordCompany))]
        public int RecordCompanyId { get; set; }

        [Required]
        [ToString]
        public int Stock { get; set; }

        [NotMapped]
        public virtual Artist Artist { get; set; }

        [NotMapped]
        public virtual RecordCompany RecordCompany { get; set; }

        [ToString]
        public DateTime? ReleaseDate { get; set; }

        public override string ToString()
        {
            string x = string.Empty;

            foreach (var item in this.GetType().GetProperties().Where(x => x.GetCustomAttribute<ToStringAttribute>() != null))
            {
                if (item.Name == "ReleaseDate" && item.GetValue(this) == null)
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
