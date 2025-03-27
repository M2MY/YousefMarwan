using System;
using UnityEngine;

public class CountersHandler : MonoBehaviour
{
    public static CountersHandler instance;

    [Serializable]
    public struct Counters
    {
        public int gameOver;
        public float timePlayed;
        public int littleCreaturesKilled;
        public int bossKilled;
        public int jump;
        public float timeGoRight;
        public float timeGoLeft;
        public int slash;
        public int dash;
        public int teleportation;
        public int fireOfDeath;
    }

    public Counters counters;



    private void Awake()
    {
        #region Instance
        if (instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;

        #endregion


    }

    private void Start()
    {
        counters = SaveHandler.instance.GetSaveData().counters;
    }


    private void FixedUpdate()
    {
        counters.timePlayed += Time.fixedDeltaTime;
    }
}
