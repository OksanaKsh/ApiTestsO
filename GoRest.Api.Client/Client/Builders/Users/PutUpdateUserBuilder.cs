using GoRest.Api.Client.Client.Models;
namespace GoRest.Api.Client.Client.Builder
{
    public class PutUpdateUserBuilder
    {
        private readonly UpdateUserModel _createUser;
        public PutUpdateUserBuilder()
        {
            this._createUser = GetDefault();
        }

        public PutUpdateUserBuilder With(Action<UpdateUserModel> updateAction)
        {
            updateAction.Invoke(this._createUser);
            return this;
        }

        public UpdateUserModel Build()
        {
            return this._createUser;
        } 

        public UpdateUserModel GetDefault()
        {
            var model = new UpdateUserModel()
            {
                Email = new Random().Next(1111, 5555) + "@gmail.com",
                Gender = Gender.Female,
                Name = Guid.NewGuid().ToString(),
                Status = Status.Active
            };

            return model;
        }
    }
}
