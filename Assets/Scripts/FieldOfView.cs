using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public Transform target = null;

    public float meshResolution;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    private GuardBehavior guard;
    private bool isInFOV;

    // Start is called before the first frame update
    void Start()
    {
        guard = this.GetComponentInParent<GuardBehavior>();
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        isInFOV = false;

        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        target = null;
        isInFOV = false;
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform visibleTarget = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (visibleTarget.position - transform.position).normalized;

            float angleToTarget = Vector3.Angle(transform.forward, dirToTarget);
            if(angleToTarget < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, visibleTarget.position);

                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    target = visibleTarget;
                    isInFOV = true;
                }
            }
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        for(int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;

            ViewCastInfo newViewCast = ViewCast(angle);

            viewPoints.Add(newViewCast.points);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if(i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();

    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius , globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 points;
        public float dst;
        public float angles;

        public ViewCastInfo(bool _hit, Vector3 _points, float _dst, float _angles)
        {
            hit = _hit;
            points = _points;
            dst = _dst;
            angles = _angles;
        }
    }


    private void Update()
    {
        CancelInvoke();

        if (!isInFOV && guard.GetGuardState() == GuardState.PURSUE)
        {
            Invoke("ResetGuard", 3.5f);
        }
    }

    private void ResetGuard()
    {
        guard.ResetGuardState();
    }

    void LateUpdate()
    {
        DrawFieldOfView();
    }

    public bool GetIsInFOV()
    {
        return isInFOV;
    }

    public Transform GetTarget()
    {
        return target;
    }
}
