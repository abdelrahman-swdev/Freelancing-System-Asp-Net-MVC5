using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancingSystemProject.Models
{
    public class Posts
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Job Budget")]
        public string JobBudget { get; set; }


        [Required]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }


        [Required]
        [Display(Name = "Job Description")]
        public string JobDescription { get; set; }

        [Display(Name ="Approval")]
        public bool IsApprovedByAdmin { get; set; }

        [Required]
        [Display(Name = "Job Type")]
        public int JobTypeId { get; set; }
        public virtual JobType JobType { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}