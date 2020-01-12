using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Particles : MonoBehaviour
{
    Queue<GameObject> shredParticlesAvailable = new Queue<GameObject>();
    Queue<GameObject> shredParticlesUnAvailable = new Queue<GameObject>();
    Queue<Rigidbody> shredParticlesBodies = new Queue<Rigidbody>();
    Queue<Rigidbody> kinematicShredParticlesBodies = new Queue<Rigidbody>();
    public float setActiveFalseTimer;
    public float setKinematicTimer;
    public float setKinematicVelocity;
    public GameObject shredPrefab;
    [Range(0, 1)]
    public float spawnFactor;
    public float spawnForceFactor;
    public Transform cheese;
    float deltaPosition;
    Vector3 tempPos;
    GameObject tempObject;
    Rigidbody tempBody;
    GameObject particlesParent;
    float speed;
    void Start()
    {
        particlesParent = new GameObject();
        particlesParent.name = "Particles Parent";
        for (int i = 0; i < 500; i++)
        {
            GameObject temp = Instantiate(shredPrefab, transform.position, Random.rotation, particlesParent.transform);
            tempBody = temp.GetComponent<Rigidbody>();
            tempBody.isKinematic = true;
            kinematicShredParticlesBodies.Enqueue(tempBody);
            shredParticlesAvailable.Enqueue(temp);
            temp.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        speed = MovingHandler.currentSpeed;
        if (Mathf.Abs(spawnFactor*speed)>0)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (PlayerPrefs.GetInt("isvibration") == 0)
        {
        
        Vibration.Vibrate(20);
        }
#endif
#if UNITY_IOS && !UNITY_EDITOR
         if (PlayerPrefs.GetInt("isvibration") == 0)
        {
        Vibration.VibratePop();
        }
#endif
            if (shredParticlesAvailable.Count > 0)
            {

                tempObject = shredParticlesAvailable.Dequeue();
                tempObject.transform.position = transform.position;
                tempObject.SetActive(true);
                shredParticlesUnAvailable.Enqueue(tempObject);
                tempBody = kinematicShredParticlesBodies.Dequeue();
                tempBody.isKinematic = false;
                tempBody.velocity=new Vector3(1, 1, 0) * spawnForceFactor;
                shredParticlesBodies.Enqueue(tempBody);

                StartCoroutine(SetKinematic(tempBody)); //Uncomment if you want the particles to be kinematic after they decrease under specified velocity

                // StartCoroutine(SetActiveFalse(tempObject, tempBody)); //Uncomment if you want the particles to be disabled and put back into the pool after specified time

            }
            else
            {
                GameObject temp2 = Instantiate(shredPrefab, transform.position, Quaternion.identity, particlesParent.transform);
                shredParticlesAvailable.Enqueue(temp2);
                temp2.SetActive(false);
                Rigidbody tempBody2 = temp2.GetComponent<Rigidbody>();
                tempBody2.isKinematic = true;
                tempBody2.velocity = Vector3.zero;
                kinematicShredParticlesBodies.Enqueue(tempBody2);

                StartCoroutine(SetKinematic(tempBody2)); //Uncomment if you want the particles to be kinematic after they decrease under specified velocity
                // StartCoroutine(SetActiveFalse(temp2, tempBody2)); //Uncomment if you want the particles to be disabled and put back into the pool after specified time

            }
        }
    }

    IEnumerator SetActiveFalse(GameObject particle, Rigidbody particleBody)
    {


        yield return new WaitForSeconds(setActiveFalseTimer);

        particleBody.isKinematic = true;
        particleBody.velocity = Vector3.zero;
        particleBody.angularVelocity = Vector3.zero;
        particle.transform.localPosition = Vector3.zero;
        particle.SetActive(false);
        shredParticlesAvailable.Enqueue(shredParticlesUnAvailable.Dequeue());
        kinematicShredParticlesBodies.Enqueue(shredParticlesBodies.Dequeue());
        yield return null;
    }
    IEnumerator SetKinematic(Rigidbody particleBody)
    {

        yield return new WaitForSeconds(setKinematicTimer);
        if (particleBody.velocity.sqrMagnitude < setKinematicVelocity && particleBody.transform.position.y < 0.9f)
        {
            particleBody.isKinematic = true;
            particleBody.velocity = Vector3.zero;
        }
        else
        {
            StartCoroutine(SetKinematic(particleBody));
        }
    }
}
