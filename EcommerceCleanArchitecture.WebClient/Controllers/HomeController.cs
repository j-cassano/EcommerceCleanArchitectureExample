using EcommerceCleanArchitecture.ApplicationDomain.InputPorts;
using EcommerceCleanArchitecture.ApplicationDomain.Output;
using EcommerceCleanArchitecture.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceCleanArchitecture.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUseCaseInputPort<ProductListViewModel> _listProductsUseCase;

        public HomeController(ILogger<HomeController> logger, IUseCaseInputPort<ProductListViewModel> listProductsUseCase)
        {
            _logger = logger;
            _listProductsUseCase = listProductsUseCase;
        }

        public async Task<IActionResult> Index()
        {
            var productViewModel = await _listProductsUseCase.ExecuteAsync();
            return View(productViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}