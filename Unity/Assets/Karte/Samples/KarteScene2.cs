using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KarteScene2 : MonoBehaviour
{
    public void OnMoveToScene1Click() {
        SceneManager.LoadScene("sampleScene");
    }
}
