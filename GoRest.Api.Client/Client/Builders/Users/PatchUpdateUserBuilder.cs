using GoRest.Api.Client.Client.Models;

namespace GoRest.Api.Client.Client.Builder
{
    public class PatchUpdateUserBuilder
    {
        private readonly UpdateUserModel _createUser;
        public PatchUpdateUserBuilder()
        {
            this._createUser = GetDefault();
        } 

        public PatchUpdateUserBuilder With(Action<UpdateUserModel> updateAction)
        {
            updateAction.Invoke(this._createUser);
            return this;
        }

        public UpdateUserModel Build()
        {
            return this._createUser;
        }

        public UpdateUserModel Build(Gender gender, Status status)
        {
            this._createUser.Gender = gender;
            this._createUser.Status = status;
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
