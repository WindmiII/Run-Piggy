using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnPlayPressed()
    {
        GameManager.Instance.ResetSession(); // safely reset score/coins
        SceneManager.LoadScene("PlayScene");
    }

    public void ResetSaveData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All save data wiped.");
    }
}