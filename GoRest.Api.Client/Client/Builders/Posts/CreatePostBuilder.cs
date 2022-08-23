using GoRest.Api.Client.Client.Models;
namespace GoRest.Api.Client.Client.Builder.PostsApi
{
    public class CreatePostBuilder
    {
        private readonly CreatePostModel _createPost;

        public CreatePostBuilder()
        {
            this._createPost = GetDefault();
        }

        public CreatePostBuilder With(Action<CreatePostModel> updateAction)
        {
            updateAction.Invoke(this._createPost);
            return this;
        }

        public CreatePostModel Build()
        {
            return this._createPost;
        }

        public CreatePostModel GetDefault()
        {
            var model = new CreatePostModel()
            {
                Title = new Random().Next(1111, 5555).ToString(),
                Body = Guid.NewGuid().ToString(),
            };

            return model;
        }
    }
}
