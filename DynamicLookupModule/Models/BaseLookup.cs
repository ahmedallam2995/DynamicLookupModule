
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DynamicLookupModule.Models
{
    public class BaseLookup : BaseLookup<int>
    {
       
    }

    public class BaseLookup<T> 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual T Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(400)]
        public virtual string Name { get; set; }
    }
}
