<Query Kind="Statements">
  <Connection>
    <ID>d6df3abb-2723-455c-83be-abe6ad24fc4d</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAHCZ2AFJP+k+nhYTxBNFrYQAAAAACAAAAAAAQZgAAAAEAACAAAADcDTPo09gUKTNxJQ61JPx3Ory1l0ytG2+WfZicR2jiaQAAAAAOgAAAAAIAACAAAADq4ukxHvqn6g3wizB71hOONZd3lvZPJRuwqLtZADRl2GAAAAAYdlFwuZVRzOPK0Y2DCmNKNmCFM+SDp7sZNFydaihydni1xaBoLSFxjXiZ2yUzAV7/daRseqBVduz3mdzCnDqUQpNFtX9TGLFAYsS6KJ2JHqwuLxrjUs4Z8fihWu4gaxBAAAAAF7GmupfcbN2p2hxQWDJ4QDFynZxcIFTlS++caEbOdT85YffsrM4WVLgeCbaDh/PQ/iMLSnHoh5aBtrOCVZKJcg==</CustomCxString>
    <Server>inms-oracle.massey.ac.nz</Server>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAHCZ2AFJP+k+nhYTxBNFrYQAAAAACAAAAAAAQZgAAAAEAACAAAAAwXCewvHdJB64P2O7SYhWTi10vXPoD23q6pXYUCknwMgAAAAAOgAAAAAIAACAAAABUEQp8C/b9nyulzTYLOev68Br9xEgFnGo6HOk24/XkchAAAADhCJae2CmIvukwEAiioAkTQAAAAMOV9H37KDcr/folEvPBeHS9Zpp107gxc2JFHyM/NgT5GX2IQHQHjpq3VRfNOM0hNeN44ecKXzLwDjy2PlYe76k=</Password>
    <UserName>IT337008</UserName>
    <DisplayName>orcl.massey.ac.nz</DisplayName>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>false</UseOciMode>
      <SID>orcl</SID>
      <Port>1521</Port>
    </DriverData>
  </Connection>
</Query>

/*Section C (LINQ Queries)*/


/*
q. 
List all faculty who earn 80,000 or over. (1 mark)
*/
var queryQ = 
	from f in Faculties
	where f.FSalary >= 80000
	select new{
		ID = f.FID,
		Name = f.FFirst + " " + f.FLast,
		Salary = f.FSalary
	};
queryQ.Dump();


/*
r. 
List all courses that have MIS in their course number. (1 mark)
*/
var queryR = 
	from c in Courses
	where c.CourseNo.Contains("MIS")
	select new{
		Number = c.CourseNo,
		Name = c.CourseName,
		Credits = c.Credits
	};
queryR.Dump();


/*
s. 
List all faculty and their location details. (1 mark)
*/
var queryS = 
	from f in Faculties
	join l in Locations on f.LocID equals l.LocID
	select new{
		FacultyID = f.FID,
		FacultyName = f.FFirst + " " + f.FLast,
		BuildingCode = l.BldgCode,
		Room = l.Room
	};
queryS.Dump();


/*
t. 
Display the total number of rooms in each building. (1.5 marks)
*/
var queryT = 
	from l in Locations 
	group l.Room by l.BldgCode into g
	select new
	{
		BuildingCode = g.Key,
		NumberOfRoom = g.Count()
	};
queryT.Dump();


/*
u. 
Display total number of students supervised by each faculty in the order of faculty last name. (1.5 marks)
*/
var queryU = 
	from f in Faculties
	join s in Students on f.FID equals s.FID
	group f by new{f.FID, f.FLast} into g		//include ID in group key in case of same-name-faculty; include LastName in group key for sorting
	orderby g.Key.FLast
	select new
	{
		FacultyName = g.Select(m => m.FFirst + " " + m.FLast).First(),
		NumberOfStudents = g.Count()
	}
	;
queryU.Dump();