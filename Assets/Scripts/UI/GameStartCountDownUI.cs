using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class GameStartCountDownUI : MonoBehaviour
{

    private const string NUMBER_PUPUP = "NumberPopup";
    
    [SerializeField] private TextMeshProUGUI countdownText;


    private Animator animator;
    private int previousCountdownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        int countDownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer());
        countdownText.text = countDownNumber.ToString();

        //If previousCountdownNumber is different from countDownNumber
        if(previousCountdownNumber != countDownNumber)
        {
            previousCountdownNumber = countDownNumber;
            animator.SetTrigger(NUMBER_PUPUP);
            SoundManager.Instance.PlayCountDownSound();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
