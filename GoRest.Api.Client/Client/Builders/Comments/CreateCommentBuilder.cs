using GoRest.Api.Client.Client.Models;
namespace GoRest.Api.Client.Client.Builder
{
    public class CreateCommentBuilder
    {
        private readonly CreateCommentModel _createPost;

        public CreateCommentBuilder()
        {
            this._createPost = GetDefault();
        }

        public CreateCommentBuilder With(Action<CreateCommentModel> updateAction)
        {
            updateAction.Invoke(this._createPost);
            return this;
        }

        public CreateCommentModel Build()
        {
            return this._createPost;
        }

        public CreateCommentModel GetDefault()
        {
            var model = new CreateCommentModel()
            {
                Name = new Random().Next(1111, 5555).ToString(),
                Body = Guid.NewGuid().ToString(),
                Email = new Random().Next(1111, 5555) + "@gmail.com",
            };

            return model;
        }
    }
}
