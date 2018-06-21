using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using System;
using Android.Text;
using Android.Nfc;

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
                Toast.MakeText(this, "メッセージ", ToastLength.Short).Show();
                _deltaSoundPlayer.PlaySound();
            };
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

            //成功！と表示してみる
            Toast.MakeText(this, "Success！", ToastLength.Short).Show();
            _deltaSoundPlayer.PlaySound();
        }
    }
}

