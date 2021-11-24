using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App02._2
{
    public class Login
    {
        const string NAME_WINDOW = "main";

        public Login()
        {

        }

        public string Name { get; set; }
        public List<Window> Windows { get; set; } = new List<Window>();

        public bool IsCorrectLogin()
        {
            var resultSearch = Windows.Where(tempTitle => tempTitle.Title == NAME_WINDOW).ToList();

            if (resultSearch.Count() == 1)
            {
                return resultSearch[0].Top.HasValue && resultSearch[0].Left.HasValue && resultSearch[0].Width.HasValue && resultSearch[0].Height.HasValue;
            }

            return resultSearch.Count() == 0 ;
        }
        public override string ToString()
        {
            StringBuilder loginStringBuilder = new StringBuilder();

            loginStringBuilder.Append($"Login: {Name}\n");

            foreach (var itemWindow in Windows)
            {
                loginStringBuilder.Append($"\t{itemWindow}\n");
            }

            return loginStringBuilder.ToString();
        }

    }
}
