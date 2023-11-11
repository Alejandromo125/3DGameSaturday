using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    bool CanOpen = false;
    public Door[] doors;
    public KeyColor myColor;
    bool locked = false;
    Animator key;
    // Start is called before the first frame update
    void Start()
    {
       key = GetComponent<Animator>();
    }

    public void UseKey()
    {
        foreach (Door door in doors) 
        {
            door.OpenClose();
        }
    }

    public bool CheckTheKey()
    {
        if(GameManager.gameManager.redKey > 0 && myColor == KeyColor.Red)
        {
            GameManager.gameManager.redKey--;
            GameManager.gameManager.redKeyText.text = GameManager.gameManager.redKey.ToString();
            locked = true;
            return true;
        }
        else if (GameManager.gameManager.greenKey > 0 && myColor == KeyColor.Green)
        {
            GameManager.gameManager.greenKey--;
            GameManager.gameManager.greenKeyText.text = GameManager.gameManager.greenKey.ToString();
            locked = true;
            return true;
        }
        else if (GameManager.gameManager.goldKey > 0 && myColor == KeyColor.Gold)
        {
            GameManager.gameManager.goldKey--;
            GameManager.gameManager.goldKeyText.text = GameManager.gameManager.goldKey.ToString();
            locked = true;
            return true;
        }
        else
        {
            Debug.Log("You do not have a key");
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (CanOpen && !locked)
        {
            GameManager.gameManager.SetUseInfo("Press E to open lock");
        }
        if(Input.GetKeyDown(KeyCode.E) && CanOpen && !locked)
        {
            key.SetBool("useKey", CheckTheKey());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CanOpen = true;
            Debug.Log("You can use the lock");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            CanOpen = false;
            GameManager.gameManager.SetUseInfo("");
            Debug.Log("You cannot use the lock");
        }
    }
}
