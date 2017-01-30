using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using Microsoft.AspNet.Identity;
using MvcPL.Infrastructure.Extensions;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize]
    public class UserAccountController : Controller
    {
        private readonly IUserService userservice;
        private readonly IAlbumService albumservice;
        private readonly IPhotoService photoservice;
        private readonly ILikeService likeservice;

        public UserAccountController(IUserService userService, IAlbumService albumService, IPhotoService photoService,ILikeService likeService)
        {
            this.userservice = userService;
            this.albumservice = albumService;
            this.photoservice = photoService;
            this.likeservice = likeService;
        }

        public ActionResult UserPage(int? id)
        {
            if (id == null)
            {
                var idIsAuthenticated = this.GetUserId();
                var userAuthenticatedAlbum = new UserAlbumModel(
                    userservice.GetUserEntity(idIsAuthenticated).ToMvcUser(),
                    albumservice.GetByUserId(idIsAuthenticated).ToMvcAlbums());
                return View(userAuthenticatedAlbum);
            }
            var userAlbum = new UserAlbumModel(userservice.GetUserEntity(id.Value).ToMvcUser(),
                albumservice.GetByUserId(id.Value).ToMvcAlbums());
            return View(userAlbum);

        }

        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            var user = userservice.GetUserEntity(id);
            return PartialView(user.ToMvcUser());

        }

        [HttpPost]
        public ActionResult EditProfile(UserViewModel userdata)
        {
            userservice.ChangeProfileInfo(userdata.ToBllUser());
            return View("UserPage",
                new UserAlbumModel(userservice.GetUserEntity(userdata.UserId).ToMvcUser(),
                    albumservice.GetByUserId(userdata.UserId).ToMvcAlbums()));

        }

        [HttpGet]
        public ActionResult Upload(int id)
        {
            return View("LoadProfileImage", userservice.GetUserEntity(id).ToMvcUser());
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload, int id)
        {
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                userservice.ChangeProfilePhoto(id, $"/Content/Images/UsersImages/profile{id}.jpg" + fileName);
                upload.SaveAs(Server.MapPath($"~/Content/Images/UsersImages/profile{id}.jpg" + fileName));

            }
            return View("UserPage",
                new UserAlbumModel(userservice.GetUserEntity(id).ToMvcUser(), albumservice.GetByUserId(id).ToMvcAlbums()));
        }

        [HttpGet]
        public ActionResult CreateAlbum(int id)
        {
            return View(userservice.GetUserEntity(id).ToMvcUser());
        }

        [HttpPost]
        public ActionResult CreateAlbum(AlbumViewModel album)
        {
            album.User = userservice.GetUserEntity(album.UserId).ToMvcUser();
            album.AlbumImage = "/Content/Images/insta_icon.png";
            albumservice.CreateAlbum(album.ToBllAlbum());
            return RedirectToAction("UserPage", "UserAccount", new {id = album.UserId});
        }

        public ActionResult AlbumView(int id)
        {
            var album = albumservice.GetAlbumEntity(id).ToMvcAlbum();
            var photos = photoservice.GetByAlbumId(id).ToMvcPhotos();
            return View(new AlbumPhotoModel(album, photos));
        }

        public ActionResult AddPhoto(int id)
        {
            return PartialView(albumservice.GetAlbumEntity(id).ToMvcAlbum());
        }

        [HttpPost]
        public ActionResult AddPhoto(IEnumerable<HttpPostedFileBase> uploads, int id)
        {
            foreach (var file in uploads)
            {
                if (file != null)
                {
                    string fileName = System.IO.Path.GetFileName(file.FileName);
                    photoservice.AddToAlbum(id,
                        $"/Content/Images/UsersImages/profile{albumservice.GetAlbumEntity(id).UserId}_{id}" + fileName);
                    file.SaveAs(
                        Server.MapPath(
                            $"~/Content/Images/UsersImages/profile{albumservice.GetAlbumEntity(id).UserId}_{id}" +
                            fileName));
                }
            }
            return RedirectToAction("AlbumView", "UserAccount", new {id});
        }

        [HttpPost]
        public ActionResult SearchResult(string searchString)
        {
            var users = userservice.GetAllUserEntities();
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(user => user.UserName.Contains(searchString));
            }
            return View(users.Select(user => user.ToMvcUser()));
        }

        [HttpGet]
        public ActionResult Rating()
        {
            return View("UserPage");
        }
        //[HttpGet]
        //public ActionResult Rating(int id)
        //{
        //    List<LikeEntity> likes = likeservice.GetAllLikeEntities().Where(v => v.PhotoId == id).ToList();
        //    int nooflikes = likes.Count;
        //    ViewBag.noLikes = nooflikes;
        //    return Json(ViewBag.noLikes, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public void Rating(int id)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated == true)
            {
                var like = new LikeViewModel() {PhotoId = id, UserId = this.GetUserId()};
                

                var userLike =
                    likeservice.GetAllLikeEntities()
                    .Where(l => l.UserId == like.UserId 
                    && l.PhotoId == like.PhotoId);
                if (userLike.Count() == 0)
                {
                    likeservice.CreateLike(like.ToBllLike());
                }
                else if(userLike.Count()==1)
                {
                    var likeDelete =
                        likeservice.GetAllLikeEntities()
                            .FirstOrDefault(l => l.UserId == like.UserId && l.PhotoId == like.PhotoId);
                    likeservice.DeleteLike(likeDelete);

                }
            }
            else
            {

            }
        }
    }
}