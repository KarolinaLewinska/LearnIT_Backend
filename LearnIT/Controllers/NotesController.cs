using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LearnIT.Models;

namespace LearnIT.Controllers
{
    [RoutePrefix("learn-it/materialy")]
    public class NotesController : ApiController
    {
        private readonly databaselearnitEntities _dbContext = new databaselearnitEntities();

        [Route("wszystko")]
        [HttpGet]
        public IHttpActionResult GetAllNotes()
        {
            return Ok(_dbContext.Notes.ToList());
        }

        [Route("zagadnienie/{id}")]
        [HttpGet]
        public IHttpActionResult GetNote([FromUri] int id)
        {
            return Ok(_dbContext.Notes.Find(id));
        }

        [Route("dodaj-material")]
        [HttpPost]
        public IHttpActionResult AddNote([FromBody] Note newNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dbContext.Notes.Add(newNote);
            _dbContext.SaveChanges();
            return Ok(newNote.Id);
        }

        [Route("edycja-materialu/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateNotes([FromUri] int id, [FromBody] Note modifiedNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Nieprawidłowe żądanie");
            }
            var noteDb = _dbContext.Notes
                .Where(s => s.Id == modifiedNote.Id).FirstOrDefault<Note>();
            
            if (noteDb != null)
            {
                noteDb.Title = modifiedNote.Title;
                noteDb.Category = modifiedNote.Category;
                noteDb.KeyWords = modifiedNote.KeyWords;
                noteDb.Description = modifiedNote.Description;
                noteDb.Link = modifiedNote.Link;
                noteDb.Date = modifiedNote.Date;
                noteDb.Author = modifiedNote.Author;
                noteDb.University = modifiedNote.University;
                noteDb.Email = modifiedNote.Email;

                _dbContext.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            
            return Ok("Zaktualizowano dane dotyczące materiału o id: " + id);
        }

     
        [Route("usun-material/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var NoteToDelete = _dbContext.Notes.Find(id);
            _dbContext.Notes.Remove(NoteToDelete);
            _dbContext.SaveChanges();
            return Ok("Usunięto dane");
        }
    }
}
