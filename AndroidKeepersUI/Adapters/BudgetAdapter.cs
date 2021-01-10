using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using AndroidKeepersUI.Data_Models;
using Android.Graphics;
namespace AndroidKeepersUI.Adapters
{
    class BudgetAdapter : RecyclerView.Adapter
    {
        public event EventHandler<BudgetAdapterClickEventArgs> ItemClick;
        public event EventHandler<BudgetAdapterClickEventArgs> ItemLongClick;
        List<Budget> Items;

        public BudgetAdapter(List<Budget> Data)
        {
            Items = Data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.budgetrow, parent, false);

            var vh = new BudgetAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var holder = viewHolder as BudgetAdapterViewHolder;
            holder.managerNameText.Text = Items[position].ManagerName;
            holder.teamNameText.Text = Items[position].TeamName;
            holder.budgetText.Text = Items[position].Amount;
        }

        public override int ItemCount => Items.Count;

        void OnClick(BudgetAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(BudgetAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
    }

    public class BudgetAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView managerNameText { get; set; }
        public TextView teamNameText { get; set; }
        public TextView budgetText { get; set; }

        public BudgetAdapterViewHolder(View itemView, Action<BudgetAdapterClickEventArgs> clickListener,
                            Action<BudgetAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
            managerNameText = (TextView)itemView.FindViewById(Resource.Id.managerNameText);
            teamNameText = (TextView)itemView.FindViewById(Resource.Id.teamNameText);
            budgetText = (TextView)itemView.FindViewById(Resource.Id.budgetText);

            itemView.Click += (sender, e) => clickListener(new BudgetAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new BudgetAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class BudgetAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}