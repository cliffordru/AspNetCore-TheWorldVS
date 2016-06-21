using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorldVS.ViewModels
{
    public class TripViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Name { get; set; }
        // C# 6 feature - initialize
        public DateTime Created { get; set; } = DateTime.UtcNow;
        
    }
}
