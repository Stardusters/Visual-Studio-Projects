using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhoteService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RaceController(IRaceRepository raceRepository, IPhoteService photeService, IHttpContextAccessor httpContextAccessor)
        {
            _raceRepository = raceRepository;
            _photoService = photeService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> lstRace = await _raceRepository.GetAll();
            return View(lstRace);
        }
        public async Task<IActionResult> Detail(int id)
        {
            //Race race = _context.Races.Include(a => a.Address).SingleOrDefault(r => r.Id == id);
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }
        public IActionResult Create()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createRaceVM = new CreateRaceViewModel() { AppUserId = currentUserId };
            return View(createRaceVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);

                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = raceVM.AppUserId,
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                    }
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index","Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }
            return View(raceVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null)
                return View("Error");
            var raceVM = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                RaceCategory = race.RaceCategory
            };
            return View(raceVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to Edit club");
                return View("Edit", raceVM);
            }
            var userClub = await _raceRepository.GetByIdAsyncNoTracking(id);

            if (userClub != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userClub.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to delete photo");
                    return View(raceVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = new Race
                {
                    Id = id,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    AddressId = raceVM.AddressId,
                    Address = raceVM.Address,
                    Image = photoResult.Url.ToString()
                };
                _raceRepository.Update(race);
                return RedirectToAction("Index");
            }
            else
                return View(raceVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var RaceDetail = await _raceRepository.GetByIdAsync(id);
            if (RaceDetail == null) { return View("Error"); }
            return View(RaceDetail);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRace(int id)
        {
            var RaceDetail = await _raceRepository.GetByIdAsync(id);
            if(RaceDetail == null) { return View("Error"); }

            _raceRepository.Delete(RaceDetail);
            return RedirectToAction("Index");
        }
    }
}
