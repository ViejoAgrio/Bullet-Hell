using UnityEngine;
using TMPro;

public class ObjectCounter : MonoBehaviour
{
    public string objectTag;  // Tag de los objetos a contar
    public TextMeshProUGUI countText; // Referencia al componente TextMeshProUGUI

    void Update()
    {
        // Contar los objetos en la escena que tienen el tag especificado
        int objectCount = GameObject.FindGameObjectsWithTag(objectTag).Length;

        // Actualizar el texto en pantalla
        countText.text = "Proyectiles en escena:" + objectCount.ToString();
    }
}
