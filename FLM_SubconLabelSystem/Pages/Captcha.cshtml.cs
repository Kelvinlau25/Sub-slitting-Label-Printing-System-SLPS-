using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PFRLabelIssuing.Pages
{
    public class CaptchaModel : PageModel
    {
        public IActionResult OnGet()
        {
            using (var objBMP = new Bitmap(180, 51))
            using (var objGraphics = Graphics.FromImage(objBMP))
            {
                objGraphics.Clear(Color.OrangeRed);
                objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                using (var objFont = new Font("Arial", 30, FontStyle.Regular))
                {
                    string randomStr = GeneratePassword();
                    HttpContext.Session.SetString("randomStr", randomStr);
                    objGraphics.DrawString(randomStr, objFont, Brushes.White, 2, 2);
                }

                using (var ms = new MemoryStream())
                {
                    objBMP.Save(ms, ImageFormat.Gif);
                    return File(ms.ToArray(), "image/gif");
                }
            }
        }

        private static string GeneratePassword()
        {
            string allowedChars = "a,b,c,d,e,f,g,h,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "2,3,4,5,6,7,8,9";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string passwordString = "";
            Random rand = new Random();
            for (int i = 0; i < 6; i++)
            {
                passwordString += arr[rand.Next(0, arr.Length)];
            }
            return passwordString;
        }
    }
}
