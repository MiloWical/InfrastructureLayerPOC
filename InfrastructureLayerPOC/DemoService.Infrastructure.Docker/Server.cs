// ***********************************************************************
// Assembly         : DemoService.Infrastructure.Docker
// Author           : Milo.Wical
// Created          : 03-11-2018
//
// Last Modified By : Milo.Wical
// Last Modified On : 03-12-2018
// ***********************************************************************
// <copyright file="Server.cs" company="DemoService.Infrastructure.Docker">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace DemoService.Infrastructure.Docker
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using Contract;
    using Contract.SettingProvider;
    using Implementation;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Class Server.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// The container
        /// </summary>
        private static IServiceProvider _container;

        /// <summary>
        /// The service
        /// </summary>
        private static IDemonstrationService _service;

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(params string[] args)
        {
            Bootstrap();

            var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddress = ipHostInfo.AddressList.First(addr => addr.AddressFamily == AddressFamily.InterNetwork);

            var serverSocket = new Socket(ipAddress.AddressFamily,
                                          SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine($"Host IP {ipAddress}");
            var endpoint = new IPEndPoint(IPAddress.Any, 54321);
            serverSocket.Bind(endpoint);
            serverSocket.Listen(1); //POC code, so I don't need a long pending connection queue

            var socketBuffer = new byte[1024];
            var messageBuffer = new StringBuilder();

            while (true)
            {
                var clientSocket = serverSocket.Accept();

                messageBuffer.Clear();

                var bytesReceived = clientSocket.Receive(socketBuffer);

                messageBuffer.Append(Encoding.ASCII.GetString(socketBuffer, 0, bytesReceived));

                ProcessMessage(messageBuffer.ToString(), clientSocket);

                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }

        /// <summary>
        /// Bootstraps this instance.
        /// </summary>
        /// <remarks>This uses the Microsoft.Extensions.DependencyInjection IoC mechanism
        /// because Unity's container configuration isn't available for .NET core.
        /// While we could still use the main container implementation without too
        /// much trouble, we can't load from the App.config like we used to.</remarks>
        private static void Bootstrap()
        {
            _container = new ServiceCollection()
                .AddSingleton(typeof(ISettingProvider), new DictionarySettingProvider(1, "InitialSetting"))
                .AddSingleton<IDemonstrationService, DemonstrationService>()
                .BuildServiceProvider();

            _service = _container.GetService<IDemonstrationService>();
        }

        /// <summary>
        /// Processes the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="client">The client.</param>
        private static void ProcessMessage(string message, Socket client)
        {
            Console.WriteLine($"Message Received: {message}");

            if (message == "GET")
            {
                client.Send(Encoding.ASCII.GetBytes(_service.GetLatestVersionedSetting()));
            }
            else if (message.StartsWith("GET_VER"))
            {
                var tokens = message.Split("::");
                client.Send(Encoding.ASCII.GetBytes(_service.GetVersionedSetting(int.Parse(tokens[1]))));
            }
            else if (message.StartsWith("ADD"))
            {
                var tokens = message.Split("::");
                _service.AddVersionedSetting(tokens[1]);
            }
            else if (message.StartsWith("DEL"))
            {
                var tokens = message.Split("::");
                client.Send(Encoding.ASCII.GetBytes(_service.RemoveVersionedSetting(int.Parse(tokens[1])).ToString()));
            }
            else if (message.StartsWith("UPD"))
            {
                var tokens = message.Split("::");
                _service.UpdateVersionedSetting(int.Parse(tokens[1]), tokens[2]);
            }
        }
    }
}
