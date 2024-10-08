using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Sockets;

var port = 27001;
var ipAddress = IPAddress.Parse("10.1.18.16");
var ep = new IPEndPoint(ipAddress, port);

using var listener = new TcpListener(ep);
try
{
    listener.Start();
    while (true)
    {
        var client = listener.AcceptTcpClient();
        _ = Task.Run(() =>
        {
            Console.WriteLine($"{client.Client.RemoteEndPoint} connected");
            var stream = client.GetStream();
            var path = "image5.png";
            using (var fs = new FileStream(path, FileMode.OpenOrCreate,FileAccess.Write))
            {
                int len = 0;
                var bytes=new byte[1024];
                while((len=stream.Read(bytes, 0, len)) > 0)
                {
                    fs.Write(bytes, 0, len);
                }

                
            }
            
        });
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}