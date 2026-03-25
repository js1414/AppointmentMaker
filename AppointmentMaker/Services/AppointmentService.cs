using AppointmentMaker.Models;
using AppointmentMaker.Repositories;

namespace AppointmentMaker.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;

        public AppointmentService(IAppointmentRepository repo) => _repo = repo;

        public async Task<IEnumerable<AppointmentModel>> GetAll()
        {
          return await _repo.GetAll();
        }

        public async Task<AppointmentModel?> GetById(int id)
        {
           return await _repo.GetById(id);
        }
        public async Task Create(AppointmentModel product)
        {
           await _repo.Create(product);
        }

        public async Task Delete(int id)
        {
           await _repo.Delete(id);
        }

        public async Task Update(int id, AppointmentModel product)
        {
           await _repo.Update(id, product);
        }
    }
}
