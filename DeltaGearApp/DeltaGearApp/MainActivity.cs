﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using System;
using Android.Text;
using Android.Nfc;
using Android.Speech.Tts;
using Android.Speech;

namespace DeltaGearApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class MainActivity : AppCompatActivity
    {
        private NfcReader _nfcReader;
        private DeltaSoundPlayer _deltaSoundPlayer;
        private ViewCreator _viewCreator;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _viewCreator = new ViewCreator();
            SetContentView(_viewCreator.Layout);

            _nfcReader = new NfcReader();
            _deltaSoundPlayer = new DeltaSoundPlayer();
            
            _viewCreator.Button.Click += (sender, e) =>
            {
                Toast.MakeText(this, "Standing by", ToastLength.Short).Show();
                _deltaSoundPlayer.PlaySound(0);
            };

            Intent intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
           // intent.PutExtra(RecognizerIntent.ExtraPrompt, Application.Context.GetString(Resource.String.messageSpeakNow));
            intent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            intent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            intent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            intent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);

            intent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
            StartActivityForResult(intent, 10);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (_nfcReader.Init(this))
            {
                _nfcReader.SetNfcReading();
            }
            else
            {
                Console.WriteLine("Unavailable NFC");
                Finish();
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            //_deltaSoundPlayer.Destroy();
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            String action = intent.Action;
            if (TextUtils.IsEmpty(action))
            {
                return;
            }
            if (!action.Equals(NfcAdapter.ActionTagDiscovered))
            {
                return;
            }

            Toast.MakeText(this, "Complete", ToastLength.Short).Show();
            _deltaSoundPlayer.PlaySound(2);
        }

        protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
        {
            if (requestCode == 10)
            {
                if (resultVal == Result.Ok)
                {
                    var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    if (matches.Count != 0)
                    {
                        Toast.MakeText(this, "Complete", ToastLength.Short).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "Complete", ToastLength.Short).Show();
                    }
                }
            }

            base.OnActivityResult(requestCode, resultVal, data);
        }
    }
}

