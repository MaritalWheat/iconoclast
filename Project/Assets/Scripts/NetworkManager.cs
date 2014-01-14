using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {

	private float m_buttonX;
	private float m_buttonY;
	private float m_buttonH;
	private float m_buttonW;
	private Rect m_serverRect, m_clientRect;
	private bool m_creatingServer, m_refreshHosts;
	private List<HostData> m_hostsAvailable;
	private bool m_hostTurnFinished, m_clientTurnFinished;
	private const string GAME_NAME = "iconoclast_the_game_1234";
	private Server m_server;
	public static NetworkManager m_instance;

	// Use this for initialization
	void Start () {
		if (m_instance==null) {
			m_instance = this;
		}
		m_buttonX = Screen.width - Screen.width * .2f;
		m_buttonY = Screen.height - Screen.height * .05f;
		m_buttonW = Screen.width * .2f;
		m_buttonH = Screen.height * .05f;
		m_clientRect = new Rect(m_buttonX, m_buttonY, m_buttonW, m_buttonH);
		m_serverRect = new Rect (m_buttonX, m_buttonY - (m_buttonH * 2), m_buttonW, m_buttonH);
	}
	
	// Update is called once per frame
	void Update () {
		if (m_refreshHosts) {
			if (MasterServer.PollHostList().Length > 0) {
				m_refreshHosts = false;
				m_hostsAvailable = new List<HostData>(MasterServer.PollHostList());
			}
		}
	}

	//Show the buttons to create or to connect to a server
	void OnGUI() {
		if (!Network.isClient && !Network.isServer) {
			if (GUI.Button(m_serverRect, "Create Server") ) {
				CreateServer();
			}
			//Do shit to allow clients to connect
			if (GUI.Button(m_clientRect, "Connect to server") ) {
				MasterServer.RequestHostList(GAME_NAME);
				m_refreshHosts = true;
			}

			if (m_hostsAvailable != null) {
				int index = 0;
				foreach(HostData host in m_hostsAvailable) {
					if (GUI.Button(new Rect(Screen.width/2.0f, 100 + (index * m_buttonH), m_buttonW, m_buttonH), host.gameName) ) {
						Network.Connect(host);
					}
				}
			}
		}
	}

	//Initializes a server on port 25251(random choice), that will only let one other person connect
	void CreateServer() {
		Network.InitializeServer (2, 25251, !Network.HavePublicAddress());
		//Register the server on Unity's Master Server. Game_Name must be unique.
		MasterServer.RegisterHost(GAME_NAME, "Iconoclast Test");
		m_server = gameObject.AddComponent<Server>();
		m_creatingServer = true;
	}

	//Just listens for the confirmation that the server has been created
	void OnMasterServerEvent(MasterServerEvent mse) {
		if (mse == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log("We are registered");
			//Create Game Manager

		}
	}
	

	void SendCharacterLocations(List<Vector2> newLocs)
	{
		if (Network.isClient) {
			for(int i = 0; i<newLocs.Count; i++) {
				networkView.RPC("UpdateEnemyLocation", RPCMode.Others, i, new Vector3(newLocs[i].x, newLocs[i].y, 0));
			}
		}
	}

	//Both client and host call this function. The function then decides what to do.
	//This function will send to the server the proposed location of every character and the proposed attacks
	public void FinishPlayerTurn() {
		int index = 0;

		//If it is the client, we will need to call RPC functions which will in turn call the server
		if (Network.isClient) {
			foreach (Vector2 pos in GameManager.m_instance.GetCharacterPositions()) {
				networkView.RPC("SetClientProposedCharacterLocation", RPCMode.Server, index, new Vector3(pos.x, pos.y));
				index++;
			}
			networkView.RPC("FinishClientTurn", RPCMode.Server);
		} else { //Else, just directly call the server
			foreach (Vector2 pos in GameManager.m_instance.GetCharacterPositions()) {
				m_server.SetProposedLocation(true, index, pos);
				index++;
			}
			m_server.FinishTurn(true);
		}
	}

	//Start the Hosts turn and call the rpc to start the clients turn
	public void StartTurns()
	{
		networkView.RPC("StartClientTurn", RPCMode.All);
	}
	
	[RPC]
	public void StartClientTurn() {
		GameManager.m_instance.StartNextTurn();
	}

	[RPC]
	public void FinishClientTurn(){
		m_server.FinishTurn(false);
	}

	[RPC]
	public void SetClientProposedCharacterLocation(int characterNum, Vector3 position)
	{
		m_server.SetProposedLocation(false, characterNum, new Vector2(position.x, position.y));
	}
}
