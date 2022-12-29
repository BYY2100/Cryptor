using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace security_project.Pages
{
    public class AutokeyModel : PageModel
    {
        private static String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public void OnGet()
        {
        }

        public void OnPostEncrypt()
        {
            string msg = Request.Form["message"];
            string key = Request.Form["key"];
            msg = msg.ToUpper();
            key = key.ToUpper();

            Regex w = new Regex("[-+]?d*.?d+");
            if (w.IsMatch(key))
                key = "" + alphabet[Int32.Parse(key) % 26];

            string result = encrypt(msg, key);
            ViewData["Message"] = result.ToString();
        }

        public void OnPostDecrypt()
        {
            string msg = Request.Form["message"];
            string key = Request.Form["key"];
            msg = msg.ToUpper();
            key = key.ToUpper();

            Regex w = new Regex("[-+]?d*.?d+");
            if (w.IsMatch(key))
                key = "" + alphabet[Int32.Parse(key) % 26];

            string result = decrypt(msg, key);
            ViewData["Message"] = result.ToString();
        }

        public static string encrypt(string msg,
                                     string key)
        {
            int len = msg.Length;

            // generating the keystream
            string newKey = key + msg;
            newKey = newKey.Substring(0, newKey.Length
                                      - key.Length);
            string encryptMsg = "";

            // applying encryption algorithm
            for (int x = 0; x < len; x++)
            {
                int first = alphabet.IndexOf(msg[x]);
                int second = alphabet.IndexOf(newKey[x]);
                int total = (first + second) % 26;
                encryptMsg += alphabet[total]; ;
            }
            return encryptMsg;
        }

        public static string decrypt(string msg,
                                            string key)
        {
            string currentKey = key;
            string decryptMsg = "";

            // applying decryption algorithm
            for (int x = 0; x < msg.Length; x++)
            {
                int get1 = alphabet.IndexOf(msg[x]);
                int get2 = alphabet.IndexOf(currentKey[x]);
                int total = (get1 - get2) % 26;
                total = (total < 0) ? total + 26 : total;
                decryptMsg += alphabet[total];
                currentKey += alphabet[total];
            }
            return decryptMsg;
        }
    }
}

