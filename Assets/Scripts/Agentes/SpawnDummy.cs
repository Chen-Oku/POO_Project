#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using UnityEngine;

public class SpawnDummy : MonoBehaviour
{
    public Transform spawnPoint;
    public float respawnDelay = 10f;
    
    [SerializeField] private GameObject _dummyPrefab;
    private GameObject currentDummy;
    private bool isSpawning;
    
    private void Awake()
    {
        // Si no se asignó un prefab, intentar encontrarlo automáticamente
        if (_dummyPrefab == null)
        {
            FindDummyPrefab();
        }
    }
    
    private void FindDummyPrefab()
    {
#if UNITY_EDITOR
        // Esta parte solo se ejecuta en el editor, no en builds
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            
            if (prefab != null && prefab.GetComponent<PortadorNoJugable>() != null)
            {
                _dummyPrefab = prefab;
                Debug.Log($"Encontrado prefab automáticamente: {prefab.name} en {path}");
                break;
            }
        }
        
        if (_dummyPrefab == null)
        {
            Debug.LogWarning("No se encontró ningún prefab con el componente PortadorNoJugable. Asígnalo manualmente.");
        }
#endif
    }

    void Update()
    {
        // Si no hay un jefe activo y no se está esperando para spawnear, invocar al jefe
        if (currentDummy == null && !isSpawning)
        {
            StartCoroutine(SpawnDummyRoutine());
        }
    }

    IEnumerator SpawnDummyRoutine()
    {
        isSpawning = true;
        Debug.Log("GolemBoss Spawn Start method called."); // Mensaje de depuración

        // Esperar el tiempo de respawn
        yield return new WaitForSeconds(respawnDelay);

        // Spawnear el GolemBoss
        currentDummy = Instantiate(_dummyPrefab, spawnPoint.position, spawnPoint.rotation);

        isSpawning = false;
    }
}