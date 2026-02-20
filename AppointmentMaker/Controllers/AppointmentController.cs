using AppointmentMaker.Data;
using AppointmentMaker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentMaker.Controllers
{

    public class AppointmentController : Controller
    {
       //Create in stance for database access as blueprint
       private readonly ApplicationDbContext _db;

        public AppointmentController(ApplicationDbContext context)
            => _db = context; //Inject the reel database instance

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Assign  as a list from the database, the whole table Appointment's records to variable appointments.
            var appointments = await _db.Appointments.ToListAsync();
            //Create a list of the records in descending order, meaning, older dates first
            appointments.OrderByDescending(m => m.Date).ToList();
            return View(appointments); // show the list on Index.cshtml view.
        }

        // DETAILS (GET)
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            //Search the record reffering to its Id, and assign
            //that record to variable appointmentDetails.
            var appointmentDetail = await _db.Appointments.FindAsync(id);

            if (appointmentDetail == null) // Check if that 
                return NotFound();

            //If values exist, return that record to details.cshtml view.
            return View(appointmentDetail);
        }

        // DETAILS (POST)
        [HttpPost]
        public async Task<IActionResult> Details(AppointmentModel model) // makes the model available
        {
            //Check if modelstate approve vailidation
            if (!ModelState.IsValid)
            {
                //if not, show validation errors
                return View(model);
            }

            //if validation approve, add data to the database.
            _db.Appointments.Add(model);
            await _db.SaveChangesAsync();

            //Redirect to show the Index.cshtml view
            return RedirectToAction("Index");

        }

        // CREATE (GET)
        [HttpGet]
        public Task<IActionResult> Create()
        {
            //This line is used inside an async method, but the method returns an IActionResult.
            //Because the method is asynchronous, it must return a Task<IActionResult>.
            return Task.FromResult<IActionResult>(View());
        }

        // CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(AppointmentModel appointment)
        {
            //If data validation is not correct such as wrong format,
            //invalid properties on create.cshtml form.
            //return the same view and display type of error.
            if (!ModelState.IsValid)
                return View(appointment);

            //If validation aprouved
            //Add and save new record to the appointment table
            await _db.Appointments.AddAsync(appointment);
            await _db.SaveChangesAsync();


           // return RedirectToAction("Details", new { id = appointment.Id });
            return RedirectToAction("Index");
        }

        // DELETE (GET)
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            //Get record by the provided Id, assign
            //the record to variable appointment.
            var appointment = await _db.Appointments
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null) // Check for record values existance
            {
                return NotFound();
            }

            //Values exists
            //return or display the record on delete.cshtml form
            return View(appointment); // show confirmation page
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")] 
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        { //Get record by the provided Id, assign
            //the record to variable appointment.
            var appointment = await _db.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null) // Check if record has values.
            {
                //If no values exist
                return NotFound();
            }

            //If values exist
            //Remove the record from the database
            _db.Appointments.Remove(appointment);
            _db.SaveChanges();

            return RedirectToAction("Index"); //Display idenx view.
        }

        // EDIT (GET)
        [HttpGet]
        //HttpGet means, read from database
        public async Task<IActionResult> Edit(Guid id)
        {   
            //Use the provided id to find the record from database,
            //and pass it to appointment variable.
            var appointment = await _db.Appointments.FindAsync(id);

            if (appointment == null) //Check if the appointment exists.
            {
                return NotFound(); //Generate error message the record does not exist.
            }

            //if the record exits
            //Display the record on Edit cshtlm view form.
            //if it exits
            return View(appointment);
        }

        // EDIT (POST)
        [HttpPost]
        //HttpPost means write to the database
        public async Task<IActionResult> Edit(Guid id, AppointmentModel model)
        {
            //ModelState is a built-in dictionary that stores:
            //- Form input values sent from the user
            //- Validation errors for those values

            //ASP.NET MVC uses ModelState to decide:
            //- Is the form valid ?
            //- Should it show error messages ?
            //- Should it return the view back to the user ?
            if (!ModelState.IsValid)
            {
                return View(model); //return the same view if validation fails
            }

            //Assign the found record to appointment if ModelSate is valid
            var appointment = await _db.Appointments.FindAsync(id);

            // return an error message
            // if the record has no information or data.
            if (appointment == null)
            {
                return NotFound(); 
            }

            //if the record has information,
            // update record properties by the newly assigned values from edit.cshtml  form.
            appointment.Name = model.Name;
            appointment.Date = model.Date;
            appointment.DoctorRoom = model.DoctorRoom;
            appointment.PainLevel = model.PainLevel;

            //Save the change and redirect to index.cshtml view
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
