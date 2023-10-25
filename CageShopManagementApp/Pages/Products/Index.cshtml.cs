using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Repositories;

namespace CageShopManagementApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IBirdCageRepository _birdCageRepository;

        public IndexModel(IBirdCageRepository birdCageRepository)
        {
            _birdCageRepository = birdCageRepository;
        }

        public string UserName { get; set; }
        public string Role { get; set; }
        public List<BirdCage> ReadyMadeCages { get; set; }

        public void OnGet()
        {
            UserName = HttpContext.Session.GetString("UserName");
            Role = HttpContext.Session.GetString("Role");
            ReadyMadeCages = _birdCageRepository.GetAvailableReadyMadeCages().ToList();
        }
    }
}