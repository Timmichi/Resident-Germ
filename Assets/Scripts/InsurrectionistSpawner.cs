using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsurrectionistSpawner : MonoBehaviour
{
    public Insurrectionist insurrectionistPrefab;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 10.0f;
    public float spawnDistance = 15.0f;
    public int spawnAmount = 1;
    private void Start() {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate); // Invoke a method every x amount of seconds (2 seconds in this case)
    }
    private void Spawn() {  
        for (int i=0; i < this.spawnAmount; i++) {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = this.transform.position + (spawnDirection* this.spawnDistance);
            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);
            Insurrectionist insurrectionist = Instantiate(this.insurrectionistPrefab, spawnPoint, rotation); 
            insurrectionist.size = Random.Range(insurrectionist.minSize, insurrectionist.maxSize);
            insurrectionist.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
