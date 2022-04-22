using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CoolBooks.Data;
using CoolBooks.Models;
using CoolBooks.ViewModels;


namespace CoolBooks.Data
{
    public interface test
    {
        public string Like(int id, bool status, string user);
        public int? Getlikecounts(int id);
        public int? Getdislikecounts(int id);
        public List<ReviewLikes> GetallUser(int id);
    }
    public class ReviewLikeDislike : test
    {
        private readonly CoolBooksContext _context;
        private readonly UserManager<CoolBooksUser> userManager;
        private readonly SignInManager<CoolBooksUser> signInManager;


        public ReviewLikeDislike(CoolBooksContext context, UserManager<CoolBooksUser> userManager, SignInManager<CoolBooksUser> signInManager)
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
                var review = db.Review.FirstOrDefault(x => x.Id == id);
                var toggle = false;
                //Likes? like = db.Likes.FirstOrDefault(x => x.ReviewId == id);
                ReviewLikes? like = db.ReviewLikes.FirstOrDefault(x => x.ReviewId == id && x.UserId == user);
                // https://localhost:7107/Reviews/Like/?id=1&status=true fel länknamn när man trycker på knapp
                // userid blir null?? fix it!

                if (like == null)
                {
                    like = new ReviewLikes();
                    like.UserId = user;  //userManager.GetUserId(User);
                    //string test = userManager.GetUserId(User);
                    //string test2 = userManager.GetUserAsync(User).Result.ToString();
                    like.IsLike = status;
                    like.ReviewId = id;
                    if (status)
                    {

                        if (review.LikeCount == null)
                        {
                            //review.LikeCount = review.LikeCount ? ? 0 + 1;
                            //review.DisLikeCount = review.DisLikeCount ? ? 0;

                            // review.LikeCount = 0; // Workaround?
                        }
                        else
                        {
                            review.LikeCount = review.LikeCount + 1;
                        }
                    }
                    else
                    {
                        if (review.DisLikeCount == null)
                        {
                            //review.DisLikeCount = review.DisLikeCount ? ? 0 + 1;
                            //review.LikeCount = review.LikeCount ? ? 0;
                            
                            //review.DisLikeCount = 0; // Workaround?
                        }
                        else
                        {
                            review.DisLikeCount = review.DisLikeCount + 1;
                        }
                    }
                    db.ReviewLikes.Add(like);
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

                        review.LikeCount = review.LikeCount + 1;
                        if (review.DisLikeCount == 0 || review.DisLikeCount < 0)
                        {
                            review.DisLikeCount = 0;
                        }
                        else
                        {
                            review.DisLikeCount = review.DisLikeCount - 1;
                        }
                    }
                    else
                    {

                        review.DisLikeCount = review.DisLikeCount + 1;
                        if (review.LikeCount == 0 || review.LikeCount < 0)
                        {
                            review.LikeCount = 0;
                        }
                        else
                        {
                            review.LikeCount = review.LikeCount - 1;
                        }
                    }
                }
                db.SaveChanges();
                return review.LikeCount + "/" + review.DisLikeCount;
            }
        }

        public int? Getlikecounts(int id)
        {
            //using (var db = _context)
                var db = _context;
            {
                var count = (from x in db.Review where (x.Id == id && x.LikeCount != null) select x.LikeCount).FirstOrDefault();
                return count;
            }
        }

        public int? Getdislikecounts(int id)
        {
            //using (var db = _context)
            {
                var db = _context;
                var count = (from x in db.Review where x.Id == id && x.DisLikeCount != null select x.DisLikeCount).FirstOrDefault();
                return count;
            }
        }

        public List<ReviewLikes> GetallUser(int id)
        {
            //using (var db = _context)
            {
                var db = _context;
                var count = (from x in db.ReviewLikes where x.ReviewId == id select x).ToList();
                return count;
            }
        }
    }
}

