using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controljuego : MonoBehaviour
{
    // Start is called before the first frame update
    public void CambiarNivel(int nivel)
    {
        if (nivel == 0) {
            SceneManager.LoadScene("Main Scene");
        } else {
            SceneManager.LoadScene("Nivel " + nivel);
        }        
    }
}
