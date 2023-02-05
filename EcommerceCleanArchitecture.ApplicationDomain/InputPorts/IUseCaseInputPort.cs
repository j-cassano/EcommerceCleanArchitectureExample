namespace EcommerceCleanArchitecture.ApplicationDomain.InputPorts
{
    public interface IUseCaseInputPort<ViewModelType>
    {
        Task<ViewModelType> ExecuteAsync();
    }
}