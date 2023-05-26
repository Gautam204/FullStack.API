using FullStack.API.Data;
using FullStack.API.Models.DTO;
using FullStack.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/content")]
    public class UserPostsController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;

        public UserPostsController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }

        [HttpGet]
        [Route("allposts")]
        public async Task<IActionResult> GetAllUserPosts()
        {
            var userposts = await _fullStackDbContext.UserPosts.ToListAsync();

            return Ok(userposts);

        }

        [HttpGet]
        [Route("allsubscriptionplanlimit")]
        public async Task<IActionResult> GetAllSubscriptionPlanLimit()
        {
            var subscriptionplanlimit = await _fullStackDbContext.SubscriptionPlanLimits.ToListAsync();

            return Ok(subscriptionplanlimit);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetUserPostbyId([FromRoute] Guid id)
        {
            var userpost = await _fullStackDbContext.UserPosts.FirstOrDefaultAsync(x => x.Id == id);

            if (userpost == null)
            {
                return NotFound();
            }

            return Ok(userpost);

        }

        [HttpGet]
        [Route("{username}")]
        public async Task<IActionResult> GetUserPostbyUserName([FromRoute] string username)
        {
            var userpost = await _fullStackDbContext.UserPosts.FirstOrDefaultAsync(x => x.UserName == username);

            if (userpost == null)
            {
                return NotFound();
            }

            return Ok(userpost);

        }

        [HttpPost]
        [Route("addPost")]
        public async Task<IActionResult> AddPost(AddPostRequest addPostRequest)
        {
            //convert DTO to Entity 

            var userpost = new UserPost()
            {
                IsScheduledPost = addPostRequest.IsScheduledPost,
                // PublishOnDate = addPostRequest.PublishOnDate,
                PublishOnDate = addPostRequest.PublishOnTime,
                PublishOnTime = addPostRequest.PublishOnTime, // added //added 1st May
                Title = addPostRequest.Title,
                PostType = addPostRequest.PostType,
                PostContentText = addPostRequest.PostContentText,
                PostAttachmentURL = addPostRequest.PostAttachmentURL,
                PostStatus = addPostRequest.PostStatus,
                UserName = addPostRequest.UserName,
                SocialNetworkType = addPostRequest.SocialNetworkType
            };


            userpost.Id = Guid.NewGuid();
            userpost.PostedOn = DateTime.Now;
            //userpost.PublishOnTime = DateTime.Now; //Hardcoded

            await _fullStackDbContext.UserPosts.AddAsync(userpost);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(userpost);

        }

        [HttpPut]
        [Route("UpdatePost/{id:Guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid id, UpdatePostRequest updatePostRequest)
        {

            // Check if Exists
            var existinguserpost = await _fullStackDbContext.UserPosts.FindAsync(id);

            if (existinguserpost != null)
            {

                existinguserpost.IsScheduledPost = updatePostRequest.IsScheduledPost;
                existinguserpost.PublishOnDate = updatePostRequest.PublishOnTime;
                existinguserpost.PublishOnTime = updatePostRequest.PublishOnTime; //added 1st May
                existinguserpost.Title = updatePostRequest.Title;
                existinguserpost.PostType = updatePostRequest.PostType;
                existinguserpost.PostContentText = updatePostRequest.PostContentText;
                existinguserpost.PostAttachmentURL = updatePostRequest.PostAttachmentURL;
                existinguserpost.PostStatus = updatePostRequest.PostStatus;
                existinguserpost.SocialNetworkType = updatePostRequest.SocialNetworkType;


                await _fullStackDbContext.SaveChangesAsync();

                return Ok(existinguserpost);
            }

            return NotFound();

        }

        [HttpDelete]
        [Route("cancel/{id:Guid}")]
        public async Task<IActionResult> DeleteUserPost([FromRoute] Guid id)
        {
            var userpost = await _fullStackDbContext.UserPosts.FindAsync(id);

            if (userpost == null)
            {
                return NotFound();
            }

            _fullStackDbContext.UserPosts.Remove(userpost);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(userpost);
        }
 
    }
}
