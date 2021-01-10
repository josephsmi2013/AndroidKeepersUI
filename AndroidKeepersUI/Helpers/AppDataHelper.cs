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
using Firebase;
using Firebase.Database;

namespace AndroidKeepersUI.Helpers
{
    public static class AppDataHelper
    {
        public static FirebaseDatabase GetDatabase()
        {

            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseDatabase database;

            if (app == null)
            {
                var option = new FirebaseOptions.Builder()
                    .SetApplicationId("androidkeepersui")
                    .SetApiKey("AIzaSyBVYM3TBBaY8WP83eQoL43vqKtfzG5wI4g")
                    .SetDatabaseUrl("https://androidkeepersui.firebaseio.com")
                    .SetStorageBucket("androidkeepersui.appspot.com")
                    .Build();

                app = FirebaseApp.InitializeApp(Application.Context, option);
                database = FirebaseDatabase.GetInstance(app);
            }
            else
            {
                database = FirebaseDatabase.GetInstance(app);
            }

            return database;
        }
    }
}