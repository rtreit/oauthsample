using System;
using System.Net;

class OAuthCodeListener
{
    public static string OAuthListener(string[] prefixes)
    {
        string code = "";
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
        bool hasCode = context.Request.RawUrl.Contains("code=");
        if (hasCode == true)
        {
            code = context.Request.RawUrl;
            string[] split_results = code.Split("=");
            if(split_results.Length >= 2)
            {
                string raw_code = split_results[1];
                if(split_results.Length==2)
                {
                    code = raw_code;
                }
                else
                {
                    string[] code_split = raw_code.Split("&");
                    code = code_split[0];
                }
                if (code.Split(".")[2].Length ==37)
                {
                    bool is_msa_account = true;
                }
            }
            
        }
        string responseString = $"<HTML><BODY> Received OAuth Code!<br><br><b>{code}</b></BODY></HTML>";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        System.IO.Stream output = response.OutputStream;
        output.Write(buffer,0,buffer.Length);
        output.Close();
        listener.Stop();
        return code;
    }
  public static void Main()
  //serve forever
  {     while(true)
    {
            string incoming = OAuthListener(new string[] { "http://127.0.0.1:8076/"});    
            Console.WriteLine(incoming);
    }
  }
  
}