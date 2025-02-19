IF EXISTS (SELECT * FROM sys.databases WHERE name = 'SWP391_ChildGrowthTrackingSystem')
BEGIN
    DROP DATABASE SWP391_ChildGrowthTrackingSystem;
END

CREATE DATABASE SWP391_ChildGrowthTrackingSystem;

USE SWP391_ChildGrowthTrackingSystem;

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
    Name VARCHAR(255) NOT NULL,                             -- Tên người dùng
    Gender NVARCHAR(20) NOT NULL,                           -- Giới tính (Female, Male, Other)
    DateOfBirth DATE,                                       -- Ngày sinh
    Address VARCHAR(255) NOT NULL,                          -- Địa chỉ
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
    EmergencyContact VARCHAR(255) NOT NULL,                 -- Thông tin liên lạc khẩn cấp
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
    Name VARCHAR(255) NOT NULL,                             -- Tên trẻ
    DateOfBirth DATE NOT NULL,                              -- Ngày sinh
    Gender NVARCHAR(20) NOT NULL,                           -- Giới tính (Female, Male, Other)
    BirthWeight FLOAT NOT NULL,                             -- Cân nặng lúc sinh
    BirthHeight FLOAT NOT NULL,                             -- Chiều cao lúc sinh
    BloodType VARCHAR(10),                                  -- Nhóm máu
    Allergies NVARCHAR(2000),                               -- Dị ứng (nếu có)
    Notes NVARCHAR(2000),                                   -- Ghi chú thêm
	RelationshipToMember VARCHAR(50) NOT NULL,              -- Mối quan hệ với member
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
    FOREIGN KEY (RecordedBy) REFERENCES UserAccounts(UserID) -- Liên kết với bảng UserAccounts
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
    PackageName VARCHAR(255) NOT NULL,                      -- Tên gói thành viên
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
    FeatureName VARCHAR(255) NOT NULL,
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
    TransactionType NVARCHAR(100) NOT NULL,         -- Loại giao dịch (ví dụ: Đăng ký, Mua thẻ, v.v.)
    PaymentMethod NVARCHAR(50) NOT NULL,            -- Cổng thanh toán (VnPay, MoMo, Internet Banking)
    TransactionDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Thời điểm giao dịch (ngày người dùng bắt đầu giao dịch)
    PaymentDate DATETIME,                           -- Thời điểm thanh toán (ngày thanh toán được xử lý thành công, cập nhật khi Status = 'Success')
    GatewayTransactionID VARCHAR(255),              -- Mã giao dịch từ cổng thanh toán (ví dụ: mã giao dịch VnPay, MoMo)
    PaymentStatus NVARCHAR(50) NOT NULL DEFAULT 'Active',-- Trạng thái thanh toán (Pending, Success, Failed, Cancelled, Refunded)
    Description VARCHAR(255),                       -- Mô tả giao dịch
    FOREIGN KEY (UserID) REFERENCES UserAccounts(UserID),
    FOREIGN KEY (MemberMembershipID) REFERENCES MemberMemberships(MemberMembershipID)
);

-- Table Doctors
CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),
    UserID UNIQUEIDENTIFIER NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PhoneNumber VARCHAR(20) UNIQUE NOT NULL,
    Degree VARCHAR(255) NOT NULL,
	HospitalName VARCHAR(255),                      -- Tên bệnh viện làm việc
    HospitalAddress VARCHAR(255),                   -- Địa chỉ bệnh viện
    Biography NVARCHAR(2000),
    Status NVARCHAR(50) NOT NULL DEFAULT 'Active',
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES UserAccounts(UserID)
);

-- Table Specializations
CREATE TABLE Specializations (
    SpecializationID INT PRIMARY KEY IDENTITY(1,1),             -- Sử dụng UNIQUEIDENTIFIER làm khóa chính và tự động tạo GUID
    SpecializationName VARCHAR(255) NOT NULL,                   -- Tên chuyên ngành không thể null
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
    RequestDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Ngày tạo yêu cầu tư vấn
    Description NVARCHAR(2000),                                      -- Mô tả chi tiết về yêu cầu tư vấn
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                  -- Trạng thái yêu cầu: 0 (pending), 1 (approved), 2 (rejected)
    Urgency NVARCHAR(50),                                            -- Mức độ khẩn cấp: low, medium, high
    Attachments VARCHAR(255),                                        -- Đường dẫn tệp đính kèm
    Category NVARCHAR(100),                                          -- Danh mục tư vấn: ví dụ: dinh dưỡng, phát triển, ...
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Thời gian yêu cầu được tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,           -- Thời gian cập nhật yêu cầu
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID),             -- Liên kết với bảng Members
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID)               -- Liên kết với bảng Children
);

