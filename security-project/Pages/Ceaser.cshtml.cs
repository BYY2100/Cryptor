using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace security_project.Pages
{
    public class CeaserModel : PageModel
    {

        public void OnGet()
        {
        }

        public void OnPostEncrypt()
        {
            string msg = Request.Form["message"];
            string temp_key = Request.Form["key"];
            int key = Int32.Parse(temp_key);
            msg = msg.ToLower();
            StringBuilder result = new();

            for (int i = 0; i < msg.Length; i++)
            {
                if (char.IsUpper(msg[i]))
                {
                    char ch = (char)(((int)msg[i] +
                                    key - 65) % 26 + 65);
                    result.Append(ch);
                }
                else
                {
                    char ch = (char)(((int)msg[i] +
                                    key - 97) % 26 + 97);
                    result.Append(ch);
                }
            }


            ViewData["Message"] = result.ToString();
        }

        public void OnPostDecrypt()
        {
            string msg = Request.Form["message"];
            string temp_key = Request.Form["key"];
            int key = Int32.Parse(temp_key);

            msg = msg.ToLower();

            StringBuilder result = new();

            for (int i = 0; i < msg.Length; i++)
            {
                if (char.IsUpper(msg[i]))
                {
                    char ch = (char)(((int)msg[i] - 'A' -
                                    key + 26) % 26 + 'A');
                    result.Append(ch);
                }
                else
                {
                    char ch = (char)(((int)msg[i] - 'a' -
                                    key + 26) % 26 + 'a');
                    result.Append(ch);
                }
            }


            ViewData["Message"] = result.ToString();
        }


    }
}
