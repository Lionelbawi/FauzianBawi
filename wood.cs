using UnityEngine;

public class Wood : MonoBehaviour
{
    public GameObject uiWood;   // Drag UI kayu dari Canvas

    private void Start()
    {
        if (uiWood != null)
            uiWood.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (uiWood != null)
                uiWood.SetActive(true);

            Destroy(gameObject);
        }
    }
}
