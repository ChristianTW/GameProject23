using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FOGPIUtilities.Manager
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager instance;
        public UpdateScoreEvent updateScore;
        public int score { get; private set; }
        public int multiplier { get; private set; }

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                score = 0;
                Destroy(this);
                return;
            }

            score = 0;
            multiplier = 1;

            if (updateScore == null)
            {
                updateScore = new UpdateScoreEvent();
            }
        }

        public void AddPoints(int _amount)
        {
            AddPoints(_amount, null);
        }

        public void AddPoints(int _amount, Vector3? _location = null)
        {
            score += (_amount * multiplier);

            updateScore.Invoke(new ScoreInfo(score, _amount, _location));
        }

        public void ResetScore()
        {
            score = 0;
            multiplier = 1;

            updateScore.Invoke(new ScoreInfo(score, 0, null));
        }
    }

    public class ScoreInfo
    {
        public int score;
        public int delta;
        public Vector3? location;

        public ScoreInfo(int _score, int _delta, Vector3? _location)
        {
            score = _score;
            delta = _delta;
            location = _location;
        }
    }

    [System.Serializable]
    public class UpdateScoreEvent : UnityEngine.Events.UnityEvent<ScoreInfo> {}

}