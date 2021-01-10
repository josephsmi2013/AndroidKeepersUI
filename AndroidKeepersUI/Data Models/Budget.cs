using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroidKeepersUI.Data_Models
{
    public class Budget
    {
        public string ManagerName { get; set; }
        public string TeamName { get; set; }
        public string Amount { get; set; }
        public string ID { get; set; }
    }
}