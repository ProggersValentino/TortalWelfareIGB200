using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// the purpose of this script is to display the dialogue 
/// </summary>
public class DialogueSpitter : MonoBehaviour
{
    public List<DialogueSO> dialogueSections;

    //private Dictionary<string, List<string>> dialogueKnowledgeBase = new Dictionary<string, List<string>>();
    private Dictionary<int, List<string>> dialogueKnowledgeBaseInt = new Dictionary<int, List<string>>();

    public List<string> dialogue = new List<string>();
    
    public float textSpeed;

    private List<string> defaultText = new List<string>();

    private bool isDisplayingText = false;

    private int dialogueIndex = 0;
    private string dialogueSearchKey;
    
    public PlayerInput input;
    private InputAction dialogueInteract;

    public TextMeshProUGUI textComponent;
    public GameObject dialogueGO;
    public string currentPlayerInput; //for the InputSwitcher when we want to change to and fro 

    public UnityEvent OnDialogueComplete;
    
    private void OnEnable()
    {
        dialogueInteract = input.actions["ProgressDialogue"];
        dialogueInteract.performed += OnClickDialogue;

        DialogueEventSystem.StartDialogue += StartDialogue;
        DialogueEventSystem.StopDialogue += StopDialogue;
    }

    private void OnDisable()
    {
        dialogueInteract.performed -= OnClickDialogue;
        
        DialogueEventSystem.StartDialogue -= StartDialogue;
        DialogueEventSystem.StopDialogue -= StopDialogue;
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultText.Add("Woah oh, no text supplied");
        
        //GrabAllDialogueSections();
        
        //StartCoroutine(ReadThroughText("tutorial"));
        
        //DialogueEventSystem.OnStartDialogue(dialogue);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabAllDialogueSections()
    {

        Dictionary<string, List<string>> temp = new Dictionary<string, List<string>>();
        foreach (DialogueSO seciton in dialogueSections)
        {
            temp = seciton.CovertToKnowledgeBase();
            
            // //adding the 
            // dialogueKnowledgeBase = dialogueKnowledgeBase.Concat(temp).ToDictionary(x => x.Key, x => x.Value);
        }
    }
    /// <summary>
    /// everytime we click we do an action related to the dialogue
    /// </summary>
    /// <param name="context"></param>
    void OnClickDialogue(InputAction.CallbackContext context)
    {
       
        ProgressThroughDialogue(dialogue);
    }

    public void ProgressThroughDialogue(List<string> dialogueConversation)
    {
            if (isDisplayingText)
            {
                Debug.LogWarning("we clicked through dia");
                StopCoroutine(ReadThroughText(dialogueConversation));
                isDisplayingText = false;
                textComponent.text = dialogueConversation[dialogueIndex];
            }
            else if (!isDisplayingText)
            {
                if(dialogueIndex >= dialogueConversation.Count - 1)
                {
                    DialogueEventSystem.OnStopDialogue();
                    //OnDialogueComplete?.Invoke();
                    return;
                }
                
                dialogueIndex++;
                textComponent.text = string.Empty;
                StartCoroutine(ReadThroughText(dialogueConversation));
            }
        
    }

    /// <summary>
    /// when we want to start the dialogue
    /// </summary>
    /// <param name="dialogueSearchKey"></param>
    public void StartDialogue(List<string> dialogueConversation)
    {
        InputSwitcher.SwitchInput(currentPlayerInput, "Dialogue");
        dialogueGO.SetActive(true);
        dialogue = dialogueConversation;
        StartCoroutine(ReadThroughText(dialogueConversation));
    }

    /// <summary>
    /// when we wnat to stop the dialogue 
    /// </summary>
    public void StopDialogue()
    {
        InputSwitcher.SwitchInput("Dialogue", currentPlayerInput);
        textComponent.text = string.Empty;
        dialogueIndex = 0; 
        dialogueGO.SetActive(false);
    }
    
    /// <summary>
    /// reads through each string char by char until otherwise
    /// </summary>
    /// <param name="indexOfDialogue"></param>
    /// <returns></returns>
    IEnumerator ReadThroughText(List<string> indexOfDialogue)
    { 
        string line = indexOfDialogue[dialogueIndex]; //dialogueKnowledgeBase.GetValueOrDefault(indexOfDialogue, defaultText)[dialogueIndex];
       isDisplayingText = true;
       for (int i = 0; i < line.Length; i++)
       {
          char c = line[i];
          if (c == '<')
          {
             string richText = string.Empty;
             while (c != '>')
             {
                richText += c;
                i++;
                c = line[i];
             }
             textComponent.text += richText + '>';
          }
          else if(isDisplayingText)
          {
             textComponent.text += c;
             yield return new WaitForSeconds(textSpeed);
          }
       }
       
       isDisplayingText = false;
    }
}
