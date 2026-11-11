using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }

    private VisualElement m_HealthBar;
    private VisualElement m_WinScreen;
    private VisualElement m_LoseScreen;
    private VisualElement m_DialogueBox;
    private Label m_DialogueLabel;

    private float m_TimerDisplay;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        m_HealthBar = root.Q<VisualElement>("HealthBar");
        m_WinScreen = root.Q<VisualElement>("WinScreenContainer");
        m_LoseScreen = root.Q<VisualElement>("LoseScreenContainer");
        m_DialogueBox = root.Q<VisualElement>("NPCDialogue");
        m_DialogueLabel = root.Q<Label>("DialogueText");

        if (m_WinScreen != null) m_WinScreen.style.opacity = 0;
        if (m_LoseScreen != null) m_LoseScreen.style.opacity = 0;
        if (m_DialogueBox != null) m_DialogueBox.style.opacity = 0;

        m_TimerDisplay = -1.0f;
    }

    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                if (m_DialogueBox != null)
                    m_DialogueBox.style.opacity = 0;
            }
        }
    }

    public void SetHealthValue(float percentage)
    {
        if (m_HealthBar != null)
        {
            m_HealthBar.style.width = Length.Percent(percentage * 100);
        }
    }

    public void DisplayWinScreen()
    {
        if (m_WinScreen != null)
            m_WinScreen.style.opacity = 1.0f;
    }

    public void DisplayLoseScreen()
    {
        if (m_LoseScreen != null)
            m_LoseScreen.style.opacity = 1.0f;
    }

    public void DisplayDialogue(string dialogueText)
    {
        if (m_DialogueBox != null)
        {
            if (m_DialogueLabel != null)
                m_DialogueLabel.text = dialogueText;

            m_DialogueBox.style.opacity = 1.0f;
            m_TimerDisplay = 4.0f;
        }
    }
}