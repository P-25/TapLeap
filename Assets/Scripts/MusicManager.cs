using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Image SoundOnIcon;
    public Image SoundOffIcon;
    bool muted = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("muted")){
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }else{
            Load();
        }
        updateButtonIcon();

        AudioListener.pause = muted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonPress()
    {
        if(muted == false){
            muted = true; 
            AudioListener.pause = true;
        }else{
             muted = false; 
            AudioListener.pause = false;
        }

        Save();
        updateButtonIcon();
    }

    void Load()
    {
        muted = PlayerPrefs.GetInt("muted", muted ? 1 : 0) == 1;
    }
    void Save()
    {
       PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

    void updateButtonIcon()
    {
       if(muted == false){
            SoundOnIcon.enabled = true;
            SoundOffIcon.gameObject.SetActive(false);
       }else{
            SoundOnIcon.enabled = false;
            SoundOffIcon.gameObject.SetActive(true);
       }
    }
}
