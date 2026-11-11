using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Додано для нового Input System

namespace Beginner2D
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }

        private bool m_IsGameOver = false;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        public void WinGame()
        {
            if (m_IsGameOver) return;
            m_IsGameOver = true;

            if (UIHandler.instance != null)
            {
                UIHandler.instance.DisplayWinScreen();
            }
        }

        public void LoseGame()
        {
            if (m_IsGameOver) return;
            m_IsGameOver = true;

            if (UIHandler.instance != null)
            {
                UIHandler.instance.DisplayLoseScreen();
            }
        }

        private void Update()
        {
            // Перевіряємо натискання клавіші R через новий Input System
            if (m_IsGameOver && Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}