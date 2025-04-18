namespace MyTaskApp.Models
{
    public sealed class AboutPageModel
    {
        private String _description;
        private Boolean _isAdmin;

        public String Description { get { return _description; }  set { _description = value; } }
        public Boolean IsAdmin {  get { return _isAdmin; } set { _isAdmin = value; } }
    }
}
