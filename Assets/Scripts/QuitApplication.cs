using UnityEngine;

public class QuitApplication : MonoBehaviour
{

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
            Debug.Log("按下ESC");
        }
    }
}
