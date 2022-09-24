using System;  
using System.Net;  
using System.Net.Sockets;  
using System.Text;
public class Server
{
    public Server(int port)
    {
        // Create a new socket and listen on port 8080
        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        listener.Bind(new IPEndPoint(IPAddress.Any, port));
        listener.Listen(10);

        // Start listening for connections
        while (true)
        {
            Socket client = listener.Accept();
            // Start a new thread for the client
            Thread thread = new Thread(new ParameterizedThreadStart(HandleClient));
            thread.Start(client);
        }
    }

    private void HandleClient(object client)
    {
        Socket socket = (Socket)client;
        // Read the data from the client
        byte[] buffer = new byte[1024];
        //recive all the data from the client
        socket.Receive(buffer);
        string data = Encoding.UTF8.GetString(buffer);
       //send the data back to the Pipe
       Pipe pipe = new Pipe(data);
        //read data from output.txt
        var reader = new StreamReader("Output.txt");
        string output = reader.ReadToEnd();
        // Write the data to the client
        socket.Send(Encoding.UTF8.GetBytes(output));
        // Close the client
        socket.Close();
    }

    public void Close()
    {
        // Close the server
        Environment.Exit(0);
    }
}