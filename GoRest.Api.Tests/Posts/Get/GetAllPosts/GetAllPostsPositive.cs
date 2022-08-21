using API_Tests.Asserts;
using API_Tests.Helpers;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace API_Tests.Posts.Get.GetAllPosts
{
    [TestFixture]
    public class GetAllPostsPositiveTests
    {
        [Test]
        public async Task VerifyGetAllPostsReturnInfo()
        {
            // Arrange
            (string userId, string postId) createdPost = await new CreateEntities().CreatePost();

            //Act
            var response = await GoRestClient.For<IPostsApi>().GetAllPosts(createdPost.userId);

            // Assert
            PostsAsserts.VerifyGetAllPosts(response);
        }
    }
}
