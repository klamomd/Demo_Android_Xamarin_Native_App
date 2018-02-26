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
using CodingTest1.Droid.Models;
using CodingTest1.Droid.Helpers;

namespace CodingTest1.Droid.Adapters
{
    public class UserAdapter : BaseAdapter<User>
    {
        // CTOR
        public UserAdapter(List<User> users)
        {
            userList = users;
        }

        // VARIABLES
        private List<User> userList;

        // PROPERTIES
        public override User this[int position]
        {
            get { return userList[position]; }
        }

        public override int Count
        {
            get { return userList.Count; }
        }


        // FUNCTIONS
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.UserListItem, parent, false);

                var username = view.FindViewById<TextView>(Resource.Id.UserName);
                var password = view.FindViewById<TextView>(Resource.Id.Password);

                view.Tag = new ViewHolder() { Username = username, Password = password };
            }

            var holder = (ViewHolder)view.Tag;

            holder.Username.Text = userList[position].Username;
            holder.Password.Text = userList[position].Password;


            return view;

        }

        // Function to overwrite the user list, since it was not doing so through other methods.
        public void UpdateUserList(List<User> newUserList)
        {
            if (newUserList != null)
            {
                userList = newUserList;
                NotifyDataSetChanged();
            }
        }
    }
}