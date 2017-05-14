﻿using UnityEngine;

public class FoodSpawnerScript : MonoSingleton<FoodSpawnerScript>
{
    public GameObject foodPrefab;
    public GameObject foodContainer;
    public Collider spawnArea;

    private float spawnThreshold;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        spawnThreshold = 0.7f;
    }

    public void SpawnFood(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject instance = Instantiate(foodPrefab, GenerateRandomPosition(), Quaternion.identity);
            instance.transform.parent = foodContainer.transform;
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        float xPos = Random.Range(-spawnArea.bounds.size.x / 2 * spawnThreshold, spawnArea.bounds.size.x / 2 * spawnThreshold);
        float yPos = spawnArea.transform.position.y;
        float zPos = Random.Range(-spawnArea.bounds.size.z / 2 * spawnThreshold, spawnArea.bounds.size.z / 2 * spawnThreshold);
        Vector3 randomPosition = new Vector3(xPos, yPos, zPos);
        return randomPosition;
    }
}
