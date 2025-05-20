using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Panggil OnTargetDestroyed dan berikan objek ini sebagai argumen
            gameManager.OnTargetDestroyed(gameObject);
            Destroy(gameObject);
            Debug.Log("Tambah Score");
        }
    }
}