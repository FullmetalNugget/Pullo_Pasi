using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1; // esim. pisteet, kolikot, energia

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Lisää pisteet tai muu toiminto
            Destroy(gameObject);
            Debug.Log("Objekt lähdemýs");
            PlayerInventory inventory = collision.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.Add(value);
            }

            // Tuhoa esine
        }
    }
}