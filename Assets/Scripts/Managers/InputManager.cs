using UnityEngine;

public class InputManager : MonoBehaviour
{   
    public static InputManager Instance;
   
 
   
    //Metoda urachaminia przy starcie 
    private void Awake()
    {
        if(Instance==null)
          Instance = this;
        else
            Destroy(Instance);

    }
    private void CustomUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Flipping the screen

        }   
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Obraca bronie wogol statku  przy pomocy a/d lub prawo lewo
            float dir = Input.GetAxis("Horizontal");
        }

        //Prze³acza sie miedzy broniami 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
       
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {

        }
       
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            //gracz wybiera jedna z trzech kart ulepszen
        }


        

    }
}
