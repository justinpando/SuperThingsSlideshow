using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoViewer : MonoBehaviour
{
    public Image displayImage;
    public Sprite[] sprites;
    public int index = 0;

    GameObject[] gameObj;
    Texture2D[] textList;

    string[] files;
    string pathPreFix;

    // Use this for initialization
    void Start()
    {
        //Change this to change pictures folder
        string path = @"C:\Users\Public\Pictures\Sample Pictures\";

        pathPreFix = @"file://";

        files = System.IO.Directory.GetFiles(path, "*.jpg");

        //gameObj = GameObject.FindGameObjectsWithTag("Pics");

        //StartCoroutine(LoadImages());
    }

    public void ShowPreviousImage()
    {
        index--;
        if (index < 0) index = sprites.Length - 1;
        ShowImage(index);
    }

    public void ShowNextImage()
    {
        index++;
        if (index == sprites.Length) index = 0;
        ShowImage(index);
    }

    public void ShowImage(int newIndex)
    {
        Mathf.Clamp(index, 0, sprites.Length - 1);
        //Debug.Log(index);
        displayImage.sprite = sprites[index];

        index = newIndex;
    }

    private IEnumerator LoadImages()
    {
        //load all images in default folder as textures and apply dynamically to plane game objects.
        //6 pictures per page
        textList = new Texture2D[files.Length];

        int dummy = 0;
        foreach (string tstring in files)
        {

            string pathTemp = pathPreFix + tstring;
            WWW www = new WWW(pathTemp);
            yield return www;
            Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            www.LoadImageIntoTexture(texTmp);

            textList[dummy] = texTmp;

            gameObj[dummy].GetComponent<Renderer>().material.SetTexture("_MainTex", texTmp);
            dummy++;
        }

    }
}