using UnityEngine;

public class MonsterDeath : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject Maincanvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);    
        Maincanvas.SetActive(true);    
    }


    public void MonsterKilled()
    {
        Maincanvas.SetActive(false);        
        canvas.SetActive(true); 
        Time.timeScale= 0;  
    }

}
