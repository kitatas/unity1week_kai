using System;
using System.Collections.Generic;
using System.Threading;
using Common.View.Button;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Common.Sound.SE
{
    public sealed class SeController : BaseAudioSource
    {
        private List<SeData> _seData;

        [Inject]
        private void Construct(SeTable seTable)
        {
            _seData = seTable.seData;
        }

        public void PlaySe(SeType seType)
        {
            var seData = _seData.Find(data => data.seType == seType);
            if (seData == null)
            {
                return;
            }

            if (seData.audioClip == null)
            {
                return;
            }

            audioSource.PlayOneShot(seData.audioClip);
        }

        public async UniTaskVoid DelayPlaySeAsync(SeType seType, float delayTime, CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime), cancellationToken: token);

            PlaySe(seType);
        }

        public void PlaySe(ButtonType buttonType)
        {
            PlaySe(GetSeType(buttonType));
        }

        private static SeType GetSeType(ButtonType type)
        {
            switch (type)
            {
                case ButtonType.Decision:
                    return SeType.Decision;
                case ButtonType.Cancel:
                    return SeType.Cancel;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}