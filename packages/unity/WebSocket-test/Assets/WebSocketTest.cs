using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Unity.Networking.Transport;
using System.Net;

using System.Net.WebSockets;
using System.Threading;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;

public class WebSocketTest : MonoBehaviour
{
    NetworkDriver m_Driver;
    NetworkConnection m_Connection;

    ClientWebSocket _clientWebSocket;

    [SerializeField] SocketControlled socketControlled;

    bool isMovingUp = false;
    bool isMovingDown = false;

    // Start is called before the first frame update
    void Start()
    {
        _clientWebSocket = new ClientWebSocket();

        ConnectToWebSocket();
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isMovingUp) return;
            isMovingUp = true;
            SendToWebSocket("up");
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            if (!isMovingUp) return;
            isMovingUp = false;
            SendToWebSocket("su");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isMovingDown) return;
            isMovingDown = true;
            SendToWebSocket("dn");
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            if (!isMovingDown) return;
            isMovingDown = false;
            SendToWebSocket("sd");
        }

    }

    async void ConnectToWebSocket()
    {
        await _clientWebSocket.ConnectAsync(new System.Uri("wss://unity-cf-relay.peculiarnewbie.workers.dev/api/room/hecc/websocket"), CancellationToken.None);

        Debug.Log("Connected to the server");

        ReceiveWebSocket();

    }

    void Move()
    {

        if (isMovingUp)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }

        if (isMovingDown)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        }
    }

    async void SendToWebSocket(string message)
    {
        byte[] byteArray = Encoding.UTF8.GetBytes(message);
        await _clientWebSocket.SendAsync(byteArray, WebSocketMessageType.Text, true, CancellationToken.None);
        Debug.Log("Sent to the server");
    }

    async void ReceiveWebSocket()
    {
        try
        {
            using (var ms = new MemoryStream())
            {
                while (_clientWebSocket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult result;
                    do
                    {
                        var messageBuffer = WebSocket.CreateClientBuffer(1024, 16);
                        result = await _clientWebSocket.ReceiveAsync(messageBuffer, CancellationToken.None);
                        ms.Write(messageBuffer.Array, messageBuffer.Offset, result.Count);
                    }
                    while (!result.EndOfMessage);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var msgString = Encoding.UTF8.GetString(ms.ToArray());
                        Debug.Log("[WS] Received from the server: " + msgString);

                        if (msgString == "up") socketControlled.StartMovingUp();
                        else if (msgString == "dn") socketControlled.StartMovingDown();
                        else if (msgString == "su") socketControlled.StopMovingUp();
                        else if (msgString == "sd") socketControlled.StopMovingDown();

                    }
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.Position = 0;
                }
            }
        }
        catch (InvalidOperationException)
        {
            Debug.Log("[WS] Tried to receive message while already reading one.");
        }

    }

    public void OnDestroy()
    {
        m_Driver.Dispose();
    }
}
