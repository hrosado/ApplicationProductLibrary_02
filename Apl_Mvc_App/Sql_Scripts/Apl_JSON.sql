DECLARE @APLFileItems VARCHAR(MAX)

SELECT @APLFileItems =
	BulkColumn
FROM OPENROWSET(BULK'C:\Users\1260021520E\Documents\09_APL\ApplicationProductLibrary_02\Apl_Console_App\Data\files.json', SINGLE_BLOB) JSON

--SELECT @APLFileItems
IF (ISJSON(@APLFileItems) = 1)
	BEGIN
		PRINT 'JSON File is valid';
		DELETE FROM APLFileItems WHERE FileName IS NOT NULL;
		INSERT INTO APLFileItems
		SELECT *
		FROM OPENJSON(@APLFileItems)
		WITH (
			FileName nvarchar(300)	'$.FileName',
			TimeStamp datetime		'$.TimeStamp',
			FileSize nvarchar(50)	'$.FileSize'
		)
		--WHERE APLFileItems.FileName <> '$.FileName'
	END
ELSE
	BEGIN
		PRINT 'JSON File is invalid';
	END