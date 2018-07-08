using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DeltaGearApp
{
    class DeltaSoundPlayer
    {
        public enum _soundIdNum
        {
            Standingby,
            Waiting,
            Complete,
            Max
        };
        private SoundPool _soundPoolStandingBy;
        private SoundPool _soundPoolWaiting;
        private SoundPool _soundPoolComplete;
        private int _soundIdStandingBy;
        private int _soundIdWaiting;
        private int _soundIdComplete;

        public DeltaSoundPlayer()
        {
            _soundPoolStandingBy = new SoundPool(1, Stream.Music, 0);
            _soundIdStandingBy = _soundPoolStandingBy.Load(Application.Context, Resource.Raw.delta_clicked_to_standing_by, 0);

            _soundPoolWaiting = new SoundPool(1, Stream.Music, 0);
            _soundIdWaiting = _soundPoolWaiting.Load(Application.Context, Resource.Raw.delta_waiting, 0);

            _soundPoolComplete = new SoundPool(1, Stream.Music, 0);
            _soundIdComplete = _soundPoolComplete.Load(Application.Context, Resource.Raw.delta_complete, 0);
        }

        public void PlayRecognitionedSound()
        {
            _soundPoolStandingBy.Play(_soundIdStandingBy, 1.0f, 1.0f, 1, 0, 1.0f);
            _soundPoolWaiting.Play(_soundIdWaiting, 1.0f, 1.0f, 1, -1, 1.0f);
        }

        public void PlayCompleteSound()
        {
            _soundPoolWaiting.Stop(_soundIdWaiting);
            _soundPoolComplete.Play(_soundIdComplete, 1.0f, 1.0f, 1, 0, 1.0f); ;
        }

        public void Destroy()
        {
            _soundPoolStandingBy.Release();
            _soundPoolWaiting.Release();
            _soundPoolComplete.Release();
        }
    }
}