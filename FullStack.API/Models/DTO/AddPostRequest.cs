using System.ComponentModel.DataAnnotations;

namespace FullStack.API.Models.DTO
{
    public class AddPostRequest
    {
        public bool IsScheduledPost { get; set; } //To Check whether to scheduled a Post or not

        [DataType(DataType.Date)]
        public DateTime PublishOnDate { get; set; }  //PublishedOnDate can be current or future date

        public DateTime PublishOnTime { get; set; } //added 1st May

        public string Title { get; set; }

        [StringLength(10)]
        public string PostType { get; set; } // Text, Image, Video

        public string PostContentText { get; set; }

        [StringLength(200)]
        public string PostAttachmentURL { get; set; }


        [StringLength(10)]
        public string PostStatus { get; set; }


        [StringLength(10)]
        public string UserName { get; set; }


        [StringLength(20)]
        public string SocialNetworkType { get; set; }
    }
}
