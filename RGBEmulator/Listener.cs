﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RGBEmulator
{
    class Listener
    {
        private UdpClient _udp;
        private IPEndPoint _endpoint;
        public Listener(int port)
        {
            _endpoint = new IPEndPoint(IPAddress.Any, port);
            _udp = new UdpClient(_endpoint);

            Console.WriteLine("UDP Listener on port {0}", port);
        }

        public void Listen(Action<int,byte,byte,byte,int> action)
        {
            while (true)
            {
                var index =  BitConverter.ToInt16(_udp.Receive(ref _endpoint), 0);
                var r = _udp.Receive(ref _endpoint)[0];
                var g = _udp.Receive(ref _endpoint)[0];
                var b = _udp.Receive(ref _endpoint)[0];
                var intensity = BitConverter.ToInt16(_udp.Receive(ref _endpoint), 0);

                action(index, r, g, b, intensity);
            }
        }
    }
}
