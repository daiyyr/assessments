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
k.1
statement level
Make sure the status of a term is CLOSED if the term started 2 years ago.
*/
CREATE OR REPLACE TRIGGER TRG_TERM_STATUS_CLOSED
AFTER INSERT ON TERM
BEGIN
  UPDATE TERM
    SET STATUS = 'CLOSED'
      WHERE START_DATE <= add_months(CURRENT_DATE, -2*12);
END;
-- UPDATE TERM SET STATUS = 'OPEN' WHERE START_DATE='15/05/18'
-- select * from term;
-- insert into TERM values(10, 'Spring 2018', 'OPEN', '18/01/2018');
-- select * from term;


/*
k.2
row level
Make sure the status of a term is OPEN if the START_DATE is in future.
*/
CREATE OR REPLACE TRIGGER TRG_TERM_STATUS_OPEN
BEFORE INSERT OR UPDATE OF STATUS, START_DATE ON TERM
FOR EACH ROW
BEGIN
  IF :NEW.START_DATE > CURRENT_DATE THEN
    :NEW.STATUS := 'OPEN';
  END IF;
END;
-- select * from term;
-- UPDATE TERM SET STATUS = 'CLOSE' WHERE START_DATE='09/01/21';
-- insert into TERM values(11, 'WINTER 2020', 'OPEN', '18/12/2020');
-- select * from term;

