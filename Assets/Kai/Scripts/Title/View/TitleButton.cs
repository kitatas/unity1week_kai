using Common.View.Button;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Title.View
{
    [RequireComponent(typeof(ButtonActivator))]
    [RequireComponent(typeof(ButtonAnimator))]
    public sealed class TitleButton : MonoBehaviour
    {
        private ButtonActivator _buttonActivator;
        private ButtonAnimator _buttonAnimator;

        private void Awake()
        {
            _buttonActivator = GetComponent<ButtonActivator>();
            _buttonAnimator = GetComponent<ButtonAnimator>();
        }

        private void Start()
        {
            GetComponent<Button>()
                .OnClickAsObservable()
                .Subscribe(_ => _buttonAnimator.Play())
                .AddTo(this);
        }

        public void Activate(bool value)
        {
            _buttonActivator.Activate(value);
        }
    }
}