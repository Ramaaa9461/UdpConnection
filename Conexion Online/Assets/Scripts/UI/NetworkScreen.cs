using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

public class NetworkScreen : MonoBehaviourSingleton<NetworkScreen>
{
    [SerializeField] GameObject tcpConnection;
    [Space]
    [SerializeField] GameObject networkScreenUDP;
    [SerializeField] GameObject networkScreenTCP;
    [SerializeField] GameObject chatScreenUDP;
    [SerializeField] GameObject chatScreenTCP;
    [Space]
    public Button connectBtnUdp;
    public Button startServerBtnUdp;
    public InputField portInputFieldUdp;
    public InputField addressInputFieldUdp;
    [Space]
    public Button connectBtnTcp;
    public Button startServerBtnTcp;
    public InputField portInputFieldTcp;
    public InputField addressInputFieldTcp;


     void Awake()
    {

        connectBtnUdp.onClick.AddListener(OnConnectBtnClickUdp);
        startServerBtnUdp.onClick.AddListener(OnStartServerBtnClickUdp);

        connectBtnTcp.onClick.AddListener(OnConnectBtnClickTcp);
        startServerBtnTcp.onClick.AddListener(OnStartServerBtnClickTcp);
    }

    private void OnConnectBtnClickUdp()
    {
        IPAddress ipAddress = IPAddress.Parse(addressInputFieldUdp.text);
        int port = System.Convert.ToInt32(portInputFieldUdp.text);

        NetworkManager.Instance.StartClient(ipAddress, port);
        
        changeScreenUDP();
    }

    private void OnStartServerBtnClickUdp()
    {
        int port = System.Convert.ToInt32(portInputFieldUdp.text);
        NetworkManager.Instance.StartServer(port);
        
        changeScreenUDP();
    }



    private void OnConnectBtnClickTcp()
    {
        IPAddress ipAddress = IPAddress.Parse(addressInputFieldTcp.text);
        int port = System.Convert.ToInt32(portInputFieldTcp.text);

        tcpConnection.AddComponent<TCPTestClient>().startTpcCleint(ipAddress,port);
        //NetworkManager.Instance.StartClient(ipAddress, port);

        changeScreenTCP();
    }

    private void OnStartServerBtnClickTcp()
    {
        IPAddress ipAddress = IPAddress.Parse(addressInputFieldTcp.text);

        int port = System.Convert.ToInt32(portInputFieldTcp.text);
        
        tcpConnection.AddComponent<TCPTestServer>().startTpcServer(ipAddress, port);
        //NetworkManager.Instance.StartServer(port);

        changeScreenTCP();
    }

    void changeScreenTCP()
    {
        networkScreenTCP.SetActive(false);
        networkScreenUDP.SetActive(false);
        chatScreenTCP.SetActive(true);
    }
    void changeScreenUDP()
    {
        networkScreenTCP.SetActive(false);
        networkScreenUDP.SetActive(false);
        chatScreenUDP.SetActive(true);
    }
}


//https://gist.github.com/danielbierwirth/0636650b005834204cb19ef5ae6ccedb