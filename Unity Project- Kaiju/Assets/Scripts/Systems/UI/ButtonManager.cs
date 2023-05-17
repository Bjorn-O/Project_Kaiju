using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject[] menuButtons;
    [SerializeField] private int buttonsToHide;

    public void LoadSpecifiedScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LevelSelect(bool loadLevelSelect) //Load or unload the level select buttons. True = load, false = unload.
    {
        if (loadLevelSelect) //Load level select
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
                if (i < buttonsToHide)
                {
                    menuButtons[i].SetActive(false);
                }
                else
                {
                    menuButtons[i].SetActive(true);
                }
            }
        }
        else //Unload level select
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
                if (i < buttonsToHide)
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
}