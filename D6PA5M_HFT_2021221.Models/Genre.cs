using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace D6PA5M_HFT_2021221.Models
{
    [Table("Genres")]
    public class Genre
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
        public virtual ICollection<Artist> Artists { get; set; }

        public override string ToString()
        {
            string x = string.Empty;

            foreach (var item in this.GetType().GetProperties().Where(x => x.GetCustomAttribute<ToStringAttribute>() != null))
            {
                x += "   ";
                x += item.Name + "\t=> ";
                x += item.GetValue(this);
                x += "\n";
            }

            return x;
        }
    }
}
