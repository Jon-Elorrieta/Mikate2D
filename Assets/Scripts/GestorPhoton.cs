using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;
using TMPro;

public class GestorPhoton : MonoBehaviourPunCallbacks
{

    public GameObject[] playerPrefabs; // Un array de prefabs para cada jugador

    // Interfaz de usuario para mostrar el estado del juego
    public TextMeshProUGUI estadoJuegoText;

    // N�mero esperado de jugadores
    private int jugadoresEsperados = 4;

    // Indica si la partida ha comenzado
    private bool partidaComenzada = false;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Conectado al master");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    // M�todo que se ejecuta cuando un jugador se une a la sala
    public override void OnJoinedRoom()
    {
        // Obt�n el ID del jugador local
        int playerID = PhotonNetwork.LocalPlayer.ActorNumber;

        // Instancia el prefab del jugador correspondiente
        GameObject playerPrefab = playerPrefabs[playerID - 1];
        GameObject playerObject = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

        // Comprueba si se ha alcanzado el n�mero esperado de jugadores
        if (PhotonNetwork.CurrentRoom.PlayerCount == jugadoresEsperados && !partidaComenzada)
        {
            // Comienza la partida
            ComenzarPartida();
        }

        // No es necesario instanciar las cartas aqu�, ya que est�n en el prefab del jugador
        // Las cartas se inicializar�n autom�ticamente cuando se instancie el jugador

        ActualizarEstadoJuego();
        //TuMetodoDondeQuieresLlamarInicializarJuego();
    }

    // M�todo que se ejecuta cuando un jugador abandona la sala
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // Actualiza el estado del juego cuando un jugador sale
        ActualizarEstadoJuego();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ActualizarEstadoJuego();
    }

    // M�todo para actualizar el estado del juego
    private void ActualizarEstadoJuego()
    {
        if (estadoJuegoText != null)
        {
            // Muestra el n�mero actual de jugadores en la sala
            estadoJuegoText.text = $"Esperando Jugadores: {PhotonNetwork.CurrentRoom.PlayerCount}/{jugadoresEsperados}";
        }
    }

    // M�todo para comenzar la partida
    private void ComenzarPartida()
    {
        partidaComenzada = true;

        // Aqu� puedes agregar la l�gica de inicio de la partida
        Debug.Log("La partida ha comenzado!");
        TuMetodoDondeQuieresLlamarInicializarJuego();

        // Por ejemplo, podr�as desactivar el texto de estado del juego
        if (estadoJuegoText != null)
        {
            estadoJuegoText.gameObject.SetActive(false);
        }
    }

    private void TuMetodoDondeQuieresLlamarInicializarJuego()
    {
        // Puedes encontrar el objeto que contiene LogicaDelJuego utilizando su nombre o tag
        GameObject logicaDelJuegoObj = GameObject.Find("ScriptController");

        // Si encuentras el objeto, obt�n la referencia al script LogicaDelJuego y llama a la funci�n
        if (logicaDelJuegoObj != null)
        {
            CodigoJuegoMikate logicaDelJuego = logicaDelJuegoObj.GetComponent<CodigoJuegoMikate>();

            if (logicaDelJuego != null)
            {
                logicaDelJuego.InicializarJuego();
            }
            else
            {
                Debug.LogError("No se encontr� el script LogicaDelJuego en el objeto especificado.");
            }
        }
        else
        {
            Debug.LogError("No se encontr� el objeto especificado que contiene LogicaDelJuego.");
        }
    }


}
