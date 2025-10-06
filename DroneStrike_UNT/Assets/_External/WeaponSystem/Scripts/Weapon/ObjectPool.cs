using System.Collections;
using UnityEngine;

namespace HWRWeaponSystem
{
    public class ObjectPool : MonoBehaviour
    {
        [HideInInspector]
        public bool Active;
        [HideInInspector]
        public GameObject Prefab;
        public float LifeTime;
        private LineRenderer lineRenderer;
        private Vector3 positionTemp;
        private Rigidbody rigidBody;
        private Quaternion rotationTemp;
        private Vector3 scaleTemp;
        private TrailRenderer[] trailRenderers;
        private float[] trailTemp;

        private void Awake()
        {
            scaleTemp = transform.localScale;
            positionTemp = transform.position;
            rotationTemp = transform.rotation;
            rigidBody = GetComponent<Rigidbody>();
            lineRenderer = GetComponent<LineRenderer>();

            trailRenderers = GetComponentsInChildren<TrailRenderer>();
            trailTemp = new float[trailRenderers.Length];
            for (var i = 0; i < trailRenderers.Length; i++) trailTemp[i] = trailRenderers[i].time;
        }

        private void OnEnable()
        {
            if (LifeTime > 0) StartCoroutine(setDestrying(LifeTime));
        }

        public virtual void OnSpawn(Vector3 position, Vector3 scale, Quaternion rotation, GameObject prefab, float lifeTime)
        {
            if (lifeTime != -1)
                LifeTime = lifeTime;

            if (GetComponent<Renderer>())
                GetComponent<Renderer>().enabled = true;

            Prefab = prefab;
            transform.position = position;
            transform.rotation = rotation;
            transform.localScale = scale;
            scaleTemp = transform.localScale;
            positionTemp = transform.position;
            rotationTemp = transform.rotation;

            if (rigidBody)
            {
                rigidBody.linearVelocity = Vector3.zero;
                rigidBody.angularVelocity = Vector3.zero;
            }

            if (lineRenderer)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position);
            }

            if (GetComponent<ParticleSystem>()) GetComponent<ParticleSystem>().Play();

            for (var i = 0; i < trailRenderers.Length; i++) trailRenderers[i].time = trailTemp[i];

            Active = true;

            gameObject.SetActive(true);

            if (LifeTime > 0) StartCoroutine(setDestrying(LifeTime));
        }


        private IEnumerator setDestrying(float time)
        {
            yield return new WaitForSeconds(time);
            OnDestroyed();
        }

        public void SetDestroy(float time)
        {
            StartCoroutine(setDestrying(time));
        }

        public void Destroying(float time)
        {
            SetDestroy(time);
        }

        public void Destroying()
        {
            if (GetComponent<Renderer>())
                GetComponent<Renderer>().enabled = false;

            transform.localScale = scaleTemp;
            transform.position = positionTemp;
            transform.rotation = rotationTemp;
            if (rigidBody)
            {
                rigidBody.linearVelocity = Vector3.zero;
                rigidBody.angularVelocity = Vector3.zero;
            }

            if (lineRenderer)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position);
            }

            if (GetComponent<ParticleSystem>()) GetComponent<ParticleSystem>().Stop();

            foreach (var trail in trailRenderers)
            {
                trail.time = 0;
                trail.Clear();
            }

            gameObject.SetActive(false);
            Active = false;
        }

        public virtual void OnDestroyed()
        {
            Destroying();
        }
    }
}