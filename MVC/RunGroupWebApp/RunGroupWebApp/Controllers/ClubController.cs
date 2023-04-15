using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModel;

namespace RunGroupWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhoteService _photoService;
        public ClubController(IClubRepository clubRepository, IPhoteService photeService)
        {
            _clubRepository = clubRepository;
            _photoService = photeService;
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
            return View();
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
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                    }
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }
            return View(clubVM);
        }
    }
}
