using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;

namespace NotesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private static readonly List<Note> _notes = new();
        private static int _currentId = 1;

        // POST /api/notes
        [HttpPost]
        public IActionResult CreateNote([FromBody] CreateNoteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var note = new Note
            {
                Id = _currentId++,
                Title = dto.Title,
                Text = dto.Text,
                CreatedAt = DateTime.Now
            };

            _notes.Add(note);
            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        // GET /api/notes
        [HttpGet]
        public IActionResult GetAllNotes() => Ok(_notes);

        // GET /api/notes/{id}
        [HttpGet("{id}")]
        public IActionResult GetNoteById(int id)
        {
            var note = _notes.FirstOrDefault(n => n.Id == id);
            return note == null
                ? NotFound(new { message = $"Заметка {id} не найдена" })
                : Ok(note);
        }

        // DELETE /api/notes/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var note = _notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
                return NotFound(new { message = $"Заметка {id} не найдена" });

            _notes.Remove(note);
            return NoContent();
        }
    }
}