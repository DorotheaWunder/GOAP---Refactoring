using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionScript : MonoBehaviour
{
    [Header("Scan Settings")]
    public float raycastInterval = 0.5f;
    public int rayDensity = 36;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public List<string> detectableTags = new() { "Player", "Health", "Enemy" };
    public string lastDetectedTag;

    [Header("Detection Ranges")]
    public float detectionRange = 20f;
    public float shootingRange = 10f;
    public float meleeRange = 5f;
    public float fieldOfView = 120f;
    public float communicationRange = 70f;

    [Header("References")]
    public GOAP_Agent agent;

    public enum DetectedRangeType { None, Detection, Shooting, Melee, Communication }
    public DetectedRangeType currentRange = DetectedRangeType.None;

    [SerializeField] private GameObject detectedTarget = null;
    [SerializeField] private bool lineOfSightBlocked = true;

    private void Start()
    {
        StartCoroutine(ScanForTargets());
    }

    private IEnumerator ScanForTargets()
    {
        while (true)
        {
            ScanEnemyVision();
            ScanCommunicationRange();
            yield return new WaitForSeconds(raycastInterval);
        }
    }

    private void ScanEnemyVision()
    {
        detectedTarget = null;
        currentRange = DetectedRangeType.None;
        lineOfSightBlocked = true;

        float stepAngle = fieldOfView / rayDensity;
        float halfFOV = fieldOfView * 0.5f;

        for (float angle = -halfFOV; angle <= halfFOV; angle += stepAngle)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, detectionRange, targetLayer))
            {
                if (detectableTags.Contains(hit.transform.tag))
                {
                    float distance = Vector3.Distance(transform.position, hit.point);
                    detectedTarget = hit.transform.gameObject;
                    lastDetectedTag = hit.transform.tag;

                    if (distance <= meleeRange) currentRange = DetectedRangeType.Melee;
                    else if (distance <= shootingRange) currentRange = DetectedRangeType.Shooting;
                    else currentRange = DetectedRangeType.Detection;

                    Vector3 directionToTarget = detectedTarget.transform.position - transform.position;
                    lineOfSightBlocked = Physics.Raycast(transform.position, directionToTarget.normalized, directionToTarget.magnitude, obstructionLayer);

                    break;
                }
            }
        }

        agent.Target = detectedTarget;
    }

    private void ScanCommunicationRange()
    {
        if (agent.NearbyAllies == null)
            agent.NearbyAllies = new List<GameObject>();
        else
            agent.NearbyAllies.Clear();

        for (int angle = 0; angle < 360; angle += 10)
        {
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, communicationRange, targetLayer))
            {
                if (hit.transform.CompareTag("Enemy") && hit.transform.gameObject != gameObject)
                {
                    agent.NearbyAllies.Add(hit.transform.gameObject);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, communicationRange);
        
        Gizmos.color = Color.black;
        float halfFOV = fieldOfView * 0.5f;
        Quaternion leftRayRotation = Quaternion.Euler(0, -halfFOV, 0);
        Quaternion rightRayRotation = Quaternion.Euler(0, halfFOV, 0);
        Vector3 leftRay = leftRayRotation * transform.forward * detectionRange;
        Vector3 rightRay = rightRayRotation * transform.forward * detectionRange;
        Gizmos.DrawLine(transform.position, transform.position + leftRay);
        Gizmos.DrawLine(transform.position, transform.position + rightRay);
        
        if (detectedTarget != null)
        {
            Gizmos.color = lineOfSightBlocked ? Color.white : Color.red;
            Gizmos.DrawLine(transform.position, detectedTarget.transform.position);
        }
    }
}