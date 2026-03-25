using WeatherAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        // In-memory list acting as the "database"
        private static List<Student> students = new List<Student>
        {
            new Student { Id = "NP01MS7A240036", Name = "Alice Sharma", Age = 20, Course = "Computer Science" },
            new Student { Id = "NP01MS7A240037", Name = "Bob Thapa",   Age = 22, Course = "Mathematics" }
        };

        // A) GET api/student/getall
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(students);
        }

        // B) GET api/student/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound(new { message = $"Student with ID '{id}' not found." });

            return Ok(student);
        }

        // C) POST api/student/add
        [HttpPost("add")]
        public IActionResult Add([FromBody] Student student)
        {
            if (student == null || string.IsNullOrWhiteSpace(student.Id) || string.IsNullOrWhiteSpace(student.Name))
                return BadRequest(new { message = "Invalid student data. Id and Name are required." });

            if (students.Any(s => s.Id == student.Id))
                return Conflict(new { message = $"Student with ID '{student.Id}' already exists." });

            students.Add(student);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        // D) PUT api/student/update
        [HttpPut("update")]
        public IActionResult Update([FromBody] Student updatedStudent)
        {
            if (updatedStudent == null || string.IsNullOrWhiteSpace(updatedStudent.Id))
                return BadRequest(new { message = "Invalid student data. Id is required." });

            var existing = students.FirstOrDefault(s => s.Id == updatedStudent.Id);
            if (existing == null)
                return NotFound(new { message = $"Student with ID '{updatedStudent.Id}' not found." });

            existing.Name   = updatedStudent.Name;
            existing.Age    = updatedStudent.Age;
            existing.Course = updatedStudent.Course;

            return Ok(new { message = "Student updated successfully.", student = existing });
        }

        // E) DELETE api/student/delete/{id}
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound(new { message = $"Student with ID '{id}' not found." });

            students.Remove(student);
            return Ok(new { message = $"Student '{student.Name}' deleted successfully." });
        }
    }
}