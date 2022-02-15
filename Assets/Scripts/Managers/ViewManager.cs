using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Managers
{
    public class ViewManager : SingletonBehaviour<ViewManager>
    {
        [SerializeField] private View[] views;
        [SerializeField] private View startingView;
        private View _currentView;
        private readonly Stack<View> _history = new Stack<View>();

        private void Awake()
        {
            InitializeSingleton();
        }

        public void Start()
        {
            foreach (var view in views)
            {
                view.Initialize();  
                view.Hide();
            }
            
            if(startingView)
                Show(startingView, true);
        }

        public static T GetView<T>() where T : View
        {
            foreach (var view in Instance.views)
            {
                if (view is T tView)
                {
                    return tView;
                }
            }

            return null;
        }

        public static void Show<T>(bool remember = true, bool hidePrevious = true) where T : View
        {
            foreach (var view in Instance.views)
            {
                if (!(view is T)) 
                    continue;
            
                if (Instance._currentView)
                {
                    if(remember)
                        Instance._history.Push(Instance._currentView);
                    
                    if(hidePrevious)
                        Instance._currentView.Hide();
                }

                Instance._currentView = view;
                view.Show();
                break;
            }
        }

        public static void ShowPopup<T>() where T : View
        {
            foreach (var view in Instance.views)
            {
                if (!(view is T)) 
                    continue;
                
                view.Show();
                break;
            }
        }

        public static void Show(View view, bool remember = true, bool hidePrevious = true)
        {
            if (Instance._currentView)
            {
                if(remember)
                    Instance._history.Push(Instance._currentView);
            
                if(hidePrevious)
                    Instance._currentView.Hide();
            }

            Instance._currentView = view;
            view.Show();
        }

        public static void ShowLast()
        {
            if(Instance._history.Count != 0)
                Show(Instance._history.Pop(), false);
        }
    }
}
