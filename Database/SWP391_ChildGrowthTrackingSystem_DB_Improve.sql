USE master
GO
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'SWP391_ChildGrowthTrackingSystemDB')
BEGIN
    DROP DATABASE SWP391_ChildGrowthTrackingSystemDB;
END
GO
CREATE DATABASE SWP391_ChildGrowthTrackingSystemDB;
GO
USE SWP391_ChildGrowthTrackingSystemDB;
GO
-- Table Roles
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName VARCHAR(255) NOT NULL,                        -- Tên của vai trò (ví dụ: Admin, Member, Doctor)
    Description NVARCHAR(2000),                            -- Mô tả chi tiết về vai trò
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',         -- Trạng thái (Inactive, Active, Pending, etc.)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Thời gian tạo vai trò
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP  -- Thời gian cập nhật vai trò
);

-- Table UserAccounts
CREATE TABLE UserAccounts (
    UserID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),    -- ID người dùng
    Username VARCHAR(255) UNIQUE NOT NULL,                  -- Tên người dùng duy nhất
    Email VARCHAR(255) UNIQUE NOT NULL,                     -- Email duy nhất
    PhoneNumber VARCHAR(20) UNIQUE NOT NULL,                -- Số điện thoại duy nhất
    Name NVARCHAR(255) NOT NULL,                            -- Tên người dùng
    Gender NVARCHAR(20) NOT NULL,                           -- Giới tính (Female, Male, Other)
    DateOfBirth DATE,                                       -- Ngày sinh
    Address NVARCHAR(255) NOT NULL,                         -- Địa chỉ
    Password VARCHAR(255) NOT NULL,                         -- Mật khẩu
    RegistrationDate DATETIME NOT NULL,                     -- Ngày đăng ký
    LastLogin DATETIME,                                     -- Ngày đăng nhập lần cuối
    ProfilePicture VARBINARY(2000),                         -- Lưu ảnh dưới dạng nhị phân
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',          -- Trạng thái người dùng
    VerificationCode VARCHAR(10),                           -- Mã xác thực
    IsVerified BIT DEFAULT 0,                               -- Trạng thái xác thực người dùng
    RoleID INT NOT NULL,                                    -- Vai trò người dùng (tham chiếu tới Roles)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo người dùng
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật người dùng
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)           -- Liên kết với bảng Roles
);

-- Table Members
CREATE TABLE Members (
    MemberID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),  -- ID của thành viên
    UserID UNIQUEIDENTIFIER UNIQUE NOT NULL,                -- ID người dùng liên kết với bảng UserAccounts
    EmergencyContact NVARCHAR(255) NOT NULL,                -- Thông tin liên lạc khẩn cấp
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',          -- Trạng thái thành viên (1: active, 2: inactive, 3: pending, etc.)
    JoinDate DATETIME NOT NULL,                             -- Ngày gia nhập
    LeaveDate DATETIME,                                     -- Ngày rời khỏi hệ thống (nếu có)
	Notes NVARCHAR(2000),                                   -- Ghi chú thêm
    FOREIGN KEY (UserID) REFERENCES UserAccounts(UserID)    -- Liên kết với bảng UserAccounts
);

-- Table Children
CREATE TABLE Children (
    ChildID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),   -- ID của trẻ
    MemberID UNIQUEIDENTIFIER  NOT NULL,                    -- ID thành viên liên kết với bảng Members
    Name NVARCHAR(255) NOT NULL,                            -- Tên trẻ
    DateOfBirth DATE NOT NULL,                              -- Ngày sinh
    Gender NVARCHAR(20) NOT NULL,                           -- Giới tính (Female, Male, Other)
    BirthWeight FLOAT,                                      -- Cân nặng lúc sinh
    BirthHeight FLOAT,                                      -- Chiều cao lúc sinh
    BloodType VARCHAR(10),                                  -- Nhóm máu
    Allergies NVARCHAR(2000),                               -- Dị ứng (nếu có)
    Notes NVARCHAR(2000),                                   -- Ghi chú thêm
	RelationshipToMember NVARCHAR(50) NOT NULL,             -- Mối quan hệ với member
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',          -- Trạng thái (active, inactive, pending, v.v.)
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo thông tin trẻ
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật thông tin (nếu có thay đổi)
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID)     -- Liên kết với bảng Members
);

-- Table GrowthRecords
CREATE TABLE GrowthRecords (
    RecordID INT PRIMARY KEY IDENTITY(1,1),               -- ID bản ghi
    ChildID UNIQUEIDENTIFIER NOT NULL,                    -- ID của trẻ
    RecordedBy UNIQUEIDENTIFIER NOT NULL,                 -- ID người ghi nhận
    Weight FLOAT NOT NULL,                                -- Cân nặng của trẻ
    Height FLOAT NOT NULL,                                -- Chiều cao của trẻ
    HeadCircumference FLOAT,                              -- Vòng đầu
    MuscleMass FLOAT,                                     -- Khối lượng cơ bắp
    ChestCircumference FLOAT,                             -- Vòng ngực
    NutritionalStatus NVARCHAR(50),                       -- Tình trạng dinh dưỡng
    FerritinLevel FLOAT,                                  -- Mức ferritin
    Triglycerides FLOAT,                                  -- Mức triglycerides
    BloodSugarLevel FLOAT,                                -- Mức đường huyết
    PhysicalActivityLevel NVARCHAR(50),                   -- Mức độ hoạt động thể chất
    HeartRate INT,                                        -- Nhịp tim
    BloodPressure FLOAT,                                  -- Huyết áp
    BodyTemperature FLOAT,                                -- Nhiệt độ cơ thể
    OxygenSaturation FLOAT,                               -- Mức độ bão hòa oxy
    SleepDuration FLOAT,                                  -- Thời gian ngủ
    Vision NVARCHAR(50),                                  -- Tình trạng thị giác
    Hearing NVARCHAR(50),                                 -- Tình trạng thính giác
    ImmunizationStatus NVARCHAR(2000),                    -- Tình trạng tiêm chủng
    MentalHealthStatus NVARCHAR(50),                      -- Tình trạng sức khỏe tâm thần
    GrowthHormoneLevel FLOAT,                             -- Mức độ hormone tăng trưởng
    AttentionSpan NVARCHAR(50),                           -- Thời gian chú ý
    NeurologicalReflexes NVARCHAR(255),                   -- Phản xạ thần kinh
    DevelopmentalMilestones NVARCHAR(255),                -- Mốc phát triển
    Notes NVARCHAR(2000),                                 -- Ghi chú khác
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',        -- Trạng thái
    Verified BIT DEFAULT 0,                               -- Trạng thái xác nhận
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,         -- Thời gian tạo bản ghi
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,         -- Thời gian cập nhật bản ghi
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID),   -- Liên kết với bảng Children
    FOREIGN KEY (RecordedBy) REFERENCES Members(MemberID) -- Liên kết với bảng UserAccounts
);

-- Table Promotions
CREATE TABLE Promotions (
    PromotionID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), -- Mã khuyến mãi
    PromotionCode VARCHAR(50) NOT NULL,                       -- Mã khuyến mãi duy nhất
    Description NVARCHAR(2000),                               -- Mô tả chi tiết về khuyến mãi
    DiscountPercent INT NOT NULL,                             -- Phần trăm giảm giá
    MinPurchaseAmount DECIMAL(10, 2),                         -- Mức mua tối thiểu để áp dụng khuyến mãi
    MaxDiscountAmount DECIMAL(10, 2),                         -- Giới hạn giảm giá tối đa
    ApplicablePackageIDs NVARCHAR(2000),                      -- Các gói thành viên áp dụng (nếu có)
    TargetAudience VARCHAR(255),                              -- Đối tượng áp dụng khuyến mãi
    StartDate DATE NOT NULL,                                  -- Ngày bắt đầu khuyến mãi
    EndDate DATE NOT NULL,                                    -- Ngày kết thúc khuyến mãi
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',            -- Trạng thái khuyến mãi (Active, Inactive, Expired, Cancelled)
    RedemptionCount INT DEFAULT 0,                            -- Số lần mã đã được sử dụng
    UsageLimit INT,                                           -- Giới hạn số lần sử dụng khuyến mãi
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,    -- Thời gian tạo khuyến mãi
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,    -- Thời gian cập nhật khuyến mãi
    CreatedBy UNIQUEIDENTIFIER NOT NULL,                      -- Người tạo mã khuyến mãi
    ModifiedBy UNIQUEIDENTIFIER NOT NULL,                     -- Người sửa mã gần đây
    FOREIGN KEY (CreatedBy) REFERENCES UserAccounts(UserID),
    FOREIGN KEY (ModifiedBy) REFERENCES UserAccounts(UserID)
);

-- Table MembershipPackages
CREATE TABLE MembershipPackages (
    PackageID INT PRIMARY KEY IDENTITY(1,1),
    PackageName NVARCHAR(255) NOT NULL,                     -- Tên gói thành viên
    Description NVARCHAR(2000),                             -- Mô tả về gói thành viên
    Price DECIMAL(10, 2) NOT NULL,                          -- Giá gói thành viên
    Currency NVARCHAR(10) DEFAULT 'VND',                    -- Đơn vị tiền tệ của giá gói
    DurationMonths INT NOT NULL,                            -- Thời gian sử dụng gói trong tháng
    TrialPeriodDays INT,                                    -- Thời gian dùng thử (nếu có)
    MaxChildrenAllowed INT NOT NULL,                        -- Số lượng trẻ tối đa được phép trong gói
    SupportLevel NVARCHAR(255),                             -- Mức độ hỗ trợ của gói thành viên
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',          -- Trạng thái của gói thành viên (1: active, 0: inactive, 2: pending)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo gói thành viên
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian cập nhật
);

-- Table PackagePromotions
CREATE TABLE PackagePromotions (
    PackageID INT NOT NULL,
    PromotionID UNIQUEIDENTIFIER NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,                         -- Trạng thái hoạt động của mối quan hệ (1: Active, 0: Inactive)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian mối quan hệ này được tạo
	UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (PackageID, PromotionID),
    FOREIGN KEY (PackageID) REFERENCES MembershipPackages(PackageID),
    FOREIGN KEY (PromotionID) REFERENCES Promotions(PromotionID)
);

-- Table Features
CREATE TABLE Features (
    FeatureID INT PRIMARY KEY IDENTITY(1,1),
    FeatureName NVARCHAR(255) NOT NULL,
    Description NVARCHAR(2000),
	Status NVARCHAR(50) NOT NULL DEFAULT 'Active',
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Table PackageFeatures
CREATE TABLE PackageFeatures (
    PackageID INT,                                           -- Gói thành viên
    FeatureID INT,                                           -- Tính năng
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian tạo
	UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	Status NVARCHAR(50) NOT NULL DEFAULT 'Active',
    PRIMARY KEY (PackageID, FeatureID),                      -- Khóa chính kép
    FOREIGN KEY (PackageID) REFERENCES MembershipPackages(PackageID),
    FOREIGN KEY (FeatureID) REFERENCES Features(FeatureID)
);

-- Table MemberMemberships
CREATE TABLE MemberMemberships (
    MemberMembershipID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    MemberID UNIQUEIDENTIFIER NOT NULL,                               -- Liên kết với bảng Members
    PackageID INT NOT NULL,                                           -- Liên kết với bảng MembershipPackages
    StartDate DATETIME NOT NULL,                                      -- Ngày bắt đầu gói thành viên
    EndDate DATETIME NOT NULL,                                        -- Ngày kết thúc gói thành viên
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',                    -- Trạng thái của gói thành viên (Active, Inactive, Pending, Suspended, Expired, Canceled, Renewing, Trial)
    IsActive BIT NOT NULL DEFAULT 1,                                  -- Trạng thái hoạt động của gói
    Description NVARCHAR(2000),                                       -- Mô tả gói thành viên
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,            -- Thời gian tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,            -- Thời gian cập nhật
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID),              -- Liên kết với bảng Members
    FOREIGN KEY (PackageID) REFERENCES MembershipPackages(PackageID)  -- Liên kết với bảng MembershipPackages
);

CREATE TABLE Transactions (
    TransactionID UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    UserID UNIQUEIDENTIFIER NOT NULL,
    MemberMembershipID UNIQUEIDENTIFIER NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    Currency NVARCHAR(50) NOT NULL,
    TransactionType NVARCHAR(100) NOT NULL,                      -- Loại giao dịch (ví dụ: Đăng ký, Mua thẻ, v.v.)
    PaymentMethod NVARCHAR(50) NOT NULL,                         -- Cổng thanh toán (VnPay, MoMo, Internet Banking)
    TransactionDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Thời điểm giao dịch (ngày người dùng bắt đầu giao dịch)
    PaymentDate DATETIME,                                        -- Thời điểm thanh toán (ngày thanh toán được xử lý thành công, cập nhật khi Status = 'Success')
    GatewayTransactionID bigint NOT NULL,                        -- Mã giao dịch từ cổng thanh toán (ví dụ: mã giao dịch VnPay, MoMo)
    PaymentStatus NVARCHAR(50) NOT NULL DEFAULT 'Active',        -- Trạng thái thanh toán (Pending, Success, Failed, Cancelled, Refunded)
    Description NVARCHAR(255),                                   -- Mô tả giao dịch
    FOREIGN KEY (UserID) REFERENCES UserAccounts(UserID),
    FOREIGN KEY (MemberMembershipID) REFERENCES MemberMemberships(MemberMembershipID)
);

-- Table Doctors
CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),
    UserID UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PhoneNumber VARCHAR(20) UNIQUE NOT NULL,
    Degree NVARCHAR(255) NOT NULL,
	HospitalName NVARCHAR(255),                      -- Tên bệnh viện làm việc
    HospitalAddress NVARCHAR(255),                   -- Địa chỉ bệnh viện
    Biography NVARCHAR(2000),
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES UserAccounts(UserID)
);

-- Table Specializations
CREATE TABLE Specializations (
    SpecializationID INT PRIMARY KEY IDENTITY(1,1),             -- Sử dụng UNIQUEIDENTIFIER làm khóa chính và tự động tạo GUID
    SpecializationName NVARCHAR(255) NOT NULL,                  -- Tên chuyên ngành không thể null
    Description NVARCHAR(2000),                                 -- Mô tả về chuyên ngành
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',              -- Trạng thái của chuyên ngành (1: active, 0: inactive)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,      -- Thời gian tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP       -- Thời gian cập nhật
);

-- Table DoctorSpecializations
CREATE TABLE DoctorSpecializations (
    DoctorSpecializationID INT PRIMARY KEY IDENTITY(1,1),                       -- ID mối quan hệ bác sĩ-chuyên ngành
    DoctorID INT NOT NULL,                                                      -- ID bác sĩ (liên kết với bảng Doctors)
    SpecializationID INT NOT NULL,                                              -- ID chuyên ngành (liên kết với bảng Specializations)
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',                              -- Trạng thái (1: active, 0: inactive)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,                      -- Thời gian tạo mối quan hệ
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,                      -- Thời gian cập nhật mối quan hệ
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),                        -- Liên kết với bảng Doctors
    FOREIGN KEY (SpecializationID) REFERENCES Specializations(SpecializationID) -- Liên kết với bảng Specializations
);

-- Table ConsultationRequests
CREATE TABLE ConsultationRequests (
    RequestID INT PRIMARY KEY IDENTITY(1,1),                         -- ID duy nhất cho yêu cầu
    MemberID UNIQUEIDENTIFIER NOT NULL,                              -- Liên kết với bảng Members
    ChildID UNIQUEIDENTIFIER NOT NULL,                               -- Liên kết với bảng Children
	DoctorID INT NOT NULL,											 -- Liên kết với bảng Doctor
    RequestDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Ngày tạo yêu cầu tư vấn
    Description NVARCHAR(MAX),                                      -- Mô tả chi tiết về yêu cầu tư vấn
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                  -- Trạng thái yêu cầu: 0 (pending), 1 (approved), 2 (rejected)
    Urgency NVARCHAR(50),                                            -- Mức độ khẩn cấp: low, medium, high
    Attachments VARCHAR(MAX),                                       -- Đường dẫn tệp đính kèm
    Category NVARCHAR(100),                                          -- Danh mục tư vấn: ví dụ: dinh dưỡng, phát triển, ...
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Thời gian yêu cầu được tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Thời gian cập nhật yêu cầu
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID),             -- Liên kết với bảng Members
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID),              -- Liên kết với bảng Children
	FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)              -- Liên kết với bảng Doctor
);

