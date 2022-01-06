DECLARE @APLFileItems VARCHAR(MAX)

SELECT @APLFileItems =
	BulkColumn
FROM OPENROWSET(BULK'C:\Users\CW2_Rosado\Documents\Repos\ApplicationProductLibrary_02\Apl_Console_App\Data\files.json', SINGLE_BLOB) JSON

--SELECT @APLFileItems
IF (ISJSON(@APLFileItems) = 1)
	BEGIN
		PRINT 'JSON File is valid';
		DELETE FROM AplDb.dbo.APLFileItems WHERE FileName IS NOT NULL;
		INSERT INTO AplDb.dbo.APLFileItems
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