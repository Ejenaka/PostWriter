using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class PostsController : Controller
    {
        private readonly BlogDbContext db = new BlogDbContext();
        private const int POSTS_PER_PAGE = 10;

        public ActionResult Index(int page = 1)
        {
            if (page < 1)
                return Index(page: 1);

            int pagesToSkip = (page - 1) * POSTS_PER_PAGE;
            var posts = db.Posts
                .Include(p => p.LikedUsers)
                .Include(p => p.Comments)
                .ToList();

            var postsOnPage = posts.OrderByDescending(p => p.PublicationDate).Skip(pagesToSkip).Take(POSTS_PER_PAGE).ToList();
            int pagesCount = posts.Count % 10 == 0 ? posts.Count / 10 : (posts.Count / 10) + 1;
            ViewData["PagesCount"] = pagesCount;

            return View(postsOnPage);
        }

        public ActionResult Create()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Home");

            return View(new Post());
        }

        public ActionResult Details(int id = 0)
        {
            var post = db.Posts
                .Include(p => p.User)
                .Include(p => p.LikedUsers)
                .Include(p => p.Comments)
                .Where(p => p.ID == id)
                .First();

            if (post == null)
                return new EmptyResult();

            post.Views++;

            Comment comment = new Comment
            {
                PostID = id
            };

            PostComment postComment = new PostComment
            {
                Post = post,
                Comment = comment
            };

            TempData["post"] = post;
            db.SaveChanges();

            return View(postComment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                User currentUser = db.Users.Find((int)Session["UserID"]);
                currentUser.BlogsCount++;
                post.AuthorID = currentUser.ID;
                post.PublicationDate = DateTime.Now;

                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(post);
        }

        public ActionResult Like(int? id)
        {
            if (!id.HasValue)
                return new HttpNotFoundResult();

            var post = db.Posts.Include(p => p.LikedUsers).Where(p => p.ID == id).First();
            if (post == null)
                return new HttpNotFoundResult();

            int currentUserID = (int)Session["UserID"];
            User currentUser = db.Users.Include(u => u.LikedPosts).Where(u => u.ID == currentUserID).First();
            bool isLikedByUser = post.LikedUsers.Where(u => u.ID == currentUser.ID).Any();
            if (isLikedByUser)
            {
                Dislike(post, currentUser);
            }
            else
            {
                post.LikedUsers.Add(currentUser);
                post.Likes++;
                currentUser.LikedPosts.Add(post);
            }

            db.SaveChanges();

            return RedirectToAction("Details", "Posts", new { id });
        }

        private void Dislike(Post likedPost, User likedUser)
        {
            likedPost.LikedUsers.Remove(likedUser);
            likedPost.Likes--;
            likedUser.LikedPosts.Remove(likedPost);
        }

        [HttpPost]
        public ActionResult AddComment(PostComment postComment)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Home");

            if (!ModelState.IsValid)
                return RedirectToAction("Details", new { id = postComment.Post.ID });

            int currentUserID = (int)Session["UserID"];
            int currentPostID = ((Post)TempData["post"]).ID;
            User currentUser = db.Users.Where(u => u.ID == currentUserID).First();
            Post currentPost = db.Posts.Where(p => p.ID == currentPostID).First();
            currentUser.Comments.Add(postComment.Comment);
            currentPost.Comments.Add(postComment.Comment);

            postComment.Comment.PostDate = DateTime.Now;
            postComment.Comment.UserID = currentUserID;
            postComment.Comment.User = currentUser;

            db.Comments.Add(postComment.Comment);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = postComment.Comment.PostID });
        }
    }
}