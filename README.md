# classroomapi
project with .Net WebApi, EF

Project Assumptions:

Allow students classNumber not define class
Allow class have no students

How to Use the Project:

1. Pull the project
2. Open with VS2015 in Windows OS, project use CodeFirst EF and make use of LocalDb.
3. Run the Project
4. Test with Postman

Post Content-Type: application/json

/register-class
{"classNumber":12, "teacher": "my math teacher"}

Post Content-Type: application/json

/register-student
{"id": "01000", "classNumber": 6, "score": 100}

Get

get-class-total-score/10013

Get

get-top-teacher