-- Table ConsultationResponses
CREATE TABLE ConsultationResponses (
    ResponseID INT PRIMARY KEY IDENTITY(1,1),                           -- ID phản hồi duy nhất
    RequestID INT UNIQUE NOT NULL,                                      -- Liên kết với bảng ConsultationRequests
    ResponseDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Thời gian phản hồi
    Content NVARCHAR(MAX) NOT NULL,                                    -- Nội dung phản hồi (bắt buộc)
    Attachments VARCHAR(MAX),                                          -- Đường dẫn tệp đính kèm (không bắt buộc)
    IsHelpful BIT NULL,                                                 -- Đánh giá xem phản hồi có hữu ích không (NULL nếu không đánh giá)
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                     -- Trạng thái phản hồi: 0 (pending), 1 (answered), 2 (resolved)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,              -- Thời gian tạo phản hồi
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,              -- Thời gian cập nhật phản hồi
    FOREIGN KEY (RequestID) REFERENCES ConsultationRequests(RequestID), -- Liên kết với bảng ConsultationRequests
);

-- Table RatingFeedbacks
CREATE TABLE RatingFeedbacks (
    FeedbackId INT PRIMARY KEY IDENTITY(1,1),                             -- ID phản hồi đánh giá
    UserId UNIQUEIDENTIFIER NOT NULL,                                     -- Liên kết với bảng UserAccounts
    ResponseId INT NOT NULL,                                              -- Liên kết với bảng ConsultationResponses
    Rating INT NOT NULL CHECK (Rating BETWEEN 1 AND 5),                   -- Đánh giá từ 1 đến 5
    Comment NVARCHAR(2000) NULL,                                          -- Nhận xét (không bắt buộc)
    FeedbackDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,             -- Thời gian phản hồi
    FeedbackType NVARCHAR(50) NULL DEFAULT 'general',                     -- Loại phản hồi (không bắt buộc, mặc định là 'general')
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                       -- Trạng thái: 'Pending', 'Approved', 'Rejected'
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,                -- Thời gian tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,                -- Thời gian cập nhật
    FOREIGN KEY (UserId) REFERENCES UserAccounts(UserId),                 -- Liên kết với bảng UserAccounts
    FOREIGN KEY (ResponseId) REFERENCES ConsultationResponses(ResponseId) -- Liên kết với bảng ConsultationResponses
);

-- Table Diseases
CREATE TABLE Diseases (
    DiseaseID INT PRIMARY KEY IDENTITY(1,1),                 -- ID bệnh
    DiseaseName NVARCHAR(100) NOT NULL,                      -- Tên bệnh
    LowerBoundMale FLOAT NOT NULL,                           -- Giới hạn thấp cho chỉ số đối với nam (cân nặng, chiều cao, BMI)
    UpperBoundMale FLOAT NOT NULL,                           -- Giới hạn cao cho chỉ số đối với nam
    LowerBoundFemale FLOAT NOT NULL,                         -- Giới hạn thấp cho chỉ số đối với nữ
    UpperBoundFemale FLOAT NOT NULL,                         -- Giới hạn cao cho chỉ số đối với nữ
    MinAge INT NOT NULL,                                     -- Độ tuổi nhỏ nhất có thể mắc bệnh
    MaxAge INT NOT NULL,                                     -- Độ tuổi lớn nhất có thể mắc bệnh
    Severity NVARCHAR(50) NOT NULL,                          -- Độ nghiêm trọng (High, Medium, Low)
    DiseaseType NVARCHAR(50) NOT NULL,                       -- Loại bệnh (ví dụ: Béo phì, Suy dinh dưỡng)
    Symptoms NVARCHAR(2000) NOT NULL,                        -- Triệu chứng bệnh
    Treatment NVARCHAR(2000),                                -- Phương pháp điều trị
    Prevention NVARCHAR(2000),                               -- Phương pháp phòng ngừa
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian tạo
    LastModified DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,-- Thời gian chỉnh sửa gần nhất
    Description NVARCHAR(2000),                              -- Mô tả về bệnh
    Notes NVARCHAR(2000),                                    -- Ghi chú thêm
    IsActive BIT NOT NULL DEFAULT 1                          -- Trạng thái (Hoạt động hoặc không)
);

-- Table Alerts
CREATE TABLE Alerts (
    AlertID INT PRIMARY KEY IDENTITY(1,1),
    GrowthRecordID INT NOT NULL,                                     -- Liên kết với bảng GrowthRecords
    AlertDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Ngày giờ cảnh báo
    DiseaseID INT NOT NULL,                                          -- Liên kết với bảng Diseases
    Message NVARCHAR(2000) NOT NULL,                                 -- Nội dung cảnh báo
    IsRead BIT NOT NULL DEFAULT 0,                                   -- Trạng thái đọc
    SeverityLevel NVARCHAR(50),                                      -- Mức độ nghiêm trọng
    IsAcknowledged BIT NOT NULL DEFAULT 0,                           -- Trạng thái xác nhận
    FOREIGN KEY (GrowthRecordID) REFERENCES GrowthRecords(RecordID), -- Liên kết GrowthRecords
    FOREIGN KEY (DiseaseID) REFERENCES Diseases(DiseaseID)           -- Liên kết với Diseases
);

-- Table Milestones
CREATE TABLE Milestones (
    MilestoneID INT PRIMARY KEY IDENTITY(1,1),
    MilestoneName NVARCHAR(255) NOT NULL,                 -- Tên mốc phát triển
    Description NVARCHAR(2000),                           -- Mô tả mốc phát triển
    Importance NVARCHAR(50) NOT NULL DEFAULT 'Medium',    -- Độ quan trọng
    Category NVARCHAR(100),                               -- Nhóm mốc
    MinAge INT NULL,                                      -- Tuổi bắt đầu áp dụng (tháng)
    MaxAge INT NULL,                                      -- Tuổi kết thúc áp dụng (tháng)
    IsPersonal BIT NOT NULL DEFAULT 0,                    -- Phân biệt mốc hệ thống và cá nhân
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,-- Ngày tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP -- Ngày cập nhật
);

-- Table ChildMilestones
CREATE TABLE ChildMilestones (
    ChildID UNIQUEIDENTIFIER NOT NULL,                           -- Liên kết với bảng Children
    MilestoneID INT NOT NULL,                                    -- Liên kết với bảng Milestones
    AchievedDate DATE,                                           -- Ngày đạt mốc
    Status NVARCHAR(50) NOT NULL DEFAULT 'Not Achieved',         -- Trạng thái mốc (Not Achieved, Achieved)
    Notes NVARCHAR(2000),                                        -- Ghi chú
    Guidelines NVARCHAR(2000),                                   -- Hướng dẫn
    Importance NVARCHAR(50) NOT NULL DEFAULT 'Medium',           -- Độ quan trọng
    Category NVARCHAR(100),                                      -- Nhóm mốc
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,       -- Ngày tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,       -- Ngày cập nhật
    PRIMARY KEY (ChildID, MilestoneID),                          -- Khoá chính kết hợp
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID),          -- Liên kết đến bảng Children
    FOREIGN KEY (MilestoneID) REFERENCES Milestones(MilestoneID) -- Liên kết đến bảng Milestones
);

-- Table BlogCategories
CREATE TABLE BlogCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),                             -- ID thể loại
    CategoryName NVARCHAR(255) NOT NULL,                                  -- Tên thể loại bài viết (không được NULL)
    Description NVARCHAR(2000),                                           -- Mô tả về thể loại
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,                -- Thời gian tạo thể loại
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,                -- Thời gian cập nhật thể loại
    IsActive BIT NOT NULL DEFAULT 1,                                      -- Trạng thái thể loại (1: Active, 0: Inactive)
    ParentCategoryID INT,                                                 -- ID thể loại cha (nếu có thể loại con)
    FOREIGN KEY (ParentCategoryID) REFERENCES BlogCategories(CategoryID)  -- Liên kết với thể loại cha
);

-- Table Blogs
CREATE TABLE Blogs (
    BlogID INT PRIMARY KEY IDENTITY(1,1),                          -- ID bài viết
    Title NVARCHAR(255) NOT NULL,                                  -- Tiêu đề bài viết
    Content NVARCHAR(MAX) NOT NULL,                               -- Nội dung bài viết
    AuthorID UNIQUEIDENTIFIER NOT NULL,                            -- ID tác giả (admin hoặc người dùng)
	CategoryID INT NOT NULL,                                       -- ID thể loại bài viết
	ImageBlog NVARCHAR(2000) NOT NULL,                             -- URL hoặc đường dẫn tới ảnh
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                -- Trạng thái bài viết ('Pending', 'Approved', 'Rejected')
	RejectionReason NVARCHAR(2000),                                -- Lý do từ chối (nếu có)
    Tags NVARCHAR(255),                                            -- Các tag của bài viết
    ReferenceSources NVARCHAR(2000),                               -- Các trích dẫn hoặc nguồn tài liệu tham khảo
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Thời gian tạo bài viết
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Thời gian cập nhật bài viết
    FOREIGN KEY (AuthorID) REFERENCES UserAccounts(UserID),        -- Liên kết với bảng UserAccounts (tác giả)
    FOREIGN KEY (CategoryID) REFERENCES BlogCategories(CategoryID) -- Liên kết với bảng BlogCategories
);

-- Insert Database
-- Insert Roles
INSERT INTO Roles (RoleName, Description, Status) 
VALUES 
('Member', 'Role for regular members who track child growth', 'Active'),
('Doctor', 'Role for doctors to provide consultation and advice', 'Active'),
('Admin', 'Role for administrators who manage the system', 'Active');

GO

-- Insert UserAccounts
-- Admin
INSERT INTO UserAccounts (Username, Email, PhoneNumber, Name, Gender, DateOfBirth, Address, Password, RegistrationDate, RoleID)
VALUES 
('PhucLHSE', 'phuclhse@gmail.com', '0325194357', N'Lê Hoàng Phúc', 'Male', '2003-12-14', N'Bình Định, Việt Nam', 'Admin@123456', GETDATE(), 3);

GO

-- Insert UserAccounts
-- Doctor
INSERT INTO UserAccounts (Username, Email, PhoneNumber, Name, Gender, DateOfBirth, Address, Password, RegistrationDate, RoleID)
VALUES 
('NguyenHNN', 'nguyenhnn@gmail.com', '0902653178', N'Huỳnh Nguyễn Ngọc Nguyên', 'Male', '1985-03-15', N'Bình Dương, Việt Nam', 'Doctor@Nguyen1985', GETDATE(), 2),
('TuanTM1980', 'tuantm@gmail.com', '0903151288', N'Trần Mạnh Tuấn', 'Male', '1980-08-22', N'Đà Nẵng, Việt Nam', 'Doctor@Tuan1980', GETDATE(), 2),
('TuHT1990', 'tuht@gmail.com', '0842139577', N'Huỳnh Thái Tú', 'Male', '1990-05-10', N'TP.HCM, Việt Nam', 'Doctor@Tu1990', GETDATE(), 2),
('ThaoNTT1987', 'thaontt@gmail.com', '0914751925', N'Nguyễn Thị Thanh Thảo', 'Female', '1987-07-30', N'Hải Phòng, Việt Nam', 'Doctor@Thao1987', GETDATE(), 2);

GO

-- Insert UserAccounts
-- Member
INSERT INTO UserAccounts (Username, Email, PhoneNumber, Name, Gender, DateOfBirth, Address, Password, RegistrationDate, RoleID)
VALUES 
('NguyenVA1992', 'nguyenvana1992@gmail.com', '0903000001', N'Nguyễn Văn An', 'Male', '1992-01-10', N'Hà Nội, Việt Nam', 'Member@Nguyen1992', GETDATE(), 1),
('TranTB1995', 'tranthib1995@gmail.com', '0903000002', N'Trần Thị Bông', 'Female', '1995-04-25', N'TP.HCM, Việt Nam', 'Member@Tran1995', GETDATE(), 1),
('LeVC1990', 'levanc1990@gmail.com', '0903000003', N'Lê Văn Cường', 'Male', '1990-09-15', N'Đà Nẵng, Việt Nam', 'Member@Le1990', GETDATE(), 1),
('PhamTD1993', 'phamthid1993@gmail.com', '0903000004', N'Phạm Thị Dung', 'Female', '1993-11-05', N'Hải Phòng, Việt Nam', 'Member@Pham1993', GETDATE(), 1),
('HoangVT1998', 'hoangvt1998@gmail.com', '0903000005', N'Hoàng Văn Tài', 'Male', '1998-06-20', N'Cần Thơ, Việt Nam', 'Member@Hoang1998', GETDATE(), 1),
('DangTT1994', 'dangtt1994@gmail.com', '0903000006', N'Đặng Thị Thùy', 'Female', '1994-03-30', N'Huế, Việt Nam', 'Member@Dang1994', GETDATE(), 1),
('VoVD1996', 'vovd1996@gmail.com', '0903000007', N'Võ Văn Đông', 'Male', '1996-07-18', N'Bình Dương, Việt Nam', 'Member@Vo1996', GETDATE(), 1),
('BuiTH1997', 'buith1997@gmail.com', '0903000008', N'Bùi Thị Hương', 'Female', '1997-12-25', N'Đồng Nai, Việt Nam', 'Member@Bui1997', GETDATE(), 1),
('NgoVS1991', 'ngovs1991@gmail.com', '0903000009', N'Ngô Văn Sơn', 'Male', '1991-05-05', N'Vũng Tàu, Việt Nam', 'Member@Ngovi1991', GETDATE(), 1),
('DinhTN1999', 'dinhtn1999@gmail.com', '0903010010', N'Đinh Thị Nhi', 'Female', '1999-08-12', N'Quảng Ngãi, Việt Nam', 'Member@Dinh1999', GETDATE(), 1),
('PhanVK1993', 'phanvk1993@gmail.com', '0903010011', N'Phan Văn Kha', 'Male', '1993-02-22', N'Phú Quốc, Việt Nam', 'Member@Phan1993', GETDATE(), 1),
('NguyenTL1990', 'nguyentl1990@gmail.com', '0903010012', N'Nguyễn Thị Lý', 'Female', '1990-10-30', N'Nha Trang, Việt Nam', 'Member@Nguyen1990', GETDATE(), 1),
('DoVM1994', 'dovm1994@gmail.com', '0903010013', N'Đỗ Văn Mười', 'Male', '1994-06-08', N'Hải Dương, Việt Nam', 'Member@Do1994', GETDATE(), 1),
('LaTN1992', 'latn1992@gmail.com', '0903010014', N'La Thị Nga', 'Female', '1992-09-21', N'Bắc Ninh, Việt Nam', 'Member@La1992', GETDATE(), 1),
('MacVO1995', 'macvo1995@gmail.com', '0903010015', N'Mạc Văn Ổn', 'Male', '1995-01-17', N'Thanh Hóa, Việt Nam', 'Member@Mac1995', GETDATE(), 1),
('LyTP1997', 'lyt1997@gmail.com', '0903010016', N'Lý Thị Phương', 'Female', '1997-04-05', N'Nghệ An, Việt Nam', 'Member@Ly1997', GETDATE(), 1),
('VuVQ1991', 'vuvq1991@gmail.com', '0903010017', N'Vũ Văn Quốc', 'Male', '1991-07-28', N'Hà Tĩnh, Việt Nam', 'Member@Vu1991', GETDATE(), 1),
('CaoTM1996', 'caotm1996@gmail.com', '0903010018', N'Cao Thị My', 'Female', '1996-11-10', N'Quảng Bình, Việt Nam', 'Member@Cao1996', GETDATE(), 1),
('TanVS1998', 'tanvs1998@gmail.com', '0903010019', N'Tân Văn Sang', 'Male', '1998-02-14', N'Lào Cai, Việt Nam', 'Member@Tan1998', GETDATE(), 1),
('TrinhTT1993', 'trinhtt1993@gmail.com', '0903010020', N'Trịnh Thị Thảo', 'Female', '1993-06-01', N'Yên Bái, Việt Nam', 'Member@Trinh1993', GETDATE(), 1);

GO

-- Insert Membership Packages
INSERT INTO MembershipPackages (PackageName, Description, Price, Currency, DurationMonths, MaxChildrenAllowed, Status)
VALUES
('Free', 'Free membership with basic features', 0.00, 'VND', 1200, 1, 'Active'),
('Standard', 'Standard membership with additional features', 379000.00, 'VND', 3, 2, 'Active'),
('Premium', 'Premium membership with full features', 1279000.00, 'VND', 12, 4, 'Active');

GO

-- Insert Features
INSERT INTO Features (FeatureName, Description, Status)
VALUES
('Update Growth Records', 'Update the growth records of children', 'Active'), -- F1
('Manage/Track Multiple Children', 'Manage and track multiple children', 'Active'), -- F2
('View Basic Growth Charts', 'View basic growth charts (weight, height, BMI)', 'Active'), -- F3
('View Advanced Growth Charts', 'View advanced growth charts', 'Active'), -- F3 Standard-Premium
('Nutrition and Development Alerts', 'Alerts for malnutrition, overweight, and development', 'Active'), -- F4
('Request Doctor Consultation', 'Send a request for doctor consultation', 'Active'), -- F5
('Share Health Data with Doctor', 'Share health data with doctors', 'Active'), -- F6
('Rate and Feedback Consultants', 'Rate and provide feedback to consultants', 'Active'), -- F7
('View Blogs', 'View articles and blogs', 'Active'), -- F8
('Create and Track Milestones', 'Create and track children developmental milestones', 'Active'); -- F9

