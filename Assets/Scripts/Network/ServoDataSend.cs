using UnityEngine;
using System.Net.Sockets;
using System.Text;


public class ServoDataSend : MonoBehaviour
{
   private string host = "192.168.1.9";
   private int port = 9000;
   private UdpClient client;

   void Start() {
       client = new UdpClient();
       client.Connect(host, port);
   }


   void Update() {
       if (Input.GetKeyDown(KeyCode.S)) {
           Debug.Log("Send");
           var message = Encoding.UTF8.GetBytes("Hello World!");
           client.Send(message, message.Length);
       }
   }

    private void OnDestroy() {
       client.Close();
   }
}