SET SERVEROUTPUT ON;
/*
k. 
Think of some useful business rules or situations where it would be appropriate for
your triggers to fire. Do not write triggers to do something that could be done
using other database design constraints (e.g., simple referential integrity checking,
default values to attributes, or simply saying record is being inserted, or an
attribute has a null value, etc.). Provide sensible and useful trigger examples and
do not use the already given or similar triggers for this question.
Start with first clearly explaining the context, purpose (what they will do) of your
triggers. Then provide the PL SQL code and the results. Triggers should be based
on the tables already provided. Do not unnecessarily create too many and/or
similar tables. Adding one or two tables may be okay – but justification needed.
Altering a table (adding a field) is fine.
Write two triggers one statement level and another row level. Display the
successful creation and running of the triggers. Please ensure that you also display
the relevant tables before and after (results of the trigger) the trigger is fired.
Remember to provide the purpose of your triggers (as stated in question l. below,
this helps us to evaluate your work against the stated requirements). (10 marks)
*/


/*
k.1 Statement Level
Add a new field 'ENRL_OVERFLOW' to table COURSE_SECTION, default value 0.
After adding, deleting or updating enrollments, 
if a course section exceeds the maximun enrollment, set its ENRL_OVERFLOW to 1.
If a course section does not exceed the maximun enrollment, set its ENRL_OVERFLOW to 0.
*/

-- Add column ENRL_OVERFLOW
DECLARE
v_column_exists number := 0;
BEGIN
Select count(*) into v_column_exists 
from user_tab_cols
where upper(column_name) = 'ENRL_OVERFLOW' and upper(table_name) = 'COURSE_SECTION';
if (v_column_exists = 0) then
    execute immediate 'alter table COURSE_SECTION add (ENRL_OVERFLOW NUMBER(1) DEFAULT 0)';
end if;
END;
/
-- Create trigger
CREATE OR REPLACE TRIGGER COURSE_SECTION_ENRL_OVERFLOW
AFTER INSERT OR DELETE OR UPDATE OF C_SEC_ID ON ENROLLMENT
BEGIN
  UPDATE COURSE_SECTION SET ENRL_OVERFLOW = 1
  WHERE C_SEC_ID IN 
  (
    SELECT C_SEC_ID FROM
    (
      SELECT COURSE_SECTION.C_SEC_ID, MIN(COURSE_SECTION.MAX_ENRL) AS MAX_ENRL, COUNT(*) AS enrl_count
      FROM COURSE_SECTION
      JOIN ENROLLMENT ON ENROLLMENT.C_SEC_ID = COURSE_SECTION.C_SEC_ID
      GROUP BY COURSE_SECTION.C_SEC_ID
    )WHERE enrl_count > MAX_ENRL
  );
  UPDATE COURSE_SECTION SET ENRL_OVERFLOW = 0
  WHERE C_SEC_ID IN 
  (
    SELECT C_SEC_ID FROM
    (
      SELECT COURSE_SECTION.C_SEC_ID, MIN(COURSE_SECTION.MAX_ENRL) AS MAX_ENRL, COUNT(*) AS enrl_count
      FROM COURSE_SECTION
      JOIN ENROLLMENT ON ENROLLMENT.C_SEC_ID = COURSE_SECTION.C_SEC_ID
      GROUP BY COURSE_SECTION.C_SEC_ID
    )WHERE enrl_count <= MAX_ENRL
  );
END;
/*
-- Testing
SELECT COURSE_SECTION.C_SEC_ID, MIN(COURSE_SECTION.MAX_ENRL) AS MAX_ENRL, COUNT(*) AS enrl_count, min(ENRL_OVERFLOW)
      FROM COURSE_SECTION
      JOIN ENROLLMENT ON ENROLLMENT.C_SEC_ID = COURSE_SECTION.C_SEC_ID
      WHERE COURSE_SECTION.C_SEC_ID IN (1,9,11)
      GROUP BY COURSE_SECTION.C_SEC_ID;
delete from ENROLLMENT where S_ID = 'SM100' and C_SEC_ID = 1; 
select * from COURSE_SECTION where C_SEC_ID = 1; -- shall not overflow
insert into ENROLLMENT values('SM100', 9, null); 
select * from COURSE_SECTION where C_SEC_ID = 9; -- shall overflowed
update ENROLLMENT set C_SEC_ID = 11 where S_ID = 'SM100' AND C_SEC_ID = 1;
select * from COURSE_SECTION where C_SEC_ID = 11; -- shall overflowed
update ENROLLMENT set C_SEC_ID = 1 where S_ID = 'SM100' AND C_SEC_ID = 11;
select * from COURSE_SECTION where C_SEC_ID = 11; -- shall not overflow
*/
/


