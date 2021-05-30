using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancingSystemProject.Models
{
    public class PostViewModel
    {
        public string JobDescription { get; set; }

        public IEnumerable<SendProposal> proposals { get; set; }
    }
}