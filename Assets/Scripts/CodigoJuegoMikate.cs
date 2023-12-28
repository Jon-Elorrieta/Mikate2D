using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodigoJuegoMikate : MonoBehaviour
{
    public List<GameObject> cardPrefabs1;
    public List<Image> cardPrefabsimg1;

    public List<GameObject> cardPrefabs3;
    public List<GameObject> cardPrefabs4;

    public List<Sprite> spriteList = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        // Verifica si la lista de sprites se ha cargado correctamente
        if (spriteList.Count == 0)
        {
            Debug.LogError("¡La lista de sprites está vacía! Asegúrate de cargar sprites antes de asignarlos a los espacios.");
            return;
        }

        // Verifica si hay suficientes espacios para asignar cartas
        //if (cardPrefabsimg1.Count < spriteList.Count)
        //{
        //    Debug.LogError("No hay suficientes espacios para asignar todas las cartas. Asegúrate de tener al menos tantos espacios como sprites en la lista.");
        //    return;
        //}

        // Asigna cada sprite a un espacio en la mesa
        for (int i = 0; i < spriteList.Count; i++)
        {
            // Asegúrate de que la imagen esté presente en el espacio correspondiente
            if (i < cardPrefabsimg1.Count)
            {
                cardPrefabsimg1[i].sprite = spriteList[i];
            }
            else
            {
                Debug.LogWarning("Hay más sprites en la lista de los que hay espacios para mostrar. Algunos sprites no se asignarán.");
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
