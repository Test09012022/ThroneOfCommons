using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThroneOfCommons.Core;
using System;
using System.Data.Common;
using Moq;

namespace ThroneOfCommons.Tests
{
    [TestClass]
    public class ThroneOfCommonsTest
    {
        private readonly CandidatesDbContext _candidatesDbContext;
        CandidateService service;

        public ThroneOfCommonsTest()
        {
            var services = new ServiceCollection();
            services.AddDbContext<CandidatesDbContext>(opts =>
            {
                static DbConnection CreateInMemoryDatabase()
                {
                    var connection = new SqliteConnection("Filename=ThroneOfCommons.db");
                    connection.Open();
                    return connection;
                }

                opts.UseSqlite(CreateInMemoryDatabase());
            });

            var serviceProvider = services.BuildServiceProvider();
            _candidatesDbContext = serviceProvider.GetService<CandidatesDbContext>();
            service = new CandidateService(_candidatesDbContext);
        }

        [TestMethod]
        public void TestGetAll()
        {
            var output = service.GetAll();

            Assert.IsTrue(output.Count > 0);

        }

        [TestMethod]
        public void TestAddCandidates()
        {

            Candidate candidate = new Candidate() { Name = "Shapps", Rating = 1, BiddedOn = DateTime.Now, PartyType = 1, DateOfBirth = DateTime.Now.AddYears(-50), LatestPortfolio = "Transport" };
            Assert.IsTrue(service.Add(candidate));

        }


        [TestMethod]
        public void TestDeleteCandidates()
        {
            Assert.IsTrue(service.Delete(4));

        }

        [TestMethod]
        public void TestEditCandidates()
        {
            Candidate candidate = new Candidate() { Id = 6, Name = "Shappyy", Rating = 2, BiddedOn = DateTime.Now, PartyType = 1, DateOfBirth = DateTime.Now.AddYears(-50), LatestPortfolio = "Transport" };
            Assert.IsTrue(service.Update(candidate));

        }


        public void TestMockCandidates()
        {

            //var mockService = new Mock<CandidateService>();
            //int id = 4;
            //mockService.Setup(p => p.Delete(id)).Returns(true);
            //var result = controller.CheckOut(cardMock.Object, addressInfoMock.Object);
            //Assert.IsTrue(service.Delete(4));

            //Candidate candidate = new Candidate() { Id = 6, Name = "Shappyy", Rating = 2, BiddedOn = DateTime.Now, PartyType = 1, DateOfBirth = DateTime.Now.AddYears(-50), LatestPortfolio = "Transport" };
            //Assert.IsTrue(service.Update(candidate));

        }
    }
}
