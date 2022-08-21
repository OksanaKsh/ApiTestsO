using API_Tests.Asserts;
using API_Tests.Helpers;
using FluentAssertions;
using GoRest.Api.Client.Client;
using GoRest.Api.Client.Client.Builder.PostsApi;
using GoRest.Api.Client.Client.Extentions;
using GoRest.Api.Client.Client.Interfaces.Controllers;
using NUnit.Framework;
using System.Threading.Tasks;

namespace API_Tests.Posts.Create.CreatePostNegativeTests
{
    [Parallelizable]
    [TestFixture]
    public class CreatePostNegativeTests
    {
        [Test]
        public async Task VerifyPostIsNotCreateWhenEmptyTitleAndEmptyBody()
        {
            // Arrange
            var userId = await new CreateEntities().CreateUser();
            var postModelEmptyFields = new CreatePostBuilder().With(x =>
            {
                x.Title = "";
                x.Body = "";
            }
              ).Build();

            //Act
            var responseCreatePost = await GoRestClient.For<IPostsApi>().CreatePostNegative(postModelEmptyFields, userId);

            // Assert
            PostsAsserts.VerifyPostFieldsCannotBeBlank(responseCreatePost);
        }

        [Test]
        public async Task VerifyPostIsNotCreateWhenTitleLentghMoreThen200Characters()
        {
            // Arrange
            var userId = await new CreateEntities().CreateUser();
            var postModelBigTitle = new CreatePostBuilder().With(x =>
            {
                x.Title = "QA_201_characters_entered_negative_tests ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exer";
            }
              ).Build();

            //Act
            var responseCreatePostWithBigTitle = await GoRestClient.For<IPostsApi>().CreatePostNegative(postModelBigTitle, userId);

            // Assert
            PostsAsserts.VerifyPostWithBigTitleIsNotCreated(responseCreatePostWithBigTitle);
        }

        [Test]
        public async Task VerifyPostIsNotCreateWhenBodyLentghMoreThen500Characters()
        {
            // Arrange
            var userId = await new CreateEntities().CreateUser();
            var postModelBigBody = new CreatePostBuilder().With(x =>
            {
                x.Body = "QABodyWith501Characters 12345Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Leo duis ut diam quam nulla. Nibh praesent tristique magna sit amet purus gravida. Mauris cursus mattis molestie a iaculis at erat pellentesque. Quisque sagittis purus sit amet volutpat consequat. Sed vulputate mi sit amet mauris commodo quis imperdiet massa. Pellentesque massa placerat duis ultricies lacus sed turpis. Morbi tristique senectus e1";
            }
              ).Build();

            //Act
            var responseCreatePostWithBigBody = await GoRestClient.For<IPostsApi>().CreatePostNegative(postModelBigBody, userId);

            // Assert
            PostsAsserts.VerifyPostWithBigBodyIsNotCreated(responseCreatePostWithBigBody);
        }

        [TestCase("0")]
        [TestCase("-1")]
        [TestCase("10000000")]
        public async Task VerifyPostIsNotCreatedForNotExistedUser(string userId)
        {
            // Arrange & Act
            var response = await GoRestClient.For<IPostsApi>().CreatePostNegative(new CreatePostBuilder().Build(), userId);

            // Assert
            PostsAsserts.VerifyPostForNotExistedUserIsNotCreated(response);
        }

        [Test]
        public async Task VerifyPostIsNotCreatedWhenInvalidToken()
        {
            // Arrange
            var userId = await new CreateEntities().CreateUser();

            // & Act
            var response = await GoRestClient.ForInvalidToken<IPostsApi>().CreatePostNegativeAuth(new CreatePostBuilder().Build(), userId);

            // Assert
            response.ShouldBeUnathorized();
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }

        [Test]
        public async Task VerifyPostIsNotCreatedWithoutToken()
        {
            var userId = await new CreateEntities().CreateUser();

            // & Act
            var response = await GoRestClient.ForWithoutToken<IPostsApi>().CreatePostNegativeAuth(new CreatePostBuilder().Build(), userId);

            // Assert
            response.ShouldBeUnathorized();
            response.Meta.Should().BeNull();
            response.Data.Message.Should().Be("Authentication failed");
        }
    }
}
