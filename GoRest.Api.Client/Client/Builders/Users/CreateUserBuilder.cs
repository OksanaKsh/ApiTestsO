using GoRest.Api.Client.Client.Models;

namespace GoRest.Api.Client.Client.Builder
{
    public class CreateUserBuilder
    {
        private readonly CreateUserModel _createUser;
        
        public CreateUserBuilder()
        {
            this._createUser = GetDefault();
        }

        public CreateUserBuilder With(Action<CreateUserModel> updateAction)
        {
            updateAction.Invoke(this._createUser);
            return this;
        }

        public CreateUserModel Build()
        {
            return this._createUser;
        }

        public CreateUserModel GetDefault()
        {
            var model = new CreateUserModel()
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
