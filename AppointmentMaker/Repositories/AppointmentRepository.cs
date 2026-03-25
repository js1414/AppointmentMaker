using AppointmentMaker.Data;
using AppointmentMaker.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AppointmentMaker.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _db;

        public AppointmentRepository(ApplicationDbContext db) => _db = db;

        //Read all appointment
        public async Task<IEnumerable<AppointmentModel>> GetAll()
        {
            return await  _db.Appointments.ToListAsync();
        }

        // Select an appointment by its id
        public async Task<AppointmentModel?> GetById(int id)
        {
            var appointment = _db.Appointments.FindAsync(id);

            return await appointment;

        }
        
        //Create new appointment
        public async Task Create(AppointmentModel product)
        {
             _db.Appointments.Add(product);
            await _db.SaveChangesAsync();
        }

        //Delete exisating appointment
        public async Task Delete(int id)
        {
            var appointment = await _db.Appointments.FindAsync(id);

            if(appointment != null)
            {
                _db.Appointments.Remove(appointment);
            }
            await _db.SaveChangesAsync();

        }

        //Updare existing appointment
        public async Task Update(int id,AppointmentModel appointment)
        {
            var currentAppointment = await _db.Appointments.FindAsync(id);
            
            if(currentAppointment == null)
            {
                throw new ArgumentException("Appointment no found");
            }

            //Update Fields
            currentAppointment.Name = appointment.Name;
            currentAppointment.DoctorName = appointment.DoctorName;
            currentAppointment.PainLevel = appointment.PainLevel;
            currentAppointment.Date = appointment.Date;
            currentAppointment.DoctorRoom = appointment.DoctorRoom;
           
            await _db.SaveChangesAsync();
          
        }

    }
}
