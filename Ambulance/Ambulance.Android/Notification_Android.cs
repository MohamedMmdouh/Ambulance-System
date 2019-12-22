using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Ambulance.Droid;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech.Tts;
using Android.Views;
using Android.Widget;
using static Ambulance.Notification;

[assembly: Xamarin.Forms.Dependency(typeof(Notification_Android))]

namespace Ambulance.Droid
{
    public class Notification_Android  //: ITextToSpeech
    {
        /*     TextToSpeech speaker;
             string toSpeak;

             public void Speak(string text)
             {

                 toSpeak = text;
                 if (speaker == null)
                 {
                     speaker = new TextToSpeech(MainActivity.Instance);
                 }
                 else
                 {
                     speaker.Speak(toSpeak, QueueMode.Flush, null, null);
                 }
             }

             #region IOnInitListener implementation
             public void OnInit(OperationResult status)
             {
                 if (status.Equals(OperationResult.Success))
                 {
                     speaker.Speak(toSpeak, QueueMode.Flush, null, null);
                 }
                 else
                 {

                 }
             }
             #endregion
         }*/
    }
}