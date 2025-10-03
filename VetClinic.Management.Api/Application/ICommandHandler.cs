namespace VetClinic.Management.Api.Application
{
    public interface ICommandHandler<T>
    {
        Task Handle(T command);
    }
}
