IF EXISTS (SELECT * FROM sys.databases WHERE name = 'SWP391_ChildGrowthTrackingSystemDB')
BEGIN
    DROP DATABASE SWP391_ChildGrowthTrackingSystemDB;
END

CREATE DATABASE SWP391_ChildGrowthTrackingSystemDB;

USE SWP391_ChildGrowthTrackingSystemDB;

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
    RecordID INT PRIMARY KEY IDENTITY(1,1),          -- ID bản ghi
    ChildID UNIQUEIDENTIFIER NOT NULL,               -- ID của trẻ
    RecordedBy UNIQUEIDENTIFIER NOT NULL,            -- ID người ghi nhận
    Weight FLOAT NOT NULL,                           -- Cân nặng của trẻ
    Height FLOAT NOT NULL,                           -- Chiều cao của trẻ
    HeadCircumference FLOAT,                         -- Vòng đầu
    MuscleMass FLOAT,                                -- Khối lượng cơ bắp
    ChestCircumference FLOAT,                        -- Vòng ngực
    NutritionalStatus NVARCHAR(50),                  -- Tình trạng dinh dưỡng
    FerritinLevel FLOAT,                             -- Mức ferritin
    Triglycerides FLOAT,                             -- Mức triglycerides
    BloodSugarLevel FLOAT,                           -- Mức đường huyết
    PhysicalActivityLevel NVARCHAR(50),              -- Mức độ hoạt động thể chất
    HeartRate INT,                                   -- Nhịp tim
    BloodPressure FLOAT,                             -- Huyết áp
    BodyTemperature FLOAT,                           -- Nhiệt độ cơ thể
    OxygenSaturation FLOAT,                          -- Mức độ bão hòa oxy
    SleepDuration FLOAT,                             -- Thời gian ngủ
    Vision NVARCHAR(50),                             -- Tình trạng thị giác
    Hearing NVARCHAR(50),                            -- Tình trạng thính giác
    ImmunizationStatus NVARCHAR(2000),               -- Tình trạng tiêm chủng
    MentalHealthStatus NVARCHAR(50),                 -- Tình trạng sức khỏe tâm thần
    GrowthHormoneLevel FLOAT,                        -- Mức độ hormone tăng trưởng
    AttentionSpan NVARCHAR(50),                      -- Thời gian chú ý
    NeurologicalReflexes NVARCHAR(255),              -- Phản xạ thần kinh
    DevelopmentalMilestones NVARCHAR(255),           -- Mốc phát triển
    Notes NVARCHAR(2000),                            -- Ghi chú khác
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',   -- Trạng thái
    Verified BIT DEFAULT 0,                          -- Trạng thái xác nhận
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,    -- Thời gian tạo bản ghi
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,    -- Thời gian cập nhật bản ghi
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID), -- Liên kết với bảng Children
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
    MemberID UNIQUEIDENTIFIER NOT NULL,                      -- Liên kết với bảng Members
    PackageID INT NOT NULL,                                  -- Liên kết với bảng MembershipPackages
    StartDate DATETIME NOT NULL,                             -- Ngày bắt đầu gói thành viên
    EndDate DATETIME NOT NULL,                               -- Ngày kết thúc gói thành viên
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',           -- Trạng thái của gói thành viên (Active, Inactive, Pending, Suspended, Expired, Canceled, Renewing, Trial)
    IsActive BIT NOT NULL DEFAULT 1,                         -- Trạng thái hoạt động của gói
    Description NVARCHAR(2000),                              -- Mô tả gói thành viên
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian cập nhật
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID),     -- Liên kết với bảng Members
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
    DoctorSpecializationID INT PRIMARY KEY IDENTITY(1,1),  -- ID mối quan hệ bác sĩ-chuyên ngành
    DoctorID INT NOT NULL,                                 -- ID bác sĩ (liên kết với bảng Doctors)
    SpecializationID INT NOT NULL,                         -- ID chuyên ngành (liên kết với bảng Specializations)
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',         -- Trạng thái (1: active, 0: inactive)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Thời gian tạo mối quan hệ
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Thời gian cập nhật mối quan hệ
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID),   -- Liên kết với bảng Doctors
    FOREIGN KEY (SpecializationID) REFERENCES Specializations(SpecializationID) -- Liên kết với bảng Specializations
);

