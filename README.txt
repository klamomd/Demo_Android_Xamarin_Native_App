README

Sorry for the delay in submitting this! Got a little distracted and then ran into a little bit of trouble getting it sorted out.

I decided to implement this for Android, as I only have Android phones. I tested it out on both a Samsung Galaxy S3 and an LG Nexus 5x. I've gone ahead and provided the entire solution's folder for you to look at. The .apk files are in 'CodingTest1\CodingTest1\CodingTest1.Droid\bin\Debug'.

I made some assumptions about the username requirements:
	- Must not be empty, but has no max length.
	- Must not be a duplicate of a username already in the users list.
	- Must contain alphanumeric characters only.
	- Username is case-insensitive, so a username like "TESTUSER" is considered a duplicate of "testuser", or any other case variant.

The application will save all users on app pause or exit, and reinitialize the list of users once reopened.

I've decided to make it so that when adding a user, the user will only be prompted about invalid username/password requirements upon submission. The app will display a dialog describing the first failed requirement, so for example, if a username is empty and the password is an invalid length, the dialog will only mention that the username cannot be empty. If the user then provided a username but still had an invalid password length, then the next submission would prompt them that their password must be 5-12 characters.

I've included the ability to delete users too, since that seemed necessary to remove clutter if many users were added. Simply click on the user you wish to delete and you will be prompted with a delete dialog.

I also took the time to alter the icon for shits and giggles :D

That should be it! Let me know if you have any questions or troubles running the app!

- Michael Klamo