using UnityEngine;

public class TeleportationTool : MonoBehaviour
{
    private float counter = 0;

    private bool Loaded;

    private Transform playerTransform;

    // ---

    public void LoadData(float force, Vector2 directorVector, Transform targetTransform)
    {
        GetComponent<Rigidbody2D>().velocity = directorVector * force;
        
        playerTransform = targetTransform;

        Loaded = true;
    }

    // ---

    private void Update()
    {
        if (!Loaded) return;

        counter += Time.deltaTime / Time.timeScale;

        if (counter >= SkillsHandler.teleporationToolDuration)
        {
            DestroyTool();
        }
    }

    // ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        WhatACollisionDoes();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        WhatACollisionDoes();
    }


    private void WhatACollisionDoes()
    {
        if (!Loaded) return;

        DestroyTool();
    }

    // ---

    private void DestroyTool()
    {
        AudioHandler.instance.TeleportationEffect();

        playerTransform.position = transform.position;
        Destroy(gameObject);
    }
}
