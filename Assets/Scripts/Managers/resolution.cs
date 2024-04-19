using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class resolution : MonoBehaviour {
    
    int index = 0;
    private Resolution[] resolutionList;
    /// <summary>
    /// Get resolution information
    /// </summary>
    public Resolution Get_resolution
    {
        get { return resolutionList[index]; }
    }

    [SerializeField]
    private Dropdown resoluDropdown;

    [SerializeField]
    private Toggle FWtg;//Get the toggle control for switching full screen
    public bool FullWindow { get; set; }//Whether full screen status
    //[SerializeField]
    //private Text rosoluDisplay; //resolution text display
    private void Start()
    {
        //Clear the contents of the resolution drop-down box
        //resoluDropdown.ClearOptions();
        //Initialize fullscreen switch
        FullWindow = Screen.fullScreen;
        FWtg.isOn = FullWindow;
        //Specifies the resolution array length
        resolutionList = new Resolution[Screen.resolutions.Length-6];
        //Debug.Log(Screen.currentResolution);
        //Pick useful values ​​for the resolution list
        List<string> DropdownList = new List<string>();
        for (int i = 0; i < resolutionList.Length; i++)
        {
            resolutionList[i] = Screen.resolutions[i+6];//Add the corresponding resolution to the list
            DropdownList.Add(resolutionList[i].width + " × " + resolutionList[i].height);
            // Debug.Log(resolutionList[i]);
        }
       // Debug.Log(resolutionList.Length);
        resoluDropdown.AddOptions(DropdownList);

        int _width = Screen.currentResolution.width;
        int _height = Screen.currentResolution.height;
        //Set the serial number of the current resolution
        for (int i = 0; i < resolutionList.Length; i++)
        {
            if (resolutionList[i].width == _width && resolutionList[i].height == _height)
            {
                index = i;
                continue;
            }
        }
        //rosoluDisplay.text = _width + " × " + _height;
         resoluDropdown.value = index;
        
        //resolutionText.text = _width + " × " + _height;

    }

    public void FullWindowToggle(Toggle tg) {
        FullWindow = tg.isOn;
        //Debug.Log(FullWindow);
    }
   
    public void indexChange()
    {
        index = resoluDropdown.value;
       // index = index + sum >= resolutionList.Length ? 0 : index + sum < 0 ? resolutionList.Length - 1 : index + sum;
        var _width = resolutionList[index].width;
        var _height = resolutionList[index].height;
        //rosoluDisplay.text = _width + " × " + _height;
        Screen.SetResolution(_width, _height, FullWindow);
       // Debug.Log(index);
    }
}
