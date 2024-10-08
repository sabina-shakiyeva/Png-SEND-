using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;

var port = 27001;
var ipAddress = IPAddress.Parse("10.1.18.16");
var ep = new IPEndPoint(ipAddress, port);

var client = new TcpClient();

try
{
    client.Connect(ep);
    if (client.Connected)
    {
        var networkStream=client.GetStream();
        Console.WriteLine("enter name png for screenshot:");
        //string path = "image3";
        while (true)
        {
            string path = Console.ReadLine();
            using (Bitmap bitmap = new Bitmap(1920, 1080))
            {
                using Graphics g = Graphics.FromImage(bitmap);
                g.CopyFromScreen(Point.Empty, Point.Empty, new Size(1920, 1080));
                bitmap.Save($"{path}.png", ImageFormat.Png);
            }
            var pathh = $@"C:\Users\Shaki_bf42\source\repos\ClientSide\ClientSide\bin\Debug\net8.0\{path}.png";
            using (var readFs = new FileStream(pathh, FileMode.OpenOrCreate, FileAccess.Read))
            {
                int len = 0;
                var buffer = new byte[1024];
                while ((len = readFs.Read(buffer, 0, len)) > 0)
                {
                    networkStream.Write(buffer, 0, len);
                }


            }
            networkStream.Close();
            client.Close();

        }
        

        
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
