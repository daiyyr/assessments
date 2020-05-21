-- a. 
-- Write a query that will list all the students who have had their 25th birthday 
-- (i.e.aged 25 or over). Display students’ ages in complete years (e.g. 27) 
-- and in the order of decreasing age. (1 mark) 
select S_ID, CONCAT(CONCAT(S_FIRST, ' '), S_LAST) AS NAME, S_DOB,
TRUNC(months_between(TRUNC(sysdate), S_DOB)/12, 0) as age
from student where S_DOB <= add_months(CURRENT_DATE, -25*12)
order by age desc;

-- b. 
-- Write a query that will list the total building capacity of various buildings. 
-- The rooms with a capacity of less than five must be excluded when generating a
-- building’s total capacity. This list (in the increasing order of the total capacity)
-- should only contain the buildings with a total building capacity of 150 or over. (1mark)
SELECT * FROM(
  select BLDG_CODE, SUM(CAPACITY) AS TOTAL_CAPACITY from LOCATION 
  where CAPACITY >= 5
  GROUP BY BLDG_CODE
) WHERE TOTAL_CAPACITY >= 150
;

-- c. 
-- Write a query that will list all faculty supervisors and their respective students.
-- Arrange your list in the order of faculty supervisor's names. (1 mark)
SELECT * FROM (
  SELECT 
  FACULTY.F_ID,
  MIN( CONCAT(CONCAT(FACULTY.F_FIRST, ' '), FACULTY.F_LAST)) AS supervisor,
  LISTAGG(CONCAT(CONCAT(STUDENT.S_FIRST, ' '), STUDENT.S_LAST), ', ')
    WITHIN GROUP (ORDER BY STUDENT.S_FIRST)  "STUDENTS"
  FROM FACULTY
  INNER JOIN STUDENT ON STUDENT.F_ID = FACULTY.F_ID
  GROUP BY FACULTY.F_ID
)
ORDER BY supervisor
;

-- d. 
-- Write a query that will list all the faculty members (along with the building code
-- and room number) who are located in the (BUS)iness building. (1.5 marks)
SELECT 
FACULTY.F_ID,
CONCAT(CONCAT(FACULTY.F_FIRST, ' '), FACULTY.F_LAST) AS NAME,
BLDG_CODE,
ROOM
FROM FACULTY
INNER JOIN LOCATION ON FACULTY.LOC_ID = LOCATION.LOC_ID
where BLDG_CODE = 'BUS'
;

-- e. 
-- Write a query that will list students who enrolled in the courses offered in either
-- the Fall term of 2017 or the Fall term of 2018. Do not display the duplicate student
-- names in the output. (1.5 marks)
SELECT 
MIN(STUDENT.S_ID) AS ID,
MIN(CONCAT(CONCAT(STUDENT.S_FIRST, ' '), STUDENT.S_LAST)) as NAME,
MIN(STUDENT.S_DOB) AS DOB,
MIN(TERM.TERM_DESC) AS TERM
FROM STUDENT
JOIN ENROLLMENT ON STUDENT.S_ID = ENROLLMENT.S_ID
JOIN COURSE_SECTION ON COURSE_SECTION.C_SEC_ID = ENROLLMENT.C_SEC_ID
JOIN TERM ON TERM.TERM_ID = COURSE_SECTION.TERM_ID
WHERE LOWER(REPLACE(TERM.TERM_DESC,' ','')) LIKE 'fall2017'
  or LOWER(REPLACE(TERM.TERM_DESC,' ','')) LIKE 'fall2018'
GROUP BY STUDENT.S_ID;


-- f.
-- Write a query that will list all the students (along with their grade and course
-- details) who got at least B or better grade (i.e. B or A) in any of their courses. The
-- list should be in the order of student id. (2 marks)
SELECT 
STUDENT.S_ID AS STUDENT_ID,
CONCAT(CONCAT(STUDENT.S_FIRST, ' '), STUDENT.S_LAST) as NAME,
STUDENT.S_DOB AS STUDENT_DOB,
COURSE.COURSE_NO,
COURSE.COURSE_NAME,
ENROLLMENT.GRADE
FROM STUDENT
JOIN ENROLLMENT ON STUDENT.S_ID = ENROLLMENT.S_ID
JOIN COURSE_SECTION ON COURSE_SECTION.C_SEC_ID = ENROLLMENT.C_SEC_ID
JOIN COURSE ON COURSE.COURSE_NO = COURSE_SECTION.COURSE_NO
WHERE STUDENT.S_ID IN
(
  SELECT STUDENT.S_ID
  FROM STUDENT
  JOIN ENROLLMENT ON STUDENT.S_ID = ENROLLMENT.S_ID
  JOIN COURSE_SECTION ON COURSE_SECTION.C_SEC_ID = ENROLLMENT.C_SEC_ID
  JOIN COURSE ON COURSE.COURSE_NO = COURSE_SECTION.COURSE_NO
  WHERE UPPER(REPLACE(ENROLLMENT.GRADE,' ', '')) = 'A'
  OR UPPER(REPLACE(ENROLLMENT.GRADE,' ', '')) = 'B' 
)
AND GRADE IS NOT NULL
ORDER BY STUDENT_ID,COURSE_NO
;





SELECT * FROM COURSE_SECTION;
SELECT * FROM ENROLLMENT;
SELECT * FROM COURSE;




