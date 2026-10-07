using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionController : MonoBehaviour
{
    public Transform theCam;
    public Transform minPos, maxPos;
    public float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayLevelSelectMusic();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 adjustMousePos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
        Debug.Log(adjustMousePos);

        //Horizontal
        if(adjustMousePos.x > .75f)
        {
            if (adjustMousePos.x > .9f)
            {
                theCam.position += theCam.right * cameraSpeed * Time.deltaTime;
            }
            else
            {
                theCam.position += theCam.right * cameraSpeed * Time.deltaTime * .5f;
            }
        }

        if (adjustMousePos.x < .25f)
        {
            if (adjustMousePos.x < .1f)
            {
                theCam.position -= theCam.right * cameraSpeed * Time.deltaTime;
            }
            else
            {
                theCam.position -= theCam.right * cameraSpeed * Time.deltaTime * .5f;
            }
        }

        //Vertical
        if (adjustMousePos.y > .75f)
        {
            if (adjustMousePos.y > .9f)
            {
                theCam.position += theCam.forward * cameraSpeed * Time.deltaTime;
            }
            else
            {
                theCam.position += theCam.forward * cameraSpeed * Time.deltaTime * .5f;
            }
        }

        if (adjustMousePos.y < .25f)
        {
            if (adjustMousePos.y < .1f)
            {
                theCam.position -= theCam.forward * cameraSpeed * Time.deltaTime;
            }
            else
            {
                theCam.position -= theCam.forward * cameraSpeed * Time.deltaTime * .5f;
            }
        }

        theCam.position = new Vector3(Mathf.Clamp(theCam.position.x, minPos.position.x, maxPos.position.x), theCam.position.y, 
            Mathf.Clamp(theCam.position.z, minPos.position.z, maxPos.position.z));
    }
}
