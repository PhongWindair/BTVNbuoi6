-- Tạo database
CREATE DATABASE UniversityDB;
GO

-- Sử dụng database
USE UniversityDB;
GO

-- Tạo bảng Faculty
CREATE TABLE Faculty (
    FacultyID INT PRIMARY KEY, -- Khóa chính
    FacultyName NVARCHAR(100) NOT NULL -- Tên khoa
);

-- Tạo bảng Student
CREATE TABLE Student (
    StudentID INT PRIMARY KEY, -- Khóa chính
    FullName NVARCHAR(100) NOT NULL, -- Tên sinh viên
    DiemTrungBinh FLOAT CHECK (DiemTrungBinh >= 0 AND DiemTrungBinh <= 10), -- Điểm trung bình
    FacultyID INT, -- Khóa ngoại tham chiếu đến Faculty
    CONSTRAINT FK_Student_Faculty FOREIGN KEY (FacultyID)
        REFERENCES Faculty(FacultyID)
);
