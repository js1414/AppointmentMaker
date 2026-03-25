using AppointmentMaker.Models;

namespace AppointmentMaker.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<AppointmentModel>> GetAll();
        Task<AppointmentModel?> GetById(int id);
        Task Create(AppointmentModel product);
        Task Update(int id,AppointmentModel product);
        Task Delete(int id);

    }
}
