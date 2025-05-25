create database dbNote;
go 

use dbNote;
go

create table Notes(
    Id int primary key identity(1,1), --auto Id
    Title nvarchar(100) not null,
    Content nvarchar(max),
    CreateDate date not null default getdate(), --if input exclude this date, it'll auto get the current date 
	UpdateDate date
);

INSERT INTO Notes (Title, Content)
VALUES 
    ('Meeting Notes', 'Discussed project timeline and deliverables.'),
    ('Shopping List', 'Milk, Bread, Eggs, Butter'),
    ('Book Summary', 'Summary of "The Great Gatsby" by F. Scott Fitzgerald.'),
    ('Reminder', 'Call the electrician tomorrow.');
