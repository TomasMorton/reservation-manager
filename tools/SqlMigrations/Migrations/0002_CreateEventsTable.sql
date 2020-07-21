CREATE TABLE Events (
    AggregateId uniqueidentifier,
    Data nvarchar(4000),
    Version int,
    FOREIGN KEY (AggregateId) REFERENCES Aggregates(AggregateId)
    )
