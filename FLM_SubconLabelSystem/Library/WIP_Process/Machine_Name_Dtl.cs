using System;
using System.Diagnostics;

namespace WIP_Process
{
    public class Machine_Name_Dtl
    {
        private string str_ID_MM_MACHINE;

        private string str_MACHINE_CODE;

        private string str_MACHINE_DESC;

        public string ID_MM_MACHINE
        {
            get
            {
                return this.str_ID_MM_MACHINE;
            }
            set
            {
                this.str_ID_MM_MACHINE = value;
            }
        }

        public string MACHINE_CODE
        {
            get
            {
                return this.str_MACHINE_CODE;
            }
            set
            {
                this.str_MACHINE_CODE = value;
            }
        }

        public string MACHINE_DESC
        {
            get
            {
                return this.str_MACHINE_DESC;
            }
            set
            {
                this.str_MACHINE_DESC = value;
            }
        }

        [DebuggerNonUserCode]
        public Machine_Name_Dtl() : base()
        {
        }
    }
}
