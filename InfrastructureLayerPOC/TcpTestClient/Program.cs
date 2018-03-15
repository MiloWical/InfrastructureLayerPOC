namespace TcpTestClient
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            //Code ripped off, err, "borrowed" from:
            //https://docs.microsoft.com/en-us/dotnet/framework/network-programming/synchronous-client-socket-example

            // Data buffer for incoming data.  
            var bytes = new byte[1024];

            var ipAddress = IPAddress.Parse(args[0]);
            var port = int.Parse(args[1]);

            Console.Clear();

            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.    
                //var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                //var ipAddress = ipHostInfo.AddressList.FirstOrDefault(addr => addr.AddressFamily == AddressFamily.InterNetwork);
                //var port = 54321;
                var remoteEndpoint = new IPEndPoint(ipAddress, port);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    Console.Write("Enter a message to transmit: ");

                    var command = Console.ReadLine();

                    while (!string.IsNullOrWhiteSpace(command) && !string.IsNullOrEmpty(command))
                    {
                        // Create a TCP/IP  socket.  
                        var sender = new Socket(ipAddress.AddressFamily,
                                                   SocketType.Stream, ProtocolType.Tcp);
                        sender.Connect(remoteEndpoint);

                        // Encode the data string into a byte array.  
                        var msg = Encoding.ASCII.GetBytes(command);

                        // Send the data through the socket.  
                        sender.Send(msg);

                        // Receive the response from the remote device.  
                        var bytesRec = sender.Receive(bytes);
                        Console.WriteLine("Response:  {0}",
                                          Encoding.ASCII.GetString(bytes, 0, bytesRec));

                        // Release the socket.  
                        sender.Shutdown(SocketShutdown.Both);
                        sender.Close();

                        Console.Write("\nEnter a message to transmit: ");

                        command = Console.ReadLine();
                    }

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
