using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    //Consts
    private const string EnemyTag = "Agent";

    //Serialize Fields
    [field: SerializeField] private Collider myCollider;
    [field: SerializeField] private Combat combat;

    private void OnTriggerEnter(Collider other)
    {
        //If trigger myself
        if (other == myCollider)
            return;

        //If not trigger enemy
        if (!other.CompareTag(EnemyTag))
            return;

        //Add new enemy
        if (other.TryGetComponent(out Combat enemy))
            combat.TryAddNewEnemy(enemy);
    }
}
