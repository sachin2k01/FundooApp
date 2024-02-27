using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using NLog;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using ShareFile.Api.Client.Models;
using System.Drawing;
using System.IO.Compression;
using System.Runtime.CompilerServices;


namespace RepositoryLayer.Services
{
    public class UserNotesRepo:IUserNotesRepo
    {
        private readonly FundooContext _fundooContext;

        private readonly ILogger<UserNotesRepo> _logger;
        public UserNotesRepo(FundooContext fundooContext, ILogger<UserNotesRepo> logger)
        {
            this._fundooContext = fundooContext;
            _logger = logger;
            _logger = logger;
        }

        public UserNotesEntity CreateUserNotes(UserNotesModel notes, int userId)
        {
            if(userId!=0)
            {
                _logger.LogInformation("Inside the create user Nodes");
                //IEnumerable<ImageEntity> imageList = null;
                UserEntity user=_fundooContext.Users.FirstOrDefault(u=>u.UserId==userId);
                if(user!=null)
                {
                    UserNotesEntity notesEntity = new UserNotesEntity
                    {
                        Title = notes.Title,
                        Description = notes.Description,
                        Color = notes.Color,
                        Remainder = notes.Remainder,
                        IsArchive = notes.IsArchive,
                        IsPinned = notes.IsPinned,
                        IsTrash = notes.IsTrash,
                        CreatedAt=DateTime.Now,
                        ModifiedAt=DateTime.Now,
                        UserId = userId
                    };

                    _fundooContext.UserNotes.Add(notesEntity);
                    _fundooContext.SaveChanges();
                    //if (notes.ImagePaths!=null)
                    //{
                    //    imageList = AddImages(notesEntity.NoteId, userId, notes.ImagePaths);
                    //}
                        return notesEntity;
                }
                else
                {
                    return null;
                }              
            }
            else
            {
                //_logger.LogInformation("Exit from the CreateUserNote");
                return null;
            }
           
        }

        public async Task<string> UploadImage(IFormFile formFile)
        {
            try
            {
                string originalFileName = formFile.FileName;
                string uniqueFileName = $"{Guid.NewGuid()}_{DateTime.Now.Ticks}{Path.GetExtension(originalFileName)}";

                string filePath = FileHelper.GetFilePath(uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ImageEntity> AddImages(int noteId, int userId, ICollection<IFormFile> files)
        {
            try
            {
                UserNotesEntity resNote = null;
                var user = _fundooContext.UserNotes.FirstOrDefault(n => n.UserId == userId);
                if (user != null)
                {
                    resNote = _fundooContext.UserNotes.Where(n => n.UserId == userId && n.NoteId == noteId).FirstOrDefault();
                    if (resNote != null)
                    {
                        IList<ImageEntity> images = new List<ImageEntity>();
                        foreach (var file in files)
                        {
                            ImageEntity img = new ImageEntity();
                            var uploadImageRes = UploadImage(file);
                            img.NoteId = noteId;
                            img.ImageUrl = uploadImageRes.ToString();
                            img.ImageName = file.FileName;
                            images.Add(img);
                            _fundooContext.NoteImage.Add(img);
                            _fundooContext.SaveChanges();
                            resNote.ModifiedAt = DateTime.Now;
                            _fundooContext.UserNotes.Update(resNote);
                            _fundooContext.SaveChanges();
                        }
                        return images;
                    }
                    else
                    {
                        return null;
                    }
                }

                // Return a default value if the 'if (user != null)' condition is not satisfied
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw;
            }
        }

        public UserNotesEntity GetNotesById(int noteId, int userId)
        {
            _logger.LogInformation("Inside the GetNoteByid()");
           UserNotesEntity noteinfo= _fundooContext.UserNotes.FirstOrDefault(x=>x.NoteId==noteId&&x.UserId==userId);
            _logger.LogWarning("Exit from GetNoteById()");
            return noteinfo;
        }

        public List<UserNotesEntity> GetAllNodes()
        {
            var nodes = new List<UserNotesEntity>();
            nodes=_fundooContext.UserNotes.ToList();
            return nodes;
        }

        public List<UserNotesEntity> GetUserNotesById(int userId)
        {
            try
            {
                _logger.LogInformation("Inside the GetUserById");
                var nodes = new List<UserNotesEntity>();
                nodes = _fundooContext.UserNotes.Where(x => x.UserId == userId).ToList();
                return nodes;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
                throw ex;
                
            }
        }

        public UserNotesEntity UpdateNotes(int noteId, int userId, NotesUpdateModel notesModel)
        {
            var userNotes = _fundooContext.UserNotes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
            if(userNotes != null)
            {
                userNotes.Title=notesModel.Title ?? userNotes.Title;
                userNotes.Description=notesModel.Description ?? userNotes.Description;
                userNotes.ModifiedAt=DateTime.Now;
                _fundooContext.SaveChanges();

                return userNotes;
            }
            else
            {
                return null;
            }
        }

        public UserNotesEntity DeleteNode(int id,int userId)
        {
            var delNode=_fundooContext.UserNotes.FirstOrDefault(x=>x.NoteId==id && x.UserId==userId);
            if(delNode != null)
            {
                _fundooContext.UserNotes.Remove(delNode);
                _fundooContext.SaveChanges();
                return delNode;
            }
            else
            {
                return null;
            }
        }

        public UserNotesEntity ArchieveNotes(int userId,int noteId)
        {
            var notes=_fundooContext.UserNotes.FirstOrDefault(x=>x.UserId==userId && x.NoteId==noteId);
            if(notes != null)
            {
                notes.IsArchive=!notes.IsArchive;
                _fundooContext.SaveChanges();
                return notes;
            }
            else
            {
                return null;
            }
        }


        public UserNotesEntity TrashNotes(int userId, int noteId)
        {
            var notes = _fundooContext.UserNotes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
            if (notes != null)
            {
                notes.IsTrash = !notes.IsTrash;
                _fundooContext.SaveChanges();
                return notes;
            }
            else
            {
                return null;
            }
        }

        public UserNotesEntity AddNoteColor(int userId,int noteId,string color) 
        {
            var notes = _fundooContext.UserNotes.FirstOrDefault(x => x.UserId == userId && x.NoteId == noteId);
            if(notes!=null)
            {
                notes.Color=color;
                _fundooContext.SaveChanges();
                return notes;
            }
            else
            {
                return null;
            }
        }

    }
}