-- Table ConsultationRequests
CREATE TABLE ConsultationRequests (
    RequestID INT PRIMARY KEY IDENTITY(1,1),                         -- ID duy nhất cho yêu cầu
    MemberID UNIQUEIDENTIFIER NOT NULL,                              -- Liên kết với bảng Members
    ChildID UNIQUEIDENTIFIER NOT NULL,                               -- Liên kết với bảng Children
	DoctorID INT NOT NULL,											 -- Liên kết với bảng Doctor
    RequestDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Ngày tạo yêu cầu tư vấn
    Description NVARCHAR(2000),                                      -- Mô tả chi tiết về yêu cầu tư vấn
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                  -- Trạng thái yêu cầu: 0 (pending), 1 (approved), 2 (rejected)
    Urgency NVARCHAR(50),                                            -- Mức độ khẩn cấp: low, medium, high
    Attachments VARCHAR(1000),                                       -- Đường dẫn tệp đính kèm
    Category NVARCHAR(100),                                          -- Danh mục tư vấn: ví dụ: dinh dưỡng, phát triển, ...
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Thời gian yêu cầu được tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Thời gian cập nhật yêu cầu
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID),             -- Liên kết với bảng Members
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID),              -- Liên kết với bảng Children
	FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)              -- Liên kết với bảng Doctor
);

-- Table ConsultationResponses
CREATE TABLE ConsultationResponses (
    ResponseID INT PRIMARY KEY IDENTITY(1,1),                         -- ID phản hồi duy nhất
    RequestID INT UNIQUE NOT NULL,                                           -- Liên kết với bảng ConsultationRequests
    ResponseDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Thời gian phản hồi
    Content NVARCHAR(2000) NOT NULL,                                  -- Nội dung phản hồi (bắt buộc)
    Attachments VARCHAR(1000),                                        -- Đường dẫn tệp đính kèm (không bắt buộc)
    IsHelpful BIT NULL,                                               -- Đánh giá xem phản hồi có hữu ích không (NULL nếu không đánh giá)
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                   -- Trạng thái phản hồi: 0 (pending), 1 (answered), 2 (resolved)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,            -- Thời gian tạo phản hồi
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,            -- Thời gian cập nhật phản hồi
    FOREIGN KEY (RequestID) REFERENCES ConsultationRequests(RequestID), -- Liên kết với bảng ConsultationRequests
);

-- Table RatingFeedbacks
CREATE TABLE RatingFeedbacks (
    FeedbackId INT PRIMARY KEY IDENTITY(1,1),                -- ID phản hồi đánh giá
    UserId UNIQUEIDENTIFIER NOT NULL,                        -- Liên kết với bảng UserAccounts
    ResponseId INT NOT NULL,                                 -- Liên kết với bảng ConsultationResponses
    Rating INT NOT NULL CHECK (Rating BETWEEN 1 AND 5),      -- Đánh giá từ 1 đến 5
    Comment NVARCHAR(2000) NULL,                             -- Nhận xét (không bắt buộc)
    FeedbackDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,-- Thời gian phản hồi
    FeedbackType NVARCHAR(50) NULL DEFAULT 'general',        -- Loại phản hồi (không bắt buộc, mặc định là 'general')
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',          -- Trạng thái: 'Pending', 'Approved', 'Rejected'
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,   -- Thời gian cập nhật
    FOREIGN KEY (UserId) REFERENCES UserAccounts(UserId),    -- Liên kết với bảng UserAccounts
    FOREIGN KEY (ResponseId) REFERENCES ConsultationResponses(ResponseId) -- Liên kết với bảng ConsultationResponses
);

-- Table Diseases
CREATE TABLE Diseases (
    DiseaseID INT PRIMARY KEY IDENTITY(1,1),                -- ID bệnh
    DiseaseName NVARCHAR(100) NOT NULL,                     -- Tên bệnh
    LowerBoundMale FLOAT NOT NULL,                          -- Giới hạn thấp cho chỉ số đối với nam (cân nặng, chiều cao, BMI)
    UpperBoundMale FLOAT NOT NULL,                          -- Giới hạn cao cho chỉ số đối với nam
    LowerBoundFemale FLOAT NOT NULL,                        -- Giới hạn thấp cho chỉ số đối với nữ
    UpperBoundFemale FLOAT NOT NULL,                        -- Giới hạn cao cho chỉ số đối với nữ
    MinAge INT NOT NULL,                                    -- Độ tuổi nhỏ nhất có thể mắc bệnh
    MaxAge INT NOT NULL,                                    -- Độ tuổi lớn nhất có thể mắc bệnh
    Severity NVARCHAR(50) NOT NULL,                         -- Độ nghiêm trọng (High, Medium, Low)
    DiseaseType NVARCHAR(50) NOT NULL,                      -- Loại bệnh (ví dụ: Béo phì, Suy dinh dưỡng)
    Symptoms NVARCHAR(2000) NOT NULL,                       -- Triệu chứng bệnh
    Treatment NVARCHAR(2000),                               -- Phương pháp điều trị
    Prevention NVARCHAR(2000),                              -- Phương pháp phòng ngừa
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,  -- Thời gian tạo
    LastModified DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,-- Thời gian chỉnh sửa gần nhất
    Description NVARCHAR(2000),                              -- Mô tả về bệnh
    Notes NVARCHAR(2000),                                    -- Ghi chú thêm
    IsActive BIT NOT NULL DEFAULT 1                          -- Trạng thái (Hoạt động hoặc không)
);

