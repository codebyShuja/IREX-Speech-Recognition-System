using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace IREX
{
    class System_Information
    {
        public string user_name, O_S;
        public int total_minutes;
        public void UserName()
        {
            user_name = System.Environment.UserName;
        }
        public void o_s()
        {
            O_S = System.Environment.OSVersion.ToString();
        }
        public void TotalMinutes()
        {
            total_minutes = ((System.Environment.TickCount / 1000) / 60);
        }
    }
}