using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BoolVariable _gameIsPaused;
    // Start is called before the first frame update
    void Start()
    {
        _gameIsPaused.SetValue(false);
    }

    public void OnPauseGameToggle (bool p_gameIsPaused)
    {
        if (p_gameIsPaused)
        {
            Time.timeScale = 0f;
            _gameIsPaused.SetValue(true);
        } else
        {
            Time.timeScale = 1f;
            Invoke(nameof(ResumeGame), 0.1f);
        }
    }

    private void ResumeGame()
    {
        print("resuming");
  
        _gameIsPaused.SetValue(false);
    }
}
