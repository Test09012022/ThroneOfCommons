using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThroneOfCommons.Mvc.Models
{
    public class CandidateCreateViewModel
    {

        // the name
        [RegularExpression("^[A-Za-z]+$")]
        public string Name { get; set; }

    

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
            get
            {
                return this.dateOfBirth.HasValue
                   ? this.dateOfBirth.Value
                   : DateTime.Now.AddYears(-30);
            }

            set { this.dateOfBirth = value; }
        }

        [Range(1, 10)]
        public int Party { get; set; }

        private DateTime? biddedOn = null;
        private DateTime? dateOfBirth = null;

        public string LatestPortfolio { get; set; }

        public IEnumerable<SelectListItem> PartyType
        {
            get
            {
                return new List<SelectListItem>() {
                new SelectListItem( "Conservative","1"),
                new SelectListItem( "Labour","2"),
                new SelectListItem( "Scottish National Party","3" ),
          
            };
            }
        }




    }
}
