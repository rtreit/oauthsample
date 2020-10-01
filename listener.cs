using System;
using System.Net;

class OAuthCodeListener
{
    public static string OAuthListener(string[] prefixes)
    {
        HttpListener listener = new HttpListener();
        foreach (string s in prefixes)
        {
            listener.Prefixes.Add(s);
        }
        listener.Start();
        Console.WriteLine("Listening...");
        HttpListenerContext context = listener.GetContext();
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;
        string responseString = $"<HTML><BODY> Received: {context.Request.RawUrl}</BODY></HTML>";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer,0,buffer.Length);
        output.Close();
        listener.Stop();
        return context.Request.RawUrl;
    }
  public static void Main()
  {     while(true){
        string incoming = OAuthListener(new string[] { "http://127.0.0.1:8076/"});    
        Console.WriteLine(incoming);
  }
  }
  
}