GO

-- Insert PackageFeatures
INSERT INTO PackageFeatures (PackageID, FeatureID, Status)
VALUES
-- FREE Package
(1, 1, 'Active'), -- F1: Update Growth Records
(1, 3, 'Active'), -- F3: View Basic Growth Charts
(1, 5, 'Active'), -- F4: Nutrition and Development Alerts
(1, 8, 'Active'), -- F8: View Blogs

-- STANDARD Package
(2, 1, 'Active'), -- F1: Update Growth Records
(2, 2, 'Active'), -- F2: Manage/Track Multiple Children
(2, 3, 'Active'), -- F3: View Basic Growth Charts
(2, 4, 'Active'), -- F3 Advanced Charts
(2, 5, 'Active'), -- F4: Nutrition and Development Alerts
(2, 6, 'Active'), -- F6: Share Health Data with Doctor
(2, 7, 'Active'), -- F7: Rate and Feedback Consultants
(2, 8, 'Active'), -- F8: View Blogs
(2, 9, 'Active'), -- F9: Create and Track Milestones

-- PREMIUM Package
(3, 1, 'Active'), -- F1: Update Growth Records
(3, 2, 'Active'), -- F2: Manage/Track Multiple Children
(3, 3, 'Active'), -- F3: View Basic Growth Charts
(3, 4, 'Active'), -- F3 Advanced Charts
(3, 5, 'Active'), -- F4: Nutrition and Development Alerts
(3, 6, 'Active'), -- F6: Share Health Data with Doctor
(3, 7, 'Active'), -- F7: Rate and Feedback Consultants
(3, 8, 'Active'), -- F8: View Blogs
(3, 9, 'Active'); -- F9: Create and Track Milestones

GO
-- Insert Specializations (Chuyên khoa)
INSERT INTO Specializations (SpecializationName, Description, Status)
VALUES
('Pediatrics', N'Chuyên khoa Nhi', 'Active'),
('Endocrinology', N'Chuyên khoa Nội tiết', 'Active'),
('Nutrition', N'Dinh dưỡng và chế độ ăn', 'Active'),
('Cardiology', N'Chuyên khoa Tim mạch', 'Active'),
('Neurology', N'Chuyên khoa Thần kinh', 'Active');

GO

-- Insert Doctors (Bác sĩ từ UserAccounts)
INSERT INTO Doctors (UserID, Name, Email, PhoneNumber, Degree, HospitalName, HospitalAddress, Biography, Status)
VALUES 
((SELECT UserID FROM UserAccounts WHERE Username = 'NguyenHNN'), N'Huỳnh Nguyễn Ngọc Nguyên', 'nguyenhnn@gmail.com', '0902653178', N'Bác sĩ chuyên khoa Nhi', N'Bệnh viện Nhi Đồng 1', N'TP.HCM, Việt Nam', N'Chuyên gia về nhi khoa, có nhiều năm kinh nghiệm trong nghiên cứu phát triển trẻ em.', 'Active'),
((SELECT UserID FROM UserAccounts WHERE Username = 'TuanTM1980'), N'Trần Mạnh Tuấn', 'tuantm@gmail.com', '0903151288', N'Bác sĩ chuyên khoa Nội tiết', N'Bệnh viện Bạch Mai', N'Hà Nội, Việt Nam', N'Bác sĩ chuyên khoa Nội tiết, có kinh nghiệm điều trị các bệnh về hormone tăng trưởng.', 'Active'),
((SELECT UserID FROM UserAccounts WHERE Username = 'TuHT1990'), N'Huỳnh Thái Tú', 'tuht@gmail.com', '0842139577', N'Bác sĩ Dinh dưỡng', N'Viện Dinh dưỡng Quốc gia', N'Hà Nội, Việt Nam', N'Chuyên gia về dinh dưỡng và chế độ ăn uống, giúp trẻ phát triển toàn diện.', 'Active'),
((SELECT UserID FROM UserAccounts WHERE Username = 'ThaoNTT1987'), N'Nguyễn Thị Thanh Thảo', 'thaontt@gmail.com', '0914751925', N'Bác sĩ chuyên khoa Tim mạch', N'Bệnh viện Chợ Rẫy', N'TP.HCM, Việt Nam', N'Chuyên điều trị các bệnh về tim mạch ở trẻ em.', 'Active');

GO

