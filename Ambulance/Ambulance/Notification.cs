using System;
using System.Collections.Generic;
using System.Text;

namespace Ambulance
{
    class Notification
    {
        public interface ITextToSpeech
        {
            void Speak(string text);
        }
    }
}
