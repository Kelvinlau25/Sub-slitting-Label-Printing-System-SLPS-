using System;
using System.Diagnostics;

namespace WIP_Process
{
    public class Equipment_RFID_Listing
    {
        private string str_Equipment_RFID;

        public string Equipment_RFID
        {
            get
            {
                return this.str_Equipment_RFID;
            }
            set
            {
                this.str_Equipment_RFID = value;
            }
        }

        [DebuggerNonUserCode]
        public Equipment_RFID_Listing() : base()
        {
        }
    }
}
