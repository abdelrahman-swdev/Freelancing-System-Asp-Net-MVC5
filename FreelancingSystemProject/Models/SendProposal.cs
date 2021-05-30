using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FreelancingSystemProject.Models
{
    public class SendProposal
    {
        public int Id { get; set; }

        public string Message { get; set; }

        [Display(Name = "Proposal Date")]
        public DateTime ProposalDate { get; set; }

        public int PostId { get; set; }
        public virtual Posts Post { get; set; }

        public String UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}