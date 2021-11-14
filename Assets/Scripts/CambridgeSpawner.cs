using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField] class CambridgeSpawner : MonoBehaviour
{
    [SerializeField] float trajectoryVariance = 15.0f;
    [SerializeField] float spawnRate = 2.0f;
    [SerializeField] float spawnDistance = 15.0f;
    [SerializeField] int spawnAmount = 1;
    public Cambridge cambridgePrefab;
    void Start() {
        InvokeRepeating(nameof(spawn), spawnRate, spawnRate); // Invoke a method every x amount of seconds (2 seconds in this case)
    }
    void spawn() {
        for (int i=0; i < spawnAmount; i++) {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = this.transform.position + (spawnDirection* spawnDistance);
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            Cambridge cambridge = Instantiate(this.cambridgePrefab, spawnPoint, rotation); 
            cambridge.size = Random.Range(cambridge.minSize, cambridge.maxSize);
            cambridge.setTrajectory(rotation * -spawnDirection);
        }
    }
}
