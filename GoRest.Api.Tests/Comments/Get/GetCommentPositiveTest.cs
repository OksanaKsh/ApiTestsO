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
    public class GetCommentPositiveTest
    {
        [Ignore("Bug shows not found result")]
        [Test]
        public async Task VerifyGetCommentInfo()
        {
            // Assert
            (string postId, string commentId) createdComment = await new CreateEntities().CreateComment();

            // Act
            var responseGetComment = await GoRestClient.For<ICommentsApi>().GetComment(createdComment.postId, createdComment.commentId);

            // Assert
            CommentsAsserts.VerifyGetCommentInfo(responseGetComment, createdComment.postId, createdComment.commentId);
        }
    }
}
