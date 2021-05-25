namespace Bi_Os_Coop.Class
{
    class MainMenuThings
    {
        public CPeople.Person user { get; set; }
        public string sort { get; set; }
        public bool reverse { get; set; }
        public string login { get; set; }
        public string language { get; set; }
        public void setlog(CPeople.Person user, string sort, bool reverse, string login, string language)
        {
            this.user = user;
            this.sort = sort;
            this.reverse = reverse;
            this.login = login;
            this.language = language;
        }
    }
}
