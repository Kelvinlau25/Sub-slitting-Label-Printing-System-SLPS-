using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PFRLabelIssuing.Pages.Transactions
{
    public class testModel : PageModel
    {
        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string CipherText { get; set; } = string.Empty;

        public string EncryptedResult { get; set; } = string.Empty;
        public string DecryptedResult { get; set; } = string.Empty;
        public string DecryptedCipherResult { get; set; } = string.Empty;

        public void OnGet() { }

        public void OnPostEncrypt()
        {
            EncryptedResult = GlobalFunctions.Encrypt(Password);
        }

        public void OnPostDecrypt()
        {
            if (!string.IsNullOrEmpty(EncryptedResult))
                DecryptedResult = GlobalFunctions.Decrypt(EncryptedResult);
        }

        public void OnPostDecryptCipher()
        {
            DecryptedCipherResult = GlobalFunctions.Decrypt(CipherText);
        }
    }
}
