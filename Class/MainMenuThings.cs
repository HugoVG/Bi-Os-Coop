using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bi_Os_Coop
{
    class MainMenuThings
    {
        public dynamic user { get; set; }
        public string sort { get; set; }
        public bool reverse { get; set; }
        public string login { get; set; }
        public string language { get; set; }
        public void setlog(dynamic user, string sort, bool reverse, string login, string language)
        {
            this.user = user;
            this.sort = sort;
            this.reverse = reverse;
            this.login = login;
            this.language = language;
        }
    }
}
