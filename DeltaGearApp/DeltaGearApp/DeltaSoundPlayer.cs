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
        private SoundPool _soundPool;
        private int _soundId;

        public DeltaSoundPlayer()
        {
            _soundPool = new SoundPool(1, Stream.Music, 0);
            _soundId = _soundPool.Load(Application.Context, Resource.Raw.test_sound, 0);
        }

        public void PlaySound()
        {
            _soundPool.Play(_soundId, 1.0f, 1.0f, 1, 0, 1.0f);
        }

        public void Destroy()
        {
            _soundPool.Release();
        }
    }
}