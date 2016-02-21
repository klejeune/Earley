using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EarleyParser.PartOfSpeech.Talismane {
    class Talismane {
        public IEnumerable<TalismaneAnswer> Send(string input) {
            var port = 7171;

            var ip = IPAddress.Parse("127.0.0.1");
            var endpoint = new IPEndPoint(ip, port);

            var socket = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(endpoint);

            string request = input + "\f\f\f";
            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];

            // Send request to the server.
            socket.Send(bytesSent, bytesSent.Length, 0);

            // Receive the server home page content. 
            int bytes = 0;
            string result = "";

            // The following will block until te page is transmitted. 
            do {
                bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
                result = result + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
            }
            while (bytes > 0);

            var data = result.Split('\n').Select(word => {
                var items = word.Split('\t');

                if (items.Count() > 1) {

                    return new TalismaneAnswer {
                        Token = items[0],
                        Word = items[1],
                        Lemma = items[2],
                        Tag = items[3],
                        Category = items[4],
                        MorphoSyntaxicInfo = items[5],
                        GovernorId = items[6],
                        Dependency = items[7],
                    };
                }
                else {
                    return null;
                }
            }).Where(item => item != null).ToList();

            return data;
        }
    }
}
