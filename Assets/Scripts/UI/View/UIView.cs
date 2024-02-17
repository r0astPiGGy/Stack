using System;
using UnityEngine.UIElements;

namespace UI.View
{
    public class UIView : IDisposable
    {
        private const string VisibleClass = "screen-visible";
        private const string HiddenClass = "screen-hidden";
        
        protected bool UseTransition = true;
        protected bool IsTransitioning { get; private set; }
        
        public VisualElement Root { get; }

        public UIView(VisualElement root)
        {
            Root = root ?? throw new NullReferenceException("Root is null");
        }

        public virtual void Instantiate()
        {
            SetVisualElements();
            RegisterCallbacks();
            
            Root.RegisterCallback<TransitionEndEvent>(OnTransitionEndEvent);
        }

        private void OnTransitionEndEvent(TransitionEndEvent evt)
        {
            if (evt.target != Root) return;

            IsTransitioning = false;
            
            if (Root.ClassListContains(HiddenClass))
            {
                HideImmediately();
            }
        }

        public virtual void RegisterCallbacks()
        {
            
        }

        public virtual void SetVisualElements()
        {
            
        }

        public virtual void Dispose()
        {
            
        }

        public void Show()
        {
            ShowImmediately();

            if (!UseTransition) return;
            
            IsTransitioning = true;
            
            Root.AddToClassList(VisibleClass);
            Root.BringToFront();
            Root.RemoveFromClassList(HiddenClass);
        }

        public void Hide()
        {
            if (UseTransition)
            {
                IsTransitioning = true;
                
                Root.AddToClassList(HiddenClass);
                Root.RemoveFromClassList(VisibleClass);
            }
            else
            {
                HideImmediately();
            }
        }

        public void HideImmediately()
        {
            IsTransitioning = false;
            Hide(Root);
        }

        public void ShowImmediately()
        {
            IsTransitioning = false;
            Show(Root);
        }

        public static void Show(VisualElement el)
        {
            el.style.display = DisplayStyle.Flex;
        }
        
        public static void Hide(VisualElement el)
        {
            el.style.display = DisplayStyle.None;
        }
    }
}