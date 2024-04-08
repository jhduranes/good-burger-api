using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Tables
{
    public class ItensOrder
    {
        [Key]
        public  int Id { get; set; }
        [ForeignKey("Orders"), Column(Order = 0)]
        public int IdOrder { get; set; }
        [ForeignKey("Products"), Column(Order = 1)]
        public int IdProduct { get; set; }
    }
}
