using System;
using System.ComponentModel.DataAnnotations;

namespace ThroneOfCommons.Core
{
    // pet class
    public class Candidate
    {
   
        //  id
        [Key]
        public int Id { get; set; }
        
        //  name
        public string Name { get; set; }

        //   rating
        [Range(1,10)]
        public int Rating { get; set; }

        //date of birth
        public DateTime DateOfBirth { get; set; }

        public string LatestPortfolio { get; set; }

      //  public string Party { get; set; }
      // Party type
        public int PartyType { get; set; } // 1 = Conservative , 2 = Labour, 3 = Scottish National Party




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

        private DateTime? biddedOn = null;


    }





}