using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using AndroidKeepersUI.Data_Models;
using AndroidKeepersUI.Helpers;

namespace AndroidKeepersUI.EventListeners
{
    public class BudgetListener : Java.Lang.Object, IValueEventListener
    {
        List<Budget> budgetList = new List<Budget>();

        public event EventHandler<BudgetDataEventArgs> BudgetRetrived;

        public class BudgetDataEventArgs : EventArgs
        {
            public List<Budget> Budget { get; set; }
        }

        public void OnCancelled(DatabaseError error)
        {

        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Value != null)
            {
                var child = snapshot.Children.ToEnumerable<DataSnapshot>();
                budgetList.Clear();
                foreach (DataSnapshot budgetData in child)
                {
                    Budget budget = new Budget();
                    budget.ID = budgetData.Key;
                    budget.ManagerName = budgetData.Child("managerName").Value.ToString();
                    budget.TeamName = budgetData.Child("teamName").Value.ToString();
                    budget.Amount = "$ " + budgetData.Child("amount").Value.ToString();
                    budgetList.Add(budget);
                }
                BudgetRetrived.Invoke(this, new BudgetDataEventArgs { Budget = budgetList });
            }
        }

        public void Create()
        {
            DatabaseReference budgetRef = AppDataHelper.GetDatabase().GetReference("budget");
            budgetRef.AddValueEventListener(this);
        }
    }      
}