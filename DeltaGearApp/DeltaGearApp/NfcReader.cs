using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DeltaGearApp
{
    class NfcReader
    {
        private NfcAdapter _nfcAdapter;
        private MainActivity _mainActivity;

        public Boolean Init(MainActivity mainActivity)
        {
            _mainActivity = mainActivity;
            _nfcAdapter = NfcAdapter.GetDefaultAdapter(_mainActivity);
            //NFC機能なし機種
            if (_nfcAdapter == null)
            {
                Toast.MakeText(_mainActivity, "no Nfc feature", ToastLength.Short).Show();
                return false;
            }
            //NFC通信OFFモード
            if (!_nfcAdapter.IsEnabled)
            {
                Toast.MakeText(_mainActivity, "off Nfc feature", ToastLength.Short).Show();
                return false;
            }
            return true;
        }

        public void SetNfcReading()
        {
            //NFCを見つけたときに反応させる
            //PendingIntent→タイミング（イベント発生）を指定してIntentを発生させる
            PendingIntent pendingIntent = PendingIntent.GetActivity(_mainActivity, 0, new Intent(_mainActivity, typeof(MainActivity)), 0);
            //タイミングは、タグ発見時とする。
            IntentFilter[] intentFilter = new IntentFilter[]{
                new IntentFilter(NfcAdapter.ActionTagDiscovered)
            };

            //反応するタグの種類を指定。
            String[][] techList = new String[][]{
                new string[] {
                    typeof(Android.Nfc.Tech.NfcA).FullName,
                    typeof(Android.Nfc.Tech.NfcB).FullName,
                    typeof(Android.Nfc.Tech.IsoDep).FullName,
                    typeof(Android.Nfc.Tech.MifareClassic).FullName,
                    typeof(Android.Nfc.Tech.MifareUltralight).FullName,
                    typeof(Android.Nfc.Tech.NdefFormatable).FullName,
                    typeof(Android.Nfc.Tech.NfcV).FullName,
                    typeof(Android.Nfc.Tech.NfcF).FullName,
                }
            };

            _nfcAdapter.EnableForegroundDispatch(_mainActivity, pendingIntent, intentFilter, techList);
        }

    }
}