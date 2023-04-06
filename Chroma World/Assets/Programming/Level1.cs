using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    // Loads the "movementTest" scene
    public void LoadScene()
    {
        SceneManager.LoadScene("movementTest");
    }
}