-- Table Alerts
CREATE TABLE Alerts (
    AlertID INT PRIMARY KEY IDENTITY(1,1),
    GrowthRecordID INT NOT NULL,                           -- Liên kết với bảng GrowthRecords
    AlertDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Ngày giờ cảnh báo
    DiseaseID INT NOT NULL,                                -- Liên kết với bảng Diseases
    Message NVARCHAR(2000) NOT NULL,                       -- Nội dung cảnh báo
    IsRead BIT NOT NULL DEFAULT 0,                         -- Trạng thái đọc
    SeverityLevel NVARCHAR(50),                            -- Mức độ nghiêm trọng
    IsAcknowledged BIT NOT NULL DEFAULT 0,                 -- Trạng thái xác nhận
    FOREIGN KEY (GrowthRecordID) REFERENCES GrowthRecords(RecordID), -- Liên kết GrowthRecords
    FOREIGN KEY (DiseaseID) REFERENCES Diseases(DiseaseID) -- Liên kết với Diseases
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
    ChildID UNIQUEIDENTIFIER NOT NULL,          -- Liên kết với bảng Children
    MilestoneID INT NOT NULL,                   -- Liên kết với bảng Milestones
    AchievedDate DATE,                          -- Ngày đạt mốc
    Status NVARCHAR(50) NOT NULL DEFAULT 'Not Achieved', -- Trạng thái mốc (Not Achieved, Achieved)
    Notes NVARCHAR(2000),                       -- Ghi chú
    Guidelines NVARCHAR(2000),                  -- Hướng dẫn
    Importance NVARCHAR(50) NOT NULL DEFAULT 'Medium', -- Độ quan trọng
    Category NVARCHAR(100),                                      -- Nhóm mốc
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,       -- Ngày tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,       -- Ngày cập nhật
    PRIMARY KEY (ChildID, MilestoneID),                          -- Khoá chính kết hợp
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID),          -- Liên kết đến bảng Children
    FOREIGN KEY (MilestoneID) REFERENCES Milestones(MilestoneID) -- Liên kết đến bảng Milestones
);

-- Table BlogCategories
CREATE TABLE BlogCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),                  -- ID thể loại
    CategoryName NVARCHAR(255) NOT NULL,                       -- Tên thể loại bài viết (không được NULL)
    Description NVARCHAR(2000),                                -- Mô tả về thể loại
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,     -- Thời gian tạo thể loại
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,     -- Thời gian cập nhật thể loại
    IsActive BIT NOT NULL DEFAULT 1,                           -- Trạng thái thể loại (1: Active, 0: Inactive)
    ParentCategoryID INT,                                      -- ID thể loại cha (nếu có thể loại con)
    FOREIGN KEY (ParentCategoryID) REFERENCES BlogCategories(CategoryID)  -- Liên kết với thể loại cha
);

