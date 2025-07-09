using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Effects/KeepRangeEffect")]
public class KeepRangeEffect : SO_Effect
{
    public float desiredDistance = 10f;
    public float bufferZone = 1f;

    public bool useRandomOffset = true;
    public float offsetRange = 2f;

    public override void ApplyEffect(GOAP_Agent agent, GameObject target)
    {
        if (target == null || agent == null) return;

        var nav = agent.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (nav == null) return;

        Vector3 direction = target.transform.position - agent.transform.position;
        float currentDistance = direction.magnitude;

        Vector3 moveDirection = direction.normalized;

        Vector3 destination;

        if (currentDistance > desiredDistance + bufferZone)
        {
            destination = target.transform.position - moveDirection * desiredDistance;
        }
        else if (currentDistance < desiredDistance - bufferZone)
        {
            destination = agent.transform.position - moveDirection * (desiredDistance - currentDistance);
        }
        else
        {
            destination = agent.transform.position;
        }
        
        if (useRandomOffset)
        {
            Vector2 randomCircle = Random.insideUnitCircle * offsetRange;
            destination += new Vector3(randomCircle.x, 0f, randomCircle.y);
        }

        nav.SetDestination(destination);
    }
}
