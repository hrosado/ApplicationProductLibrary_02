CREATE TABLE tblgetfileListA (excelFileName VARCHAR(100));

INSERT INTO tblgetfileListA

EXEC xp_cmdshell 'dir /B "\\hqcuilms.area52.afnoapps.usaf.mil\E\DLL_Reengineering\Dependencies_x64_Release"';

select * from tblgetfileListA

-- Source: https://sqlskull.com/2019/12/25/sql-query-to-get-the-list-of-files-in-a-folder-in-sql/