using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;

public class CollectedObjController : MonoBehaviour
{
    PlayerManager playerManager;


    [SerializeField] Transform sphere;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();

        sphere = transform.GetChild(0);

        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();

            Rigidbody rb = GetComponent<Rigidbody>();

            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            // GetComponent<Renderer>().material = playerManager.collectedObjMat;
        }
    }



    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("CollectibleObj"))
        {
            MMVibrationManager.Haptic(HapticTypes.Success);

            //playerManager.gameObject.GetComponent<AudioSource>().Play();
            if (!playerManager.collidedList.Contains(other.gameObject))
            {
                other.gameObject.tag = "CollectedObj";
                other.transform.parent = playerManager.collectedPoolTransform;
                playerManager.collidedList.Add(other.gameObject);
                other.gameObject.AddComponent<CollectedObjController>();
            }
        }
        if (other.gameObject.CompareTag("Obstacle") && !(gameObject.CompareTag("first")))
        {
            //DestroyTheObject();
            StartCoroutine(ExplodeTheObject());
        }

        if (other.gameObject.CompareTag("MultiplierLine"))
        {
            SwerveMovement.swerve.swerveSpeed = 0;
            //StartCoroutine(ExplodeTheObjectTwo());

        }

    }


    private IEnumerator ExplodeTheObjectTwo()
    {

        playerManager.collidedList.Remove(gameObject);
        gameObject.GetComponent<Rigidbody>().constraints = 0;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(200, 300), gameObject.transform.position, 0);

        yield return new WaitForSeconds(1.5f);

        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        Destroy(gameObject);
        Transform partcile = Instantiate(playerManager.partcilePrefab, transform.position, Quaternion.identity);
        partcile.GetComponent<ParticleSystem>().startColor = playerManager.collectedObjMat.color;

    }

    private IEnumerator ExplodeTheObject()
    {

        playerManager.collidedList.Remove(gameObject);
        gameObject.GetComponent<Rigidbody>().constraints = 0;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(50, 100), gameObject.transform.position, 0);

        yield return new WaitForSeconds(1);

        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        Destroy(gameObject);
        Transform partcile = Instantiate(playerManager.partcilePrefab, transform.position, Quaternion.identity);
        partcile.GetComponent<ParticleSystem>().startColor = playerManager.collectedObjMat.color;

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("CollectibleList"))
        {
            MMVibrationManager.Haptic(HapticTypes.Success);

            // playerManager.gameObject.GetComponent<AudioSource>().Play();
            print("trigger enter");
            other.transform.GetComponent<BoxCollider>().enabled = false;
            other.transform.parent = playerManager.collectedPoolTransform;

            foreach (Transform child in other.transform)
            {
                if (!playerManager.collidedList.Contains(child.gameObject))
                {
                    playerManager.collidedList.Add(child.gameObject);
                    child.gameObject.tag = "CollectedObj";
                    child.gameObject.AddComponent<CollectedObjController>();
                }


            }


        }


        if (other.gameObject.CompareTag("FinishLine"))
        {
            StartCoroutine(FinishState());


        }

        if (other.gameObject.CompareTag("StopLine"))
        {
            if (gameObject.CompareTag("first"))
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                Transform partcile = Instantiate(playerManager.partcilePrefab, transform.position, Quaternion.identity);
                partcile.GetComponent<ParticleSystem>().startColor = playerManager.collectedObjMat.color;
            }
            if (playerManager.levelState != PlayerManager.LevelState.Finished)
            {
                playerManager.levelState = PlayerManager.LevelState.Finished;

                //aktiviraj da krene ui da vristi na tebe ukoliko imas dfovoljno scalea u progress baru
            }
        }

    }

    private IEnumerator FinishState()
    {

        //play sound
        yield return new WaitForSeconds(1.4f);

        if (SceneManager.GetActiveScene().buildIndex + 1 != 4)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(0);

    }
    void DestroyTheObject()
    {
        playerManager.collidedList.Remove(gameObject);
        Destroy(gameObject);

        Transform partcile = Instantiate(playerManager.partcilePrefab, transform.position, Quaternion.identity);
        partcile.GetComponent<ParticleSystem>().startColor = playerManager.collectedObjMat.color;
    }
    public void MakeSphere()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        sphere.gameObject.GetComponent<MeshRenderer>().enabled = true;
        sphere.gameObject.GetComponent<SphereCollider>().enabled = true;
        sphere.gameObject.GetComponent<SphereCollider>().isTrigger = true;

        sphere.gameObject.GetComponent<Renderer>().material = playerManager.collectedObjMat;


    }
    public void DropObj()
    {
        sphere.gameObject.layer = 8;

        sphere.gameObject.GetComponent<SphereCollider>().isTrigger = false;
        sphere.gameObject.AddComponent<Rigidbody>();
        sphere.GetComponent<Rigidbody>().useGravity = true;
    }
}
