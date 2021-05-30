using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancingSystemProject.Models
{
    public class SavedJobs
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public virtual Posts Post { get; set; }

        public String UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}