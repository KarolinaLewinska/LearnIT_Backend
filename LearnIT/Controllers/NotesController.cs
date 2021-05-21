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
            try
            {
                var notesList = _dbContext.Notes.ToList();
                if (notesList == null)
                {
                    return ResponseMessage(Request.CreateErrorResponse
                        (HttpStatusCode.NotFound, "Data not found"));
                }
                if (!ModelState.IsValid)
                {
                    return ResponseMessage(Request.CreateErrorResponse
                           (HttpStatusCode.BadRequest, ModelState));
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, notesList));
            }
            catch(Exception exc)
            {
                throw exc.InnerException;
            }
        }

        [Route("material/{id}")]
        [HttpGet]
        public IHttpActionResult GetNote([FromUri] int id)
        {
            try
            {
                var chosenNote = _dbContext.Notes.Find(id);
                if (chosenNote == null)
                {
                    return ResponseMessage(Request.CreateErrorResponse
                        (HttpStatusCode.NotFound, "Not found any material with ID: " + id));
                }
                if (!ModelState.IsValid)
                {
                    return ResponseMessage(Request.CreateErrorResponse
                           (HttpStatusCode.BadRequest, ModelState));
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, chosenNote));
            } 
            catch(Exception exc)
            {
                throw exc.InnerException;
            }    
        }

        [Route("add-material")]
        [HttpPost]
        public IHttpActionResult AddNote([FromBody] Note newNote)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return ResponseMessage(Request.CreateErrorResponse
                                (HttpStatusCode.BadRequest, ModelState));
                    }
                    _dbContext.Notes.Add(newNote);
                    transaction.Commit();
                    _dbContext.SaveChanges();

                    return ResponseMessage(Request.CreateResponse
                        (HttpStatusCode.OK, "Successfully added material with ID: " + newNote.Id));
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                    throw exc.InnerException;
                }
            }    
        }

        [Route("edit-material/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateNotes([FromUri] int id, [FromBody] Note modifiedNote)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return ResponseMessage(Request.CreateErrorResponse
                                (HttpStatusCode.BadRequest, ModelState));
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
                        transaction.Commit();
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
                    transaction.Rollback();
                    throw exc.InnerException;
                }
            }

               
        }

        [Route("delete-material/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var NoteToDelete = _dbContext.Notes.Find(id);

                    if (NoteToDelete == null)
                    {
                        return ResponseMessage(Request.CreateErrorResponse
                            (HttpStatusCode.NotFound, "Not found any material with ID: " + id));
                    }
                    if (!ModelState.IsValid)
                    {
                        return ResponseMessage(Request.CreateErrorResponse
                                 (HttpStatusCode.BadRequest, ModelState));
                    }

                    _dbContext.Notes.Remove(NoteToDelete);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, "Successfully deleted material"));
                }
                catch (Exception exc)
                {
                    transaction.Rollback();
                    throw exc.InnerException;
                }
            }

                
        }
    }
}