/*
k.2 Row Level

When inserting or updating a term,
make sure the status is CLOSED if the term started 2 year ago
*/
CREATE OR REPLACE TRIGGER TRG_TERM_STATUS_CLOSED
BEFORE INSERT OR UPDATE OF STATUS, START_DATE ON TERM
FOR EACH ROW
BEGIN
  IF :NEW.START_DATE <= add_months(CURRENT_DATE, -2*12) THEN
    :NEW.STATUS := 'CLOSED';
  END IF;
END;
/* 
Testing
select * from term;
UPDATE TERM SET STATUS = 'OPEN' WHERE START_DATE='15/05/18';
insert into TERM values(
  (SELECT TERM_ID + 1 FROM TERM ORDER BY TERM_ID DESC fetch first 1 row only),
  'WINTER 2020', 'OPEN', '19/01/2018'
);
select * from term;
*/
/

/*
l. 
Write a trigger that does not allow more than two 'Full' ranked professors as part of
the faculty (For example, trigger should fire if a new (third) Full professor is added
or rank of one of the existing Associate professors is updated to Full). Provide
comprehensive test data and results to confirm that the trigger works. (4 marks)
*/
CREATE OR REPLACE TRIGGER TRG_FACULTY_F_RANK
BEFORE INSERT OR UPDATE OF F_RANK ON FACULTY
FOR EACH ROW
DECLARE
  EXCEED_MAXIMUM_FULL_PROFESSORS EXCEPTION;
  CURRENT_PROFESSOR_COUNT NUMBER;
  pragma autonomous_transaction;
BEGIN
  IF 
    REPLACE(UPPER(:NEW.F_RANK),' ','') = 'FULL'
    AND (:OLD.F_RANK IS NULL OR REPLACE(UPPER(:OLD.F_RANK),' ','')!= 'FULL') -- when updating, :old.f_rank is null
  THEN
    SELECT COUNT(*)
    INTO CURRENT_PROFESSOR_COUNT 
    FROM FACULTY 
    WHERE REPLACE(UPPER(F_RANK),' ','') = 'FULL';
    IF 
      CURRENT_PROFESSOR_COUNT >= 2
    THEN
      DBMS_OUTPUT.PUT_LINE('More than 2 Full professors are not allowed.');
      RAISE EXCEED_MAXIMUM_FULL_PROFESSORS;
    END IF;
  END IF;
  
EXCEPTION
  WHEN EXCEED_MAXIMUM_FULL_PROFESSORS THEN
    RAISE_APPLICATION_ERROR(-20001, 'CANNOT INSERT OR UPDATE: More than 2 Full professors are not allowed');
END;
/* Testing
select * from FACULTY; -- 2 profesors already
INSERT INTO FACULTY values(16, 'GREEN', 'JACK', 'J', 9, '354352435', 'Assistant', 100000, 4, 6000, EMPTY_BLOB()); -- this shall succeed
INSERT INTO FACULTY values(17, 'GREEN', 'JACK', 'J', 9, '354352435', 'Full', 100000, 4, 6000, EMPTY_BLOB()); -- this shall raise exception
UPDATE FACULTY SET F_RANK = 'Assis2' WHERE F_ID = 6; -- this shall succeed
UPDATE FACULTY SET F_RANK = 'Full' WHERE F_ID = 6; -- this shall raise exception
UPDATE FACULTY SET F_RANK = 'Full', F_SALARY = 10300 WHERE F_ID = 4; -- this shall succeed (:old.f_rank = 'Full')
UPDATE FACULTY SET F_SALARY = 10300 WHERE F_ID = 4; -- this shall succeed (:old.f_rank = 'Full')
select * from FACULTY;
*/
/


