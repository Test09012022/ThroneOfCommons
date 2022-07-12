using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThroneOfCommons.Mvc.Models
{
    public class CandidateViewModel
    {

        // the name
        [RegularExpression("^[A-Za-z]+$")]
        public string Name { get; set; }

        public int Id { get; set; }

        [Range(1, 10)]
        public int Rating { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BiddedOn
        {
            get
            {
                return this.biddedOn.HasValue
                   ? this.biddedOn.Value
                   : DateTime.Now;
            }

            set { this.biddedOn = value; }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth
        {
            get; set;
        }

      
        private DateTime? biddedOn = null;

        public string LatestPortfolio { get; set; }


    }
}
