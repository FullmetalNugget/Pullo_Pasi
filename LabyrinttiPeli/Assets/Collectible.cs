using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1; // esim. pisteet, kolikot, energia

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Lisää pisteet tai muu toiminto
            Destroy(gameObject);
            Debug.Log("Objekt lähdemýs");
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.Add(value);
            }

            // Tuhoa esine
        }
    }
}