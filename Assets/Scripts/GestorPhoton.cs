using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

public class GestorPhoton : MonoBehaviourPunCallbacks
{

    public GameObject[] playerPrefabs; // Un array de prefabs para cada jugador
    //public Transform spawnPoint; // Punto de spawn de las cartas


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
    // Método que se ejecuta cuando un jugador se une a la sala
    public override void OnJoinedRoom()
    {
        // Obtén el ID único del jugador local
        int playerID = PhotonNetwork.LocalPlayer.ActorNumber;

        // Calcula la posición y la rotación de spawn en función del ID del jugador
        Vector3 spawnPosition;
        Quaternion spawnRotation;
        CalculateSpawnPositionAndRotation(playerID, out spawnPosition, out spawnRotation);

        // Instancia el prefab del jugador en la posición y rotación calculadas
        PhotonNetwork.Instantiate(playerPrefabs[playerID - 1].name, spawnPosition, spawnRotation);
        Debug.Log("Conectado " + playerPrefabs[playerID - 1].name);
    }


    // Método para calcular la posición y rotación de spawn en función del ID del jugador
    private void CalculateSpawnPositionAndRotation(int playerID, out Vector3 spawnPosition, out Quaternion spawnRotation)
    {
        float tableSize = 5f; // Tamaño de la mesa cuadrada

        switch (playerID)
        {
            case 1: // Jugador 1 (abajo)
                spawnPosition = new Vector3(0f, -tableSize / 2f, 0f);
                spawnRotation = Quaternion.Euler(0f, 0f, 0f); // Rotación para jugador abajo
                break;
            case 2: // Jugador 2 (izquierda)
                spawnPosition = new Vector3(-tableSize / 2f, 0f, 0f);
                spawnRotation = Quaternion.Euler(0f, 0f, 90f); // Rotación para jugador izquierda
                break;
            case 3: // Jugador 3 (arriba)
                spawnPosition = new Vector3(0f, tableSize / 2f, 0f);
                spawnRotation = Quaternion.Euler(0f, 0f, 180f); // Rotación para jugador arriba
                break;
            case 4: // Jugador 4 (derecha)
                spawnPosition = new Vector3(tableSize / 2f, 0f, 0f);
                spawnRotation = Quaternion.Euler(0f, 0f, -90f); // Rotación para jugador derecha
                break;
            default:
                Debug.LogError("ID de jugador no válido");
                spawnPosition = Vector3.zero;
                spawnRotation = Quaternion.identity;
                break;
        }
    }


}