-- Table ConsultationResponses
CREATE TABLE ConsultationResponses (
    ResponseID INT PRIMARY KEY IDENTITY(1,1),                         -- ID phản hồi duy nhất
    RequestID INT NOT NULL,                                           -- Liên kết với bảng ConsultationRequests
    DoctorID INT NOT NULL,                                            -- Liên kết với bảng Doctors
    ResponseDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,         -- Thời gian phản hồi
    Content NVARCHAR(2000) NOT NULL,                                  -- Nội dung phản hồi (bắt buộc)
    Attachments VARCHAR(255),                                         -- Đường dẫn tệp đính kèm (không bắt buộc)
    IsHelpful BIT NULL,                                               -- Đánh giá xem phản hồi có hữu ích không (NULL nếu không đánh giá)
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',                   -- Trạng thái phản hồi: 0 (pending), 1 (answered), 2 (resolved)
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,            -- Thời gian tạo phản hồi
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,            -- Thời gian cập nhật phản hồi
    FOREIGN KEY (RequestID) REFERENCES ConsultationRequests(RequestID), -- Liên kết với bảng ConsultationRequests
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)              -- Liên kết với bảng Doctors
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
    DiseaseName VARCHAR(100) NOT NULL,                      -- Tên bệnh
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
    Notes NVARCHAR(2000),                        -- Ghi chú
    Guidelines NVARCHAR(2000),                   -- Hướng dẫn
    Importance NVARCHAR(50) NOT NULL DEFAULT 'Medium', -- Độ quan trọng
    Category NVARCHAR(100),                     -- Nhóm mốc
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Ngày tạo
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, -- Ngày cập nhật
    PRIMARY KEY (ChildID, MilestoneID),         -- Khoá chính kết hợp
    FOREIGN KEY (ChildID) REFERENCES Children(ChildID), -- Liên kết đến bảng Children
    FOREIGN KEY (MilestoneID) REFERENCES Milestones(MilestoneID) -- Liên kết đến bảng Milestones
);

-- Table BlogCategories
CREATE TABLE BlogCategories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),                  -- ID thể loại
    CategoryName VARCHAR(255) NOT NULL,                        -- Tên thể loại bài viết (không được NULL)
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
    Title VARCHAR(255) NOT NULL,                               -- Tiêu đề bài viết
    Content NVARCHAR(2000) NOT NULL,                           -- Nội dung bài viết
    AuthorID UNIQUEIDENTIFIER NOT NULL,                        -- ID tác giả (admin hoặc người dùng)
	CategoryID INT NOT NULL,                                   -- ID thể loại bài viết
	ImageBlog NVARCHAR(2000) NOT NULL,                         -- URL hoặc đường dẫn tới ảnh
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending',            -- Trạng thái bài viết ('Pending', 'Approved', 'Rejected')
	RejectionReason NVARCHAR(2000),                            -- Lý do từ chối (nếu có)
    Tags VARCHAR(255),                                         -- Các tag của bài viết
    ReferenceSources NVARCHAR(2000),                           -- Các trích dẫn hoặc nguồn tài liệu tham khảo
	CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,     -- Thời gian tạo bài viết
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,     -- Thời gian cập nhật bài viết
    FOREIGN KEY (AuthorID) REFERENCES UserAccounts(UserID),    -- Liên kết với bảng UserAccounts (tác giả)
    FOREIGN KEY (CategoryID) REFERENCES BlogCategories(CategoryID)  -- Liên kết với bảng BlogCategories
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
INSERT INTO UserAccounts (Username, Email, PhoneNumber, Name, Gender, DateOfBirth, Address, Password, RegistrationDate, RoleID)
VALUES
('admin_user', 'admin@example.com', '0901234567', 'Admin User', 'Male', '1985-05-15', '123 Admin St.', 'password123', GETDATE(), 3),  -- Admin
('member_user_1', 'member1@example.com', '0902345678', 'Member User 1', 'Female', '1990-01-01', '123 Member St.', 'password123', GETDATE(), 1),  -- Member 1
('member_user_2', 'member2@example.com', '0903456789', 'Member User 2', 'Male', '1992-06-10', '456 Member St.', 'password123', GETDATE(), 1),  -- Member 2
('member_user_3', 'member3@example.com', '0904567890', 'Member User 3', 'Female', '1995-10-20', '789 Member St.', 'password123', GETDATE(), 1);  -- Member 3

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

