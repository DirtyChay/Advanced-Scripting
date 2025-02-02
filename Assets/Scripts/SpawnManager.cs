﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SpawnManager : MonoBehaviour
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("The different types of enemies that should be spawned and their corresponding spawn information.")]
   private EnemySpawnInfo[] m_EnemyTypes;
   #endregion

   #region Non-Editor Variables
   /* A timer for each enemy that should spawn an enemy of the corresponding
    * type when it reaches 0. Additionally, upon reaching 0, the value of the
    * timer should be reset to the appropriate value based on the enemy's spawn
    * rate. Therefore, we will have infinite spawning of the enemies.
    * 
    * Some challenges:
    * - Implement the spawning using a coroutine instead of this using this way
    * - Make the spawn rate ramp up (may require creating a mutator in the EnemySpawnInfo struct)
    */
   private float[] m_EnemySpawnTimers;
   #endregion

   #region Spawning Methods
   public void StartSpawning() {
      for (int i = 0; i < m_EnemyTypes.Length; i++) {
         StartCoroutine(Spawn(i));
      }
   }

   private IEnumerator Spawn(int enemyID) {
      EnemySpawnInfo info = m_EnemyTypes[enemyID];
      yield return new WaitForSeconds(info.FirstSpawnTime);
      Instantiate(info.EnemyPrefab);
      while (true) {
         yield return new WaitForSeconds(info.SpawnRate);
         Instantiate(info.EnemyPrefab);
      }
   }
   #endregion

   #region First Time Initialization and Set Up
   private void Awake()
   {
      // Initialize the spawn timers using the FirstSpawnTime variable
      StartSpawning();
   }
   #endregion

   #region Main Updates
   private void Update()
   {
      // You may want to use either a foreach or for loop (for scalability)
      // Check if its time to spawn a particular enemy
      // If it is, just spawn the enemy using Instantiate(m_EnemyTypes[i].EnemyPrefab)
         // Make sure to reset the timer back to the appropriate value based on SpawnRate
      // Else, increase the timer using Time.deltaTime
   }
   #endregion
}

[System.Serializable]
public struct EnemySpawnInfo
{
   #region Editor Variables
   [SerializeField]
   [Tooltip("The enemy prefab to spawn. This is what will be instantiated each time.")]
   private GameObject m_EnemyPrefab;

   [SerializeField]
   [Tooltip("The time we should wait before the first enemy is spawned.")]
   private float m_FirstSpawnTime;

   [SerializeField]
   [Range(0, 100)]
   [Tooltip("How many enemies should spawn per second.")]
   private float m_SpawnRate;
   #endregion

   #region Accessors and Mutators
   public GameObject EnemyPrefab
   {
      get { return m_EnemyPrefab; }
   }

   public float FirstSpawnTime
   {
      get { return m_FirstSpawnTime; }
   }

   // Doing (1 / SpawnRate) might be more useful than directly using SpawnRate
   public float SpawnRate
   {
      get { return m_SpawnRate; }
   }
   #endregion
}