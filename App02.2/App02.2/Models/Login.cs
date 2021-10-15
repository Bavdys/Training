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
            Windows = new List<Window>();
        }

        public string Name { get; set; }
        public bool IsCorrectLogin
        {
            get
            {
                var resultSearch = Windows.Where(tempTitle => tempTitle.Title == NAME_WINDOW);

                if (resultSearch.Count() == 0)
                {
                    return true;
                }
                else if (resultSearch.Count() == 1)
                {
                    foreach (var item in resultSearch)
                    {
                        return (item.Top.HasValue && item.Left.HasValue && item.Width.HasValue && item.Height.HasValue);
                    }
                }

                return false;

            }
        }
        public List<Window> Windows { get; set; }

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