-- Insert Doctor Specializations (Một bác sĩ có thể có nhiều chuyên ngành)
INSERT INTO DoctorSpecializations (DoctorID, SpecializationID, Status)
VALUES 
-- Huỳnh Nguyễn Ngọc Nguyên - Khoa Nhi, Nội tiết
((SELECT DoctorID FROM Doctors WHERE Name = N'Huỳnh Nguyễn Ngọc Nguyên'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Pediatrics'), 'Active'),
((SELECT DoctorID FROM Doctors WHERE Name = N'Huỳnh Nguyễn Ngọc Nguyên'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Endocrinology'), 'Active'),

-- Trần Mạnh Tuấn - Nội tiết, Thần kinh
((SELECT DoctorID FROM Doctors WHERE Name = N'Trần Mạnh Tuấn'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Endocrinology'), 'Active'),
((SELECT DoctorID FROM Doctors WHERE Name = N'Trần Mạnh Tuấn'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Neurology'), 'Active'),

-- Huỳnh Thái Tú - Dinh dưỡng, Khoa Nhi
((SELECT DoctorID FROM Doctors WHERE Name = N'Huỳnh Thái Tú'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Nutrition'), 'Active'),
((SELECT DoctorID FROM Doctors WHERE Name = N'Huỳnh Thái Tú'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Pediatrics'), 'Active'),

-- Nguyễn Thị Thanh Thảo - Tim mạch
((SELECT DoctorID FROM Doctors WHERE Name = N'Nguyễn Thị Thanh Thảo'), 
 (SELECT SpecializationID FROM Specializations WHERE SpecializationName = 'Cardiology'), 'Active');

GO

-- Insert các chương trình khuyến mãi
INSERT INTO Promotions (PromotionCode, Description, DiscountPercent, MinPurchaseAmount, MaxDiscountAmount, StartDate, EndDate, Status, CreatedBy, ModifiedBy)
VALUES
('WELCOME10', N'Giảm 10% cho thành viên mới khi đăng ký lần đầu', 10, 0, 50000, '2025-01-01', '2025-12-31', 'Active', 
 (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE'),
 (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE')),

('FAMILY20', N'Giảm 20% khi đăng ký gói thành viên Premium', 20, 1000000, 200000, '2025-03-01', '2025-06-30', 'Active', 
 (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE'),
 (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE')),

('SPRINGSALE15', N'Giảm 15% khi đăng ký gói Standard hoặc Premium', 15, 500000, 150000, '2025-04-01', '2025-05-31', 'Active', 
 (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE'),
 (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE'));

 GO

-- Lấy PromotionID của từng mã khuyến mãi
DECLARE @PromoWelcome10 UNIQUEIDENTIFIER = (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'WELCOME10');
DECLARE @PromoFamily20 UNIQUEIDENTIFIER = (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'FAMILY20');
DECLARE @PromoSpringSale15 UNIQUEIDENTIFIER = (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'SPRINGSALE15');

-- Insert PackagePromotions
INSERT INTO PackagePromotions (PackageID, PromotionID, IsActive, CreatedAt, UpdatedAt)
VALUES
-- Áp dụng WELCOME10 cho tất cả gói
(1, @PromoWelcome10, 1, GETDATE(), GETDATE()),
(2, @PromoWelcome10, 1, GETDATE(), GETDATE()),
(3, @PromoWelcome10, 1, GETDATE(), GETDATE()),

-- Áp dụng FAMILY20 cho gói Premium
(3, @PromoFamily20, 1, GETDATE(), GETDATE()),

-- Áp dụng SPRINGSALE15 cho Standard và Premium
(2, @PromoSpringSale15, 1, GETDATE(), GETDATE()),
(3, @PromoSpringSale15, 1, GETDATE(), GETDATE());

-- Insert danh sách thành viên Free
INSERT INTO Members (UserID, EmergencyContact, Status, JoinDate, Notes)
VALUES
((SELECT UserID FROM UserAccounts WHERE Username = 'NguyenVA1992'), N'Nguyễn Thị Hoa - 0903123456', 'Active', GETDATE(), N'Theo dõi con trai đầu lòng'),
((SELECT UserID FROM UserAccounts WHERE Username = 'TranTB1995'), N'Trần Văn Hùng - 0912456789', 'Active', GETDATE(), N'Theo dõi con gái mới sinh'),
((SELECT UserID FROM UserAccounts WHERE Username = 'LeVC1990'), N'Lê Thị Lan - 0938567891', 'Active', GETDATE(), N'Muốn có lịch tiêm chủng cho con'),
((SELECT UserID FROM UserAccounts WHERE Username = 'PhamTD1993'), N'Phạm Văn Hiếu - 0987456123', 'Active', GETDATE(), N'Theo dõi biểu đồ phát triển của con gái'),
((SELECT UserID FROM UserAccounts WHERE Username = 'HoangVT1998'), N'Hoàng Minh - 0978564123', 'Active', GETDATE(), N'Quan tâm đến dinh dưỡng của trẻ'),
((SELECT UserID FROM UserAccounts WHERE Username = 'DangTT1994'), N'Đặng Văn Thành - 0903123456', 'Active', GETDATE(), N'Muốn theo dõi sức khỏe tổng thể'),
((SELECT UserID FROM UserAccounts WHERE Username = 'VoVD1996'), N'Võ Thị Mai - 0912567890', 'Active', GETDATE(), N'Con mình bị nhẹ cân, cần theo dõi'),
((SELECT UserID FROM UserAccounts WHERE Username = 'BuiTH1997'), N'Bùi Văn Tiến - 0987543211', 'Active', GETDATE(), N'Muốn biết thông tin về dinh dưỡng cho bé'),
((SELECT UserID FROM UserAccounts WHERE Username = 'NgoVS1991'), N'Ngô Thị Bình - 0923123411', 'Active', GETDATE(), N'Đăng ký theo dõi con nhỏ'),
((SELECT UserID FROM UserAccounts WHERE Username = 'DinhTN1999'), N'Đinh Văn Hiếu - 0943212311', 'Active', GETDATE(), N'Con bị dị ứng thực phẩm, cần tư vấn'),
((SELECT UserID FROM UserAccounts WHERE Username = 'PhanVK1993'), N'Phan Thị Hoa - 0967891234', 'Active', GETDATE(), N'Quan tâm đến phát triển chiều cao cho bé'),
((SELECT UserID FROM UserAccounts WHERE Username = 'NguyenTL1990'), N'Nguyễn Văn Khoa - 0978564123', 'Active', GETDATE(), N'Muốn theo dõi cân nặng của con'),
((SELECT UserID FROM UserAccounts WHERE Username = 'DoVM1994'), N'Đỗ Thị Lan - 0956321789', 'Active', GETDATE(), N'Muốn biết cách tăng đề kháng cho con'),
((SELECT UserID FROM UserAccounts WHERE Username = 'LaTN1992'), N'La Văn Thành - 0945632871', 'Active', GETDATE(), N'Đăng ký theo dõi phát triển toàn diện'),
((SELECT UserID FROM UserAccounts WHERE Username = 'MacVO1995'), N'Mạc Thị Hương - 0921876543', 'Active', GETDATE(), N'Cần theo dõi tiêm chủng cho con');

GO

-- Insert Members có đăng ký gói Standard or Prenium
INSERT INTO Members (UserID, EmergencyContact, Status, JoinDate, Notes)
VALUES
((SELECT UserID FROM UserAccounts WHERE Username = 'LyTP1997'), N'Nguyễn Văn Minh - 0987654321', 'Active', GETDATE(), N'Quan tâm đến dinh dưỡng của bé'),
((SELECT UserID FROM UserAccounts WHERE Username = 'VuVQ1991'), N'Hoàng Thị Mai - 0976543210', 'Active', GETDATE(), N'Muốn theo dõi sức khỏe của con'),
((SELECT UserID FROM UserAccounts WHERE Username = 'CaoTM1996'), N'Phạm Văn Bình - 0965432109', 'Active', GETDATE(), N'Quan tâm đến phát triển thể chất'),
((SELECT UserID FROM UserAccounts WHERE Username = 'TanVS1998'), N'Bùi Thị Hoa - 0954321098', 'Active', GETDATE(), N'Theo dõi lịch tiêm chủng'),
((SELECT UserID FROM UserAccounts WHERE Username = 'TrinhTT1993'), N'Lê Văn Hùng - 0943210987', 'Active', GETDATE(), N'Cần tư vấn về dinh dưỡng');

GO

-- 15 thanh vien Free (Khong nang cap)
INSERT INTO MemberMemberships (MemberID, PackageID, StartDate, EndDate, Status, IsActive, Description)
SELECT MemberID, 1, GETDATE(), DATEADD(MONTH, 1200, GETDATE()), 'Active', 1, N'Gói Free'
FROM Members 
WHERE UserID IN (
    (SELECT UserID FROM UserAccounts WHERE Username = 'NguyenVA1992'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'TranTB1995'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'LeVC1990'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'PhamTD1993'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'HoangVT1998'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'DangTT1994'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'VoVD1996'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'BuiTH1997'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'NgoVS1991'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'DinhTN1999'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'PhanVK1993'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'NguyenTL1990'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'DoVM1994'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'LaTN1992'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'MacVO1995')
);

GO

-- 5 thành viên có gói Free nhưng đã nâng cấp (trạng thái Inactive)
INSERT INTO MemberMemberships (MemberID, PackageID, StartDate, EndDate, Status, IsActive, Description)
SELECT MemberID, 1, GETDATE(), DATEADD(MONTH, 1200, GETDATE()), 'Inactive', 0, N'Gói Free - Không còn sử dụng'
FROM Members 
WHERE UserID IN (
    (SELECT UserID FROM UserAccounts WHERE Username = 'LyTP1997'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'VuVQ1991'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'CaoTM1996'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'TanVS1998'),
    (SELECT UserID FROM UserAccounts WHERE Username = 'TrinhTT1993')
);

GO

-- 5 thanh vien nang cap len Standard hoac Premium
INSERT INTO MemberMemberships (MemberID, PackageID, StartDate, EndDate, Status, IsActive, Description)
SELECT 
    m.MemberID, 
    CASE 
        WHEN u.Username IN (N'LyTP1997', N'VuVQ1991') THEN 2  -- Standard
        WHEN u.Username IN (N'CaoTM1996', N'TanVS1998', N'TrinhTT1993') THEN 3  -- Premium
    END AS PackageID,
    GETDATE(), 
    CASE 
        WHEN u.Username IN (N'LyTP1997', N'VuVQ1991') THEN DATEADD(MONTH, 3, GETDATE())  
        WHEN u.Username IN (N'CaoTM1996', N'TanVS1998', N'TrinhTT1993') THEN DATEADD(MONTH, 12, GETDATE())  
    END AS EndDate,
    'Active', 1, 
    CASE 
        WHEN u.Username IN (N'LyTP1997', N'VuVQ1991') THEN N'Nâng cấp lên Standard'  
        WHEN u.Username IN (N'CaoTM1996', N'TanVS1998', N'TrinhTT1993') THEN N'Nâng cấp lên Premium'  
    END AS Description
FROM UserAccounts u
JOIN Members m ON u.UserID = m.UserID
WHERE u.Username IN (N'LyTP1997', N'VuVQ1991', N'CaoTM1996', N'TanVS1998', N'TrinhTT1993');

GO

-- Insert Transactions Fail
INSERT INTO Transactions (UserID, MemberMembershipID, Amount, Currency, TransactionType, PaymentMethod, TransactionDate, PaymentDate, GatewayTransactionID, PaymentStatus, Description)
SELECT 
    u.UserID, 
    mm.MemberMembershipID, 
    379000.00,  -- Gói Standard
    'VND', 'Subscription', 'VnPay',
    GETDATE(),  -- Thời điểm giao dịch
    NULL,  -- Giao dịch thất bại không có PaymentDate
    300002,  -- Mã giao dịch thất bại
    'Failed',  
    N'Giao dịch thất bại'
FROM UserAccounts u
JOIN Members m ON u.UserID = m.UserID
JOIN MemberMemberships mm ON m.MemberID = mm.MemberID
WHERE u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 1; -- Chỉ lấy gói bị lỗi

GO

-- Insert Transactions Success
INSERT INTO Transactions (UserID, MemberMembershipID, Amount, Currency, TransactionType, PaymentMethod, TransactionDate, PaymentDate, GatewayTransactionID, PaymentStatus, Description)
SELECT DISTINCT 
    u.UserID, 
    mm.MemberMembershipID, 
    CASE 
        WHEN u.Username IN (N'LyTP1997', N'VuVQ1991') THEN 379000.00  
        WHEN u.Username IN (N'CaoTM1996', N'TanVS1998', N'TrinhTT1993') THEN 1279000.00  
    END AS Amount,
    'VND', 'Subscription', 'VnPay',
    GETDATE(),
    CASE 
        WHEN u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 1 THEN NULL  
        ELSE GETDATE()  
    END AS PaymentDate,
    CASE 
        WHEN u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 1 THEN 300002  
        WHEN u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 0 THEN 300003  
        WHEN u.Username = N'LyTP1997' THEN 300001  
        WHEN u.Username = N'CaoTM1996' THEN 300004  
        WHEN u.Username = N'TanVS1998' THEN 300005  
        WHEN u.Username = N'TrinhTT1993' THEN 300006  
    END AS GatewayTransactionID,
    CASE 
        WHEN u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 1 THEN 'Failed'  
        ELSE 'Success'  
    END AS PaymentStatus,
    CASE 
        WHEN u.Username = N'LyTP1997' THEN N'Thanh toán gói Standard 3 tháng'  
        WHEN u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 1 THEN N'Giao dịch thất bại'  
        WHEN u.Username = N'VuVQ1991' AND mm.PackageID = 2 AND mm.IsActive = 0 THEN N'Thanh toán lại sau thất bại'  
        WHEN u.Username = N'CaoTM1996' THEN N'Thanh toán gói Premium 12 tháng'  
        WHEN u.Username = N'TanVS1998' THEN N'Thanh toán gói Premium 12 tháng'  
        WHEN u.Username = N'TrinhTT1993' THEN N'Thanh toán gói Premium 12 tháng'  
    END AS Description
FROM UserAccounts u
JOIN Members m ON u.UserID = m.UserID
JOIN MemberMemberships mm ON m.MemberID = mm.MemberID
WHERE 
    u.Username IN (N'LyTP1997', N'VuVQ1991', N'CaoTM1996', N'TanVS1998', N'TrinhTT1993')
    AND (
        mm.IsActive = 1  -- Chỉ lấy gói đang hoạt động
        OR (u.Username = N'VuVQ1991' AND mm.PackageID = 2) -- Chắc chắn lấy giao dịch thất bại
    );

 GO

 -- Insert Blog Categories (Danh mục cha)
INSERT INTO BlogCategories(CategoryName, Description, IsActive)
VALUES
    ('Getting Pregnant', N'Thông tin và tài liệu dành cho người chuẩn bị mang thai.', 1),
    ('Baby', N'Thông tin dành cho bố mẹ có con sơ sinh và trẻ nhỏ.', 1),
    ('Toddler', N'Nội dung liên quan đến trẻ mới biết đi và sự phát triển ban đầu.', 1),
    ('Child', N'Nội dung cho cha mẹ có con trong độ tuổi mầm non và tiểu học.', 1),
    ('Teenager', N'Thông tin và lời khuyên dành cho cha mẹ có con tuổi teen.', 1);

-- Insert Blog Subcategories (Danh mục con) dựa vào danh mục cha
DECLARE @GettingPregnant INT = (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Getting Pregnant');
DECLARE @Baby INT = (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Baby');
DECLARE @Toddler INT = (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Toddler');
DECLARE @Child INT = (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Child');
DECLARE @Teenager INT = (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Teenager');

INSERT INTO BlogCategories (CategoryName, Description, ParentCategoryID, IsActive)
VALUES
    -- Getting Pregnant (6 subcategories)
    ('Ovulation', N'Kiến thức về rụng trứng', @GettingPregnant, 1),
    ('Fertility', N'Thông tin về khả năng sinh sản', @GettingPregnant, 1),
    ('Pregnancy Tests', N'Các phương pháp thử thai', @GettingPregnant, 1),
    ('Nutrition Before Pregnancy', N'Dinh dưỡng trước khi mang thai', @GettingPregnant, 1),
    ('Exercise & Fitness', N'Tập thể dục và sức khỏe sinh sản', @GettingPregnant, 1),
    ('Common Pregnancy Myths', N'Những quan niệm sai lầm về mang thai', @GettingPregnant, 1),

    -- Baby (7 subcategories)
    ('Breastfeeding', N'Hướng dẫn và lợi ích của việc cho con bú', @Baby, 1),
    ('Sleep Tips', N'Mẹo giúp bé ngủ ngon', @Baby, 1),
    ('Newborn Care', N'Chăm sóc trẻ sơ sinh', @Baby, 1),
    ('Baby Milestones', N'Sự phát triển quan trọng của bé', @Baby, 1),
    ('Vaccination', N'Lịch tiêm phòng cho bé', @Baby, 1),
    ('Babyproofing', N'Làm thế nào để giữ an toàn cho bé', @Baby, 1),
    ('Teething', N'Mọc răng và cách giảm đau cho bé', @Baby, 1),

    -- Toddler (6 subcategories)
    ('Potty Training', N'Cách hướng dẫn trẻ đi vệ sinh', @Toddler, 1),
    ('Nutrition', N'Dinh dưỡng cần thiết cho trẻ', @Toddler, 1),
    ('Preschool', N'Chuẩn bị cho trẻ đi mẫu giáo', @Toddler, 1),
    ('Speech Development', N'Phát triển ngôn ngữ ở trẻ', @Toddler, 1),
    ('Discipline', N'Rèn luyện thói quen cho trẻ', @Toddler, 1),
    ('Sleep Routine', N'Xây dựng thói quen ngủ cho trẻ', @Toddler, 1),

    -- Child (5 subcategories)
    ('Education', N'Phát triển kỹ năng học tập', @Child, 1),
    ('Health Tips', N'Mẹo giữ sức khỏe cho trẻ', @Child, 1),
    ('Outdoor Activities', N'Các hoạt động ngoài trời', @Child, 1),
    ('Screen Time', N'Kiểm soát thời gian sử dụng thiết bị điện tử', @Child, 1),
    ('Emotional Development', N'Phát triển cảm xúc ở trẻ nhỏ', @Child, 1),

    -- Teenager (6 subcategories)
    ('Teen Mental Health', N'Sức khỏe tinh thần cho tuổi teen', @Teenager, 1),
    ('Social Media', N'Ảnh hưởng của mạng xã hội với tuổi teen', @Teenager, 1),
    ('Teen Education', N'Giáo dục dành cho tuổi teen', @Teenager, 1),
    ('Peer Pressure', N'Áp lực từ bạn bè đồng trang lứa', @Teenager, 1),
    ('Career Guidance', N'Định hướng nghề nghiệp cho tuổi teen', @Teenager, 1),
    ('Teen Relationships', N'Các mối quan hệ của tuổi teen', @Teenager, 1);

GO

-- Lấy UserID của Admin Phúc
DECLARE @AdminID UNIQUEIDENTIFIER = (SELECT UserID FROM UserAccounts WHERE Username = 'PhucLHSE');

-- Lấy UserID của các bác sĩ
DECLARE @DoctorNguyenID UNIQUEIDENTIFIER = (SELECT UserID FROM UserAccounts WHERE Username = 'NguyenHNN'); -- Huỳnh Nguyễn Ngọc Nguyên (Nhi khoa)
DECLARE @DoctorTuanID UNIQUEIDENTIFIER = (SELECT UserID FROM UserAccounts WHERE Username = 'TuanTM1980'); -- Trần Mạnh Tuấn (Nội tiết)
DECLARE @DoctorTuID UNIQUEIDENTIFIER = (SELECT UserID FROM UserAccounts WHERE Username = 'TuHT1990'); -- Huỳnh Thái Tú (Dinh dưỡng)
DECLARE @DoctorThaoID UNIQUEIDENTIFIER = (SELECT UserID FROM UserAccounts WHERE Username = 'ThaoNTT1987'); -- Nguyễn Thị Thanh Thảo (Tim mạch)

-- Insert Blogs (Bài viết mẫu)
INSERT INTO Blogs (Title, Content, AuthorID, CategoryID, ImageBlog, Status, Tags, ReferenceSources, CreatedAt, UpdatedAt)
VALUES
    -- Bài viết của Admin
    (N'Ovulation signs and how to track them', 
    N'Ovulation plays an important role in conception. This article helps you identify signs such as cervical mucus, basal body temperature, and ovulation test kits.', 
    @AdminID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Ovulation'),
    'https://plus.unsplash.com/premium_photo-1676049342411-c118fe1570b2?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Approved', 
    N'ovulation, ovulation tracking, fertility',
    'https://www.mayoclinic.org, https://www.webmd.com',
    GETDATE(), GETDATE()),

    (N'Are home pregnancy tests accurate?', 
    N'A home pregnancy test is a quick method for detecting pregnancy. This article explains how it works, its accuracy, and the best time to take the test.', 
    @AdminID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Pregnancy Tests'),
    'https://images.unsplash.com/photo-1533483595632-c5f0e57a1936?q=80&w=2080&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Approved', 
    N'pregnancy test, pregnancy detection, home pregnancy test',
    'https://www.healthline.com, https://www.nhs.uk',
    GETDATE(), GETDATE()),

    -- Bài viết của Doctor Huỳnh Nguyễn Ngọc Nguyên (Pediatric Specialist)
    (N'Tips for helping your baby sleep well', 
    N'Sleep is crucial for the development of newborns. Learn methods that can help your baby sleep deeper and avoid waking up abruptly.', 
    @DoctorNguyenID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Sleep Tips'),
    'https://images.unsplash.com/photo-1470116945706-e6bf5d5a53ca?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Approved', 
    N'baby sleep, sleep tips, deep sleep',
    'https://www.sleepfoundation.org, https://www.aap.org',
    GETDATE(), GETDATE()),

    -- Bài viết của Doctor Huỳnh Thái Tú (Nutrition Specialist)
    (N'Nutrition importance for young children', 
    N'Young children need to be provided with sufficient nutrition for full development. This article lists essential food groups.', 
    @DoctorTuID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Nutrition'),
    'https://plus.unsplash.com/premium_photo-1676049342406-20d6e89dc79c?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Approved', 
    N'child nutrition, toddler meals, baby food',
    'https://www.nhs.uk, https://www.aap.org',
    GETDATE(), GETDATE()),

    -- Bài viết của Doctor Nguyễn Thị Thanh Thảo (Cardiology & Teen Mental Health Specialist)
    (N'What should parents do when their child enters adolescence?', 
    N'Adolescence is a period when children undergo physical and mental changes. Parents need to understand how to support and accompany their children during this time.', 
    @DoctorThaoID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Teen Mental Health'),
    'https://images.unsplash.com/photo-1515488042361-ee00e0ddd4e4?q=80&w=2075&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Approved', 
    N'teen mental health, parenting teenagers, mental support',
    'https://www.psychologytoday.com, https://www.nimh.nih.gov',
    GETDATE(), GETDATE());

GO

-- Insert Diseases
INSERT INTO Diseases (DiseaseName, LowerBoundMale, UpperBoundMale, LowerBoundFemale, UpperBoundFemale, 
                      MinAge, MaxAge, Severity, DiseaseType, Symptoms, Treatment, Prevention, 
                      Description, Notes, IsActive)
VALUES
     -- 1. Suy dinh dưỡng nặng (Severe Malnutrition)
    ('Severe Malnutrition', 10.0, 16.0, 9.0, 16.0, 1, 10, 'High', 'Nutritional Deficiency',
     N'Severe weight loss, muscle atrophy, slow development, fatigue',
     N'High-calorie diet, protein-rich foods, medical intervention',
     N'Ensure adequate nutrition, monitor growth',
     N'Severe malnutrition due to prolonged nutritional deficiency.',
     N'Medical intervention is required immediately to prevent dangerous complications.', 1),

    -- 2. Suy dinh dưỡng nhẹ (Mild Malnutrition)
    ('Mild Malnutrition', 16.0, 18.5, 16.0, 18.5, 1, 18, 'Medium', 'Nutritional Deficiency',
     N'Slight underweight, frequent illness, low energy',
     N'Balanced diet, nutritional supplements if needed',
     N'Encourage healthy eating, increase protein intake',
     N'Mild malnutrition, deficiency in some essential micronutrients.',
     N'Common in children with an unbalanced diet.', 1),

    -- 3. Anemia (Thiếu máu)
    ('Anemia', 10.0, 18.5, 9.5, 18.0, 3, 16, 'Medium', 'Blood Disorder',
     'Paleness, fatigue, shortness of breath, dizziness',
     'Iron supplements, iron-rich diet (red meat, leafy greens)',
     'Maintain a diet rich in iron and vitamin C, regular health check-ups',
     'A condition where there is a deficiency of red blood cells or hemoglobin in the blood.',
     'Common in children due to poor diet and rapid growth phases.', 1),

    -- 4. Diabetes Type 1 (Tiểu đường tuýp 1)
    ('Diabetes Type 1', 15.0, 25.0, 14.0, 24.0, 6, 18, 'High', 'Endocrine Disorder',
     'Frequent urination, excessive thirst, weight loss, fatigue',
     'Insulin therapy, carbohydrate management, regular monitoring',
     'Healthy diet, physical activity, routine glucose level checks',
     'A chronic disease where the pancreas produces little or no insulin.',
     'Requires lifelong insulin therapy to regulate blood sugar levels.', 1),

    -- 5. Stunted Growth (Chậm phát triển chiều cao)
    ('Stunted Growth', 12.0, 18.0, 11.0, 17.0, 1, 10, 'Medium', 'Growth Disorder',
     'Short stature, delayed physical development, cognitive impairment',
     'Nutritional supplements, balanced diet, medical monitoring',
     'Ensure adequate food intake, early intervention for nutrient deficiencies',
     'A condition where a child’s growth rate is significantly lower than expected.',
     'Can be caused by poor nutrition, infections, or genetic conditions.', 1),

    -- 6. Asthma (Hen suyễn)
    ('Asthma', 18.0, 28.0, 17.0, 27.0, 4, 18, 'Medium', 'Respiratory Disorder',
     'Shortness of breath, wheezing, coughing, chest tightness',
     'Inhalers, medication, avoiding triggers (allergens, smoke)',
     'Avoid smoking exposure, control allergens at home',
     'A chronic respiratory condition where the airways become inflamed and narrow.',
     'Can be triggered by allergens, pollution, or respiratory infections.', 1),

    -- 7. Rickets (Còi xương)
    ('Rickets', 10.0, 17.0, 9.5, 16.5, 1, 10, 'Medium', 'Bone Disorder',
     'Delayed growth, weak bones, skeletal deformities',
     'Vitamin D and calcium supplements, sunlight exposure',
     'Ensure a diet rich in vitamin D, allow outdoor playtime',
     'A disorder caused by vitamin D, calcium, or phosphate deficiency leading to soft bones.',
     'Common in children with limited sun exposure or poor diet.', 1),

    -- 8. Thừa cân (Overweight)
    ('Overweight', 25.0, 30.0, 24.0, 29.9, 5, 18, 'Medium', 'Nutritional Disorder',
     'Rapid weight gain, easily fatigued, shortness of breath, difficulty moving',
     'Calorie-controlled diet, increased physical activity',
     'Develop healthy eating habits, maintain regular physical exercise',
     'A condition where excess fat accumulates in the body but has not yet reached obesity.',
     'May lead to obesity if not controlled early.', 1),

    -- 9. Béo phì (Obesity)
    ('Obesity', 30.0, 40.0, 30.0, 39.9, 5, 18, 'High', 'Nutritional Disorder',
     'Excessive fat accumulation, difficulty breathing, joint pain, risk of diabetes',
     'Weight-loss diet, regular exercise, medical monitoring',
     'Change eating habits, limit sugar and fat intake',
     'Obesity is a condition where excessive fat is stored in the body, affecting health.',
     'A risk factor for cardiovascular diseases and diabetes.', 1),

    -- 10. Hypertension (Cao huyết áp ở trẻ)
    ('Hypertension', 20.0, 40.0, 19.0, 39.0, 10, 18, 'High', 'Cardiovascular Disorder',
     'High blood pressure, headaches, dizziness, blurred vision',
     'Lifestyle changes, low-salt diet, medical treatment if severe',
     'Encourage exercise, avoid high-sodium and processed foods',
     'A condition where a child has abnormally high blood pressure, increasing cardiovascular risks.',
     'Can be associated with obesity, genetic factors, or poor diet.', 1),

    -- 11. Failure to Thrive (Không phát triển đúng chuẩn)
    ('Failure to Thrive', 10.0, 18.0, 9.5, 17.5, 1, 5, 'High', 'Developmental Disorder',
     'Slow weight gain, delayed milestones, lack of energy',
     'Nutritional therapy, parental guidance, medical monitoring',
     'Early detection, frequent weight monitoring, improved diet',
     'A condition where a child’s growth is significantly below the expected rate.',
     'Requires medical evaluation to determine underlying causes.', 1);

GO

-- Insert Children
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, Notes, RelationshipToMember, Status, CreatedAt, UpdatedAt)
SELECT MemberID, N'Nguyễn Minh Khang', '2008-06-15', 'Male', 3.2, 50, 'O+', NULL, N'Đang học cấp 3, yêu thích bóng đá', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'NguyenVA1992')
UNION ALL
SELECT MemberID, N'Trần Gia Hân', '2012-08-20', 'Female', 3.0, 48, 'A+', N'Dị ứng sữa', N'Học sinh tiểu học, thích vẽ tranh', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'TranTB1995')
UNION ALL
SELECT MemberID, N'Lê Hoàng Nam', '2018-12-10', 'Male', 3.1, 49, 'B-', NULL, N'Bé hiếu động, thích khám phá', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'LeVC1990')
UNION ALL
SELECT MemberID, N'Phạm Khánh Linh', '2023-03-25', 'Female', 3.4, 51, 'AB+', N'Dị ứng phấn hoa', N'Sơ sinh, cần theo dõi đặc biệt', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'PhamTD1993')
UNION ALL
SELECT MemberID, N'Hoàng Bảo Long', '2016-11-07', 'Male', 3.6, 52, 'O-', NULL, N'Bé thích vận động, học lớp 1', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'HoangVT1998')
UNION ALL
SELECT MemberID, N'Đặng Gia Bảo', '2010-02-14', 'Male', 3.2, 49, 'A-', NULL, N'Chơi bóng rổ giỏi, thích khoa học', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'DangTT1994')
UNION ALL
SELECT MemberID, N'Võ Thảo My', '2015-09-10', 'Female', 3.5, 50, 'B+', N'Dị ứng mèo', N'Yêu thích đọc sách, học giỏi', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'VoVD1996')
UNION ALL
SELECT MemberID, N'Bùi Phúc An', '2022-01-21', 'Male', 3.1, 48, 'O+', NULL, N'Bé hay cười, thích chơi với mẹ', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'BuiTH1997')
UNION ALL
SELECT MemberID, N'Mạc Thị Minh Anh', '2008-04-05', 'Female', 3.3, 49, 'A+', NULL, N'Yêu thích ca hát, tham gia đội văn nghệ', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'MacVO1995')
UNION ALL
SELECT MemberID, N'Ngô Hải Đăng', '2017-07-29', 'Male', 3.4, 50, 'B-', NULL, N'Bé thông minh, thích lắp ráp đồ chơi', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'NgoVS1991')
UNION ALL
SELECT MemberID, N'Đinh Thị Ngọc Hân', '2011-10-18', 'Female', 3.0, 48, 'O-', N'Dị ứng hải sản', N'Học sinh giỏi, yêu thích tiếng Anh', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'DinhTN1999')
UNION ALL
SELECT MemberID, N'Phan Nguyễn Gia Huy', '2009-05-23', 'Male', 3.3, 51, 'A+', NULL, N'Đang học cấp 2, thích chơi thể thao', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'PhanVK1993')
UNION ALL
SELECT MemberID, N'Nguyễn Thị Bảo Trân', '2014-12-01', 'Female', 3.2, 50, 'AB-', NULL, N'Yêu thích hội họa, vẽ rất đẹp', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'NguyenTL1990')
UNION ALL
SELECT MemberID, N'Đỗ Hoàng Nam Phong', '2007-06-22', 'Male', 3.5, 52, 'B+', NULL, N'Chơi cờ vua rất giỏi, đã đạt giải', N'Con trai', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'DoVM1994')
UNION ALL
SELECT MemberID, N'La Thị Hải Yến', '2013-09-17', 'Female', 3.1, 49, 'A-', NULL, N'Yêu thích thiên nhiên, thích trồng cây', N'Con gái', 'Active', GETDATE(), GETDATE()
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'LaTN1992');

GO

-- Thành viên StandardUser1 có 2 con
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, Notes, RelationshipToMember, Status, CreatedAt, UpdatedAt)
SELECT MemberID, N'Nguyễn Đặng Tuấn Kiệt', '2019-05-10', 'Male', 3.5, 51, 'B+', NULL, N'Bé rất hiếu động', N'Con trai', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'LyTP1997')
UNION ALL
SELECT MemberID, N'Nguyễn Đặng Ngọc Anh', '2022-01-25', 'Female', 3.1, 49, 'O-', NULL, N'Hay khóc đêm', N'Con gái', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'LyTP1997');

GO

-- Thành viên StandardUser2 có 2 con
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, Notes, RelationshipToMember, Status, CreatedAt, UpdatedAt)
SELECT MemberID, N'Vũ Bùi Thanh Tú', '2020-07-18', 'Male', 3.3, 50, 'A-', N'Dị ứng hải sản', N'Cần theo dõi chế độ ăn', N'Con trai', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'VuVQ1991')
UNION ALL
SELECT MemberID, N'Vũ Huyền Hà My', '2021-09-10', 'Female', 3.2, 49, 'AB-', NULL, N'Rất ngoan', N'Con gái', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'VuVQ1991');

GO

-- Thành viên PremiumUser1 có 2 con
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, Notes, RelationshipToMember, Status, CreatedAt, UpdatedAt)
SELECT MemberID, N'Phạm Quang Huy', '2015-11-12', 'Male', 3.4, 50, 'O+', NULL, N'Rất thích thể thao', N'Con trai', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'CaoTM1996')
UNION ALL
SELECT MemberID, N'Phạm Mai Chi', '2023-02-15', 'Female', 3.0, 48, 'A+', N'Dị ứng trứng', N'Cẩn thận với thực phẩm chứa trứng', N'Con gái', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'CaoTM1996');

GO

-- Thành viên PremiumUser2 có 2 con
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, Notes, RelationshipToMember, Status, CreatedAt, UpdatedAt)
SELECT MemberID, N'Tân Lê Tuấn Phong', '2014-06-20', 'Male', 3.8, 52, 'B+', NULL, N'Bé thích lập trình', N'Con trai', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'TanVS1998')
UNION ALL
SELECT MemberID, N'Tân Minh Ngọc', '2019-10-05', 'Female', 3.2, 50, 'AB+', NULL, N'Nhút nhát, cần động viên', N'Con gái', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'TanVS1998');

