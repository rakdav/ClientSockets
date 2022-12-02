using System.Net;
using System.Net.Sockets;
using System.Text;

try
{
    SendMessage();
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
void SendMessage()
{
    byte[] buffer = new byte[1024];
    // IPHostEntry ipHost = Dns.GetHostEntry("localhost");
    IPAddress ipAddress = IPAddress.Parse("192.168.113.117");
    IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
    Socket sender=new Socket(AddressFamily.InterNetwork, 
        SocketType.Stream, ProtocolType.Tcp);
    sender.Connect(remoteEP);
    Console.Write("Введите сообщение:");
    string message=Console.ReadLine();
    Console.WriteLine("Сокет соединяется с "
        +sender.RemoteEndPoint.ToString());
    byte[] msg=Encoding.Unicode.GetBytes(message);
    int bytesSend=sender.Send(msg);
    int bytesRec=sender.Receive(buffer);
    Console.WriteLine("\nответ от сервера:" +
        Encoding.Unicode.GetString(buffer, 0, bytesRec));
    if(message.IndexOf("<TheEnd>")==-1)
    {
        SendMessage();
        sender.Shutdown(SocketShutdown.Both);
        sender.Close();
    }

}
