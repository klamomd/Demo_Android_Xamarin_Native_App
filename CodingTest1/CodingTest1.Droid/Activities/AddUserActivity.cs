using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using CodingTest1.Droid.Helpers;

namespace CodingTest1.Droid.Activities
{
	[Activity (Label = "Add New User")]
	public class AddUserActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
            base.OnCreate (bundle);

            this.SetContentView(Resource.Layout.AddUserLayout);

            Bundle addUserBundle = Intent.GetBundleExtra("addUserBundle");

            Button addUserButton = FindViewById<Button>(Resource.Id.addUserButton);
            Button cancelButton = FindViewById<Button>(Resource.Id.cancelButton);

            EditText usernameField = FindViewById<EditText>(Resource.Id.usernameField);
            EditText passwordField = FindViewById<EditText>(Resource.Id.passwordField);

            string[] existingUsersArray = addUserBundle.GetStringArray("existingUsers");
            List<string> existingUsers = new List<string>(existingUsersArray);

            // Check the provided values. If valid, create a new user and finish the activity. If invalid, provide an alert describing the issue.
            addUserButton.Click += (sender, e) =>
            {
                string username = usernameField.Text;
                string password = passwordField.Text;

                // Strings to hold title and message for AlertDialog if username or password are invalid.
                string alertMessage = "";
                string alertTitle = "";
                bool areBothStringsValid = true;

                // Check username validity. Username requirements:
                // 1. Not empty
                // 2. Alphanumeric characters only, no whitespace either
                // 3. Not a duplicate username, regardless of case
                if (username.Length == 0)
                {
                    alertTitle = "Invalid Username";
                    alertMessage = "Username cannot be empty.";
                    areBothStringsValid = false;
                }
                else if (StringChecker.ContainsInvalidCharacter(username))
                {
                    alertTitle = "Invalid Username";
                    alertMessage = "Username can only contain alphanumeric characters.";
                    areBothStringsValid = false;
                }
                // Check username.ToLower because duplicates are case-insensitive.
                else if (existingUsers.Contains(username.ToLower()))
                {
                    alertTitle = "Invalid Username";
                    alertMessage = "Username already exists.";
                    areBothStringsValid = false;
                }

                // Check password validity. Password requirements:
                // 1. Not empty
                // 2. Length between 5 and 12 characters (inclusive)
                // 3. Alphanumeric characters only, no whitespace either
                // 4. At least 1 alphabet character
                // 5. At least 1 numeric character
                // 6. No repeating sequences
                if (password.Length == 0)
                {
                    alertTitle = "Invalid Password";
                    alertMessage = "Password cannot be empty.";
                    areBothStringsValid = false;
                }
                else if (!StringChecker.IsPasswordLengthValid(password))
                {
                    alertTitle = "Invalid Password";
                    alertMessage = "Password length must be between 5 and 12 characters.";
                    areBothStringsValid = false;
                }
                else if (StringChecker.ContainsInvalidCharacter(password))
                {
                    alertTitle = "Invalid Password";
                    alertMessage = "Password cannot contain non-alphanumeric characters.";
                    areBothStringsValid = false;
                }
                else if (!StringChecker.ContainsAlphaCharacter(password))
                {
                    alertTitle = "Invalid Password";
                    alertMessage = "Password must contain at least one letter.";
                    areBothStringsValid = false;
                }
                else if (!StringChecker.ContainsNumericCharacter(password))
                {
                    alertTitle = "Invalid Password";
                    alertMessage = "Password must contain at least one number.";
                    areBothStringsValid = false;
                }
                else if (StringChecker.ContainsRepeatingSequence(password))
                {
                    alertTitle = "Invalid Password";
                    alertMessage = "Password cannot contain any repeating sequences.";
                    areBothStringsValid = false;
                }

                // Create alert dialog if strings are not valid.
                if (!areBothStringsValid)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle(alertTitle);
                    alert.SetMessage(alertMessage);
                    // OK button has a handler that does nothing, since the user should just go back to the AddUser screen.
                    alert.SetPositiveButton("OK", (senderAlert, args) => { });
                    Dialog dialog = alert.Create();
                    dialog.Show();
                }
                // If all is good, pass the username and password back through the bundle.
                else
                {
                    addUserBundle.PutString("newUserName", username);
                    addUserBundle.PutString("newUserPassword", password);
                    SetResult(Result.Ok, Intent);
                    Finish();
                }
            };

            // Cancel button just passes back a Canceled result and closes the AddUser screen.
            cancelButton.Click += (sender, e) =>
            {
                SetResult(Result.Canceled);
                Finish();
            };
        }
    }
}


