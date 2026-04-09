using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;

public partial class Captcha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Create object of Bitmap Class and set its width and height.
        Bitmap objBMP = new Bitmap(180, 51);
        // Create Graphics object and assign bitmap object to graphics' object.
        Graphics objGraphics = Graphics.FromImage(objBMP);
        objGraphics.Clear(Color.OrangeRed);
        objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        Font objFont = new Font("arial", 30, FontStyle.Regular);
        // Generating random 6 digit random number
        string randomStr = GeneratePassword();
        // Set this random number in session
        Session.Add("randomStr", randomStr);
        objGraphics.DrawString(randomStr, objFont, Brushes.White, 2, 2);
        Response.ContentType = "image/GIF";
        objBMP.Save(Response.OutputStream, ImageFormat.Gif);
        objFont.Dispose();
        objGraphics.Dispose();
        objBMP.Dispose();
    }

    public string GeneratePassword()
    {
        // Below code describes how to create random numbers. Some of the digits and letters
        // are omitted because they look the same like "i","o","1","0","I","O".
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