/*
m. 
Write a procedure to insert a new faculty record. The procedure should also
automatically calculate the faculty salary value. This calculated salary is 15% less
than the average salary of the existing faculty members.
Provide rest of the attribute values as input parameters. Execute your procedure to
insert at least one faculty record. (3 marks)
*/
CREATE OR REPLACE PROCEDURE insert_faculty_auto_salary( 
  Lastname VARCHAR, Firstname VARCHAR, MI CHAR, LocationID NUMBER, Phone VARCHAR, RankS VARCHAR, Super NUMBER, PIN NUMBER
)
IS
BEGIN
INSERT INTO faculty values (
  (SELECT F_ID + 1 FROM faculty ORDER BY F_ID DESC fetch first 1 row only),
  Lastname,
  Firstname,
  MI,
  LocationID,
  Phone,
  RankS,
  (select ROUND(0.85 * avg(f_salary), 0) from faculty), 
          -- no definition for 'existing faculty members', then we include all faculties when calculating avg
  Super,
  PIN,
  EMPTY_BLOB()
);
END;
/
-- Testing
exec insert_faculty_auto_salary('Ye', 'Lucy', 'J', 9, '43254325', 'Associate', 4, 9877);
-- select * from faculty;



/*
n. 
Write a trigger to check that when salary is updated for an existing faculty the raise
is not over 4%.
*/
CREATE OR REPLACE TRIGGER TRG_FACULTY_F_SALARY_RAISE
BEFORE UPDATE OF F_SALARY ON FACULTY
FOR EACH ROW
DECLARE
  EXCEED_MAX_SALARY_RAISE EXCEPTION;
  pragma autonomous_transaction;
BEGIN
  IF 
    :NEW.F_SALARY / :OLD.F_SALARY > 1.04 -- SHOULD NOT OVER 4%
  THEN
    DBMS_OUTPUT.PUT_LINE('Salary raise shall not over 4%');
    RAISE EXCEED_MAX_SALARY_RAISE;
  END IF;
  
EXCEPTION
  WHEN EXCEED_MAX_SALARY_RAISE THEN
    RAISE_APPLICATION_ERROR(-20002, 'CANNOT UPDATE: Salary raise shall not over 4%');
END;
/
/*
Testing
select * from faculty where f_id = 3; -- orginal salary: $60000
Update FACULTY set F_SALARY = 70000 where f_id = 3; -- this shall raise EXCEED_MAX_SALARY_RAISE exception
Update FACULTY set F_SALARY = 62000 where f_id = 3; -- this shall succeed
*/



/*
o. 
Write a cursor to list course sections for all the MIS courses (along with their
courses names and credits). (3 marks)
*/
DECLARE CURSOR cursor_mic_course_section IS
SELECT COURSE.COURSE_NO, COURSE.COURSE_NAME, CREDITS, TERM_ID, SEC_NUM,C_SEC_DAY
FROM COURSE JOIN COURSE_SECTION ON COURSE.COURSE_NO = COURSE_SECTION.COURSE_NO
WHERE COURSE.COURSE_NO LIKE '%MIS%';
vr_course_section cursor_mic_course_section%rowtype;
BEGIN
OPEN cursor_mic_course_section;
LOOP
FETCH cursor_mic_course_section INTO vr_course_section;
EXIT WHEN cursor_mic_course_section%NOTFOUND;
DBMS_OUTPUT.PUT_LINE(
vr_course_section.COURSE_NO || ', '
|| vr_course_section.COURSE_NAME
|| ', Credit: ' || vr_course_section.CREDITS
|| ', TermId: ' ||vr_course_section.TERM_ID
|| ', SecNum: ' || vr_course_section.SEC_NUM
|| ', Days: ' || vr_course_section.C_SEC_DAY
);
END LOOP;
CLOSE cursor_mic_course_section;
END;
/


/*
p. 
Write a function, which can be used to format faculty member’s salary to
$9,999,999.99. Do not hard code the exact salary datatype (i.e. your function
should work even if in future some minor changes are made to the salary data
type/size). Call this function in a SQL statement for displaying a faculty member’s
salary. (2 marks)
*/
CREATE OR REPLACE FUNCTION format_salary(amount number)
RETURN VARCHAR2 IS
BEGIN
  RETURN to_char(amount , '$999,999,999,999,999.99');
END format_salary;
/
-- Testing
select CONCAT(CONCAT(FACULTY.F_FIRST, ' '), FACULTY.F_LAST) as Name, 
format_salary(F_SALARY) as number_salary, 
format_salary(SALARY2) as varchar_salary      -- SALARY2 is in varchar data type '3453463'
from faculty
where F_ID = 1;




