using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBank/New Dialogue Chain")]
public class DialogueSO : ScriptableObject
{
   public string dialogueID;

   
   [TextArea(3, 10)]
   [SerializeField] private List<string> dialogueChain;
   
   public List<string> _dialogueChain
   {
      get { return dialogueChain; }
   }

   public Dictionary<string, List<string>> CovertToKnowledgeBase()
   {
      Dictionary<string, List<string>> temp = new Dictionary<string, List<string>>();
      
      temp.Add(dialogueID, dialogueChain);

      return temp;
   }
   
   // IEnumerator ReadThroughText(int indexOfDialogue, Dictionary<string, List<string>> lines, TextMeshProUGUI textComponent)
   // {
   //    string line = lines.GetValueOrDefault(dialogueID, defaultText)[0];
   //    for (int i = 0; i < line.Length; i++)
   //    {
   //       char c = line[i];
   //       if (c == '<')
   //       {
   //          string richText = string.Empty;
   //          while (c != '>')
   //          {
   //             richText += c;
   //             i++;
   //             c = line[i];
   //          }
   //          textComponent.text += richText + '>';
   //       }
   //       else
   //       {
   //          textComponent.text += c;
   //          yield return new WaitForSeconds(textSpeed);
   //       }
   //    }
   // }
}
