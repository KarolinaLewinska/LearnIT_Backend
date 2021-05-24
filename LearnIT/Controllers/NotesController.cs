using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using LearnIT.Models;

namespace LearnIT.Controllers
{
    [RoutePrefix("learn-it/materials")]
    [EnableCors(origins: "http://localhost:8080", headers: "*", methods: "*")]
    public class NotesController : ApiController
    {
        private readonly databaselearnitEntities _dbContext = new databaselearnitEntities();

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllNotes()
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateErrorResponse
                    (HttpStatusCode.BadRequest, ModelState));
            }

            try
            {
                var notesList = _dbContext.Notes.ToList();
                if (notesList == null)
                {
                    return ResponseMessage(Request.CreateErrorResponse
                        (HttpStatusCode.NotFound, "Data not found"));
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, notesList));
            }
            catch(Exception exc)
            {
                while (exc.InnerException != null)
                    exc = exc.InnerException;

                return ResponseMessage(Request.CreateResponse
                    (HttpStatusCode.InternalServerError, exc.Message));
            }
        }

        [Route("material/{id}")]
        [HttpGet]
        public IHttpActionResult GetNote([FromUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateErrorResponse
                    (HttpStatusCode.BadRequest, ModelState));
            }

            try
            {
                var selectedNote = _dbContext.Notes.Find(id);
                if (selectedNote == null)
                {
                    return ResponseMessage(Request.CreateErrorResponse
                        (HttpStatusCode.NotFound, "Not found any material with ID: " + id));
                }
                
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, selectedNote));
            } 
            catch(Exception exc)
            {
                while(exc.InnerException != null)
                    exc = exc.InnerException;

                return ResponseMessage(Request.CreateResponse
                    (HttpStatusCode.InternalServerError, exc.Message));
            }
        }

        [Route("add-material")]
        [HttpPost]
        public IHttpActionResult AddNote([FromBody] Note newNote)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateErrorResponse
                    (HttpStatusCode.BadRequest, ModelState));
            }

            try
            {
                var existingTitle = _dbContext.Notes.SingleOrDefault(x => x.Title == newNote.Title);
                var existingLink = _dbContext.Notes.SingleOrDefault(x=> x.Link == newNote.Link);
                    
                if (existingTitle != null || existingLink != null)
                {
                    return ResponseMessage(Request.CreateErrorResponse
                        (HttpStatusCode.Conflict, "Material with the same title or link already exists"));
                }

                _dbContext.Notes.Add(newNote);
                _dbContext.SaveChanges();

                return ResponseMessage(Request.CreateResponse
                    (HttpStatusCode.Created, "Successfully added material with ID: " + newNote.Id));
            }
            catch (Exception exc)
            {
                while (exc.InnerException != null)
                    exc = exc.InnerException;

                return ResponseMessage(Request.CreateResponse
                    (HttpStatusCode.InternalServerError, exc.Message));
            }     
        }

        [Route("edit-material/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateNote([FromUri] int id, [FromBody] Note modifiedNote)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateErrorResponse
                    (HttpStatusCode.BadRequest, ModelState));
            }

            try
            {
                var noteToUpdate = _dbContext.Notes
                    .Where(s => s.Id == id).FirstOrDefault<Note>();

                if (noteToUpdate != null)
                {
                    noteToUpdate.Title = modifiedNote.Title;
                    noteToUpdate.Category = modifiedNote.Category;
                    noteToUpdate.KeyWords = modifiedNote.KeyWords;
                    noteToUpdate.Description = modifiedNote.Description;
                    noteToUpdate.Link = modifiedNote.Link;
                    noteToUpdate.Date = modifiedNote.Date;
                    noteToUpdate.Author = modifiedNote.Author;
                    noteToUpdate.University = modifiedNote.University;
                    noteToUpdate.Email = modifiedNote.Email;

                    _dbContext.SaveChanges();
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse
                        (HttpStatusCode.NotFound, "Not found any material with ID: " + id));
                }
                return ResponseMessage(Request.CreateResponse
                    (HttpStatusCode.OK, "Successfully updated material with ID: " + id));
            }
            catch (Exception exc)
            {
                while (exc.InnerException != null)
                    exc = exc.InnerException;

                return ResponseMessage(Request.CreateResponse
                    (HttpStatusCode.InternalServerError, exc.Message));
            }            
        }

        [Route("delete-material/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteNote([FromUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateErrorResponse
                    (HttpStatusCode.BadRequest, ModelState));
            }

            try
            {
                var noteToDelete = _dbContext.Notes.Find(id);

                if (noteToDelete == null)
                {
                    return ResponseMessage(Request.CreateErrorResponse
                        (HttpStatusCode.NotFound, "Not found any material with ID: " + id));
                }
                    
                _dbContext.Notes.Remove(noteToDelete);
                _dbContext.SaveChanges();

                return ResponseMessage(Request.CreateResponse
                    (HttpStatusCode.OK, "Successfully deleted material"));
            }
            catch (Exception exc)
            {
                while (exc.InnerException != null)
                    exc = exc.InnerException;

                return ResponseMessage(Request.CreateResponse
                    (HttpStatusCode.InternalServerError, exc.Message));
            } 
        }
    }
}
