using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tree.Dialogue;
using TMPro;
using UnityEngine.UI;

namespace Tree.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayersConversant playersScript;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;
        [SerializeField] GameObject NPCLines;
        [SerializeField] Button nextButton;
        [SerializeField] Button quitButton;

        // Start is called before the first frame update
        void Start()
        {  
           playersScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayersConversant>();
           playersScript.onConverstaitonUpdate += UpdateUI;
           nextButton.onClick.AddListener(() =>  playersScript.Next());
           quitButton.onClick.AddListener(() =>  playersScript.Quit());

           UpdateUI();
        }

        // Update is called once per frame
        void UpdateUI()
        {
            gameObject.SetActive(playersScript.IsActive());
            if(!playersScript.IsActive())
            {
                return;
            }
            NPCLines.SetActive(!playersScript.IsChoosing());
            choiceRoot.gameObject.SetActive(playersScript.IsChoosing());
            if(playersScript.IsChoosing())
            {
                BuildPlayerLines();
            }
            else
            {
                AIText.text = playersScript.GetText();
                nextButton.gameObject.SetActive(playersScript.HasNext());
            }
            
        }

        private void BuildPlayerLines()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }

            foreach (DialogueNode choice in playersScript.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                textComp.text = choice.GetTxt();
                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => 
                {
                    playersScript.SelectChoice(choice);
                });
            }
        }
    }
}
