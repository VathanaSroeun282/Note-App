using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using NotesAPI.Models;

namespace NotesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private IConfiguration _configuration;
        public NoteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection()
        {
            return new SqlConnection(_configuration.GetConnectionString("dbNote"));
        }
        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            using var db = Connection();
            var notes = await db.QueryAsync<Note>("SELECT * FROM Notes");
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(int id)
        {
            using var db = Connection();
            var note = await db.QuerySingleOrDefaultAsync<Note>("Select * from Notes where Id = @Id", new { Id = id });
            return note != null ? Ok(note) : NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> CreateNote(Note note)
        {
            using var db = Connection();
            note.CreateDate = DateTime.Now;
            var query = "insert into Notes (Title, Content, CreateDate) values (@title, @content, @createdate)";
            await db.ExecuteAsync(query, new { title = note.Title, content = note.Content, createdate = note.CreateDate });
            return Ok("Create successs!");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(Note note, int id)
        {
            using var db = Connection();
            note.UpdateDate = DateTime.Now;
            var query = "update Notes set Title = @title, Content = @content, UpdateDate = @updatedate where Id = @id";
            await db.ExecuteAsync(query, new { note.Title, note.Content, note.UpdateDate, Id = id });
            return Ok("Note updated!");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            using var db = Connection();    
            var query = "delete from Notes where Id = @id";
            db.ExecuteAsync(query, new { Id = id });
            return Ok("Delete successs!");
        }
    }
}