-- Table Blogs
CREATE TABLE Blogs (
    BlogID INT PRIMARY KEY IDENTITY(1,1),                      -- ID bài viết
    Title NVARCHAR(255) NOT NULL,                               -- Tiêu đề bài viết
    Content NVARCHAR(2000) NOT NULL,                           -- Nội dung bài viết
    AuthorID UNIQUEIDENTIFIER NOT NULL,                        -- ID tác giả (admin hoặc người dùng)
	CategoryID INT NOT NULL,                                   -- ID thể loại bài viết
	ImageBlog NVARCHAR(2000) NOT NULL,                         -- URL hoặc đường dẫn tới ảnh
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',            -- Trạng thái bài viết ('Pending', 'Approved', 'Rejected')
	RejectionReason NVARCHAR(2000),                            -- Lý do từ chối (nếu có)
    Tags NVARCHAR(255),                                         -- Các tag của bài viết
    ReferenceSources NVARCHAR(2000),                           -- Các trích dẫn hoặc nguồn tài liệu tham khảo
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,     -- Thời gian tạo bài viết
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,     -- Thời gian cập nhật bài viết
    FOREIGN KEY (AuthorID) REFERENCES UserAccounts(UserID),    -- Liên kết với bảng UserAccounts (tác giả)
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
    -- Getting Pregnant
    ('Ovulation', N'Kiến thức về rụng trứng', @GettingPregnant, 1),
    ('Fertility', N'Thông tin về khả năng sinh sản', @GettingPregnant, 1),
    ('Pregnancy Tests', N'Các phương pháp thử thai', @GettingPregnant, 1),

    -- Baby
    ('Breastfeeding', N'Hướng dẫn và lợi ích của việc cho con bú', @Baby, 1),
    ('Sleep Tips', N'Mẹo giúp bé ngủ ngon', @Baby, 1),
    ('Newborn Care', N'Chăm sóc trẻ sơ sinh', @Baby, 1),

    -- Toddler
    ('Potty Training', N'Cách hướng dẫn trẻ đi vệ sinh', @Toddler, 1),
    ('Nutrition', N'Dinh dưỡng cần thiết cho trẻ', @Toddler, 1),
    ('Preschool', N'Chuẩn bị cho trẻ đi mẫu giáo', @Toddler, 1),

    -- Child
    ('Education', N'Phát triển kỹ năng học tập', @Child, 1),
    ('Health Tips', N'Mẹo giữ sức khỏe cho trẻ', @Child, 1),
    ('Outdoor Activities', N'Các hoạt động ngoài trời', @Child, 1),

    -- Teenager
    ('Teen Mental Health', N'Sức khỏe tinh thần cho tuổi teen', @Teenager, 1),
    ('Social Media', N'Ảnh hưởng của mạng xã hội với tuổi teen', @Teenager, 1),
    ('Teen Education', N'Giáo dục dành cho tuổi teen', @Teenager, 1);

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
    (N'Dấu hiệu rụng trứng và cách theo dõi', 
    N'Rụng trứng đóng vai trò quan trọng trong việc thụ thai. Bài viết này giúp bạn nhận biết các dấu hiệu như dịch nhầy cổ tử cung, nhiệt độ cơ thể cơ bản, và bộ kit thử rụng trứng.', 
    @AdminID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Ovulation'),
    'https://example.com/images/ovulation-tracking.jpg', 'Approved', 
    N'rụng trứng, theo dõi rụng trứng, sinh sản',
    'https://www.mayoclinic.org, https://www.webmd.com',
    GETDATE(), GETDATE()),

    (N'Thử thai tại nhà có chính xác không?', 
    N'Thử thai tại nhà là phương pháp nhanh chóng để phát hiện mang thai. Bài viết này giải thích cách hoạt động, độ chính xác, và thời điểm tốt nhất để thử.', 
    @AdminID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Pregnancy Tests'),
    'https://example.com/images/pregnancy-test.jpg', 'Approved', 
    N'thử thai, kiểm tra thai kỳ, que thử thai',
    'https://www.healthline.com, https://www.nhs.uk',
    GETDATE(), GETDATE()),

    -- Bài viết của Doctor Huỳnh Nguyễn Ngọc Nguyên (Chuyên khoa Nhi)
    (N'Bí quyết giúp bé ngủ ngon', 
    N'Giấc ngủ rất quan trọng với sự phát triển của trẻ sơ sinh. Tìm hiểu các phương pháp giúp bé ngủ sâu hơn và tránh bị giật mình.', 
    @DoctorNguyenID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Sleep Tips'),
    'https://example.com/images/baby-sleep-routine.jpg', 'Approved', 
    N'giấc ngủ trẻ em, mẹo ngủ ngon, ngủ sâu',
    'https://www.sleepfoundation.org, https://www.aap.org',
    GETDATE(), GETDATE()),

    -- Bài viết của Doctor Huỳnh Thái Tú (Chuyên gia Dinh dưỡng)
    (N'Dinh dưỡng quan trọng cho trẻ nhỏ', 
    N'Trẻ nhỏ cần được cung cấp đầy đủ chất dinh dưỡng để phát triển toàn diện. Bài viết này liệt kê các nhóm thực phẩm thiết yếu.', 
    @DoctorTuID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Nutrition'),
    'https://example.com/images/toddler-nutrition.jpg', 'Approved', 
    N'dinh dưỡng trẻ em, thực đơn cho trẻ, ăn dặm',
    'https://www.nhs.uk, https://www.aap.org',
    GETDATE(), GETDATE()),

    -- Bài viết của Doctor Nguyễn Thị Thanh Thảo (Chuyên khoa Tim mạch & Tâm lý tuổi teen)
    (N'Bố mẹ nên làm gì khi con bước vào tuổi teen?', 
    N'Tuổi teen là giai đoạn trẻ thay đổi về thể chất và tâm lý. Bố mẹ cần hiểu cách hỗ trợ và đồng hành cùng con.', 
    @DoctorThaoID, (SELECT CategoryID FROM BlogCategories WHERE CategoryName = 'Teen Mental Health'),
    'https://example.com/images/teen-stress.jpg', 'Approved', 
    N'tâm lý tuổi teen, dạy con tuổi teen, hỗ trợ tinh thần',
    'https://www.psychologytoday.com, https://www.nimh.nih.gov',
    GETDATE(), GETDATE());


