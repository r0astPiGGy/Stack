using System;
using UI.View;
using UnityEngine.UIElements;

namespace UI.Screens
{
    public class HomeScreen : UIView
    {
        public Action StartAction = default;
        
        public HomeScreen(VisualElement root) : base(root) { }

        public override void SetVisualElements()
        {
            
        }

        public override void RegisterCallbacks()
        {
            Root.RegisterCallback<PointerDownEvent>(OnStartClicked);
        }
        
        private void OnStartClicked(PointerDownEvent evt)
        {
            if (IsTransitioning) return;
            
            StartAction();
        }
        
        public override void Dispose()
        {
            Root.UnregisterCallback<PointerDownEvent>(OnStartClicked);
        }
    }
}