using API_Tests.Asserts;
using API_Tests.Helpers;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder;
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
                x.Name = "";
                x.Email = "";
                x.Body = "";
            }
              ).Build();

            //Act
            var responseCreateComment = await GoRestClient.For<ICommentsApi>().CreateCommentNegative(commentModelEmptyFields, createdPost.postId);

            // Assert
            CommentsAsserts.VerifyCommentFieldsCannotBeBlank(responseCreateComment);
        }

        [Test]
        public async Task VerifyCommentIsNotCreateWhenFieldsExceedLentgh()
        {
            // Arrange
            (string userId, string postId) createdPost = await new CreateEntities().CreatePost();
            var commentModelExceedLentghFields = new CreateCommentBuilder().With(x =>
            {
                x.Name = "QA_201_characters_entered_negative_tests ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exer";
                x.Email = "QA_201_characters_entered_negative_tests ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis no@gmail.com"; x.Body = "QA_201_characters_entered_negative_tests ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exer";
                x.Body = "QABodyWith501Characters 12345Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Leo duis ut diam quam nulla. Nibh praesent tristique magna sit amet purus gravida. Mauris cursus mattis molestie a iaculis at erat pellentesque. Quisque sagittis purus sit amet volutpat consequat. Sed vulputate mi sit amet mauris commodo quis imperdiet massa. Pellentesque massa placerat duis ultricies lacus sed turpis. Morbi tristique senectus e1";

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
    }
}
