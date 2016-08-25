﻿using System;
using System.Runtime.Serialization;

namespace Wangkanai.Extensions.Responsiveness
{
    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public class DeviceNotFoundException : ArgumentException, ISerializable
    {
        private readonly string m_invalidDeviceName; // unrecognized device name
        private static string DefaultMessage => "Device Not Supported";
        public virtual string InvalidDeviceName => m_invalidDeviceName;

        public DeviceNotFoundException() : base(DefaultMessage) { }
        public DeviceNotFoundException(string message) : base(message) { }

        public DeviceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {            
        }
        public DeviceNotFoundException(string message, string invalidDeviceName, Exception innerException) :
            base(message, innerException)
        {
            m_invalidDeviceName = invalidDeviceName;
        }       

        protected DeviceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            m_invalidDeviceName = (string)info.GetValue("InvalidDeviceName", typeof(string));
        }

        public override string Message
        {
            get
            {
                var s = base.Message;
                if (m_invalidDeviceName != null)
                {
                    var valueMessage = InvalidDeviceName;
                    if (s == null) return valueMessage;
                    return s + Environment.NewLine + valueMessage;
                }
                return s;
            }
        }

        [System.Security.SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info == null) throw new ArgumentNullException("info");
            base.GetObjectData(info, context);
            info.AddValue("InvalidDeviceName", m_invalidDeviceName, typeof(string));
        }
    }
}