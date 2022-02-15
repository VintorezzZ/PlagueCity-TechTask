using System;

namespace Utils
{
    public static class EventHub
    {
        public static event Action gameStarted;
        public static event Action<VisitorCard> visitorViewShow;
        public static event Action<VisitorCard> visitorViewClose;
        public static event Action audioSettingsChanged;
        public static event Action nextDayBegin;
        public static event Action daySkip;
        public static event Action<bool> settingsChanged;
        
        public static void OnSettingsChanged(bool isChanged)
        {
            settingsChanged?.Invoke(isChanged);
        }
        public static void OnVisitorViewShow(VisitorCard visitorCard)
        {
            visitorViewShow?.Invoke(visitorCard);
        }
        public static void OnVisitorViewClose(VisitorCard visitorCard)
        {
            visitorViewClose?.Invoke(visitorCard);
        }
        public static void OnNextDayBegin()
        {
            nextDayBegin?.Invoke();
        }
        public static void OnDaySkip()
        {
            daySkip?.Invoke();
        }
        public static void OnGameStarted()
        {
            gameStarted?.Invoke();
        }
        public static void OnAudioSettingsChanged()
        {
            audioSettingsChanged?.Invoke();
        }
    }
}