using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int value = 1; // esim. pisteet, kolikot, energia

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Lis‰‰ pisteet tai muu toiminto
            PlayerInventory inventory = collision.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.Add(value);
            }

            // Tuhoa esine
            Destroy(gameObject);
        }
    }
}