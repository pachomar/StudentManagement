
Student Management Application Exercise


Setting Up Application / Log In
=====================================================================================================================================

Once you download the repository, it is recommended that you extract inside the path "C:\git\StudentManager". This helps prevent issues 
with the database connection. Otherwise, you will need to modify the connectionString in Web project and all Tests projects. The database 
only consists on two tables: Actor and Student (with the specified fields we should use from the definition)
The application includes all the requied packages for it to run. The installed packages via NuGet are:
- Twilio
- Entity Framework
- JQuery UI
- Moq
- NUnit/NUnit3Adapter
- Moment.js

Once the application folder is extracted, go to StudentManagement and open the .sln file. You can now build and run the application.
The first screen you will see is the login screen. From the document of the exercise, there are two hardcoded users in Actor table in DB

- UserName: "sarah@example.edu" / Password: "studentAdmin" (studentAdmin)
- UserName: "bob@example.edu" / Password: "staffMember" (staffMember)

After sucessful login, the user info will be saved in the cookies in order to make use of Authorize in the Controller methods


Navigation / Student CRUD
=====================================================================================================================================
- Student List: Once logged in, you will be sent to the Student list view. On this view you will see a grid wiht students and a link 
to the details view. Also you will notice a textbox in which the grid can be filtered by last name (this was one of the requirements). 
If the logged user is an Admin, a Create button will show up on the top left. This is used to create new students

- Student Details: By clicking the Details link on a record, you will be taken to the detail view. If you are an Admin, you will see 
three Button: "Back To List" (return to the student list) - "Edit" (Redirects to the user detail edit view) - "Delete" (redirects to
user deletion screen). If the user is not Admin only "Back To List" will appear

- Student Create: All of the fields are editable except SudentNumber (it's auto-generated in the backend). Validation using Data 
Annotations will be perfmored before saving. The view has "Back To List" and "Save" buttons as requested

- Student Edit: Similar to create view, but populated with the info of the selected student. Validation via Data Annotations also take
place before saving. This view has "Back To List"/"Save"/"Cancel" button, with the requested behaviour for each of them

- Student Delete: This view has two buttons: "Delete" which will redirect to List view, and "Cancel", which will redirect to details 
view as specified in the document


Architecture Definition
=====================================================================================================================================
The project was implemented in a 3 tier layer application. This and the use of dependency injection allowed the code to be decoupled and
provided an effective way to test each layer:

- StudentManagement.Entities: Contains the model entities created with Entity Framework 6
- StudentManagement.Entities.Tests: Unit Test to validate fields are correctly setup on each entity
- StudentManagement.Services: Contains the services that will be in charge of doing database modifications using the Entities layer
- StudentManagement.Services.Test: Mocking and Unit testing of the services and their method to ensure their functionality
- StudentManagement.Web: The web MVC application used as the main project
- StudentManagement.Web.Tests: Unit testing of controllers and theur methods


Unit Testing
=====================================================================================================================================
Both StudentManagement.Web.Tests and StudentManagement.Services.Test will need to modify the connectionString to test the databse 
(unless the content of the repository is extracted in a "C:\git\StudentManager" folder. In order to change the connectionString, 
replace as follows:

  <connectionStrings>
    <add name="StudentDBEntities" 
	connectionString="metadata=res://*/StudentDBModel.csdl|res://*/StudentDBModel.ssdl|res://*/StudentDBModel.msl;
	provider=System.Data.SqlClient;provider connection string=&quot;data source=(LocalDB)\MSSQLLocalDB;
	attachdbfilename=C:\git\StudentManager\StudentManagement\StudentManagement.Web\App_Data\StudentDB.mdf;  <== Replace this path with your mdf location
	integrated security=True;connect timeout=30;MultipleActiveResultSets=True;App=EntityFramework&quot;" 
	providerName="System.Data.EntityClient" />
  </connectionStrings>
  

Notes
=====================================================================================================================================
- The phone validation is setup to suport US phonenumbers or Argentina phonenumbers (this is specified in the Web.config with 
key name="Twilio.validCultures"). If more cultures need to be added, you can edit the value in the web.config, adding the country code
separated by commas (i.e: "US,AR,FR" or "US,UK" etc.)

- The student number was something I used randomly (since the only specification for this field was to be unique and not null). I decided
to create 25 long string with numbers (since the specification said "auto-generated". You can find the class in Service layer/Utils folder

- There is a known bug, related to VS2017 redirections, on which if a url is forced (on the browser navigation bar) and the user doesn't 
have permissions to see it, the default Authorize error screen will appear. The fact that I'm using a Custom Authorize method makes it 
more complicated (would be a lot easier if Active Directory or Windows authentication was used
