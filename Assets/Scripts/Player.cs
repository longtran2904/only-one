using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public Vector3 position;
    //public GameObject levelFinishedSound;
    //public GameObject levelReset;

    private void Start()
    {
        position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        //Instantiate(levelReset, transform.position, Quaternion.identity, transform.parent);
        Destroy(this.gameObject);
    }
}
