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
        public enum SoundIdNum
        {
            Standingby,
            Waiting,
            Complete,
            Max
        };
        private SoundPool _soundPool;
        private SoundPool _soundPool2;
        private int[] soundIds = new int[3];

        public DeltaSoundPlayer()
        {
            _soundPool = new SoundPool(1, Stream.Music, 0);
            _soundPool2 = new SoundPool(1, Stream.Music, 0);
            soundIds[0] = _soundPool.Load(Application.Context, Resource.Raw.delta_clicked_to_standing_by, 0);
            soundIds[1] = _soundPool2.Load(Application.Context, Resource.Raw.delta_waiting, 0);
            soundIds[2] = _soundPool.Load(Application.Context, Resource.Raw.delta_complete, 0);
        }

        public void PlaySound(int num)
        {
            if(num >= 0 &&  num < 3)
            {
                _soundPool.Play(soundIds[num], 1.0f, 1.0f, 1, 0, 1.0f);
            }
            _soundPool2.Play(soundIds[1], 1.0f, 1.0f, 1, 0, 1.0f);
        }

        public void PlaySoundStandingBy()
        {
            PlaySound(0);
        }

        public void Destroy()
        {
            _soundPool.Release();
        }
    }
}