-- Insert Promotions
INSERT INTO Promotions (PromotionCode, Description, DiscountPercent, MinPurchaseAmount, MaxDiscountAmount, 
                        ApplicablePackageIDs, TargetAudience, StartDate, EndDate, Status, RedemptionCount, UsageLimit, 
                        CreatedBy, ModifiedBy)
VALUES
('NEWYEAR25', 'Get 20% off on all membership packages for New Year', 20, 0, 200000, 
 '1,2,3', 'All members', '2025-01-01', '2025-01-10', 'Active', 0, 1000, 
 (SELECT UserID FROM UserAccounts WHERE Username = 'admin_user'), 
 (SELECT UserID FROM UserAccounts WHERE Username = 'admin_user')),

('FREEMONTH', 'Get 1 free month when subscribing to the Premium package', 100, 1279000, 1279000, 
 '3', 'New members', '2025-02-01', '2025-02-28', 'Active', 0, 500, 
 (SELECT UserID FROM UserAccounts WHERE Username = 'admin_user'), 
 (SELECT UserID FROM UserAccounts WHERE Username = 'admin_user')),

('SUMMER50', 'Get 50% off on the Standard package during the summer', 50, 379000, 189500, 
 '2', 'All members', '2025-06-01', '2025-06-30', 'Active', 0, 500, 
 (SELECT UserID FROM UserAccounts WHERE Username = 'admin_user'), 
 (SELECT UserID FROM UserAccounts WHERE Username = 'admin_user')),

('LOYALTY10', 'Get 10% off for members who have renewed for over a year', 10, 0, 500000, 
 '2,3', 'Loyal members', '2025-01-01', '2025-12-31', 'Active', 0, NULL, 
 (SELECT UserID FROM UserAccounts WHERE Username = 'admin_user'), 
 (SELECT UserID FROM UserAccounts WHERE Username = 'admin_user'));

GO

 -- Insert PackagePromotions
 INSERT INTO PackagePromotions (PackageID, PromotionID, IsActive)
VALUES
-- NEWYEAR25 Promotions
(1, (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'NEWYEAR25'), 1),
(2, (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'NEWYEAR25'), 1),
(3, (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'NEWYEAR25'), 1),

-- FREEMONTH Promotions
(3, (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'FREEMONTH'), 1),

-- SUMMER50 Promotions
(2, (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'SUMMER50'), 1),

-- LOYALTY10 Promotions
(2, (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'LOYALTY10'), 1),
(3, (SELECT PromotionID FROM Promotions WHERE PromotionCode = 'LOYALTY10'), 1);

GO

