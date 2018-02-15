﻿using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDIPalette2OSC.Model
{
    class MidiConnector
    {
        private InputDevice inDevice = null;

        public delegate void MessageReady(int channel, int cc, int value);
        public event MessageReady MessageReadyEvent;

        public MidiConnector()
        {
            if (InputDevice.DeviceCount == 0)
            {
                throw new ArgumentOutOfRangeException("DeviceCount", "Not input MIDI Device Connected!");
            }
            inDevice = new InputDevice(0);
            inDevice.ChannelMessageReceived += HandleChannelMessage;
        }

        ~MidiConnector()
        {
            inDevice.Close();
        }

        public void StartReceiving()
        {
            inDevice.StartRecording();
        }

        private void HandleChannelMessage(object sender, ChannelMessageEventArgs e)
        {
            var channel = 1;//e.Message.MidiChannel;
            var cc = e.Message.Data1;
            var value = e.Message.Data2;

            MessageReadyEvent?.Invoke(channel,cc,value);
        }

    }
}
