using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Particles : MonoBehaviour
{
    Stack<GameObject> shredParticlesAvailable = new Stack<GameObject>();
    Stack<GameObject> shredParticlesUnAvailable = new Stack<GameObject>();
    Stack<Rigidbody> shredParticlesBodies = new Stack<Rigidbody>();
    Stack<Rigidbody> kinematicShredParticlesBodies = new Stack<Rigidbody>();
    public float setActiveFalseTimer;
    public GameObject shredPrefab;
    public float spawnFactor;
    public float spawnForceFactor;
    float deltaPosition;
    Vector3 tempPos;
    GameObject tempObject;
    Rigidbody tempBody;
    GameObject particlesParent;
    void Start()
    {
        particlesParent = new GameObject();
        particlesParent.name = "Particles Parent";
        for (int i = 0; i < 500; i++)
        {
            GameObject temp = Instantiate(shredPrefab, transform.position, Random.rotation, particlesParent.transform);
            tempBody = temp.GetComponent<Rigidbody>();
            tempBody.isKinematic = true;
            kinematicShredParticlesBodies.Push(tempBody);
            shredParticlesAvailable.Push(temp);
            temp.SetActive(false);
        }
    }

    void Update()
    {
        deltaPosition = (transform.position - tempPos).sqrMagnitude;
        if (deltaPosition > spawnFactor * deltaPosition * Time.deltaTime)
        {
            if (shredParticlesAvailable.Count > 0)
            {
                tempObject = shredParticlesAvailable.Pop();
                tempObject.transform.position = transform.position;
                tempObject.SetActive(true);
                shredParticlesUnAvailable.Push(tempObject);
                tempBody = kinematicShredParticlesBodies.Pop();
                tempBody.isKinematic = false;
                tempBody.AddForce(new Vector3(1, 1, 0) * spawnForceFactor, ForceMode.VelocityChange);
                shredParticlesBodies.Push(tempBody);
                StartCoroutine(SetActiveFalse(tempObject, tempBody));

            }
            else
            {
                GameObject temp2 = Instantiate(shredPrefab, transform.position, Quaternion.identity, particlesParent.transform);
                shredParticlesAvailable.Push(temp2);
                temp2.SetActive(false);
                Rigidbody tempBody2 = temp2.GetComponent<Rigidbody>();
                tempBody2.isKinematic = true;
                kinematicShredParticlesBodies.Push(tempBody2);
                StartCoroutine(SetActiveFalse(temp2, tempBody2));

            }
        }
        tempPos = transform.position;
    }

    IEnumerator SetActiveFalse(GameObject particle, Rigidbody particleBody)
    {
        yield return new WaitForSeconds(setActiveFalseTimer);
    
            shredParticlesAvailable.Push(shredParticlesUnAvailable.Pop());
            kinematicShredParticlesBodies.Push(shredParticlesBodies.Pop());
            particle.SetActive(false);
            particle.transform.localPosition = Vector3.zero;
            particleBody.velocity = Vector3.zero;
            particleBody.isKinematic = true;
    }
}
