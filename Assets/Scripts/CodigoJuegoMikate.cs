using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodigoJuegoMikate : MonoBehaviour
{
    //public List<GameObject> cardPrefabs1;
    //public List<Image> cardPrefabsimg1;
    //public List<Image> cardPrefabsimg2;
    //public List<Image> cardPrefabsimg3;
    //public List<Image> cardPrefabsimg4;

    //public List<GameObject> cardPrefabs3;
    //public List<GameObject> cardPrefabs4;

    public List<Sprite> spriteList = new List<Sprite>();

    // Prefabs de jugadores instaciados desde photon
    private GameObject[] jugadoresInstanciados; // Jugadores instanciados en la sala

    private Dictionary<int, List<Sprite>> cartasPorJugador = new Dictionary<int, List<Sprite>>();


    void Start()
    {
        Debug.Log("Inicio del juego");
        // Inicializa el juego, puedes llamar a esta función cuando sea necesario
        InicializarJuego();
    }

    public void InicializarJuego()
    {
        Debug.Log("Inicializando el juego");
        // Obtén los jugadores instanciados desde Photon
        jugadoresInstanciados = GameObject.FindGameObjectsWithTag("Player");

        // Imprime información sobre cada jugador
        foreach (GameObject jugador in jugadoresInstanciados)
        {
            Debug.Log($"Jugador encontrado: {jugador.name}");
        }

        // Baraja la lista de cartas
        List<Sprite> cartasBarajadas = BarajarCartas(spriteList);

        // Asigna las cartas a cada jugador sin repetir
        AsignarCartasAJugadores(cartasBarajadas);

        // Puedes acceder a las cartas de cada jugador a través de cartasPorJugador
    }

    List<Sprite> BarajarCartas(List<Sprite> cartas)
    {
        Debug.Log("Barajando cartas");
        List<Sprite> cartasBarajadas = new List<Sprite>(cartas);
        int n = cartasBarajadas.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            Sprite carta = cartasBarajadas[k];
            cartasBarajadas[k] = cartasBarajadas[n];
            cartasBarajadas[n] = carta;
        }
        return cartasBarajadas;
    }

    void AsignarCartasAJugadores(List<Sprite> cartasBarajadas)
    {
        Debug.Log("Asignando cartas a jugadores");
        int jugadorIndex = 0;

        foreach (var jugador in jugadoresInstanciados)
        {
            // Asigna las primeras tres cartas a la mano del jugador
            AsignarCartas(jugador, cartasBarajadas.GetRange(jugadorIndex * 9, 3), "Mano");

            // Asigna las siguientes tres cartas a la mesa del jugador
            AsignarCartas(jugador, cartasBarajadas.GetRange(jugadorIndex * 9 + 3, 3), "Mesa");

            // Asigna las últimas tres cartas a la mesa abajo del jugador
            AsignarCartas(jugador, cartasBarajadas.GetRange(jugadorIndex * 9 + 6, 3), "MesaAbajo");

            jugadorIndex++;
        }
    }

    void AsignarCartas(GameObject jugador, List<Sprite> cartas, string categoria)
    {
        // Lógica de asignación de cartas al jugador.
        // Ejemplo: Coloca las cartas en las posiciones correctas del jugador.
        string nombreJugador = jugador.name.Replace("(Clone)", "");
        foreach (var carta in cartas)
        {
            Debug.Log($"Asignando carta {carta.name} a la categoría {categoria} del jugador {nombreJugador}");
            // Implementa tu lógica aquí...
        }
        

        int i = 0; // Agrega esta línea para inicializar la variable i

        // Obtén referencias a las áreas de cartas del jugador
        Transform mano = jugador.transform.Find($"{nombreJugador}{categoria}1");
        Transform mano2 = jugador.transform.Find($"{nombreJugador}{categoria}2");
        Transform mano3 = jugador.transform.Find($"{nombreJugador}{categoria}3");


        Transform mesa = jugador.transform.Find($"{nombreJugador}{categoria}1");
        Transform mesa2 = jugador.transform.Find($"{nombreJugador}{categoria}2");
        Transform mesa3 = jugador.transform.Find($"{nombreJugador}{categoria}3");


        Transform mesaAbajo = jugador.transform.Find($"{nombreJugador}{categoria}1");
        Transform mesaAbajo2 = jugador.transform.Find($"{nombreJugador}{categoria}2");
        Transform mesaAbajo3 = jugador.transform.Find($"{nombreJugador}{categoria}3");

        // Lógica de asignación de cartas al jugador.
        // Coloca las cartas en las posiciones correctas del jugador.

        // Asigna las cartas a la mano
        i++;
        AsignarCartasAlArea(mano, cartas[0], $"{jugador.name}{categoria}{i}");
        i++;
        AsignarCartasAlArea(mano2, cartas[1], $"{jugador.name}{categoria}{i}");
        i++;
        AsignarCartasAlArea(mano3, cartas[2], $"{jugador.name}{categoria}{i}");

        i=0;
        // Asigna las cartas a la mesa
        i++;
        AsignarCartasAlArea(mesa, cartas[0], $"{jugador.name}{categoria}{i}");
        i++;
        AsignarCartasAlArea(mesa2, cartas[1], $"{jugador.name}{categoria}{i}");
        i++;
        AsignarCartasAlArea(mesa3, cartas[2], $"{jugador.name}{categoria}{i}");

        i = 0;
        // Asigna las cartas a la mesa abajo
        AsignarCartasAlArea(mesaAbajo, cartas[0], $"{jugador.name}{categoria}{i}");
        i++;
        AsignarCartasAlArea(mesaAbajo2, cartas[1], $"{jugador.name}{categoria}{i}");
        i++;
        AsignarCartasAlArea(mesaAbajo3, cartas[2], $"{jugador.name}{categoria}{i}");
    }

    void AsignarCartasAlArea(Transform area, Sprite carta, string categoria)
    {
        Debug.Log($"Asignando cartaS A {area.parent.name}");
        // Asegúrate de que el área y su padre existan
        if (area != null && area.parent != null)
        {

            // Obtiene o agrega el componente de imagen al GameObject de la carta
            Image cartaImage = area.gameObject.GetComponent<Image>();
            if (cartaImage == null)
            {
                cartaImage = area.gameObject.AddComponent<Image>();
            }

            // Asigna el sprite de la carta al componente de imagen
            cartaImage.sprite = carta;

        }
        else
        {
            Debug.LogError($"Área {categoria} no encontrada en el jugador {area.parent.name}");
        }
    }



    //// Start is called before the first frame update
    //void Start()
    //{
    //    // Verifica si la lista de sprites se ha cargado correctamente
    //    if (spriteList.Count == 0)
    //    {
    //        Debug.LogError("¡La lista de sprites está vacía! Asegúrate de cargar sprites antes de asignarlos a los espacios.");
    //        return;
    //    }

    //    // Verifica si hay suficientes espacios para asignar cartas
    //    //if (cardPrefabsimg1.Count < spriteList.Count)
    //    //{
    //    //    Debug.LogError("No hay suficientes espacios para asignar todas las cartas. Asegúrate de tener al menos tantos espacios como sprites en la lista.");
    //    //    return;
    //    //}

    //    // Asigna cada sprite a un espacio en la mesa
    //    for (int i = 0; i < spriteList.Count; i++)
    //    {
    //        // Asegúrate de que la imagen esté presente en el espacio correspondiente
    //        if (i < cardPrefabsimg1.Count)
    //        {
    //            cardPrefabsimg1[i].sprite = spriteList[i];
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Hay más sprites en la lista de los que hay espacios para mostrar. Algunos sprites no se asignarán.");
    //        }

    //        // Asegúrate de que la imagen esté presente en el espacio correspondiente
    //        if (i < cardPrefabsimg2.Count)
    //        {
    //            cardPrefabsimg2[i].sprite = spriteList[i];
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Hay más sprites en la lista de los que hay espacios para mostrar. Algunos sprites no se asignarán.");
    //        }

    //        // Asegúrate de que la imagen esté presente en el espacio correspondiente
    //        if (i < cardPrefabsimg3.Count)
    //        {
    //            cardPrefabsimg3[i].sprite = spriteList[i];
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Hay más sprites en la lista de los que hay espacios para mostrar. Algunos sprites no se asignarán.");
    //        }

    //        // Asegúrate de que la imagen esté presente en el espacio correspondiente
    //        if (i < cardPrefabsimg4.Count)
    //        {
    //            cardPrefabsimg4[i].sprite = spriteList[i];
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Hay más sprites en la lista de los que hay espacios para mostrar. Algunos sprites no se asignarán.");
    //        }
    //    }


    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}



}
