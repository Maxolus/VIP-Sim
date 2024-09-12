using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnitEye;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class CamSelection : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI webcamtext;
    [SerializeField]
    private WebCamInput webCamInput;
    [SerializeField]
    private Gaze gaze;

    
    private void Start()
    {
        StartCoroutine(LateStart(1));
    }

    private void Update()
    {
        webcamtext.text = webCamInput.webCamName;
    }


    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Your Function You Want to Call
        webcamtext.text = webCamInput.webCamName;
    }

    public void OnPrevCam()
    {
        webCamInput.PreviousCamera((int)webCamInput.webCamResolution.x, (int)webCamInput.webCamResolution.y);
        gaze.EyeHelper.CameraChanged(webCamInput.webCamName);
    }

    public void OnNextCam()
    {
        webCamInput.NextCamera((int)webCamInput.webCamResolution.x, (int)webCamInput.webCamResolution.y);
        gaze.EyeHelper.CameraChanged(webCamInput.webCamName);
    }
    
    
}
