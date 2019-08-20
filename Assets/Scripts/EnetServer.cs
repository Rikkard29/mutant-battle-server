using ENet;
using UnityEngine;

public class EnetServer
{
    private Host server;
    private ENet.Event netEvent;
    private bool polled;

    public EnetServer(ushort port, int maxClients)
    {
        ENet.Library.Initialize();

        Debug.Log("Starting server...");

        server = new Host();
        Address address = new Address();

        address.Port = port;
        server.Create(address, maxClients);
    }

    public void Update()
    {
        polled = false;
        while (!polled)
        {
            if (server.CheckEvents(out netEvent) <= 0)
            {
                if (server.Service(0, out netEvent) <= 0)
                    break;

                polled = true;
            }

            switch (netEvent.Type)
            {
                case ENet.EventType.None:
                    break;

                case ENet.EventType.Connect:
                    Debug.Log("Client connected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                    break;

                case ENet.EventType.Disconnect:
                    Debug.Log("Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                    break;

                case ENet.EventType.Timeout:
                    Debug.Log("Client timeout - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP);
                    break;

                case ENet.EventType.Receive:
                    Debug.Log("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " + netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID + ", Data length: " + netEvent.Packet.Length);
                    netEvent.Packet.Dispose();
                    break;
            }
        }
    }

    public void onDestroy()
    {
        server.Flush();
    }
}
