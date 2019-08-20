using ENet;
using UnityEngine;

public class Server : MonoBehaviour
{
    public ushort port;
    public int maxClients;
    private EnetServer server;

    void Start()
    {
        server = new EnetServer(this.port, this.maxClients);
    }

    void Update()
    {
        server.Update();
    }

    void onDestroy()
    {
        server.onDestroy();
    }
}
