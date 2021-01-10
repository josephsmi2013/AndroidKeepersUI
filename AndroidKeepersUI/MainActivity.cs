using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V7.Widget;
using System;
using AndroidKeepersUI.Adapters;
using System.Collections.Generic;
using AndroidKeepersUI.Data_Models;
using AndroidKeepersUI.Fragments;
using AndroidKeepersUI.EventListeners;
using System.Linq;

namespace AndroidKeepersUI
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {
        ImageView sendMoneyButton;
        RecyclerView myRecyclerView;
        List<Budget> BudgetList;

        SendMoneyFragment sendMoneyFragment;
        BudgetListener budgetListener;

        BudgetAdapter adapter;


        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            myRecyclerView = (RecyclerView)FindViewById(Resource.Id.myRecyclerView);
            sendMoneyButton = (ImageView)FindViewById(Resource.Id.sendMoneyButton);

            sendMoneyButton.Click += SendMoneyButton_Click;

            RetrieveData();

        }

        private void SendMoneyButton_Click(object sender, EventArgs e)
        {
            
            sendMoneyFragment = new SendMoneyFragment(BudgetList);
            var trans = SupportFragmentManager.BeginTransaction();
            sendMoneyFragment.Show(trans, "update budget");

        }

        private void SetupRecyclerView()
        {

            myRecyclerView.SetLayoutManager(new Android.Support.V7.Widget.LinearLayoutManager(myRecyclerView.Context));
            adapter = new BudgetAdapter(BudgetList);
            myRecyclerView.SetAdapter(adapter);

        }

        public void RetrieveData()
        {

            budgetListener = new BudgetListener();
            budgetListener.Create();
            budgetListener.BudgetRetrived += BudgetListener_BudgetRetrived;

        }

        private void BudgetListener_BudgetRetrived(object sender, BudgetListener.BudgetDataEventArgs e)
        {


            BudgetList = e.Budget;
            SetupRecyclerView();

        }



    }
}