using EcommerceCleanArchitecture.ApplicationDomain.InputPorts;
using EcommerceCleanArchitecture.ApplicationDomain.Output;
using EcommerceCleanArchitecture.WebClient.Controllers;
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
    }
}