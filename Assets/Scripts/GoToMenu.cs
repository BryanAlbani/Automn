using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    public void GoToMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
        DataManager.Instance.ResetData();
        AudioManager.Instance.PlayClic();
    }
}
