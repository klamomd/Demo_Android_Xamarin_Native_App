using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using CodingTest1.Droid.Models;
using CodingTest1.Droid.Helpers;
using CodingTest1.Droid.Adapters;

namespace CodingTest1.Droid.Activities
{
	[Activity (Label = "User Demo", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        public readonly List<User> UsersList = new List<User>();
        private UserAdapter listAdapter;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            SetContentView(Resource.Layout.Main);

            // Try to find an existing users file and store all existing users in UsersList.
            try
            {
                using (StreamReader stream = new StreamReader(Application.Context.OpenFileInput("savedUsers")))
                {
                    string line;
                    while ((line = stream.ReadLine()) != null)
                    {
                        UsersList.Add(FileHelper.ConvertFileStringToUser(line));
                    }
                }
            }
            // Do nothing if file does not exist.
            catch (Exception e) { }
            

            Button button = FindViewById<Button>(Resource.Id.addUserButton);

            // Attach the UserAdapter to convert users to list elements, and to allow the list to update (via NotifyDataSetChanged in OnActivityResult)
            ListView listView = FindViewById<ListView>(Resource.Id.userList);
            listView.ItemClick += ListView_ItemClick;

            listAdapter = new UserAdapter(UsersList);
            listView.Adapter = listAdapter;

            // Create a bundle with indexes for a new user's name and password. Also provide a list of strings of all usernames to ensure no duplicates.
            // All usernames are made lowercase because identical usernames in different cases are considered duplicates, and this makes it easier to check when adding a user.
            Bundle userBundle = new Bundle();
            userBundle.PutString("newUserName", "");
            userBundle.PutString("newUserPassword", "");
            userBundle.PutStringArray("existingUsers", UsersList.Select(user => user.Username.ToLower()).ToArray());

            // Create the AddUserActivity Intent, update the existingUsers list, and start it while expecting a result.
            button.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(AddUserActivity));
                userBundle.PutStringArray("existingUsers", UsersList.Select(user => user.Username).ToArray());
                intent.PutExtra("addUserBundle", userBundle);
                StartActivityForResult(intent, 0);
            };
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Delete User?");
            alert.SetMessage("Would you like to delete this user?");
            alert.SetPositiveButton("Yes", (senderAlert, args) =>
            {
                UsersList.RemoveAt(e.Position);
                listAdapter.UpdateUserList(UsersList);
            });
            alert.SetNegativeButton("No", (senderAlert, args) => { });
            Dialog dialog = alert.Create();
            dialog.Show();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            // Get the result, if it is OK, use the values in the bundle to add the new user.
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                Bundle userBundle = data.GetBundleExtra("addUserBundle");
                UsersList.Add(new User(userBundle.GetString("newUserName"), userBundle.GetString("newUserPassword")));
                ListView listView = FindViewById<ListView>(Resource.Id.userList);
                listAdapter.UpdateUserList(UsersList);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            Application.Context.DeleteFile("savedUsers");
            using (StreamWriter stream = new StreamWriter(Application.Context.OpenFileOutput("savedUsers", FileCreationMode.Append)))
            {
                foreach (User u in UsersList)
                {
                    stream.WriteLine(FileHelper.ConvertUserToFileString(u));
                }
            }
        }
    }
}


