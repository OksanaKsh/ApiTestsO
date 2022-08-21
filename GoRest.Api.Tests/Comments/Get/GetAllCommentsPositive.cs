using API_Tests.Asserts;
using API_Tests.Helpers;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace API_Tests.Comments.Get
{
    [Parallelizable]
    [TestFixture]
    public class GetAllCommentsTests
    {
        [Test]
        public async Task VerifyGetComments()
        {
            // Assert
            (string postId, string commentId) createdComment = await new CreateEntities().CreateComment();

            // Act
            var responseGetAllComments = await GoRestClient.For<ICommentsApi>().GetAllComments(createdComment.postId);

            // Assert
            CommentsAsserts.VerifyGetAllComments(responseGetAllComments, createdComment.postId);
        }
    }
}
