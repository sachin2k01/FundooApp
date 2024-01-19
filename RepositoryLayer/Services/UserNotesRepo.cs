using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
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
        public UserNotesRepo(FundooContext fundooContext)
        {
            this._fundooContext = fundooContext;
            
        }

        public UserNotesEntity CreateUserNotes(UserNotesModel notes, int userId)
        {
            if(userId!=0)
            {
                IEnumerable<ImageEntity> imageList = null;
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
                    if (notes.ImagePaths!=null)
                    {
                        imageList = AddImages(notesEntity.NoteId, userId, notes.ImagePaths);
                    }
                        return notesEntity;
                }
                else
                {
                    return null;
                }              
            }
            else
            {
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
                throw ex;
            }
        }

        public UserNotesEntity GetNotesById(int noteId, int userId)
        {
           UserNotesEntity noteinfo= _fundooContext.UserNotes.FirstOrDefault(x=>x.NoteId==noteId&&x.UserId==userId);
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
                var nodes = new List<UserNotesEntity>();
                nodes = _fundooContext.UserNotes.Where(x => x.UserId == userId).ToList();
                return nodes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public UserNotesEntity UpdateNotes(int  userId, int noteId, NotesUpdateModel notesModel)
        {
            var userNotes = _fundooContext.UserNotes.FirstOrDefault(x => x.NoteId == noteId && x.UserId == userId);
            if(userNotes != null)
            {
                userNotes.Title=notesModel.Title ?? userNotes.Title;
                userNotes.Description=notesModel.Description ?? userNotes.Description;
                userNotes.Color=notesModel.Color ?? userNotes.Color;
                userNotes.ModifiedAt=DateTime.Now;
                _fundooContext.SaveChanges();

                return userNotes;
            }
            else
            {
                return null;
            }
        }

        public string DeleteNode(int id,int userId)
        {
            var delNode=_fundooContext.UserNotes.FirstOrDefault(x=>x.NoteId==id && x.UserId==userId);
            if(delNode != null)
            {
                _fundooContext.UserNotes.Remove(delNode);
                _fundooContext.SaveChanges();
                return "Node Deleted Successfully";
            }
            else
            {
                return "Invalid Node";
            }

        }
    }
}
