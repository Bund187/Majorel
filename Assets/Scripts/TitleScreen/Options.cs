using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour {


    float currentVolume;
    bool mute, fullScreen;

    void Start () {
        currentVolume = 1;
	}
	
	public void VolumeControl(float volumeChange)
    {
        currentVolume = volumeChange;
        if(!mute)
            AudioListener.volume = volumeChange;
    }

    public void MuteControl()
    {
        mute = !mute;
        if (mute)
            AudioListener.volume = 0;
        else
            AudioListener.volume = currentVolume;
    }

    public void ChangeResolution(string temp)
    {
        int x = int.Parse(temp.Substring(0,4));
        int y = int.Parse(temp.Substring(4, 3));

        print("Change resolution " + x + " " + y);
        Screen.SetResolution(x, y, true);
        
    }
    
    public void AllowFullscreen()
    {
        fullScreen = !fullScreen;
        Screen.fullScreen = fullScreen;
    }
}
