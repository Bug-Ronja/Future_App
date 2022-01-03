using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Authors: Destenied to Learn und Unity3D Shool
 * Link Destenied to Learn: https://www.youtube.com/watch?v=XbdnG__wzZ8
 * Link Unity3D Shool: https://www.youtube.com/watch?v=TfShmy7ygdM
 * Customised and expanded by: Ronja Bergemann
 * **/

public class SetUpArticle : MonoBehaviour   
{
    public GameObject buttonPrefab;
    public GameObject panelToAttachButtonsTo;
    public GameObject textPanel;
    public GameObject headline;
    public GameObject buttonNextPrefab;
    public GameObject buttonPreviousPrefab;
    public GameObject shareArticleDisplay;
    public GameObject copyButton;
    public GameObject copiedMessage;
    public GameObject favoriteButton;

    private ButtonManager btnManager;
    private JSONReader jsonReader;
    private AudioManager audioManager;

    // Start is called before the first frame update
    private void Start()
    {
        btnManager = FindObjectOfType<ButtonManager>();
        jsonReader = FindObjectOfType<JSONReader>();
        audioManager = FindObjectOfType<AudioManager>();

        SetUpChapters();

        string title = "";

        JSONReader.Content con = jsonReader.contentList.GetCurrentContent(0);
        title = con.title;

        SetUparticle(title);
        SetUpNextAndPreviousButton();

        audioManager.StartAudioManager();

        copiedMessage.SetActive(false);
        copyButton.SetActive(true);
    }

    /**
     * Method sets up Chapter-Menu to navigate through magazine
     * **/
    public void SetUpChapters()
    {
        string title = "";

        for (int i = 0; i < jsonReader.contentList.GetLenght(); i++)
        {
            JSONReader.Content con = jsonReader.contentList.GetCurrentContent(i);
            title = con.title;

            // Link: https://forum.unity.com/threads/how-to-create-the-ui-button-at-runtime-through-script.386788/

            GameObject button = (GameObject)Instantiate(buttonPrefab);
            button.transform.SetParent(panelToAttachButtonsTo.transform); //Setting button parent
                                                                         //Next line assumes button has child with text as first gameobject like button created from GameObject->UI->Button
            button.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = con.title; //Changing text
            button.GetComponent<Button>().onClick.AddListener(delegate { OnClick(button); }); //Setting what button does when clicked
        }
    }
    
    /**
     * Method sets up wich functions are called when button is selected
     * **/
    public void OnClick(GameObject button)
    {
        SetUparticle(button.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text);
        SetUpNextAndPreviousButton();

        audioManager.StartAudioManager();
    }

    /**
     * Mehtod sets up article wich should be displayed
     * **/
    public void SetUparticle(string title)
    {
        //Debug.Log("Title SetUparticle: " + title);

        ResetArticle();
        btnManager.chapterMenu.SetActive(false);
        for (int i = 0; i < jsonReader.contentList.GetLenght(); i++)
        {
            JSONReader.Content con = jsonReader.contentList.GetCurrentContent(i);

            if (title.Equals(con.title))
            {
                //Changing text of headline, article and link
                headline.transform.GetComponent<TMPro.TextMeshProUGUI>().text = con.title;
                textPanel.transform.GetComponent<TMPro.TextMeshProUGUI>().text = con.article;
                shareArticleDisplay.transform.GetComponent<TMPro.TextMeshProUGUI>().text = con.link;
            }
        }
    }

    /**
     * Method resets article canvas
     * **/
    private void ResetArticle()
    {
        // Reset article and headline
        headline.transform.GetComponent<TMPro.TextMeshProUGUI>().text = "headline";
        textPanel.transform.GetComponent<TMPro.TextMeshProUGUI>().text = "there is no article";

        // Reset UI-Button visability
        copiedMessage.SetActive(false);
        copyButton.SetActive(true);
        favoriteButton.SetActive(true);
    }

    /**
     * Method sets up what articles should be loaded in next and previous Button
     * **/
    private void SetUpNextAndPreviousButton()
    {
        string title = headline.GetComponent<TMPro.TextMeshProUGUI>().text;

        // contentIndex: helper Integer to get content of contentList
        int contentIndex = 0;

        for (int i = 0; i < jsonReader.contentList.GetLenght(); i++)
        {
            if (title.Equals(jsonReader.contentList.GetCurrentContent(i).title))
            {
                contentIndex = i;
            }
        }

        if (title.Equals(jsonReader.contentList.GetCurrentContent(0).title))
        {
            // contentPointer: helper Integer to calculate with
            int contentPointer = contentIndex + 1;

            // Disables visibility of button
            buttonPreviousPrefab.SetActive(false);

            buttonNextPrefab.SetActive(true);
            buttonNextPrefab.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = jsonReader.contentList.GetCurrentContent(contentPointer).title;
            //Setting what button does when clicked
            buttonNextPrefab.GetComponent<Button>().onClick.AddListener(delegate { OnClick(buttonNextPrefab); });
        }
        else if (title.Equals(jsonReader.contentList.GetCurrentContent(jsonReader.contentList.GetLenght() - 1).title))
        {
            int contentPointer = contentIndex - 1;

            // Disables visibility of button
            buttonNextPrefab.SetActive(false);

            buttonPreviousPrefab.SetActive(true);
            buttonPreviousPrefab.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = jsonReader.contentList.GetCurrentContent(contentPointer).title;
            //Setting what button does when clicked
            buttonPreviousPrefab.GetComponent<Button>().onClick.AddListener(delegate { OnClick(buttonPreviousPrefab); });
        }
        else
        {
            int contentPointer = contentIndex + 1;

            buttonNextPrefab.SetActive(true);
            buttonNextPrefab.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = jsonReader.contentList.GetCurrentContent(contentPointer).title;
            //Setting what button does when clicked
            buttonNextPrefab.GetComponent<Button>().onClick.AddListener(delegate { OnClick(buttonNextPrefab); });

            contentPointer = contentIndex - 1;

            buttonPreviousPrefab.SetActive(true);
            buttonPreviousPrefab.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = jsonReader.contentList.GetCurrentContent(contentPointer).title;
            //Setting what button does when clicked
            buttonPreviousPrefab.GetComponent<Button>().onClick.AddListener(delegate { OnClick(buttonPreviousPrefab); });
        }
    }

    /**
     * Mehtod copies text of textfield to clipboard
     * **/
    public void CopyToClipboard()
    {
        TextEditor textEditor = new TextEditor();
        textEditor.text = shareArticleDisplay.GetComponent<TMPro.TextMeshProUGUI>().text;
        textEditor.SelectAll();
        textEditor.Copy(); // Copy string vom textEdiotor.text to Clipboard

        copyButton.SetActive(false);
        copiedMessage.SetActive(true);
    }
}
