using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Data.Enum;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;
using System.Collections.Concurrent;

namespace RunGroupWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhoteService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClubController(IClubRepository clubRepository, IPhoteService photeService, IHttpContextAccessor httpContextAccessor)
        {
            _clubRepository = clubRepository;
            _photoService = photeService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> lstClubs = await _clubRepository.GetAll();
            return View(lstClubs);
        }
        public async Task<IActionResult> Detail(int id)
        {
            //Using EntityFramework.Include to join two tables.
            //Club club = _context.Clubs.Include(a => a.Address).FirstOrDefault(x => x.Id == id);
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }
        public IActionResult Create()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createClubVM = new CreateClubViewModel() { AppUserId = currentUserId };
            return View(createClubVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = clubVM.AppUserId,
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                    }
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index","Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }
            return View(clubVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if (club == null) 
                return View("Error");
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Address,
                ClubCategory = club.ClubCategory
            };
            return View(clubVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit club");
                return View("Edit",clubVM);
            }
            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

            if(userClub != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to delete photo");
                    return View(clubVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    AddressId = clubVM.AddressId,
                    Address = clubVM.Address,
                    Image = photoResult.Url.ToString()
                };
                _clubRepository.Update(club);
                return RedirectToAction("Index");
            }
            else
                return View(clubVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var clubDetail = await _clubRepository.GetByIdAsync(id);
            if (clubDetail == null) { return View("Error"); }
            return View(clubDetail);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var clubDetail = await _clubRepository.GetByIdAsync(id);
            if (clubDetail == null) { return View("Error"); }

            _clubRepository.Delete(clubDetail); 
            return RedirectToAction("Index");
        }

    }
}
