using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ThroneOfCommons.Core;
using ThroneOfCommons.Mvc.Models;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;

namespace ThroneOfCommons.Mvc.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly CandidatesDbContext _candidatesDbContext;
        public static bool desc = false;
        public static string existingSort = "Name";
        CandidateService service;


        public CandidatesController(CandidatesDbContext candidatesDbContext)
        {
            _candidatesDbContext = candidatesDbContext;
            service = new CandidateService(_candidatesDbContext);
        }

        public IActionResult Index(string sortOrder = "Name")
        {

            try
            {
                var result = service.GetAll();
                var viewModel = from r in result
                                select new CandidateViewModel
                                {
                                    Name = r.Name,
                                    Id = r.Id,
                                    Rating = r.Rating,
                                    LatestPortfolio = r.LatestPortfolio,
                                    BiddedOn = r.BiddedOn

                                };

                switch (sortOrder)
                {

                    case "Name":
                        if (existingSort == sortOrder && desc)
                        {
                            viewModel = viewModel.OrderByDescending(r => r.Name);
                        }
                        else
                        {
                            viewModel = viewModel.OrderBy(r => r.Name);
                            desc = true;
                        }


                        break;
                    case "LatestPortfolio":
                        if (existingSort == sortOrder && desc)
                        {
                            viewModel = viewModel.OrderByDescending(r => r.LatestPortfolio);
                            desc = false;
                        }
                        else
                        {
                            viewModel = viewModel.OrderBy(r => r.LatestPortfolio);
                            desc = true;
                        }
                        break;
                    case "Rating":
                        if (existingSort == sortOrder && desc)
                        {
                            viewModel = viewModel.OrderByDescending(r => r.Rating);
                            desc = false;
                        }
                        else
                        {
                            viewModel = viewModel.OrderBy(r => r.Rating);
                            desc = true;
                        }
                        break;
                    case "BiddedOn":
                        if (existingSort == sortOrder && desc)
                        {
                            viewModel = viewModel.OrderByDescending(r => r.BiddedOn);
                            desc = false;
                        }
                        else
                        {
                            viewModel = viewModel.OrderBy(r => r.BiddedOn);
                            desc = true;
                        }
                        break;
                }
                existingSort = sortOrder;

                return View(viewModel);
            }
            catch
            {
                return View("Error", new ErrorViewModel { Error = "Unable to Load Candidates" });
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View(new CandidateCreateViewModel());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CandidateCreateViewModel candidateCreateViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var result = service.Add(new Candidate() { Name = candidateCreateViewModel.Name, BiddedOn = candidateCreateViewModel.BiddedOn, DateOfBirth = candidateCreateViewModel.DateOfBirth, PartyType = candidateCreateViewModel.Party, LatestPortfolio = candidateCreateViewModel.LatestPortfolio, Rating = candidateCreateViewModel.Rating });
                if (result)
                {
                    return View("Details", new CandidateViewModel() { Name = candidateCreateViewModel.Name, Rating = candidateCreateViewModel.Rating, LatestPortfolio = candidateCreateViewModel.LatestPortfolio, BiddedOn = candidateCreateViewModel.BiddedOn });
                }
                else
                {
                    return View("Error", new ErrorViewModel { Error = "Unable to Add Candidate" });

                }

                }
                else { return View("Error", new ErrorViewModel { Error = "Unable to Add Candidate" }); }

            }
            catch 
            {
                 return View("Error", new ErrorViewModel { Error = "Unable to Add Candidate" });
            }
        }

        [HttpGet]
        //   [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            try { 
            var result = service.GetAll().Where(r => r.Id.Equals(id)).Select(r => new CandidateViewModel
            {
                Name = r.Name,
                Id = r.Id,
                Rating = r.Rating,
                LatestPortfolio = r.LatestPortfolio,
                BiddedOn = r.BiddedOn

            }).FirstOrDefault();
            return View("Edit", result);
            }
            catch 
            {
                return View("Error", new ErrorViewModel { Error = "Unable to Load Required Candidate" });
            }
        }


        //    [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CandidateViewModel candidateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var result = service.Update(new Candidate() { Name = candidateViewModel.Name, BiddedOn = candidateViewModel.BiddedOn, Id = id, Rating = candidateViewModel.Rating, LatestPortfolio = candidateViewModel.LatestPortfolio });
                    if (result)
                    {
                        return View("Details", candidateViewModel);
                    }
                }
                return View("Edit", candidateViewModel);
            }
            catch 
            {
                return View("Error", new ErrorViewModel { Error = "Unable to Edit Candidate" });
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try { 
            var result = service.GetAll().Where(r => r.Id.Equals(id)).Select(r => new CandidateViewModel
            {
                Name = r.Name,
                Id = r.Id,
                Rating = r.Rating,
                LatestPortfolio = r.LatestPortfolio,

                BiddedOn = r.BiddedOn

            }).FirstOrDefault();
            return View("Delete", result);
            }
            catch
            {
                return View("Error", new ErrorViewModel { Error = "Unable to Load Candidate" });
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CandidateViewModel candidateViewModel)
        {
            try
            {
                if( service.Delete(id))
                { 
                return RedirectToAction("Index");
                }
                else { return View("Error", new ErrorViewModel { Error = "Unable to Delete Candidate" }); }
            }
            catch
            {
                return View("Error", new ErrorViewModel { Error = "Unable to Delete Candidate" });
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}