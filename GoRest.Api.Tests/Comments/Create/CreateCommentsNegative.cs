using API_Tests.Asserts;
using API_Tests.Helpers;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder;
using GoRest.Api.Client.Client.Extentions;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;
namespace API_Tests.Posts.Create.CreateCommentNegativeTests
{
    [TestFixture]
    public class CreateCommentNegativeTests
    {
        [Test]
        public async Task VerifyCommentIsNotCreateWhenEmptyFields()
        {
            // Arrange
            (string userId, string postId) createdPost = await new CreateEntities().CreatePost();
            var commentModelEmptyFields = new CreateCommentBuilder().With(x =>
            {
                x.Name = string.Empty;
                x.Email = string.Empty;
                x.Body = string.Empty;
            }
              ).Build();

            //Act
            var responseCreateComment = await GoRestClient.For<ICommentsApi>().CreateCommentNegative(commentModelEmptyFields, createdPost.postId);

            // Assert
            CommentsAsserts.VerifyCommentFieldsCannotBeBlank(responseCreateComment);
        }

        [Test]
        public async Task VerifyCommentIsNotCreateWhenFieldsExceedLength()
        {
            // Arrange
            (string userId, string postId) createdPost = await new CreateEntities().CreatePost();
            var commentModelExceedLentghFields = new CreateCommentBuilder().With(x =>
            {               
                x.Name = new RandomStringBuilder().GenerateRandomStringOfSpecifiedLength(201);
                x.Email = new RandomStringBuilder().GenerateRandomStringOfSpecifiedLength(201);
                x.Body = new RandomStringBuilder().GenerateRandomStringOfSpecifiedLength(501);
            }
              ).Build();

            //Act
            var responseCreateCommentWithExceedLenghFields = await GoRestClient.For<ICommentsApi>().CreateCommentNegative(commentModelExceedLentghFields, createdPost.postId);

            // Assert
            CommentsAsserts.VerifyCommentWithFieldsExceedLenghNotCreated(responseCreateCommentWithExceedLenghFields);
        }

        [TestCase("0")]
        [TestCase("-1")]
        [TestCase("10000000")]
        public async Task VerifyCommentIsNotCreatedForNotExistedPost(string postId)
        {
            // Arrange & Act
            var response = await GoRestClient.For<ICommentsApi>().CreateCommentNegative(new CreateCommentBuilder().Build(), postId);

            // Assert
            CommentsAsserts.VerifyCommentForNotExistedPostIsNotCreated(response);
        }

        [TestCase("@gmail.com")]
        [TestCase("email")]
        [TestCase("QA@gmail_with_501_chatacters@email.com")]
        public async Task VerifyCommentIsNotCreatedWhenInvalidEmail(string email)
        {
            // Arrange
            (string userId, string postId) createdPost = await new CreateEntities().CreatePost();
            var commentModelInvalidEmail = new CreateCommentBuilder().With(x =>
            {
                x.Email = email;
            }
              ).Build();

            // Act
            var response = await GoRestClient.For<ICommentsApi>().CreateCommentNegative(commentModelInvalidEmail, createdPost.postId);

            // Assert
            CommentsAsserts.VerifyCommentWitInvalidEmailIsNotCreated(response);
        }

        public async Task VerifyCommentIsNotCreatedWhenInvalidToken()
        {
            // Arrange
            (string userId, string postId) createdPost = await new CreateEntities().CreatePost();

            // & Act
            var response = await GoRestClient.ForInvalidToken<ICommentsApi>().CreateCommentNegativeAuth(new CreateCommentBuilder().Build(), createdPost.postId);

            // Assert
            response.ShouldBeUnathorized();
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }

        [Test]
        public async Task VerifyCommentIsNotCreatedWithoutToken()
        {
            (string userId, string postId) createdPost = await new CreateEntities().CreatePost();

            // & Act
            var response = await GoRestClient.ForWithoutToken<ICommentsApi>().CreateCommentNegativeAuth(new CreateCommentBuilder().Build(), createdPost.postId);

            // Assert
            response.ShouldBeUnathorized();
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }
    }
}
