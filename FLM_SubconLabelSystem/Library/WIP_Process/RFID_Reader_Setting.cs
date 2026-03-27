using System;
using System.Diagnostics;

namespace WIP_Process
{
    public class RFID_Reader_Setting
    {
        private string str_Station_Name;

        private string str_Catagory;

        private string str_Exe_Name;

        private string str_IPAddr;

        private string str_AppPath;

        public string AppPath
        {
            get
            {
                return this.str_AppPath;
            }
            set
            {
                this.str_AppPath = value;
            }
        }

        public string Catagory
        {
            get
            {
                return this.str_Catagory;
            }
            set
            {
                this.str_Catagory = value;
            }
        }

        public string Exe_Name
        {
            get
            {
                return this.str_Exe_Name;
            }
            set
            {
                this.str_Exe_Name = value;
            }
        }

        public string IP_Addr
        {
            get
            {
                return this.str_IPAddr;
            }
            set
            {
                this.str_IPAddr = value;
            }
        }

        public string Station_Name
        {
            get
            {
                return this.str_Station_Name;
            }
            set
            {
                this.str_Station_Name = value;
            }
        }

        [DebuggerNonUserCode]
        public RFID_Reader_Setting() : base()
        {
        }
    }
}
