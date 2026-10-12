using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTree : MonoBehaviour
{
    public void GoToTreeClick()
    {
        SceneManager.LoadScene("TreeScene");
        AudioManager.Instance.PlayClic();
    }
}
