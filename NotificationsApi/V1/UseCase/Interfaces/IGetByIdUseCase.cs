using NotificationsApi.V1.Boundary.Response;

namespace NotificationsApi.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        ResponseObject Execute(int id);
    }
}
