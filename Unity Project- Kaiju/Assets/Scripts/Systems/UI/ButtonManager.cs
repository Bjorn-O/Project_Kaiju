using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject[] menuButtons;

    public void LoadSpecifiedScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadLevelSelect()
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (i < 3)
            {
                menuButtons[i].SetActive(false);
            }
            else
            {
                menuButtons[i].SetActive(true);
            }
        }
    }

    public void UnloadLevelSelect()
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (i < 3)
            {
                menuButtons[i].SetActive(true);
            }
            else
            {
                menuButtons[i].SetActive(false);
            }
        }
    }
}
