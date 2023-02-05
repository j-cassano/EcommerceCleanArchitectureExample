using EcommerceCleanArchitecture.ApplicationDomain.Output;


namespace EcommerceCleanArchitecture.ApplicationDomain.InputPorts
{
    public interface IListProductsUseCase
    {
        Task<ProductListViewModel> ExecuteAsync();
    }
}
