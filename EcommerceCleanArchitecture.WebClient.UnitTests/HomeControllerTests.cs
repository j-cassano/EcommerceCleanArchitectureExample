using EcommerceCleanArchitecture.ApplicationDomain.InputPorts;
using EcommerceCleanArchitecture.ApplicationDomain.Output;
using EcommerceCleanArchitecture.WebClient.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;

namespace EcommerceCleanArchitecture.WebClient.UnitTests
{
    public class HomeControllerTests
    {
        [Test]
        public async Task Index_CallsListProductsUseCase_WhenCalled()
        {
            var automocker = new AutoMocker();
            var homeController = automocker.CreateInstance<HomeController>();
            var listProductsUseCase = automocker.GetMock<IUseCaseInputPort<ProductListViewModel>>();

            await homeController.Index();

            listProductsUseCase.Verify(x => x.ExecuteAsync(), Times.Once());
        }


        [Test]
        public async Task Index_ReturnsAViewWithAProductListViewModel_WhenCalled()
        {
            var automocker = new AutoMocker();
            var homeController = automocker.CreateInstance<HomeController>();
            var listProductsUseCase = automocker.GetMock<IUseCaseInputPort<ProductListViewModel>>();
            var testViewModel = CreateTestProductViewModel();
            listProductsUseCase.Setup(x => x.ExecuteAsync()).ReturnsAsync(testViewModel);
            var expectedProductCount = 0;

            var result = await homeController.Index() as ViewResult;
            var productListViewModel = result.Model as ProductListViewModel;

            Assert.AreEqual(expectedProductCount, productListViewModel.Products.Count);
        }


        private ProductListViewModel CreateTestProductViewModel()
        {
            var productListViewModel = new ProductListViewModel();
            productListViewModel.Products = new List<ProductViewModel>();

            return productListViewModel;
        }

    }
}