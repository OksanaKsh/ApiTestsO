using API_Tests.Asserts;
using API_Tests.Helpers;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder.PostsApi;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace API_Tests.Posts.Create.CreatePostPositiveTests
{
    [TestFixture]
    public class CreatePostNegativeTests
    {
        [Test]
        public async Task VerifyCreatePostForUser()
        {
            // Arrange
            var userId = await new CreateEntities().CreateUser();

            //Act
            var responseCreatePost = await GoRestClient.For<IPostsApi>().CreatePost(userId, new CreatePostBuilder().Build());

            // Assert
            PostsAsserts.VerifyCreatePostForUser(responseCreatePost, userId);
        }
    }
}
