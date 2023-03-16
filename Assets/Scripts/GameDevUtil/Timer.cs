using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FOGPIUtilities
{

    public class Timer : MonoBehaviour
    {

        [Tooltip("when AutoStart is set to true the timer will start running when the GameObject Loads.")]
        public bool autoStart = false;
        public bool autoRestart = false;
        public float countDownTime = 1.0f;
        public float timeScale = 1.0f;
        public float timeLeft { 
            get { 
                return m_timeLeft; 
            }
        }

        private float m_timeLeft = 0.0f;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        /// <summary>
        /// Start timer will start the timer with the values passed in or
        /// the public class variable if not values are passed it.
        /// </summary>
        /// <param name= "_countDown"> The amount of time in seconds the timer will run. </param>
        /// <param name= "_autoRestart"> If true the timer will restart when finished. </param>
        /// <return></return>
        public void StartTimer(float? _countDown = null, bool _autoRestart = false) {
            if (_countDown != null && _countDown > 0.0f)
                countDownTime = (float)_countDown;

            autoRestart = _autoRestart;

            m_timeLeft = countDownTime;
        }
    }
}
