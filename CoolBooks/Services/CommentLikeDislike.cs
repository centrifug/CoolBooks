using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CoolBooks.Data;
using CoolBooks.Models;
using CoolBooks.ViewModels;


namespace CoolBooks.Data
{
    public interface test2
    {
        public string Like(int id, bool status, string user);
        public int? Getlikecounts(int id);
        public int? Getdislikecounts(int id);
        public List<CommentLikes> GetallUser(int id);
    }
    public class CommentLikeDislike : test2
    {
        private readonly CoolBooksContext _context;
        private readonly UserManager<CoolBooksUser> userManager;
        private readonly SignInManager<CoolBooksUser> signInManager;


        public CommentLikeDislike(CoolBooksContext context, UserManager<CoolBooksUser> userManager, SignInManager<CoolBooksUser> signInManager)
        {
            _context = context; 
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public string Like(int id, bool status, string user)
        {
            //using (var db = _context)
            var db = _context;
            {
                var comment = db.Comment.FirstOrDefault(x => x.Id == id);
                var toggle = false;
                //Likes? like = db.Likes.FirstOrDefault(x => x.ReviewId == id);
                CommentLikes? like = db.CommentLikes.FirstOrDefault(x => x.CommentId == id && x.UserId == user);
                // https://localhost:7107/Reviews/Like/?id=1&status=true fel länknamn när man trycker på knapp
                // userid blir null?? fix it!

                if (like == null)
                {
                    like = new CommentLikes();
                    like.UserId = user;  //userManager.GetUserId(User);
                    //string test = userManager.GetUserId(User);
                    //string test2 = userManager.GetUserAsync(User).Result.ToString();
                    like.IsLike = status;
                    like.CommentId = id;
                    if (status)
                    {

                        if (comment.LikeCount == null)
                        {
                            //review.LikeCount = review.LikeCount ? ? 0 + 1;
                            //review.DisLikeCount = review.DisLikeCount ? ? 0;

                            // review.LikeCount = 0; // Workaround?
                        }
                        else
                        {
                            comment.LikeCount = comment.LikeCount + 1;
                        }
                    }
                    else
                    {
                        if (comment.DisLikeCount == null)
                        {
                            //review.DisLikeCount = review.DisLikeCount ? ? 0 + 1;
                            //review.LikeCount = review.LikeCount ? ? 0;
                            
                            //review.DisLikeCount = 0; // Workaround?
                        }
                        else
                        {
                            comment.DisLikeCount = comment.DisLikeCount + 1;
                        }
                    }
                    db.CommentLikes.Add(like);
                }
                else
                {
                    toggle = true;
                }
                if (toggle)
                {
                    like.UserId = user;  //userManager.GetUserId(User);
                    like.IsLike = status;
                    //like.Id = id;
                    if (status)
                    {

                        comment.LikeCount = comment.LikeCount + 1;
                        if (comment.DisLikeCount == 0 || comment.DisLikeCount < 0)
                        {
                            comment.DisLikeCount = 0;
                        }
                        else
                        {
                            comment.DisLikeCount = comment.DisLikeCount - 1;
                        }
                    }
                    else
                    {

                        comment.DisLikeCount = comment.DisLikeCount + 1;
                        if (comment.LikeCount == 0 || comment.LikeCount < 0)
                        {
                            comment.LikeCount = 0;
                        }
                        else
                        {
                            comment.LikeCount = comment.LikeCount - 1;
                        }
                    }
                }
                db.SaveChanges();
                return comment.LikeCount + "/" + comment.DisLikeCount;
            }
        }

        public int? Getlikecounts(int id)
        {
            //using (var db = _context)
                var db = _context;
            {
                var count = (from x in db.Comment where (x.Id == id && x.LikeCount != null) select x.LikeCount).FirstOrDefault();
                return count;
            }
        }

        public int? Getdislikecounts(int id)
        {
            //using (var db = _context)
            {
                var db = _context;
                var count = (from x in db.Comment where x.Id == id && x.DisLikeCount != null select x.DisLikeCount).FirstOrDefault();
                return count;
            }
        }

        public List<CommentLikes> GetallUser(int id)
        {
            //using (var db = _context)
            {
                var db = _context;
                var count = (from x in db.CommentLikes where x.CommentId == id select x).ToList();
                return count;
            }
        }
    }
}

