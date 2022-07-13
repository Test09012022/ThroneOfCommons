using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ThroneOfCommons.Core
{
    public class CandidateService : ICandidateService
    {

        private readonly CandidatesDbContext _candidatesDbContext;

        public CandidateService(CandidatesDbContext candidatesDbContext)
        {
            _candidatesDbContext = candidatesDbContext;
        }

        public List<Candidate> GetAll()
        {

            var connection = new SqliteConnection(_candidatesDbContext.Database.GetConnectionString());
            var candidatesList = new List<Candidate>();
            try
            {
                connection.Open();
            var qResult = new SqliteCommand("SELECT COUNT(*) FROM Candidates", connection).ExecuteScalar();
            for (var i = 0; i < (long)qResult; i++)
            {
                var z = _candidatesDbContext.Candidates.FindAsync(i + 1).Result;
                    if (z != null) { 
                candidatesList.Add(z);
                    }
                }
            connection.Close();
            return candidatesList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return candidatesList;
              
            }
            finally
            {
                connection.Close();
            }
        }

        public bool Add(Candidate candidate)
        {
            var connection = new SqliteConnection(_candidatesDbContext.Database.GetConnectionString());
            try
            {
                connection.Open();
                var qResult = new SqliteCommand("SELECT MAX(Id) FROM Candidates", connection).ExecuteScalar();
                candidate.Id = Convert.ToInt32(qResult) + 1;
                _candidatesDbContext.Candidates.Add(candidate);
                _candidatesDbContext.SaveChanges();
                //return candidate;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
           }
        }

        public bool Update(Candidate candidate)
        {
            _candidatesDbContext.Candidates.Update(candidate);
            return _candidatesDbContext.SaveChanges() == 1;

        }


        public bool Delete(int id)
        {

            var result = false;
            var candidate = _candidatesDbContext.Candidates.Find(id);
            if (candidate != null)
            {
                _candidatesDbContext.Candidates.Remove(candidate);
                _candidatesDbContext.SaveChanges();
                result = true;
            }
            return result;
        }

    }
}