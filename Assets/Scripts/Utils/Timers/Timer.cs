namespace Utils.Timers
{
    [System.Serializable]
    public class Timer
    {
        private float _delay;
        private float _currentTime;

        public float Delay { get => _delay; set => _delay = value; }
        
        public Timer(float delay)
        {
            _delay = delay;
            _currentTime = _delay + 1f;
        }

        public void Update(float deltaTime)
        {
            _currentTime += deltaTime;
        }
        
        public bool IsReady()
        {
            return _currentTime >= _delay;
        }

        public void Reset()
        {
            _currentTime = 0f;
        }
        
        public void ForceReady()
        {
            _currentTime = _delay;
        }

        public override string ToString()
        {
            return _currentTime + " / " + _delay;
        }
    }
}