GO

-- Thành viên PremiumUser3 có 3 con
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, Notes, RelationshipToMember, Status, CreatedAt, UpdatedAt)
SELECT MemberID, N'Lê Đức Anh', '2010-08-30', 'Male', 3.6, 52, 'O-', NULL, N'Thích bóng đá', N'Con trai', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'TrinhTT1993')
UNION ALL
SELECT MemberID, N'Lê Thị Bảo Ngọc', '2016-04-25', 'Female', 3.5, 50, 'A-', NULL, N'Học giỏi và chăm chỉ', N'Con gái', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'TrinhTT1993')
UNION ALL
SELECT MemberID, N'Lê Trịnh Minh Khoa', '2022-12-10', 'Male', 3.1, 48, 'B+', N'Dị ứng đậu phộng', N'Cần tránh các sản phẩm từ đậu phộng', N'Con trai', 'Active', GETDATE(), GETDATE() 
FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'TrinhTT1993');

GO

--Insert GrowthRecords Nguyễn Minh Khang child
INSERT INTO GrowthRecords (
    ChildID, RecordedBy, Weight, Height, HeadCircumference, MuscleMass, ChestCircumference, 
    NutritionalStatus, FerritinLevel, Triglycerides, BloodSugarLevel, PhysicalActivityLevel, 
    HeartRate, BloodPressure, BodyTemperature, OxygenSaturation, SleepDuration, Vision, 
    Hearing, ImmunizationStatus, MentalHealthStatus, GrowthHormoneLevel, AttentionSpan, 
    NeurologicalReflexes, DevelopmentalMilestones, Notes, Status, Verified, CreatedAt, UpdatedAt
) 
SELECT 
    c.ChildID, 
    m.MemberID, 
    52.0, 168.5, 56.0, 19.0, 85.0, N'Bình thường', 75.0, 1.1, 95.0, N'Cao', 
    72, 120.0, 36.6, 99.2, 7.5, N'Tốt', N'Bình thường', N'Đã tiêm đủ các mũi', N'Ổn định', 
    20.0, N'Dài', N'Bình thường', N'Hoạt động thể thao đều đặn', N'Không có vấn đề sức khỏe', 
    'Active', 1, '2024-01-05', '2024-01-05'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Nguyễn Minh Khang'

UNION ALL

SELECT 
    c.ChildID, 
    m.MemberID, 
    53.5, 169.0, 56.2, 19.3, 86.5, N'Bình thường', 76.5, 1.2, 94.8, N'Cao', 
    70, 118.5, 36.5, 99.0, 7.3, N'Tốt', N'Bình thường', N'Đã tiêm đủ các mũi', N'Ổn định', 
    20.5, N'Dài', N'Bình thường', N'Thể lực tốt, ít ốm vặt', N'Không có vấn đề sức khỏe', 
    'Active', 1, '2024-01-25', '2024-01-25'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Nguyễn Minh Khang'

UNION ALL

SELECT 
    c.ChildID, 
    m.MemberID, 
    54.2, 169.8, 56.5, 19.7, 87.0, N'Tốt', 78.0, 1.3, 94.5, N'Cao', 
    68, 117.0, 36.4, 98.9, 7.2, N'Tốt', N'Bình thường', N'Đã tiêm đủ các mũi', N'Ổn định', 
    21.0, N'Dài', N'Bình thường', N'Bắt đầu tập gym nhẹ', N'Không có vấn đề sức khỏe', 
    'Active', 1, '2024-02-15', '2024-02-15'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Nguyễn Minh Khang'

UNION ALL

SELECT 
    c.ChildID, 
    m.MemberID, 
    55.0, 170.5, 57.0, 20.0, 88.0, N'Tốt', 80.0, 1.4, 94.2, N'Cao', 
    67, 116.5, 36.4, 98.8, 7.0, N'Tốt', N'Bình thường', N'Đã tiêm đủ các mũi', N'Ổn định', 
    21.5, N'Tốt', N'Bình thường', N'Thể trạng ổn định, ăn uống khoa học', N'Không có vấn đề sức khỏe', 
    'Active', 1, '2024-03-05', '2024-03-05'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Nguyễn Minh Khang'

UNION ALL

SELECT 
    c.ChildID, 
    m.MemberID, 
    56.0, 171.0, 57.5, 20.5, 89.0, N'Tốt', 82.0, 1.5, 94.0, N'Cao', 
    65, 115.0, 36.3, 98.7, 6.8, N'Tốt', N'Bình thường', N'Đã tiêm đủ các mũi', N'Ổn định', 
    22.0, N'Tốt', N'Bình thường', N'Thể lực tối ưu, không gặp vấn đề sức khỏe', N'Không có vấn đề sức khỏe', 
    'Active', 1, '2024-03-25', '2024-03-25'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Nguyễn Minh Khang';

GO

-- Insert GrowthRecords Trần Gia Hân child
INSERT INTO GrowthRecords (
    ChildID, RecordedBy, Weight, Height, HeadCircumference, MuscleMass, ChestCircumference, 
    NutritionalStatus, FerritinLevel, Triglycerides, BloodSugarLevel, PhysicalActivityLevel, 
    HeartRate, BloodPressure, BodyTemperature, OxygenSaturation, SleepDuration, Vision, 
    Hearing, ImmunizationStatus, MentalHealthStatus, GrowthHormoneLevel, AttentionSpan, 
    NeurologicalReflexes, DevelopmentalMilestones, Notes, Status, Verified, CreatedAt, UpdatedAt
) 
SELECT 
    c.ChildID, 
    m.MemberID, 
    27.0, 120.0, 50.0, 10.0, 55.0, N'Bình thường', 65.0, 1.0, 90.0, N'Bình thường',
    80, 110.0, 36.5, 98.0, 9.0, N'Bình thường', N'Bình thường', N'Đã tiêm đầy đủ', N'Ổn định',
    18.0, N'Tốt', N'Bình thường', N'Thể chất phát triển bình thường', N'Không có vấn đề sức khỏe',
    'Active', 1, '2025-01-05', '2025-01-05'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Trần Gia Hân'

UNION ALL

SELECT 
    c.ChildID, 
    m.MemberID, 
    27.5, 121.0, 50.5, 10.2, 56.0, N'Bình thường', 66.5, 1.1, 92.0, N'Bình thường',
    82, 112.0, 36.6, 98.5, 9.2, N'Bình thường', N'Bình thường', N'Đã tiêm đầy đủ', N'Ổn định',
    18.5, N'Tốt', N'Bình thường', N'Hoạt động thể thao thường xuyên', N'Không có vấn đề sức khỏe',
    'Active', 1, '2025-02-10', '2025-02-10'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Trần Gia Hân'

UNION ALL

SELECT 
    c.ChildID, 
    m.MemberID, 
    26.0, 122.0, 51.0, 10.4, 57.0, N'Bình thường', 67.0, 1.2, 93.0, N'Bình thường',
    85, 115.0, 36.7, 99.0, 9.5, N'Bình thường', N'Bình thường', N'Đã tiêm đầy đủ', N'Ổn định',
    19.0, N'Tốt', N'Bình thường', N'Thể chất phát triển ổn định', N'Không có vấn đề sức khỏe',
    'Active', 1, '2025-03-15', '2025-03-15'
FROM Children c
JOIN Members m ON c.MemberID = m.MemberID
WHERE c.Name = N'Trần Gia Hân';

GO

-- Insert Alerts
INSERT INTO Alerts (GrowthRecordID, DiseaseID, Message, SeverityLevel) 
SELECT 
    gr.RecordID,
    d.DiseaseID,
    N'Phát hiện dấu hiệu ' + d.DiseaseName + N' ở bé Trần Gia Hân. Cần theo dõi và điều chỉnh chế độ ăn uống.',
    d.Severity
FROM GrowthRecords gr
JOIN Diseases d ON d.DiseaseName = 'Mild Malnutrition'
WHERE gr.ChildID = (SELECT ChildID FROM Children WHERE Name = N'Trần Gia Hân')
  AND gr.Weight / POWER(gr.Height / 100, 2) BETWEEN d.LowerBoundFemale AND d.UpperBoundFemale;

CREATE TABLE BmiPercentiles (
    Age INT NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    L FLOAT NOT NULL,
    M FLOAT NOT NULL,
    S FLOAT NOT NULL,
    P01 FLOAT NOT NULL,
    P50 FLOAT NOT NULL,
    P75 FLOAT NOT NULL,
    P99 FLOAT NOT NULL,
    -- Khóa chính là bộ đôi (Age, Gender)
    CONSTRAINT PK_BmiPercentiles PRIMARY KEY (Age, Gender),
    -- Ràng buộc để đảm bảo Gender chỉ nhận giá trị 'Male' hoặc 'Female'
    CONSTRAINT CHK_Gender CHECK (Gender IN ('Male', 'Female'))
);
GO

