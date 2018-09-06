DELETE FROM [Data_Lock].[DAS_DataLock]
GO
INSERT INTO [Data_Lock].[DAS_DataLock] ([Collection], [Ukprn], [LearnRefNumber], [ULN], [AimSeqNumber], [RuleId], [CollectionPeriodName], [CollectionPeriodMonth], [CollectionPeriodYear], [LastSubmission], [TNP]) VALUES ('ILR', 10001234, 'BOBXX111PBOB', 1234567890, 1, 'DLOCK_02', '1819-R01', 8, 2018, '2018-08-31 10:12:10', 2000)
INSERT INTO [Data_Lock].[DAS_DataLock] ([Collection], [Ukprn], [LearnRefNumber], [ULN], [AimSeqNumber], [RuleId], [CollectionPeriodName], [CollectionPeriodMonth], [CollectionPeriodYear], [LastSubmission], [TNP]) VALUES ('ILR', 10001234, 'PXX1234501X1', 9876543210, 1, 'DLOCK_01', '1819-R01', 8, 2018, '2018-08-31 10:12:11', 1900)
INSERT INTO [Data_Lock].[DAS_DataLock] ([Collection], [Ukprn], [LearnRefNumber], [ULN], [AimSeqNumber], [RuleId], [CollectionPeriodName], [CollectionPeriodMonth], [CollectionPeriodYear], [LastSubmission], [TNP]) VALUES ('ILR', 10005678, 'PXBTL99JBK20', 1234543210, 1, 'DLOCK_04', '1819-R01', 8, 2018, '2018-08-31 10:12:12', 2000)
GO

DELETE FROM [Data_Lock].[DAS_ValidLearners]
GO
INSERT INTO [Data_Lock].[DAS_ValidLearners] ([Ukprn], [NumberOfLearners]) VALUES (10001234, 50)
INSERT INTO [Data_Lock].[DAS_ValidLearners] ([Ukprn], [NumberOfLearners]) VALUES (10005678, 7)
GO

DELETE FROM [Data_Lock].[DAS_ValidAims]
GO
INSERT INTO [Data_Lock].[DAS_ValidAims] ([Ukprn], [NumberOfLearnersWithACT1], [NumberOfLearnersWithACT2]) VALUES (10001234, 30, 20)
INSERT INTO [Data_Lock].[DAS_ValidAims] ([Ukprn], [NumberOfLearnersWithACT1], [NumberOfLearnersWithACT2]) VALUES (10005678, 7, 0)
GO

/*
SELECT * FROM [Data_Lock].[DAS_DataLock]
SELECT * FROM [Data_Lock].[DAS_ValidLearners]
SELECT * FROM [Data_Lock].[DAS_ValidAims]
*/
