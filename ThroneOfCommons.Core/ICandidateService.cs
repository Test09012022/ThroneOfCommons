using System;
using System.Collections.Generic;
using System.Text;

namespace ThroneOfCommons.Core
{
    public interface ICandidateService
    {
            List<Candidate> GetAll();
            bool Add(Candidate candidate);
        
         
    }
}
