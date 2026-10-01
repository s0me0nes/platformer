using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text ScoreText;

    private int _needScoreToWin = 5;
    private int _score = 0;

    private void Start()
    {
        AddScore(0);
    }

    public void AddScore(int value)
    {
        _score += value;

        Refresh();
        CheckWin();
    }

    private void Refresh()
    {
        ScoreText.text = _score + "/" + _needScoreToWin;
    }

    private void CheckWin()
    {
        if (_score == _needScoreToWin)
        {
            Debug.Log("You Win!");
        }
    }
}