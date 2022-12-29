using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace security_project.Pages
{
    public class VignereModel : PageModel
    {
        public void OnGet()
        {
        }

        static String generateKey(String str, String key)
        {
            int x = str.Length;

            for (int i = 0; ; i++)
            {
                if (x == i)
                    i = 0;
                if (key.Length == str.Length)
                    break;
                key += (key[i]);
            }
            return key;
        }

        // This function returns the encrypted text
        // generated with the help of the key
        static String encrypt(String str, String key)
        {
            String cipher_text = "";

            for (int i = 0; i < str.Length; i++)
            {
                // converting in range 0-25
                int x = (str[i] + key[i]) % 26;

                // convert into alphabets(ASCII)
                x += 'A';

                cipher_text += (char)(x);
            }
            return cipher_text;
        }

        // This function decrypts the encrypted text
        // and returns the original text
        static String decrypt(String cipher_text, String key)
        {
            String orig_text = "";

            for (int i = 0; i < cipher_text.Length &&
                                    i < key.Length; i++)
            {
                // converting in range 0-25
                int x = (cipher_text[i] -
                            key[i] + 26) % 26;

                // convert into alphabets(ASCII)
                x += 'A';
                orig_text += (char)(x);
            }
            return orig_text;
        }

        public void OnPostEncrypt()
        {
            string msg = Request.Form["message"];
            string temp_key = Request.Form["key"];


            msg = msg.ToLower();
            temp_key = temp_key.ToLower();

            string key = generateKey(msg, temp_key);
            string result = encrypt(msg, key);
            ViewData["Message"] = result.ToString();
        }


        public void OnPostDecrypt()
        {
            string msg = Request.Form["message"];
            string temp_key = Request.Form["key"];

            msg = msg.ToLower();
            temp_key = temp_key.ToLower();

            string key = generateKey(msg, temp_key);
            string result = decrypt(msg, key);
            ViewData["Message"] = result.ToString();
        }
    }
}
