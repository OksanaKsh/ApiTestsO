using API_Tests.Asserts;
using API_Tests.Helpers;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace API_Tests.Posts.Get.GetPost
{
    [TestFixture]
    public class GetPostPositiveTests
    {
        [Ignore ("Bug:Cannot get the created for user post")]
        [Test]
        public async Task VerifyGetPostReturnsInfo()
        {
            // Arrange
            (string userId, string postId) createdPost = await new CreateEntities().CreatePost();

            // Act
            var responseGetPost = await GoRestClient.For<IPostsApi>().GetPost(createdPost.userId, createdPost.postId);

            // Assert
            PostsAsserts.VerifyGetPostInfo(responseGetPost, createdPost.userId, createdPost.postId);
        }
    }
}
