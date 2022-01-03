using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/**
 * Author: Destenied to Learn
 * Link: https://www.youtube.com/watch?v=XbdnG__wzZ8
 * Customised and expanded by: Ronja Bergemann
 * **/

public class JSONReader : MonoBehaviour
{
    public TextAsset textJSON;

    [System.Serializable]
    public class Content
    {
        public string edition;
        public string link;
        public string title;
        public string article;
    }

    [System.Serializable]
    public class ContentList
    {
        public Content[] content;

        public int GetLenght()
        {
            return content.Length;
        }

        public Content GetCurrentContent(int i)
        {
            return content[i];
        }
    }

    public ContentList contentList = new ContentList();

    // Start is called before the first frame update
    void Start()
    {
        contentList = JsonUtility.FromJson<ContentList>(textJSON.text);
    }

    public int GetContentListLength()
    {
        return contentList.GetLenght();
    }
    
    public Content GetCurrentContent(int i)
    {
        return contentList.GetCurrentContent(i);
    }
}
