using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    void Awake()
    {
        //Change this to change pictures folder
        //string path = @"C:\Users\Public\Pictures\Sample Pictures\";

        string path = Application.persistentDataPath;

        Debug.Log(Application.dataPath);

        pathPreFix = @"file://";
        
        files = System.IO.Directory.GetFiles(path, "*.jpg");

        //StartCoroutine(LoadImages());
        LoadImagesAsSprites();
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

    void LoadImagesAsSprites()
    {
        sprites = new Sprite[files.Length];

        for(int n = 0; n < files.Length; n++)
        {
            string item = files[n];

            byte[] bytes = File.ReadAllBytes(item);
            Texture2D texture = new Texture2D(900, 900, TextureFormat.RGB24, false);
            texture.filterMode = FilterMode.Trilinear;
            texture.LoadImage(bytes);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.0f), 1.0f);
            sprites[n] = sprite;
        }
        //byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "/sprite.png");
        //Texture2D texture = new Texture2D(900, 900, TextureFormat.RGB24, false);
        //texture.filterMode = FilterMode.Trilinear;
        //texture.LoadImage(bytes);
        //Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.0f), 1.0f);

        displayImage.sprite = sprites[0];
    }

    //private IEnumerator LoadImages()
    //{
    //    //load all images in default folder as textures and apply dynamically to plane game objects.
    //    //6 pictures per page
    //    textList = new Texture2D[files.Length];

    //    int dummy = 0;
    //    foreach (string tstring in files)
    //    {

    //        string pathTemp = pathPreFix + tstring;
    //        WWW www = new WWW(pathTemp);
    //        yield return www;
    //        Texture2D texTmp = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
    //        www.LoadImageIntoTexture(texTmp);

    //        textList[dummy] = texTmp;

    //        gameObj[dummy].GetComponent<Renderer>().material.SetTexture("_MainTex", texTmp);
    //        dummy++;
    //    }

    //}
}