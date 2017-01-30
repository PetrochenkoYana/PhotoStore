using System;
using System.Collections.Generic;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using DAL.Interfacies.Repository;
using BLL.Mappers;

namespace BLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork uow;
        private readonly IPhotoRepository photoRepository;

        public PhotoService(IUnitOfWork uow, IPhotoRepository repository)
        {
            this.uow = uow;
            this.photoRepository = repository;
        }
        public void CreatePhoto(PhotoEntity photo)
        {
            photoRepository.Create(photo.ToDal());
            uow.Commit();
        }

        public void DeletePhoto(PhotoEntity photo)
        {
            photoRepository.Delete(photo.ToDal());
        }

        public IEnumerable<PhotoEntity> GetByAlbumId(int albumId)
        {
            return photoRepository.GetByAlbumId(albumId).ToBll();
        }

        public PhotoEntity GetPhotoEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void AddToAlbum(int albumId,string path)
        {
            photoRepository.AddToAlbum(albumId,path);
            uow.Commit();
        }
    }
}
