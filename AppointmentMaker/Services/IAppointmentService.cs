using AppointmentMaker.Models;

namespace AppointmentMaker.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentModel>> GetAll();
        Task<AppointmentModel?> GetById(int id);
        Task Create(AppointmentModel product);
        Task Update(int id, AppointmentModel product);
        Task Delete(int id);

    }
}
