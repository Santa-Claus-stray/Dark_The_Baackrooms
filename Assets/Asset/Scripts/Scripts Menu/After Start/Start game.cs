using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        // ������� �� Level 0;
        SceneManager.LoadScene("Level 0");
    }
}