using EcommerceCleanArchitecture.ApplicationDomain.InputPorts;
using EcommerceCleanArchitecture.ApplicationDomain.Output;

namespace EcommerceCleanArchitecture.ApplicationDomain.UseCases
{
    internal class ListProductsUseCase : IListProductsUseCase
    {
        public async Task<ProductListViewModel> ExecuteAsync()
        {
            var productListViewModel = new ProductListViewModel();
            await Task.Delay(1000);

            return productListViewModel; 
        }
    }
}
