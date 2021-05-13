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
            try
            {
                var notesList = _dbContext.Notes.ToList();
                if (notesList == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Brak danych w bazie", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                    //return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Nieprawidłowe żądanie", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.BadRequest
                    };
                    throw new HttpResponseException(response);
                    //return BadRequest("Nieprawidłowe żądanie");
                }

                return Ok(notesList);
            }
            catch(Exception exc)
            {
                throw exc;
            }
        }

        [Route("zagadnienie/{id}")]
        [HttpGet]
        public IHttpActionResult GetNote([FromUri] int id)
        {
            try
            {
                var chosenNote = _dbContext.Notes.Find(id);
                if (chosenNote == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Podane zagadnienie nie istnieje", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };

                    throw new HttpResponseException(response);
                    //return NotFound();

                }
                if (!ModelState.IsValid)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Nieprawidłowe żądanie", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.BadRequest
                    };
                    throw new HttpResponseException(response);
                    //return BadRequest("Nieprawidłowe żądanie");
                }
                return Ok(chosenNote);
            } 
            catch(Exception exc)
            {
                throw exc;
            }    
        }

        [Route("dodaj-material")]
        [HttpPost]
        public IHttpActionResult AddNote([FromBody] Note newNote)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Nieprawidłowe żądanie", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.BadRequest
                    };
                    throw new HttpResponseException(response);
                    //return BadRequest(ModelState);
                }
                _dbContext.Notes.Add(newNote);
                _dbContext.SaveChanges();

                return Ok("Pomyślnie dodano materiał o id: " + newNote.Id);
            }
            catch(Exception exc)
            {
                throw exc;
            }  
        }

        [Route("edycja-materialu/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateNotes([FromUri] int id, [FromBody] Note modifiedNote)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Nieprawidłowe żądanie", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.BadRequest
                    };
                    throw new HttpResponseException(response);
                    //return BadRequest("Nieprawidłowe żądanie");
                }
                var noteDb = _dbContext.Notes
                    .Where(s => s.Id == id).FirstOrDefault<Note>();

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
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Podane zagadnienie nie istnieje", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                    };
                    throw new HttpResponseException(response);
                    //return NotFound();
                }

                return Ok("Zaktualizowano dane dotyczące materiału o id: " + id);
            }
            catch (Exception exc)
            {
                //przy nieprawidłowym id robi catch
                throw exc;
            } 
        }

        [Route("usun-material/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            try
            {
                var NoteToDelete = _dbContext.Notes.Find(id);

                if (NoteToDelete == null)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Podane zagadnienie nie istnieje", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.NotFound
                         
                    };
                    throw new HttpResponseException(response);
                    //return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Nieprawidłowe żądanie", System.Text.Encoding.UTF8, "text/plain"),
                        StatusCode = HttpStatusCode.BadRequest
                    };
                    throw new HttpResponseException(response);
                    //return BadRequest("Nieprawidłowe żądanie");
                }

                _dbContext.Notes.Remove(NoteToDelete);
                _dbContext.SaveChanges();
                return Ok("Usunięto dane");
            }
            catch(Exception exc)
            {
                //przy nieprawidłowym id wywala catch
                throw exc;
            }
        }
    }
}
