using BlazorMicroservices.Web.Utilities.Auth;

namespace BlazorMicroservices.Web.Utilities.AppStates
{
    //https://stackoverflow.com/questions/61779198/how-to-update-blazor-static-layout-from-code
    public class CurrentUserService
    {
        private UserDto? _user;
        public event Action OnChange;
        public UserDto? User
        {
            get { return _user; }
            set
            {
                if (_user != value)
                {
                    _user = value;
                    NotifyStateChanged();
                }
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