--INSERT Bmi of Girl
INSERT INTO BmiPercentiles (Age, Gender, L, M, S, P01, P50, P75, P99)
VALUES
(0, 'Female', -0.0631, 13.3363, 0.09272, 10.0, 13.3, 14.2, 16.6),
(1, 'Female', 0.3448, 14.5679, 0.09556, 10.7, 14.6, 15.5, 18.0),
(2, 'Female', 0.1749, 15.7679, 0.09371, 11.7, 15.8, 16.8, 19.5),
(3, 'Female', 0.0643, 16.3574, 0.09254, 12.3, 16.4, 17.4, 20.3),
(4, 'Female', -0.0191, 16.6703, 0.09166, 12.6, 16.7, 17.7, 20.6),
(5, 'Female', -0.0864, 16.8386, 0.09096, 12.8, 16.8, 17.9, 20.8),
(6, 'Female', -0.1429, 16.9083, 0.09036, 12.9, 16.9, 18.0, 20.9),
(7, 'Female', -0.1916, 16.902, 0.08984, 12.9, 16.9, 18.0, 20.9),
(8, 'Female', -0.2344, 16.8404, 0.08939, 12.9, 16.8, 17.9, 20.8),
(9, 'Female', -0.2725, 16.7406, 0.08898, 12.8, 16.7, 17.8, 20.7),
(10, 'Female', -0.3068, 16.6184, 0.08861, 12.8, 16.6, 17.7, 20.6),
(11, 'Female', -0.3381, 16.4875, 0.08828, 12.7, 16.5, 17.5, 20.4),
(12, 'Female', -0.3667, 16.3568, 0.08797, 12.6, 16.4, 17.4, 20.2),
(13, 'Female', -0.3932, 16.2311, 0.08768, 12.5, 16.2, 17.2, 20.1),
(14, 'Female', -0.4177, 16.1128, 0.08741, 12.5, 16.1, 17.1, 19.9),
(15, 'Female', -0.4407, 16.0028, 0.08716, 12.4, 16.0, 17.0, 19.8),
(16, 'Female', -0.4623, 15.9017, 0.08693, 12.3, 15.9, 16.9, 19.7),
(17, 'Female', -0.4825, 15.8096, 0.08671, 12.3, 15.8, 16.8, 19.5),
(19, 'Female', -0.5199, 15.6517, 0.0863, 12.2, 15.7, 16.6, 19.3),
(20, 'Female', -0.5372, 15.5855, 0.08612, 12.2, 15.6, 16.5, 19.3),
(21, 'Female', -0.5537, 15.5278, 0.08594, 12.1, 15.5, 16.5, 19.2),
(22, 'Female', -0.5695, 15.4787, 0.08577, 12.1, 15.5, 16.4, 19.1),
(23, 'Female', -0.5846, 15.438, 0.0856, 12.1, 15.4, 16.4, 19.1),
(24, 'Female', -0.5989, 15.4052, 0.08545, 12.1, 15.4, 16.3, 19.0),
(25, 'Female', -0.5684, 15.659, 0.08452, 12.3, 15.7, 16.6, 19.3),
(26, 'Female', -0.5684, 15.6308, 0.08449, 12.3, 15.6, 16.6, 19.3),
(27, 'Female', -0.5684, 15.6037, 0.08446, 12.2, 15.6, 16.5, 19.2),
(28, 'Female', -0.5684, 15.5777, 0.08444, 12.2, 15.6, 16.5, 19.2),
(29, 'Female', -0.5684, 15.5523, 0.08443, 12.2, 15.6, 16.5, 19.2),
(30, 'Female', -0.5684, 15.5276, 0.08444, 12.2, 15.5, 16.5, 19.1),
(31, 'Female', -0.5684, 15.5034, 0.08448, 12.2, 15.5, 16.4, 19.1),
(32, 'Female', -0.5684, 15.4798, 0.08455, 12.1, 15.5, 16.4, 19.1),
(33, 'Female', -0.5684, 15.4572, 0.08467, 12.1, 15.5, 16.4, 19.0),
(34, 'Female', -0.5684, 15.4356, 0.08484, 12.1, 15.4, 16.4, 19.0),
(35, 'Female', -0.5684, 15.4155, 0.08506, 12.1, 15.4, 16.3, 19.0),
(36, 'Female', -0.5684, 15.3968, 0.08535, 12.0, 15.4, 16.3, 19.0),
(37, 'Female', -0.5684, 15.3796, 0.08569, 12.0, 15.4, 16.3, 19.0),
(38, 'Female', -0.5684, 15.3638, 0.08609, 12.0, 15.4, 16.3, 19.0),
(39, 'Female', -0.5684, 15.3493, 0.08654, 12.0, 15.3, 16.3, 19.0),
(40, 'Female', -0.5684, 15.3358, 0.08704, 11.9, 15.3, 16.3, 19.0),
(41, 'Female', -0.5684, 15.3233, 0.08757, 11.9, 15.3, 16.3, 19.0),
(42, 'Female', -0.5684, 15.3116, 0.08813, 11.9, 15.3, 16.3, 19.0),
(43, 'Female', -0.5684, 15.3007, 0.08872, 11.9, 15.3, 16.3, 19.1),
(44, 'Female', -0.5684, 15.2905, 0.08931, 11.8, 15.3, 16.3, 19.1),
(45, 'Female', -0.5684, 15.2814, 0.08991, 11.8, 15.3, 16.3, 19.1),
(46, 'Female', -0.5684, 15.2732, 0.09051, 11.8, 15.3, 16.3, 19.1),
(47, 'Female', -0.5684, 15.2661, 0.0911, 11.8, 15.3, 16.3, 19.1),
(48, 'Female', -0.5684, 15.2602, 0.09168, 11.7, 15.3, 16.3, 19.2),
(49, 'Female', -0.5684, 15.2556, 0.09227, 11.7, 15.3, 16.3, 19.2),
(50, 'Female', -0.5684, 15.2523, 0.09286, 11.7, 15.3, 16.3, 19.2),
(51, 'Female', -0.5684, 15.2503, 0.09345, 11.7, 15.3, 16.3, 19.2),
(52, 'Female', -0.5684, 15.2496, 0.09403, 11.7, 15.2, 16.3, 19.3),
(53, 'Female', -0.5684, 15.2502, 0.0946, 11.6, 15.3, 16.3, 19.3),
(54, 'Female', -0.5684, 15.2519, 0.09515, 11.6, 15.3, 16.3, 19.3),
(55, 'Female', -0.5684, 15.2544, 0.09568, 11.6, 15.3, 16.3, 19.4),
(56, 'Female', -0.5684, 15.2575, 0.09618, 11.6, 15.3, 16.3, 19.4),
(57, 'Female', -0.5684, 15.2612, 0.09665, 11.6, 15.3, 16.3, 19.4),
(58, 'Female', -0.5684, 15.2653, 0.09709, 11.6, 15.3, 16.3, 19.4),
(59, 'Female', -0.5684, 15.2698, 0.0975, 11.6, 15.3, 16.3, 19.5),
(60, 'Female', -0.5684, 15.2747, 0.09789, 11.6, 15.3, 16.3, 19.5),
(61, 'Female', -0.8886, 15.2441, 0.09692, 11.689, 15.244, 16.306, 19.605),
(62, 'Female', -0.9068, 15.2434, 0.09738, 11.682, 15.243, 16.311, 19.643),
(63, 'Female', -0.9248, 15.2433, 0.09783, 11.676, 15.243, 16.317, 19.681),
(64, 'Female', -0.9427, 15.2438, 0.09829, 11.671, 15.244, 16.324, 19.721),
(65, 'Female', -0.9605, 15.2448, 0.09875, 11.665, 15.245, 16.331, 19.763),
(66, 'Female', -0.978, 15.2464, 0.0992, 11.661, 15.246, 16.339, 19.804),
(67, 'Female', -0.9954, 15.2487, 0.09966, 11.657, 15.249, 16.347, 19.848),
(68, 'Female', -1.0126, 15.2516, 0.10012, 11.653, 15.252, 16.357, 19.892),
(69, 'Female', -1.0296, 15.2551, 0.10058, 11.649, 15.255, 16.367, 19.938),
(70, 'Female', -1.0464, 15.2592, 0.10104, 11.646, 15.259, 16.377, 19.985),
(71, 'Female', -1.063, 15.2641, 0.10149, 11.645, 15.264, 16.388, 20.033),
(72, 'Female', -1.0794, 15.2697, 0.10195, 11.643, 15.27, 16.401, 20.082),
(73, 'Female', -1.0956, 15.276, 0.10241, 11.642, 15.276, 16.414, 20.133),
(74, 'Female', -1.1115, 15.2831, 0.10287, 11.641, 15.283, 16.427, 20.186),
(75, 'Female', -1.1272, 15.2911, 0.10333, 11.641, 15.291, 16.442, 20.239),
(76, 'Female', -1.1427, 15.2998, 0.10379, 11.642, 15.3, 16.458, 20.295),
(77, 'Female', -1.1579, 15.3095, 0.10425, 11.643, 15.31, 16.475, 20.352),
(78, 'Female', -1.1728, 15.32, 0.10471, 11.645, 15.32, 16.492, 20.41),
(79, 'Female', -1.1875, 15.3314, 0.10517, 11.647, 15.331, 16.511, 20.47),
(80, 'Female', -1.2019, 15.3439, 0.10562, 11.651, 15.344, 16.53, 20.531),
(81, 'Female', -1.216, 15.3572, 0.10608, 11.654, 15.357, 16.551, 20.594),
(82, 'Female', -1.2298, 15.3717, 0.10654, 11.659, 15.372, 16.573, 20.659),
(83, 'Female', -1.2433, 15.3871, 0.107, 11.664, 15.387, 16.596, 20.726),
(84, 'Female', -1.2565, 15.4036, 0.10746, 11.671, 15.404, 16.62, 20.794),
(85, 'Female', -1.2693, 15.4211, 0.10792, 11.677, 15.421, 16.645, 20.864),
(86, 'Female', -1.2819, 15.4397, 0.10837, 11.685, 15.44, 16.671, 20.935),
(87, 'Female', -1.2941, 15.4593, 0.10883, 11.693, 15.459, 16.699, 21.009),
(88, 'Female', -1.306, 15.4798, 0.10929, 11.702, 15.48, 16.727, 21.084),
(89, 'Female', -1.3175, 15.5014, 0.10974, 11.712, 15.501, 16.757, 21.16),
(90, 'Female', -1.3287, 15.524, 0.1102, 11.722, 15.524, 16.788, 21.239),
(91, 'Female', -1.3395, 15.5476, 0.11065, 11.733, 15.548, 16.819, 21.318),
(92, 'Female', -1.3499, 15.5723, 0.1111, 11.745, 15.572, 16.852, 21.399),
(93, 'Female', -1.36, 15.5979, 0.11156, 11.757, 15.598, 16.887, 21.483),
(94, 'Female', -1.3697, 15.6246, 0.11201, 11.77, 15.625, 16.922, 21.567),
(95, 'Female', -1.379, 15.6523, 0.11246, 11.783, 15.652, 16.958, 21.653),
(96, 'Female', -1.388, 15.681, 0.11291, 11.798, 15.681, 16.995, 21.74),
(97, 'Female', -1.3966, 15.7107, 0.11335, 11.813, 15.711, 17.034, 21.828),
(98, 'Female', -1.4047, 15.7415, 0.1138, 11.828, 15.742, 17.073, 21.919),
(99, 'Female', -1.4125, 15.7732, 0.11424, 11.845, 15.773, 17.114, 22.01),
(100, 'Female', -1.4199, 15.8058, 0.11469, 11.862, 15.806, 17.156, 22.103),
(101, 'Female', -1.427, 15.8394, 0.11513, 11.879, 15.839, 17.198, 22.198),
(102, 'Female', -1.4336, 15.8738, 0.11557, 11.897, 15.874, 17.242, 22.293),
(103, 'Female', -1.4398, 15.909, 0.11601, 11.915, 15.909, 17.286, 22.389),
(104, 'Female', -1.4456, 15.9451, 0.11644, 11.934, 15.945, 17.331, 22.486),
(105, 'Female', -1.4511, 15.9818, 0.11688, 11.954, 15.982, 17.377, 22.584),
(106, 'Female', -1.4561, 16.0194, 0.11731, 11.974, 16.019, 17.424, 22.683),
(107, 'Female', -1.4607, 16.0575, 0.11774, 11.994, 16.058, 17.472, 22.783),
(108, 'Female', -1.465, 16.0964, 0.11816, 12.014, 16.096, 17.52, 22.882),
(109, 'Female', -1.4688, 16.1358, 0.11859, 12.035, 16.136, 17.569, 22.983),
(110, 'Female', -1.4723, 16.1759, 0.11901, 12.057, 16.176, 17.618, 23.084),
(111, 'Female', -1.4753, 16.2166, 0.11943, 12.078, 16.217, 17.669, 23.186),
(112, 'Female', -1.478, 16.258, 0.11985, 12.1, 16.258, 17.72, 23.289),
(113, 'Female', -1.4803, 16.2999, 0.12026, 12.122, 16.3, 17.771, 23.391),
(114, 'Female', -1.4823, 16.3425, 0.12067, 12.145, 16.343, 17.823, 23.494),
(115, 'Female', -1.4838, 16.3858, 0.12108, 12.168, 16.386, 17.876, 23.598),
(116, 'Female', -1.485, 16.4298, 0.12148, 12.191, 16.43, 17.93, 23.701),
(117, 'Female', -1.4859, 16.4746, 0.12188, 12.215, 16.475, 17.984, 23.806),
(118, 'Female', -1.4864, 16.52, 0.12228, 12.24, 16.52, 18.039, 23.911),
(119, 'Female', -1.4866, 16.5663, 0.12268, 12.264, 16.566, 18.096, 24.017),
(120, 'Female', -1.4864, 16.6133, 0.12307, 12.29, 16.613, 18.152, 24.123),
(121, 'Female', -1.4859, 16.6612, 0.12346, 12.315, 16.661, 18.21, 24.23),
(122, 'Female', -1.4851, 16.71, 0.12384, 12.342, 16.71, 18.269, 24.337),
(123, 'Female', -1.4839, 16.7595, 0.12422, 12.368, 16.76, 18.328, 24.444),
(124, 'Female', -1.4825, 16.81, 0.1246, 12.396, 16.81, 18.389, 24.553),
(125, 'Female', -1.4807, 16.8614, 0.12497, 12.424, 16.861, 18.45, 24.661),
(126, 'Female', -1.4787, 16.9136, 0.12534, 12.452, 16.914, 18.512, 24.77),
(127, 'Female', -1.4763, 16.9667, 0.12571, 12.481, 16.967, 18.575, 24.88),
(128, 'Female', -1.4737, 17.0208, 0.12607, 12.511, 17.021, 18.64, 24.991),
(129, 'Female', -1.4708, 17.0757, 0.12643, 12.54, 17.076, 18.705, 25.102),
(130, 'Female', -1.4677, 17.1316, 0.12678, 12.571, 17.132, 18.771, 25.213),
(131, 'Female', -1.4642, 17.1883, 0.12713, 12.602, 17.188, 18.838, 25.325),
(132, 'Female', -1.4606, 17.2459, 0.12748, 12.634, 17.246, 18.906, 25.438),
(133, 'Female', -1.4567, 17.3044, 0.12782, 12.666, 17.304, 19.974, 25.55),
(134, 'Female', -1.4526, 17.3637, 0.12816, 12.699, 17.364, 19.044, 25.664),
(135, 'Female', -1.4482, 17.4238, 0.12849, 12.732, 17.424, 19.114, 25.777),
(136, 'Female', -1.4436, 17.4847, 0.12882, 12.766, 17.485, 19.186, 25.891),
(137, 'Female', -1.4389, 17.5464, 0.12914, 12.801, 17.546, 19.258, 26.005),
(138, 'Female', -1.4339, 17.6088, 0.12946, 12.835, 17.609, 19.331, 26.12),
(139, 'Female', -1.4288, 17.6719, 0.12978, 12.87, 17.672, 19.404, 26.235),
(140, 'Female', -1.4235, 17.7357, 0.13009, 12.906, 17.736, 19.478, 26.35),
(141, 'Female', -1.418, 17.8001, 0.1304, 12.942, 17.8, 19.553, 26.465),
(142, 'Female', -1.4123, 17.8651, 0.1307, 12.978, 17.865, 19.629, 26.579),
(143, 'Female', -1.4065, 17.9306, 0.13099, 13.015, 17.931, 19.705, 26.694),
(144, 'Female', -1.4006, 17.9966, 0.13129, 13.052, 17.997, 19.781, 26.809),
(145, 'Female', -1.3945, 18.063, 0.13158, 13.089, 18.063, 19.858, 26.924),
(146, 'Female', -1.3883, 18.1297, 0.13186, 13.126, 18.13, 19.935, 27.038),
(147, 'Female', -1.3819, 18.1967, 0.13214, 13.164, 18.197, 20.012, 27.152),
(148, 'Female', -1.3755, 18.2639, 0.13241, 13.201, 18.264, 20.09, 27.265),
(149, 'Female', -1.3689, 18.3312, 0.13268, 13.239, 18.331, 20.167, 27.378),
(150, 'Female', -1.3621, 18.3986, 0.13295, 13.276, 18.399, 20.245, 27.49),
(151, 'Female', -1.3553, 18.466, 0.13321, 13.314, 18.466, 20.323, 27.601),
(152, 'Female', -1.3483, 18.5333, 0.13347, 13.351, 18.533, 20.4, 27.712),
(153, 'Female', -1.3413, 18.6006, 0.13372, 13.389, 18.601, 20.477, 27.821),
(154, 'Female', -1.3341, 18.6677, 0.13397, 13.426, 18.668, 20.555, 27.929),
(155, 'Female', -1.3269, 18.7346, 0.13421, 13.463, 18.735, 20.631, 28.036),
(156, 'Female', -1.3195, 18.8012, 0.13445, 13.499, 18.801, 20.708, 28.143),
(157, 'Female', -1.3121, 18.8675, 0.13469, 13.536, 18.868, 20.784, 28.248),
(158, 'Female', -1.3046, 18.9335, 0.13492, 13.572, 18.934, 20.859, 28.352),
(159, 'Female', -1.297, 18.9991, 0.13514, 13.608, 18.999, 20.934, 28.454),
(160, 'Female', -1.2894, 19.0642, 0.13537, 13.643, 19.064, 21.009, 28.556),
(161, 'Female', -1.2816, 19.1289, 0.13559, 13.678, 19.129, 21.083, 28.655),
(162, 'Female', -1.2739, 19.1931, 0.1358, 13.713, 19.193, 21.156, 28.753),
(163, 'Female', -1.2661, 19.2567, 0.13601, 13.748, 19.257, 21.229, 28.85),
(164, 'Female', -1.2583, 19.3197, 0.13622, 13.781, 19.32, 21.301, 28.946),
(165, 'Female', -1.2504, 19.382, 0.13642, 13.815, 19.382, 21.372, 29.039),
(166, 'Female', -1.2425, 19.4437, 0.13662, 13.848, 19.444, 21.443, 29.132),
(167, 'Female', -1.2345, 19.5045, 0.13681, 13.88, 19.504, 21.512, 29.222),
(168, 'Female', -1.2266, 19.5647, 0.137, 13.912, 19.565, 21.581, 29.311),
(169, 'Female', -1.2186, 19.624, 0.13719, 13.943, 19.624, 21.648, 29.398),
(170, 'Female', -1.2107, 19.6824, 0.13738, 13.973, 19.682, 21.715, 29.484),
(171, 'Female', -1.2027, 19.74, 0.13756, 14.003, 19.74, 21.781, 29.568),
(172, 'Female', -1.1947, 19.7966, 0.13774, 14.033, 19.797, 21.845, 29.65),
(173, 'Female', -1.1867, 19.8523, 0.13791, 14.061, 19.852, 21.909, 29.729),
(174, 'Female', -1.1788, 19.907, 0.13808, 14.089, 19.907, 21.971, 29.808),
(175, 'Female', -1.1708, 19.9607, 0.13825, 14.116, 19.961, 22.032, 29.884),
(176, 'Female', -1.1629, 20.0133, 0.13841, 14.143, 20.013, 22.092, 29.958),
(177, 'Female', -1.1549, 20.0648, 0.13858, 14.168, 20.065, 22.151, 30.031),
(178, 'Female', -1.147, 20.1152, 0.13873, 14.194, 20.115, 22.208, 30.1),
(179, 'Female', -1.139, 20.1644, 0.13889, 14.218, 20.164, 22.264, 30.169),
(180, 'Female', -1.1311, 20.2125, 0.13904, 14.241, 20.212, 22.319, 30.235),
(181, 'Female', -1.1232, 20.2595, 0.1392, 14.263, 20.26, 22.373, 30.3),
(182, 'Female', -1.1153, 20.3053, 0.13934, 14.285, 20.305, 22.425, 30.361),
(183, 'Female', -1.1074, 20.3499, 0.13949, 14.306, 20.35, 22.476, 30.422),
(184, 'Female', -1.0996, 20.3934, 0.13963, 14.326, 20.393, 22.525, 30.48),
(185, 'Female', -1.0917, 20.4357, 0.13977, 14.345, 20.436, 22.573, 30.536),
(186, 'Female', -1.0838, 20.4769, 0.13991, 14.364, 20.477, 22.62, 30.591),
(187, 'Female', -1.076, 20.517, 0.14005, 14.381, 20.517, 22.666, 30.643),
(188, 'Female', -1.0681, 20.556, 0.14018, 14.398, 20.556, 22.71, 30.693),
(189, 'Female', -1.0603, 20.5938, 0.14031, 14.415, 20.594, 22.754, 30.742),
(190, 'Female', -1.0525, 20.6306, 0.14044, 14.43, 20.631, 22.795, 30.789),
(191, 'Female', -1.0447, 20.6663, 0.14057, 14.445, 20.666, 22.836, 30.834),
(192, 'Female', -1.0368, 20.7008, 0.1407, 14.458, 20.701, 22.876, 30.877),
(193, 'Female', -1.029, 20.7344, 0.14082, 14.472, 20.734, 22.914, 30.918),
(194, 'Female', -1.0212, 20.7668, 0.14094, 14.484, 20.767, 22.951, 30.957),
(195, 'Female', -1.0134, 20.7982, 0.14106, 14.496, 20.798, 22.987, 30.995),
(196, 'Female', -1.0055, 20.8286, 0.14118, 14.506, 20.829, 23.021, 31.031),
(197, 'Female', -0.9977, 20.858, 0.1413, 14.517, 20.858, 23.055, 31.065),
(198, 'Female', -0.9898, 20.8863, 0.14142, 14.526, 20.886, 23.087, 31.098),
(199, 'Female', -0.9819, 20.9137, 0.14153, 14.535, 20.914, 23.119, 31.128),
(200, 'Female', -0.974, 20.9401, 0.14164, 14.543, 20.94, 23.149, 31.157),
(201, 'Female', -0.9661, 20.9656, 0.14176, 14.55, 20.966, 23.178, 31.186),
(202, 'Female', -0.9582, 20.9901, 0.14187, 14.556, 20.99, 23.206, 31.212),
(203, 'Female', -0.9503, 21.0138, 0.14198, 14.562, 21.014, 23.233, 31.236),
(204, 'Female', -0.9423, 21.0367, 0.14208, 14.568, 21.037, 23.259, 31.259),
(205, 'Female', -0.9344, 21.0587, 0.14219, 14.573, 21.059, 23.285, 31.281),
(206, 'Female', -0.9264, 21.0801, 0.1423, 14.577, 21.08, 23.309, 31.302),
(207, 'Female', -0.9184, 21.1007, 0.1424, 14.581, 21.101, 23.333, 31.321),
(208, 'Female', -0.9104, 21.1206, 0.1425, 14.584, 21.121, 23.355, 31.339),
(209, 'Female', -0.9024, 21.1399, 0.14261, 14.587, 21.14, 23.378, 31.357),
(210, 'Female', -0.8944, 21.1586, 0.14271, 14.589, 21.159, 23.399, 31.373),
(211, 'Female', -0.8863, 21.1768, 0.14281, 14.591, 21.177, 23.42, 31.388),
(212, 'Female', -0.8783, 21.1944, 0.14291, 14.593, 21.194, 23.44, 31.403),
(213, 'Female', -0.8703, 21.2116, 0.14301, 14.594, 21.212, 23.46, 31.417),
(214, 'Female', -0.8623, 21.2282, 0.14311, 14.595, 21.228, 23.479, 31.43),
(215, 'Female', -0.8542, 21.2444, 0.1432, 14.596, 21.244, 23.498, 31.441),
(216, 'Female', -0.8462, 21.2603, 0.1433, 14.596, 21.26, 23.516, 31.453),
(217, 'Female', -0.8382, 21.2757, 0.1434, 14.596, 21.276, 23.534, 31.464),
(218, 'Female', -0.8301, 21.2908, 0.14349, 14.596, 21.291, 23.551, 31.474),
(219, 'Female', -0.8221, 21.3055, 0.14359, 14.595, 21.306, 23.568, 31.484),
(220, 'Female', -0.814, 21.32, 0.14368, 14.594, 21.32, 23.585, 31.493),
(221, 'Female', -0.806, 21.3341, 0.14377, 14.593, 21.334, 23.601, 31.502),
(222, 'Female', -0.798, 21.348, 0.14386, 14.592, 21.348, 23.617, 31.51),
(223, 'Female', -0.7899, 21.3617, 0.14396, 14.59, 21.362, 23.633, 31.519),
(224, 'Female', -0.7819, 21.3752, 0.14405, 14.589, 21.375, 23.648, 31.526),
(225, 'Female', -0.7738, 21.3884, 0.14414, 14.587, 21.388, 23.663, 31.533),
(226, 'Female', -0.7658, 21.4014, 0.14423, 14.585, 21.401, 23.678, 31.54),
(227, 'Female', -0.7577, 21.4143, 0.14432, 14.583, 21.414, 23.693, 31.547),
(228, 'Female', -0.7496, 21.4269, 0.14441, 14.58, 21.427, 23.707, 31.553);
GO

