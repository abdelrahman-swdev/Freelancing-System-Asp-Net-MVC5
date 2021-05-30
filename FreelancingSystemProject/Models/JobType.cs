using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FreelancingSystemProject.Models
{
    public class JobType
    {
        
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Type { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}