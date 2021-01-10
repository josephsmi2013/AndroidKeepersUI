using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using FR.Ganfra.Materialspinner;
using Java.Util;
using AndroidKeepersUI.Helpers;
using SupportV7 = Android.Support.V7.App;
using System.Collections;
using Java.Lang;
using AndroidKeepersUI.EventListeners;
using AndroidKeepersUI.Data_Models;
using Android.Text;
using Java.Interop;

namespace AndroidKeepersUI.Fragments
{
    public class SendMoneyFragment : Android.Support.V4.App.DialogFragment
    {

        TextInputLayout budgetText;
        MaterialSpinner managerNameSpinner;
        Button submitButton;

        List<string> managerNameList;
        ArrayAdapter<string> adapter;
        string managerName;
        
        List<Budget> thisBudgetList;
        string budget;

        //public bool IsEmpty => throw new NotImplementedException();
        //private EditText budgetEditText;

        public SendMoneyFragment()
        {

        }

        public SendMoneyFragment(List<Budget> budgetList)
        {
            thisBudgetList = budgetList;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            View view = inflater.Inflate(Resource.Layout.sendmoney, container, false);

            budgetText = (TextInputLayout)view.FindViewById(Resource.Id.budgetText);
            managerNameSpinner = (MaterialSpinner)view.FindViewById(Resource.Id.managerNameSpinner);
            submitButton = (Button)view.FindViewById(Resource.Id.submitButton);

           // budgetEditText = (EditText)view.FindViewById(Resource.Id.budgetEditText);

            submitButton.Click += SubmitButton_Click;
            
            SetupStatusPinner();

            return view;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {


            budget = budgetText.EditText.Text;
            


            SupportV7.AlertDialog.Builder saveDataAlert = new SupportV7.AlertDialog.Builder(Activity);
            saveDataAlert.SetTitle("SEND DRAFT MONEY");
            saveDataAlert.SetMessage("Are you sure?");
            saveDataAlert.SetPositiveButton("Continue", (senderAlert, args) =>
            {

                UpdateBudget();

                this.Dismiss();
            });
            saveDataAlert.SetNegativeButton("Cancel", (senderAlert, args) =>
            {
                saveDataAlert.Dispose();
            });

            saveDataAlert.Show();

        }


        private void UpdateBudget()
        {
            int newAmount;

            for (int i = 0; i < thisBudgetList.Count; i++)
            {
                if (thisBudgetList[i].ManagerName == managerName)
                {


                    newAmount = Int32.Parse(thisBudgetList[i].Amount.Substring(2)) + Int32.Parse(budget);
                    AppDataHelper.GetDatabase().GetReference("budget/" + thisBudgetList[i].ID + "/amount").SetValue(newAmount);

                    break;

                }
            }


        }

        public void SetupStatusPinner()
        {
            managerNameList = new List<string>();
            managerNameList.Add("Cord");
            managerNameList.Add("Darren");
            managerNameList.Add("Ezra");
            managerNameList.Add("Joe");
            managerNameList.Add("Justin");
            managerNameList.Add("Liz");
            managerNameList.Add("Matt");
            managerNameList.Add("Mike");
            managerNameList.Add("Ray");
            managerNameList.Add("Tim");

            adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerDropDownItem, managerNameList);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            managerNameSpinner.Adapter = adapter;
            managerNameSpinner.ItemSelected += StatusSpinner_ItemSelected;
        }

        private void StatusSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position != -1)
            {
                managerName = managerNameList[e.Position];
            }
        }        

    }
}