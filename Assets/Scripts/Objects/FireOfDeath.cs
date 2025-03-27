using UnityEngine;

public class FireOfDeath : MonoBehaviour
{
    private Transform transformToFollow;

    private float lifeCounter;

    public void LoadData (Transform playerTransform)
    {
        transformToFollow = playerTransform;
    }

    private void Update()
    {
        transform.position = transformToFollow.position;

        transform.Rotate(Vector3.forward, -45 * Time.deltaTime);

        lifeCounter += Time.deltaTime / Time.timeScale;

        if (lifeCounter > SkillsHandler.fireOfDeathDuration)
        {
            Destroy(gameObject);
        }
    }
}
