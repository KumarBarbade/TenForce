TenForce Hiring – Developer – Test and Taste
General Overview
This exercise is about extending an existing project. The project handles data from a public API and
writes it to the console (screen) and a file (on disk).
The existing project can be found here:
https://drive.google.com/file/d/1C2NQCIpFv-wqpTF7_kjsE6bA4b-GqzK-/view?usp=sharing
Public API
The API we are using for this exercise is an API on a dataset that describes objects in our Solar System.
The home page for the API can be found here:  https://api.le-systeme-solaire.net/en/  . To explore the API
there is also support for a swagger interface:  https://api.le-systeme-solaire.net/swagger/  , alternatively,
tools such as POSTMan or Fiddler can be used for the same purpose.
There is no authentication on the API, so we do not expect this to be handled in the code provided.
The Dataset holds only two object types, knownCounts and bodies. For our exercise we are only
interested in the bodies data.
Exercise
Overview
The goal of the exercise is to extend the functionality offered by the application. The application offers
the ability to create a list of all planets and the moons that belong to them and to list all moons and
output their mass.
The extension will be to create a list of all planets that have at least one moon and list the average
temperature for those planets’ moons. To do so, you are invited to change any part of the code as you
see fit. Currently the system has no notion of temperature. To help you, the output services are already
creating these lists, but they do not calculate the average temperature. However, the planet class has a
method that provides the average temperature. This method can be used as a starting point.
Please motivate the changes you introduce in the domain model and in the API interface.
Preparing your environment
●  Create a local git repository to store your code and setup
●  Create a main branch.
●  Create a new branch you will use to do your intermediate commits to (name:
"TenForce-yourinitials")
●  Check out "TenForce-yourinitials"
●  Create a new .net solution and add a C# console project
The code
●  Create several classes in the console project to have a clear separation between data access,
data object, data handling
●  Enrich your code with comments and error handling. Also keep the users informed by showing
the different steps the code is in, during execution. ("Loading data...", "Writing data...", ...)
●  Make regular commits to your branch "TenForce-yourinitials"
●  You can use nuget packages and open source libraries to your likings.
Wrapping up
Once the project is finished merge your branch to the master branch and share Public Repo link with us.
Please note that it is not the aim to finish the entire exercise or use highly complex optimized code. The
aim is rather for us to get a feel for how you solve problems. And feel free to surprise us!
If you have any questions or things are unclear, you can contact us by email.
