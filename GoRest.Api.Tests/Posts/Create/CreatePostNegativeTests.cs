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
                x.Title = string.Empty;
                x.Body = string.Empty;
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
                x.Title = new RandomStringBuilder().GenerateRandomStringOfSpecifiedLength(201);
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
                x.Body = new RandomStringBuilder().GenerateRandomStringOfSpecifiedLength(501);
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
