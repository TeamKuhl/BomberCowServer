using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace BomberCowServer
{

    class Server
    {

        // TCP
        private TcpListener tcpListener;
        private Thread listenThread;
        private List<TcpClient> allClients = new List<TcpClient>();

        /// <summary>
        ///     Starts the server
        /// </summary>
        /// <param name="port">The port to start the server on.</param>
        /// <returns>Success of start process.</returns>
        public Boolean start(int port)
        {
            // set up tcp listener
            this.tcpListener = new TcpListener(IPAddress.Any, port);

            // set up thread & start the server
            this.listenThread = new Thread(new ThreadStart(ListenForClients));
            this.listenThread.Start();

            return true;
        }

        /// <summary>
        ///     Stops the server
        /// </summary>
        /// <returns>Success of stop process.</returns>
        public Boolean stop()
        {
            return false;
        }

        /// <summary>
        ///     Sends data to a client
        /// </summary>
        /// <param name="client">The client to send the message to.</param>
        /// <param name="message">The message to send.</param>
        /// <returns>Success of send process.</returns>
        public Boolean send(TcpClient client, String message)
        {
            // get client stream
            NetworkStream clientStream = client.GetStream();

            // encode message
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = encoder.GetBytes(message);

            // send to client
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();

            return true;
        }

        /// <summary>
        ///     Send data to all clients
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <returns>Success of send process.</returns>
        public Boolean sendToAll(String message)
        {
            // loop all clients
            foreach (TcpClient client in this.allClients)
            {
                // send message
                this.send(client, message);
            }
            return false;
        }

        /// <summary>
        ///     Listener for Communication
        /// </summary>
        private void ListenForClients()
        {
            this.tcpListener.Start();

            while (true)
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();

                //create a thread to handle communication 
                //with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        /// <summary>
        ///     Handles client communication.
        /// </summary>
        /// <param name="client">Client object</param>
        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;

            // add to client list
            this.allClients.Add(tcpClient);

            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;

                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    //a socket error has occured
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();
                Console.WriteLine(encoder.GetString(message, 0, bytesRead));
            }

            // remove from array
            this.allClients.Remove(tcpClient);

            tcpClient.Close();
        }

    }
}
