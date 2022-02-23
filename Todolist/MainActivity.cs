using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using Todolist.Helper;
using AlertDialog = Android.App.AlertDialog;

namespace Todolist
{
    [Activity(Label = "TodoList", Theme = "@style/Theme.AppCompat.Light", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
       
        
        private TextView textView;
        private ListView listView;
        DbHelper dbHelper;
        private CustomAdapter adapter;
        private EditText editText;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_item, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.actions:
                    editText = new EditText(this);
                    AlertDialog alertDialog = new AlertDialog.Builder(this)
                        .SetTitle("Add New Task")
                        .SetView(editText)
                        .SetPositiveButton("Add", OkAction)
                        .Create();
                    alertDialog.Show();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            string task = editText.Text;
            dbHelper.InsertNewTask(task);
            LoadTaskList();
        }

        internal void LoadTaskList()
        {
            List<string> taskList = dbHelper.getTaskList();
            adapter = new CustomAdapter(this, taskList, dbHelper);
            listView.Adapter = adapter;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.activity_main);
            dbHelper = new DbHelper(this);
            listView = FindViewById<ListView>(Resource.Id.listView1);
            //Load Data  
            LoadTaskList();
        }

        
    }
}