using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetJapan : MonoBehaviour
{
    //Boolean
    public bool canDestroy;

    //Transforms
    GameObject destroyPos;

    //Colors
    public Material redColor;
    public Material blueColor;

    //Corners
    public Renderer topLeft;
    public Renderer topRight;
    public Renderer bottomLeft;
    public Renderer bottomRight;

    //Center
    public Renderer center;

    //References
    PlayerJapan playerScript;
    TargetSpawn spawnScript;

    public void Start()
    {
        destroyPos = GameObject.Find("DestroyPosition");
        spawnScript = FindObjectOfType<TargetSpawn>();
        playerScript = FindObjectOfType<PlayerJapan>();

    }

    void OnCollisionEnter(Collision other)
    {


        if (other.gameObject.tag == "Shuriken")
        {
            playerScript.ScoreUp();
            topLeft.material = blueColor;
            topRight.material = blueColor;
            bottomLeft.material = blueColor;
            bottomRight.material = blueColor;
            center.material = blueColor;
            Destroy(this.gameObject, 3);

        }


        if (other.gameObject.tag == "Target")
        {

        }

    }







}