using API_Tests.Asserts;
using API_Tests.Helpers;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;
namespace API_Tests.Comments.Create
{
    [TestFixture]
    public class CreateCommentPositiveTests
    {
        [Test]
        public async Task VerifyCommentIsCreated()
        {
            // Arrange
            (string userId, string postId) createdPost = await new CreateEntities().CreatePost();

            //Act
            var responseCreateComment = await GoRestClient.For<ICommentsApi>().CreateComment(new CreateCommentBuilder().Build(), createdPost.postId);

            // Assert
            CommentsAsserts.VerifyCreateCommentForPost(responseCreateComment, createdPost.postId);
        }
    }
}