-- Insert Members
INSERT INTO Members (UserID, EmergencyContact, JoinDate, Status)
VALUES
((SELECT UserID FROM UserAccounts WHERE Username = 'member_user_1'), '123-456-789', GETDATE(), 'Active'),  -- Member 1
((SELECT UserID FROM UserAccounts WHERE Username = 'member_user_2'), '234-567-890', GETDATE(), 'Active'),  -- Member 2
((SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3'), '345-678-901', GETDATE(), 'Active');  -- Member 3

GO

-- Insert MemberMemberships
INSERT INTO MemberMemberships (MemberID, PackageID, StartDate, EndDate, Status)
VALUES
((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_1')), 
(SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Free'), GETDATE(), DATEADD(MONTH, 1, GETDATE()), 'Active'),  -- Member 1 - Free

((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_2')), 
(SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Free'), GETDATE(), DATEADD(MONTH, 1, GETDATE()), 'Inactive'),  -- Member 2 - Free, then Standard

((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_2')), 
(SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Standard'), DATEADD(MONTH, 1, GETDATE()), DATEADD(MONTH, 13, GETDATE()), 'Active'),  -- Member 2 - Standard

((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3')), 
(SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Free'), GETDATE(), DATEADD(MONTH, 1, GETDATE()), 'Inactive'),  -- Member 3 - Free, then Premium

((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3')), 
(SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Premium'), DATEADD(MONTH, 1, GETDATE()), DATEADD(MONTH, 13, GETDATE()), 'Active');  -- Member 3 - Premium

GO

-- Insert Transactions
INSERT INTO Transactions (UserID, MemberMembershipID, Amount, Currency, TransactionType, PaymentMethod, TransactionDate, PaymentDate, PaymentStatus)
VALUES
((SELECT UserID FROM UserAccounts WHERE Username = 'member_user_2'), 
(SELECT MemberMembershipID FROM MemberMemberships WHERE MemberID = (SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_2')) 
AND PackageID = (SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Standard')), 379000.00, 'VND', 'Purchase', 'VnPay', GETDATE(), GETDATE(), 'Success'),

((SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3'), 
(SELECT MemberMembershipID FROM MemberMemberships WHERE MemberID = (SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3')) 
AND PackageID = (SELECT PackageID FROM MembershipPackages WHERE PackageName = 'Premium')), 1279000.00, 'VND', 'Purchase', 'VnPay', GETDATE(), GETDATE(), 'Success');

GO

-- Insert Children
INSERT INTO Children (MemberID, Name, DateOfBirth, Gender, BirthWeight, BirthHeight, BloodType, Allergies, RelationshipToMember, Status)
VALUES
((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_1')), 
'Child 1', '2022-01-01', 'Female', 3.2, 50, 'O', NULL, 'Daughter', 'Active'),  -- Member 1
((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_2')), 
'Child 2', '2021-06-15', 'Male', 3.5, 52, 'A', NULL, 'Son', 'Active'),  -- Member 2
((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3')), 
'Child 3', '2020-12-10', 'Female', 3.0, 48, 'B', NULL, 'Daughter', 'Active'),  -- Member 3
((SELECT MemberID FROM Members WHERE UserID = (SELECT UserID FROM UserAccounts WHERE Username = 'member_user_3')), 
'Child 4', '2019-11-25', 'Male', 3.4, 49, 'AB', NULL, 'Son', 'Active');  -- Member 3

GO

--Insert Diseases
INSERT INTO Diseases (DiseaseName, LowerBoundMale, UpperBoundMale, LowerBoundFemale, UpperBoundFemale, MinAge, MaxAge, Severity, DiseaseType, Symptoms, Treatment, Prevention, Description, Notes, IsActive)
VALUES
    ('Obesity', 25.0, 40.0, 24.0, 39.0, 5, 18, 'High', 'Metabolic Disorder',
     'Excessive body fat, difficulty in breathing, fatigue, joint pain',
     'Balanced diet, regular exercise, medical consultation if needed',
     'Promote healthy eating habits, increase physical activity',
     'A condition characterized by excessive fat accumulation, leading to health risks.',
     'Monitor BMI regularly to prevent severe complications.', 1),

    ('Malnutrition', 10.0, 15.0, 9.0, 14.0, 1, 10, 'High', 'Nutritional Deficiency',
     'Underweight, weak immune system, delayed growth, brittle hair',
     'High-protein and nutrient-rich diet, vitamin supplements',
     'Ensure proper nutrition intake, promote breastfeeding for infants',
     'A condition resulting from lack of essential nutrients in the diet.',
     'Common in developing countries, affects cognitive development.', 1),

    ('Anemia', 11.0, 14.0, 10.0, 13.5, 3, 16, 'Medium', 'Blood Disorder',
     'Paleness, fatigue, shortness of breath, dizziness',
     'Iron supplements, iron-rich diet (red meat, leafy greens)',
     'Maintain a diet rich in iron and vitamin C, regular health check-ups',
     'A condition where there is a deficiency of red blood cells or hemoglobin in the blood.',
     'Common in children due to poor diet and rapid growth phases.', 1),

    ('Diabetes Type 1', 15.0, 25.0, 14.0, 24.0, 6, 18, 'High', 'Endocrine Disorder',
     'Frequent urination, excessive thirst, weight loss, fatigue',
     'Insulin therapy, carbohydrate management, regular monitoring',
     'Healthy diet, physical activity, routine glucose level checks',
     'A chronic disease where the pancreas produces little or no insulin.',
     'Requires lifelong insulin therapy to regulate blood sugar levels.', 1),

    ('Stunted Growth', 12.0, 18.0, 11.0, 17.0, 1, 10, 'Medium', 'Developmental Disorder',
     'Short stature, delayed physical development, cognitive impairment',
     'Nutritional supplements, balanced diet, medical monitoring',
     'Ensure adequate food intake, early intervention for nutrient deficiencies',
     'A condition where a child’s growth rate is significantly lower than expected.',
     'Can be caused by poor nutrition, infections, or genetic conditions.', 1),

    ('Asthma', 18.0, 28.0, 17.0, 27.0, 4, 18, 'Medium', 'Respiratory Disorder',
     'Shortness of breath, wheezing, coughing, chest tightness',
     'Inhalers, medication, avoiding triggers (allergens, smoke)',
     'Avoid smoking exposure, control allergens at home',
     'A chronic respiratory condition where the airways become inflamed and narrow.',
     'Can be triggered by allergens, pollution, or respiratory infections.', 1);
