using UnityEngine;
using UnityEngine.SceneManagement;

public class JustOpen : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private string playerName;

    private void OnMouseDown() {
        GameManager.Instance.SetPlayerName(playerName);
        SceneManager.LoadScene(sceneName);
    }
}