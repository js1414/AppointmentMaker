using AppointmentMaker.Data;
using AppointmentMaker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentMaker.Controllers
{

    public class AppointmentController : Controller
    {
        //create and ibitialize list of appointments
       // static  List<AppointmentModel> appointments = new List<AppointmentModel>();
       private readonly ApplicationDbContext _db;

        public AppointmentController(ApplicationDbContext context) => _db = context;
        public async Task<IActionResult> Index()
        {
            var appointments = await _db.Appointments.ToListAsync();
            appointments.OrderByDescending(m => m.Date).ToList();
            return View(appointments);

            //var patients = _db.Appointments;
            //return View(patients); // this is to return a list of appointments or objects saved in the list in the index form
        }

        public IActionResult Create()
        {
            return View();        
        }

        //[HttpPost]
        //public IActionResult Create(Appointment model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model); // Show validation errors
        //    }

        //    _context.Appointments.Add(model);
        //    _context.SaveChanges();

        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        // GET: Appointment/Delete/5

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var appointment = await _db.Appointments
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment); // show confirmation page
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var appointment = await _db.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            _db.Appointments.Remove(appointment);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // [HttpPost]
        public async Task<IActionResult> Details(AppointmentModel model)  //AppointmentModel appointment = new AppointmentModel { ... };
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Show validation errors
            }

             _db.Appointments.Add(model);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
            //var newAppointment = _db.Appointments.Add(appointment); // Pass the new object to the list appointements
            //_db.SaveChanges();
            //return View("Details", appointment); // this return one appointment
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var appointment = await _db.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, AppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var appointment = await _db.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            // Update properties manually
            appointment.Name = model.Name;
            appointment.Date = model.Date;
            appointment.PatientWorth = model.PatientWorth;
            appointment.DoctorName = model.DoctorName;
            appointment.PainLevel = model.PainLevel;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
