using GoRest.Api.Client.Client.Models;
namespace GoRest.Api.Client.Client.Builder
{
    public class PatchUpdateUserBuilder
    {
        private readonly PatchUpdateUserInfoModel _updateUser;

        public PatchUpdateUserBuilder()
        {
             this._updateUser = new PatchUpdateUserInfoModel();           
        } 

        public PatchUpdateUserBuilder With(Action<PatchUpdateUserInfoModel> updateAction)
        {
            updateAction.Invoke(this._updateUser);
            return this;
        } 

        public PatchUpdateUserInfoModel Build()
        {
            return this._updateUser;
        }
    }
}
