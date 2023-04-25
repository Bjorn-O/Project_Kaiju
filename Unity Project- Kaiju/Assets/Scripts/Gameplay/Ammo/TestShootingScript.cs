using UnityEngine;

public class TestShootingScript : MonoBehaviour
{
    [SerializeField] private OverheatingController overheatingController;

    void Update()
    {
        // check for fire input
        if (Input.GetMouseButton(0) && overheatingController.isOverheated == false)
        {
            overheatingController.Fire();
            print("shooting");
        }
        else
        {
            overheatingController.CoolDown();
            print("cooling");
        }
    }


}
