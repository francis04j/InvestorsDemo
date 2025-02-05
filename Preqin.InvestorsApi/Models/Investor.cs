using System.ComponentModel.DataAnnotations.Schema;

namespace Preqin.InvestorsApi.Models;

[Table("investors")]
public class Investor
    {
        [Column("investor_id")]
        public int InvestorId { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("type")]
        public string Type { get; set; }
        
        [Column("country")]
        public string Country { get; set; }
        
        [Column("date_added")]
        public DateTime DateAdded { get; set; }
        
        [Column("last_updated")]
        public DateTime LastUpdated { get; set; }
        
        public ICollection<Commitment>? Commitments { get; set; }
    }