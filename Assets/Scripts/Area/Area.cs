using System;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public static Area Instance;

    [field: Header("Data")]
    [field: SerializeField] public AreaData AreaData { get; private set; }

    [field: Header("Decorations")]
    [field: SerializeField, Range(0, 50)] private int maxDecorations;
    [field: SerializeField, Range(1, 5)] private float decorationOffset;
    [field: SerializeField] private Transform decorationsContainer;
    [field: SerializeField] private List<GameObject> decorationsPrefabs = new();

    private List<GameObject> spawnedDecorations = new();

    private void Awake() => Instance = this;

    private void Start()
    {
        SpawnDecorations();
    }

    private void OnDestroy()
    {
        ClearDecorations();
    }

    private void OnValidate()
    {
        AreaData.ValidateAreaData();
    }

    public Vector3 GetRandomPointInActiveArea()
    {
        Vector3 randomPoint = Vector3.zero;
        randomPoint.x = UnityEngine.Random.Range(0, AreaData.ActiveAreaBounds.x);
        randomPoint.z = UnityEngine.Random.Range(0, AreaData.ActiveAreaBounds.y);

        return randomPoint;
    }

    public Vector3 GetRandomPointInInactiveArea()
    {
        Vector3 randomPoint = Vector3.zero;

        randomPoint.x = UnityEngine.Random.Range(AreaData.MinInactiveAreaBounds.x,
            AreaData.MaxInactiveAreaBounds.x);

        randomPoint.z = UnityEngine.Random.Range(AreaData.MinInactiveAreaBounds.y,
            AreaData.MaxInactiveAreaBounds.y);

        if (randomPoint.x > 0 && randomPoint.x <= AreaData.ActiveAreaBounds.x
            || randomPoint.y > 0 && randomPoint.y <= AreaData.ActiveAreaBounds.y)
            return GetRandomPointInInactiveArea();

        return randomPoint;
    }

    private void ClearDecorations()
    {
        foreach (Transform child in decorationsContainer)
            DestroyImmediate(child.gameObject);

        spawnedDecorations.Clear();
    }

    private Vector3 GenerateDecorationPosition(GameObject decoration)
    {
        Vector3 position = GetRandomPointInInactiveArea();
        bool isValidPosition = true;

        for (int i = 0; i < spawnedDecorations.Count; i++)
        {
            if (Vector3.Distance(position, spawnedDecorations[i].transform.position) > decorationOffset)
                continue;

            isValidPosition = false;
            break;
        }

        if (!isValidPosition)
            return GenerateDecorationPosition(decoration);

        position.y = decoration.transform.position.y;
        Debug.Log(decoration.transform.position.y);
        return position;
    }

    private void GenerateDecoration()
    {
        GameObject decoration = decorationsPrefabs[UnityEngine.Random.Range(0, decorationsPrefabs.Count)];
        decoration.transform.position = GenerateDecorationPosition(decoration);

        GameObject spawnedDecoration = Instantiate(decoration, decorationsContainer);
        spawnedDecorations.Add(spawnedDecoration);
    }

    private void SpawnDecorations()
    {
        ClearDecorations();

        for (int i = 0; i < maxDecorations; i++)
            GenerateDecoration();
    }
}
