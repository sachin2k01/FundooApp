using Microsoft.AspNetCore.Http;
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

        //public async Task<string> UploadImage(IFormFile _formFile)
        //{
        //    string FileName = " ";
        //    try
        //    {
        //        FileInfo fileInfo = new FileInfo(_formFile.FileName);
        //        FileName = _formFile.FileName + "_" + DateTime.Now.Ticks.ToString() + fileInfo.Extension;
        //        var _GetFilePath = FileHelper.GetFilePath(FileName);
        //        using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
        //        {
        //            await _formFile.CopyToAsync(_FileStream);
        //        }
        //        return FileName;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public async Task<string> UploadImage(IFormFile formFile)
        {
            try
            {
                string originalFileName = formFile.FileName;
                string uniqueFileName = $"{Guid.NewGuid()}_{DateTime.Now.Ticks}{Path.GetExtension(originalFileName)}";

                string filePath = Path.Combine(FileHelper.GetFilePath(""), uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                return uniqueFileName;
            }
            catch (Exception ex)
            {
                // Log the exception details, and either rethrow it or return a meaningful result
                // Logging example:
                // _logger.LogError($"Error uploading image: {ex}");
                throw;
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


    }
}
