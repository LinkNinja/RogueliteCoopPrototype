using UnityEngine;

public class PlayerInputActionsSingleton : MonoBehaviour
{
 
    
    public static PlayerInputActions Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = new PlayerInputActions();
            Instance.Enable();
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