--INSERT Bmi of Boy
INSERT INTO BmiPercentiles (Age, Gender, L, M, S, P01, P50, P75, P99)
VALUES
(0, 'Male', -0.3053, 13.4069, 0.0956, 10.1, 13.4, 14.3, 18.3),
(1, 'Male', 0.2708, 14.9441, 0.09027, 11.2, 14.9, 15.9, 19.6),
(2, 'Male', 0.1118, 16.3195, 0.08677, 12.4, 16.3, 17.3, 21.3),
(3, 'Male', 0.0068, 16.8987, 0.08495, 13.0, 16.9, 17.9, 22.0),
(4, 'Male', -0.0727, 17.1579, 0.08378, 13.3, 17.2, 18.2, 22.3),
(5, 'Male', -0.137, 17.2919, 0.08296, 13.4, 17.3, 18.3, 22.4),
(6, 'Male', -0.1913, 17.3422, 0.08234, 13.5, 17.3, 18.3, 22.5),
(7, 'Male', -0.2385, 17.3288, 0.08183, 13.6, 17.3, 18.3, 22.5),
(8, 'Male', -0.2802, 17.2647, 0.0814, 13.5, 17.3, 18.2, 22.4),
(9, 'Male', -0.3176, 17.1662, 0.08102, 13.5, 17.2, 18.1, 22.3),
(10, 'Male', -0.3516, 17.0488, 0.08068, 13.4, 17.0, 18.0, 22.1),
(11, 'Male', -0.3828, 16.9239, 0.08037, 13.3, 16.9, 17.9, 22.0),
(12, 'Male', -0.4115, 16.7981, 0.08009, 13.3, 16.8, 17.7, 21.8),
(13, 'Male', -0.4382, 16.6743, 0.07982, 13.2, 16.7, 17.6, 21.6),
(14, 'Male', -0.463, 16.5548, 0.07958, 13.1, 16.6, 17.5, 21.5),
(15, 'Male', -0.4863, 16.4409, 0.07935, 13.0, 16.4, 17.4, 21.3),
(16, 'Male', -0.5082, 16.3335, 0.07913, 13.0, 16.3, 17.2, 21.2),
(17, 'Male', -0.5289, 16.2329, 0.07892, 12.9, 16.2, 17.1, 21.1),
(18, 'Male', -0.5484, 16.1392, 0.07873, 12.8, 16.1, 17.0, 21.0),
(19, 'Male', -0.5669, 16.0528, 0.07854, 12.8, 16.1, 16.9, 20.8),
(20, 'Male', -0.5846, 15.9743, 0.07836, 12.7, 16.0, 16.9, 20.7),
(21, 'Male', -0.6014, 15.9039, 0.07818, 12.7, 15.9, 16.8, 20.6),
(22, 'Male', -0.6174, 15.8412, 0.07802, 12.7, 15.8, 16.7, 20.6),
(23, 'Male', -0.6328, 15.7852, 0.07786, 12.6, 15.8, 16.7, 20.5),
(24, 'Male', -0.6473, 15.7356, 0.07771, 12.6, 15.7, 16.6, 20.4),
(25, 'Male', -0.584, 15.98, 0.07792, 12.8, 16.0, 16.9, 20.7),
(26, 'Male', -0.5497, 15.9414, 0.078, 12.7, 15.9, 16.8, 20.6),
(27, 'Male', -0.5166, 15.9036, 0.07808, 12.7, 15.9, 16.8, 20.6),
(28, 'Male', -0.485, 15.8667, 0.07818, 12.6, 15.9, 16.7, 20.5),
(29, 'Male', -0.4552, 15.8306, 0.07829, 12.6, 15.8, 16.7, 20.5),
(30, 'Male', -0.4274, 15.7953, 0.07841, 12.5, 15.8, 16.7, 20.4),
(31, 'Male', -0.4016, 15.7606, 0.07854, 12.5, 15.8, 16.6, 20.3),
(32, 'Male', -0.3782, 15.7267, 0.07867, 12.5, 15.7, 16.6, 20.3),
(33, 'Male', -0.3572, 15.6934, 0.07882, 12.4, 15.7, 16.6, 20.2),
(34, 'Male', -0.3388, 15.661, 0.07897, 12.4, 15.7, 16.5, 20.2),
(35, 'Male', -0.3231, 15.6294, 0.07914, 12.4, 15.6, 16.5, 20.2),
(36, 'Male', -0.3101, 15.5988, 0.07931, 12.3, 15.6, 16.5, 20.1),
(37, 'Male', -0.3, 15.5693, 0.0795, 12.3, 15.6, 16.4, 20.1),
(38, 'Male', -0.2927, 15.541, 0.07969, 12.3, 15.5, 16.4, 20.1),
(39, 'Male', -0.2884, 15.514, 0.0799, 12.2, 15.5, 16.4, 20.0),
(40, 'Male', -0.2869, 15.4885, 0.08012, 12.2, 15.5, 16.4, 20.0),
(41, 'Male', -0.2881, 15.4645, 0.08036, 12.2, 15.5, 16.3, 20.0),
(42, 'Male', -0.2919, 15.442, 0.08061, 12.1, 15.4, 16.3, 20.0),
(43, 'Male', -0.2981, 15.421, 0.08087, 12.1, 15.4, 16.3, 20.0),
(44, 'Male', -0.3067, 15.4013, 0.08115, 12.1, 15.4, 16.3, 20.0),
(45, 'Male', -0.3174, 15.3827, 0.08144, 12.1, 15.4, 16.3, 20.0),
(46, 'Male', -0.3303, 15.3652, 0.08174, 12.1, 15.4, 16.2, 20.0),
(47, 'Male', -0.3452, 15.3485, 0.08205, 12.0, 15.3, 16.2, 20.0),
(48, 'Male', -0.3622, 15.3326, 0.08238, 12.0, 15.3, 16.2, 20.0),
(49, 'Male', -0.3811, 15.3174, 0.08272, 12.0, 15.3, 16.2, 20.0),
(50, 'Male', -0.4019, 15.3029, 0.08307, 12.0, 15.3, 16.2, 20.1),
(51, 'Male', -0.4245, 15.2891, 0.08343, 12.0, 15.3, 16.2, 20.1),
(52, 'Male', -0.4488, 15.2759, 0.0838, 12.0, 15.3, 16.2, 20.1),
(53, 'Male', -0.4747, 15.2633, 0.08418, 11.9, 15.3, 16.2, 20.1),
(54, 'Male', -0.5019, 15.2514, 0.08457, 11.9, 15.3, 16.2, 20.2),
(55, 'Male', -0.5303, 15.24, 0.08496, 11.9, 15.2, 16.2, 20.2),
(56, 'Male', -0.5599, 15.2291, 0.08536, 11.9, 15.2, 16.1, 20.3),
(57, 'Male', -0.5905, 15.2188, 0.08577, 11.9, 15.2, 16.1, 20.3),
(58, 'Male', -0.6223, 15.2091, 0.08617, 11.9, 15.2, 16.1, 20.3),
(59, 'Male', -0.6552, 15.2, 0.08659, 11.9, 15.2, 16.1, 20.4),
(60, 'Male', -0.6892, 15.1916, 0.087, 11.9, 15.2, 16.1, 20.5),
(61, 'Male', -0.7387, 15.2641, 0.0839, 12.041, 15.264, 16.172, 20.355),
(62, 'Male', -0.7621, 15.2616, 0.08414, 12.039, 15.262, 16.173, 20.392),
(63, 'Male', -0.7856, 15.2604, 0.08439, 12.037, 15.26, 16.175, 20.432),
(64, 'Male', -0.8089, 15.2605, 0.08464, 12.037, 15.26, 16.179, 20.474),
(65, 'Male', -0.8322, 15.2619, 0.0849, 12.038, 15.262, 16.184, 20.519),
(66, 'Male', -0.8554, 15.2645, 0.08516, 12.039, 15.264, 16.191, 20.567),
(67, 'Male', -0.8785, 15.2684, 0.08543, 12.042, 15.268, 16.198, 20.618),
(68, 'Male', -0.9015, 15.2737, 0.0857, 12.045, 15.274, 16.208, 20.671),
(69, 'Male', -0.9243, 15.2801, 0.08597, 12.049, 15.28, 16.218, 20.726),
(70, 'Male', -0.9471, 15.2877, 0.08625, 12.054, 15.288, 16.23, 20.785),
(71, 'Male', -0.9697, 15.2965, 0.08653, 12.06, 15.296, 16.244, 20.846),
(72, 'Male', -0.9921, 15.3062, 0.08682, 12.066, 15.306, 16.258, 20.91),
(73, 'Male', -1.0144, 15.3169, 0.08711, 12.073, 15.317, 16.273, 20.975),
(74, 'Male', -1.0365, 15.3285, 0.08741, 12.08, 15.328, 16.29, 21.044),
(75, 'Male', -1.0584, 15.3408, 0.08771, 12.088, 15.341, 16.307, 21.114),
(76, 'Male', -1.0801, 15.354, 0.08802, 12.096, 15.354, 16.326, 21.188),
(77, 'Male', -1.1017, 15.3679, 0.08833, 12.105, 15.368, 16.345, 21.263),
(78, 'Male', -1.123, 15.3825, 0.08865, 12.114, 15.382, 16.365, 21.341),
(79, 'Male', -1.1441, 15.3978, 0.08898, 12.124, 15.398, 16.386, 21.421),
(80, 'Male', -1.1649, 15.4137, 0.08931, 12.133, 15.414, 16.407, 21.504),
(81, 'Male', -1.1856, 15.4302, 0.08964, 12.143, 15.43, 16.429, 21.588),
(82, 'Male', -1.206, 15.4473, 0.08998, 12.154, 15.447, 16.452, 21.675),
(83, 'Male', -1.2261, 15.465, 0.09033, 12.164, 15.465, 16.476, 21.764),
(84, 'Male', -1.246, 15.4832, 0.09068, 12.175, 15.483, 16.5, 21.855),
(85, 'Male', -1.2656, 15.5019, 0.09103, 12.186, 15.502, 16.525, 21.948),
(86, 'Male', -1.2849, 15.521, 0.09139, 12.198, 15.521, 16.55, 22.044),
(87, 'Male', -1.304, 15.5407, 0.09176, 12.209, 15.541, 16.577, 22.142),
(88, 'Male', -1.3228, 15.5608, 0.09213, 12.221, 15.561, 16.603, 22.243),
(89, 'Male', -1.3414, 15.5814, 0.09251, 12.232, 15.581, 16.631, 22.346),
(90, 'Male', -1.3596, 15.6023, 0.09289, 12.244, 15.602, 16.658, 22.45),
(91, 'Male', -1.3776, 15.6237, 0.09327, 12.257, 15.624, 16.686, 22.557),
(92, 'Male', -1.3953, 15.6455, 0.09366, 12.269, 15.646, 16.715, 22.667),
(93, 'Male', -1.4126, 15.6677, 0.09406, 12.281, 15.668, 16.744, 22.779),
(94, 'Male', -1.4297, 15.6903, 0.09445, 12.294, 15.69, 16.774, 22.892),
(95, 'Male', -1.4464, 15.7133, 0.09486, 12.307, 15.713, 16.804, 23.009),
(96, 'Male', -1.4629, 15.7368, 0.09526, 12.32, 15.737, 16.835, 23.127),
(97, 'Male', -1.479, 15.7606, 0.09567, 12.333, 15.761, 16.867, 23.249),
(98, 'Male', -1.4947, 15.7848, 0.09609, 12.346, 15.785, 16.898, 23.373),
(99, 'Male', -1.5101, 15.8094, 0.09651, 12.359, 15.809, 16.931, 23.499),
(100, 'Male', -1.5252, 15.8344, 0.09693, 12.373, 15.834, 16.963, 23.627),
(101, 'Male', -1.5399, 15.8597, 0.09735, 12.386, 15.86, 16.996, 23.756),
(102, 'Male', -1.5542, 15.8855, 0.09778, 12.4, 15.886, 17.03, 23.89),
(103, 'Male', -1.5681, 15.9116, 0.09821, 12.414, 15.912, 17.064, 24.024),
(104, 'Male', -1.5817, 15.9381, 0.09864, 12.428, 15.938, 17.099, 24.161),
(105, 'Male', -1.5948, 15.9651, 0.09907, 12.442, 15.965, 17.134, 24.3),
(106, 'Male', -1.6076, 15.9925, 0.09951, 12.457, 15.992, 17.17, 24.442),
(107, 'Male', -1.6199, 16.0205, 0.09994, 12.472, 16.02, 17.206, 24.585),
(108, 'Male', -1.6318, 16.049, 0.10038, 12.487, 16.049, 17.243, 24.731),
(109, 'Male', -1.6433, 16.0781, 0.10082, 12.502, 16.078, 17.28, 24.879),
(110, 'Male', -1.6544, 16.1078, 0.10126, 12.518, 16.108, 17.319, 25.029),
(111, 'Male', -1.6651, 16.1381, 0.1017, 12.534, 16.138, 17.357, 25.182),
(112, 'Male', -1.6753, 16.1692, 0.10214, 12.55, 16.169, 17.397, 25.337),
(113, 'Male', -1.6851, 16.2009, 0.10259, 12.567, 16.201, 17.438, 25.495),
(114, 'Male', -1.6944, 16.2333, 0.10303, 12.584, 16.233, 17.479, 25.654),
(115, 'Male', -1.7032, 16.2665, 0.10347, 12.602, 16.266, 17.521, 25.814),
(116, 'Male', -1.7116, 16.3004, 0.10391, 12.62, 16.3, 17.564, 25.977),
(117, 'Male', -1.7196, 16.3351, 0.10435, 12.639, 16.335, 17.608, 26.142),
(118, 'Male', -1.7271, 16.3704, 0.10478, 12.658, 16.37, 17.652, 26.306),
(119, 'Male', -1.7341, 16.4065, 0.10522, 12.677, 16.406, 17.697, 26.474),
(120, 'Male', -1.7407, 16.4433, 0.10566, 12.697, 16.443, 17.743, 26.644),
(121, 'Male', -1.7468, 16.4807, 0.10609, 12.718, 16.481, 17.79, 26.814),
(122, 'Male', -1.7525, 16.5189, 0.10652, 12.738, 16.519, 17.837, 26.985),
(123, 'Male', -1.7578, 16.5578, 0.10695, 12.76, 16.558, 17.886, 27.158),
(124, 'Male', -1.7626, 16.5974, 0.10738, 12.781, 16.597, 17.935, 27.332),
(125, 'Male', -1.767, 16.6376, 0.1078, 12.803, 16.638, 17.984, 27.505),
(126, 'Male', -1.771, 16.6786, 0.10823, 12.826, 16.679, 18.035, 27.682),
(127, 'Male', -1.7745, 16.7203, 0.10865, 12.849, 16.72, 18.086, 27.857),
(128, 'Male', -1.7777, 16.7628, 0.10906, 12.872, 16.763, 18.138, 28.032),
(129, 'Male', -1.7804, 16.8059, 0.10948, 12.896, 16.806, 18.191, 28.21),
(130, 'Male', -1.7828, 16.8497, 0.10989, 12.92, 16.85, 18.244, 28.387),
(131, 'Male', -1.7847, 16.8941, 0.1103, 12.945, 16.894, 18.298, 28.564),
(132, 'Male', -1.7862, 16.9392, 0.1107, 12.97, 16.939, 18.353, 28.74),
(133, 'Male', -1.7873, 16.985, 0.1111, 12.996, 16.985, 18.408, 28.916),
(134, 'Male', -1.7881, 17.0314, 0.1115, 13.022, 17.031, 18.464, 29.093),
(135, 'Male', -1.7884, 17.0784, 0.11189, 13.048, 17.078, 18.521, 29.267),
(136, 'Male', -1.7884, 17.1262, 0.11228, 13.075, 17.126, 18.578, 29.442),
(137, 'Male', -1.788, 17.1746, 0.11266, 13.102, 17.175, 18.636, 29.614),
(138, 'Male', -1.7873, 17.2236, 0.11304, 13.13, 17.224, 18.695, 29.787),
(139, 'Male', -1.7861, 17.2734, 0.11342, 13.157, 17.273, 18.754, 29.959),
(140, 'Male', -1.7846, 17.324, 0.11379, 13.186, 17.324, 18.815, 30.129),
(141, 'Male', -1.7828, 17.3752, 0.11415, 13.215, 17.375, 18.875, 30.297),
(142, 'Male', -1.7806, 17.4272, 0.11451, 13.245, 17.427, 18.937, 30.464),
(143, 'Male', -1.778, 17.4799, 0.11487, 13.275, 17.48, 19.0, 30.63),
(144, 'Male', -1.7751, 17.5334, 0.11522, 13.305, 17.533, 19.063, 30.794),
(145, 'Male', -1.7719, 17.5877, 0.11556, 13.337, 17.588, 19.127, 30.955),
(146, 'Male', -1.7684, 17.6427, 0.1159, 13.368, 17.643, 19.191, 31.116),
(147, 'Male', -1.7645, 17.6985, 0.11623, 13.4, 17.698, 19.257, 31.273),
(148, 'Male', -1.7604, 17.7551, 0.11656, 13.433, 17.755, 19.323, 31.43),
(149, 'Male', -1.7559, 17.8124, 0.11688, 13.466, 17.812, 19.39, 31.584),
(150, 'Male', -1.7511, 17.8704, 0.1172, 13.5, 17.87, 19.457, 31.736),
(151, 'Male', -1.7461, 17.9292, 0.11751, 13.534, 17.929, 19.526, 31.886),
(152, 'Male', -1.7408, 17.9887, 0.11781, 13.569, 17.989, 19.595, 32.033),
(153, 'Male', -1.7352, 18.0488, 0.11811, 13.604, 18.049, 19.665, 32.178),
(154, 'Male', -1.7293, 18.1096, 0.11841, 13.639, 18.11, 19.735, 32.322),
(155, 'Male', -1.7232, 18.171, 0.11869, 13.675, 18.171, 19.806, 32.46),
(156, 'Male', -1.7168, 18.233, 0.11898, 13.711, 18.233, 19.877, 32.6),
(157, 'Male', -1.7102, 18.2955, 0.11925, 13.748, 18.296, 19.949, 32.733),
(158, 'Male', -1.7033, 18.3586, 0.11952, 13.785, 18.359, 20.022, 32.865),
(159, 'Male', -1.6962, 18.4221, 0.11979, 13.822, 18.422, 20.095, 32.996),
(160, 'Male', -1.6888, 18.486, 0.12005, 13.86, 18.486, 20.168, 33.122),
(161, 'Male', -1.6811, 18.5502, 0.1203, 13.897, 18.55, 20.241, 33.243),
(162, 'Male', -1.6732, 18.6148, 0.12055, 13.935, 18.615, 20.315, 33.364),
(163, 'Male', -1.6651, 18.6795, 0.12079, 13.973, 18.68, 20.389, 33.48),
(164, 'Male', -1.6568, 18.7445, 0.12102, 14.011, 18.744, 20.463, 33.592),
(165, 'Male', -1.6482, 18.8095, 0.12125, 14.049, 18.81, 20.537, 33.701),
(166, 'Male', -1.6394, 18.8746, 0.12148, 14.087, 18.875, 20.611, 33.809),
(167, 'Male', -1.6304, 18.9398, 0.1217, 14.125, 18.94, 20.685, 33.913),
(168, 'Male', -1.6211, 19.005, 0.12191, 14.163, 19.005, 20.758, 34.011),
(169, 'Male', -1.6116, 19.0701, 0.12212, 14.201, 19.07, 20.832, 34.107),
(170, 'Male', -1.602, 19.1351, 0.12233, 14.239, 19.135, 20.906, 34.202),
(171, 'Male', -1.5921, 19.2, 0.12253, 14.276, 19.2, 20.979, 34.292),
(172, 'Male', -1.5821, 19.2648, 0.12272, 14.314, 19.265, 21.052, 34.378),
(173, 'Male', -1.5719, 19.3294, 0.12291, 14.351, 19.329, 21.125, 34.462),
(174, 'Male', -1.5615, 19.3937, 0.1231, 14.387, 19.394, 21.197, 34.544),
(175, 'Male', -1.551, 19.4578, 0.12328, 14.424, 19.458, 21.269, 34.622),
(176, 'Male', -1.5403, 19.5217, 0.12346, 14.46, 19.522, 21.341, 34.698),
(177, 'Male', -1.5294, 19.5853, 0.12363, 14.497, 19.585, 21.413, 34.769),
(178, 'Male', -1.5185, 19.6486, 0.1238, 14.532, 19.649, 21.484, 34.84),
(179, 'Male', -1.5074, 19.7117, 0.12396, 14.568, 19.712, 21.554, 34.906),
(180, 'Male', -1.4961, 19.7744, 0.12412, 14.603, 19.774, 21.625, 34.97),
(181, 'Male', -1.4848, 19.8367, 0.12428, 14.638, 19.837, 21.694, 35.034),
(182, 'Male', -1.4733, 19.8987, 0.12443, 14.673, 19.899, 21.764, 35.093),
(183, 'Male', -1.4617, 19.9603, 0.12458, 14.707, 19.96, 21.832, 35.151),
(184, 'Male', -1.45, 20.0215, 0.12473, 14.741, 20.022, 21.901, 35.208),
(185, 'Male', -1.4382, 20.0823, 0.12487, 14.774, 20.082, 21.969, 35.261),
(186, 'Male', -1.4263, 20.1427, 0.12501, 14.807, 20.143, 22.036, 35.313),
(187, 'Male', -1.4143, 20.2026, 0.12514, 14.84, 20.203, 22.103, 35.36),
(188, 'Male', -1.4022, 20.2621, 0.12528, 14.872, 20.262, 22.169, 35.41),
(189, 'Male', -1.39, 20.3211, 0.12541, 14.904, 20.321, 22.235, 35.455),
(190, 'Male', -1.3777, 20.3796, 0.12554, 14.935, 20.38, 22.3, 35.499),
(191, 'Male', -1.3653, 20.4376, 0.12567, 14.966, 20.438, 22.364, 35.542),
(192, 'Male', -1.3529, 20.4951, 0.12579, 14.997, 20.495, 22.428, 35.582),
(193, 'Male', -1.3403, 20.5521, 0.12591, 15.027, 20.552, 22.491, 35.62),
(194, 'Male', -1.3277, 20.6085, 0.12603, 15.056, 20.608, 22.554, 35.658),
(195, 'Male', -1.3149, 20.6644, 0.12615, 15.085, 20.664, 22.616, 35.693),
(196, 'Male', -1.3021, 20.7197, 0.12627, 15.113, 20.72, 22.677, 35.728),
(197, 'Male', -1.2892, 20.7745, 0.12638, 15.141, 20.774, 22.738, 35.759),
(198, 'Male', -1.2762, 20.8287, 0.1265, 15.168, 20.829, 22.798, 35.792),
(199, 'Male', -1.2631, 20.8824, 0.12661, 15.195, 20.882, 22.857, 35.821),
(200, 'Male', -1.2499, 20.9355, 0.12672, 15.221, 20.936, 22.916, 35.849),
(201, 'Male', -1.2366, 20.9881, 0.12683, 15.247, 20.988, 22.974, 35.875),
(202, 'Male', -1.2233, 21.04, 0.12694, 15.272, 21.04, 23.032, 35.901),
(203, 'Male', -1.2098, 21.0914, 0.12704, 15.297, 21.091, 23.088, 35.924),
(204, 'Male', -1.1962, 21.1423, 0.12715, 15.321, 21.142, 23.145, 35.947),
(205, 'Male', -1.1826, 21.1925, 0.12726, 15.344, 21.192, 23.2, 35.97),
(206, 'Male', -1.1688, 21.2423, 0.12736, 15.367, 21.242, 23.255, 35.989),
(207, 'Male', -1.155, 21.2914, 0.12746, 15.389, 21.291, 23.309, 36.007),
(208, 'Male', -1.141, 21.34, 0.12756, 15.411, 21.34, 23.363, 36.024),
(209, 'Male', -1.127, 21.388, 0.12767, 15.432, 21.388, 23.416, 36.042),
(210, 'Male', -1.1129, 21.4354, 0.12777, 15.452, 21.435, 23.468, 36.057),
(211, 'Male', -1.0986, 21.4822, 0.12787, 15.472, 21.482, 23.52, 36.071),
(212, 'Male', -1.0843, 21.5285, 0.12797, 15.491, 21.528, 23.571, 36.084),
(213, 'Male', -1.0699, 21.5742, 0.12807, 15.51, 21.574, 23.621, 36.096),
(214, 'Male', -1.0553, 21.6193, 0.12816, 15.528, 21.619, 23.67, 36.105),
(215, 'Male', -1.0407, 21.6638, 0.12826, 15.546, 21.664, 23.72, 36.115),
(216, 'Male', -1.026, 21.7077, 0.12836, 15.563, 21.708, 23.768, 36.124),
(217, 'Male', -1.0112, 21.751, 0.12845, 15.579, 21.751, 23.815, 36.13),
(218, 'Male', -0.9962, 21.7937, 0.12855, 15.595, 21.794, 23.862, 36.136),
(219, 'Male', -0.9812, 21.8358, 0.12864, 15.61, 21.836, 23.909, 36.14),
(220, 'Male', -0.9661, 21.8773, 0.12874, 15.624, 21.877, 23.954, 36.145),
(221, 'Male', -0.9509, 21.9182, 0.12883, 15.638, 21.918, 23.999, 36.148),
(222, 'Male', -0.9356, 21.9585, 0.12893, 15.651, 21.958, 24.043, 36.151),
(223, 'Male', -0.9202, 21.9982, 0.12902, 15.663, 21.998, 24.087, 36.151),
(224, 'Male', -0.9048, 22.0374, 0.12911, 15.675, 22.037, 24.13, 36.151),
(225, 'Male', -0.8892, 22.076, 0.1292, 15.687, 22.076, 24.172, 36.15),
(226, 'Male', -0.8735, 22.114, 0.1293, 15.697, 22.114, 24.214, 36.149),
(227, 'Male', -0.8578, 22.1514, 0.12939, 15.707, 22.151, 24.255, 36.147),
(228, 'Male', -0.8419, 22.1883, 0.12948, 15.717, 22.188, 24.295, 36.143);
GO