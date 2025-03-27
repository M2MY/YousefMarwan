using UnityEngine;

public class Sign : MonoBehaviour
{
    [SerializeField] private GameObject messageToShow;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;

        messageToShow.SetActive(true);
    } 

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;

        messageToShow.SetActive(false);
    }
}
