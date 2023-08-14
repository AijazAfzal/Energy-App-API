using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Energie.Infrastructure.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnergyAnalysis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyAnalysis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HelpCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Month",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Month", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notification_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAdmin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAdmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAdmin_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyHelpCategorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyHelpCategorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyHelpCategorys_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergyAnalysisQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnergyAnalysisID = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyAnalysisQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergyAnalysisQuestions_EnergyAnalysis_EnergyAnalysisID",
                        column: x => x.EnergyAnalysisID,
                        principalTable: "EnergyAnalysis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyHelps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnContribution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requestvia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Conditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HelpCategoryId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyHelps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyHelps_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyHelps_HelpCategory_HelpCategoryId",
                        column: x => x.HelpCategoryId,
                        principalTable: "HelpCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDepartmentHelp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contribution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requestvia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoreInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    HelpCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDepartmentHelp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDepartmentHelp_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyDepartmentHelp_HelpCategory_HelpCategoryId",
                        column: x => x.HelpCategoryId,
                        principalTable: "HelpCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: true),
                    LanguageID = table.Column<int>(type: "int", nullable: true),
                    Is_Notification = table.Column<bool>(type: "bit", nullable: false),
                    ShowOnboarding = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUser_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyUser_Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DepatrmentTip",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnergyAnalysisQuestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepatrmentTip", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DepatrmentTip_EnergyAnalysisQuestions_EnergyAnalysisQuestionsId",
                        column: x => x.EnergyAnalysisQuestionsId,
                        principalTable: "EnergyAnalysisQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnergyAnalysisQuestionsId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tips_EnergyAnalysisQuestions_EnergyAnalysisQuestionsId",
                        column: x => x.EnergyAnalysisQuestionsId,
                        principalTable: "EnergyAnalysisQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DepartmentEnergyPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavouriteTipId = table.Column<int>(type: "int", nullable: false),
                    TipTypeId = table.Column<int>(type: "int", nullable: false),
                    ResponsiblePersonId = table.Column<int>(type: "int", nullable: true),
                    CompanyUserId = table.Column<int>(type: "int", nullable: false),
                    PlanEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanStatusId = table.Column<int>(type: "int", nullable: false),
                    IsReminder = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentEnergyPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentEnergyPlans_CompanyUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentEnergyPlans_CompanyUser_ResponsiblePersonId",
                        column: x => x.ResponsiblePersonId,
                        principalTable: "CompanyUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DepartmentEnergyPlans_PlanStatus_PlanStatusId",
                        column: x => x.PlanStatusId,
                        principalTable: "PlanStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentEnergyPlans_TipType_TipTypeId",
                        column: x => x.TipTypeId,
                        principalTable: "TipType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentFavouriteHelps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyDepartmentHelpId = table.Column<int>(type: "int", nullable: false),
                    CompanyUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentFavouriteHelps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentFavouriteHelps_CompanyDepartmentHelp_CompanyDepartmentHelpId",
                        column: x => x.CompanyDepartmentHelpId,
                        principalTable: "CompanyDepartmentHelp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentFavouriteHelps_CompanyUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergyPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FavouriteTipId = table.Column<int>(type: "int", nullable: false),
                    TipTypeId = table.Column<int>(type: "int", nullable: false),
                    ResponsiblePersonId = table.Column<int>(type: "int", nullable: true),
                    CompanyUserId = table.Column<int>(type: "int", nullable: false),
                    PlanEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanStatusId = table.Column<int>(type: "int", nullable: false),
                    IsReminder = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergyPlan_CompanyUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergyPlan_CompanyUser_ResponsiblePersonId",
                        column: x => x.ResponsiblePersonId,
                        principalTable: "CompanyUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EnergyPlan_PlanStatus_PlanStatusId",
                        column: x => x.PlanStatusId,
                        principalTable: "PlanStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergyPlan_TipType_TipTypeId",
                        column: x => x.TipTypeId,
                        principalTable: "TipType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergyScore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    MonthId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CompanyUserID = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergyScore_CompanyUser_CompanyUserID",
                        column: x => x.CompanyUserID,
                        principalTable: "CompanyUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EnergyScore_Month_MonthId",
                        column: x => x.MonthId,
                        principalTable: "Month",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CompanyUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Feedbacks_CompanyUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyNotifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyUserID = table.Column<int>(type: "int", nullable: true),
                    MonthId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    PopUp = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyNotifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MonthlyNotifications_CompanyUser_CompanyUserID",
                        column: x => x.CompanyUserID,
                        principalTable: "CompanyUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MonthlyNotifications_Month_MonthId",
                        column: x => x.MonthId,
                        principalTable: "Month",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyUserID = table.Column<int>(type: "int", nullable: true),
                    NotificationTypeId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_CompanyUser_CompanyUserID",
                        column: x => x.CompanyUserID,
                        principalTable: "CompanyUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationTypes_NotificationTypeId",
                        column: x => x.NotificationTypeId,
                        principalTable: "NotificationTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserDepartmentTip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnergyAnalysisQuestionsId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDepartmentTip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDepartmentTip_CompanyUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDepartmentTip_EnergyAnalysisQuestions_EnergyAnalysisQuestionsId",
                        column: x => x.EnergyAnalysisQuestionsId,
                        principalTable: "EnergyAnalysisQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEnergyAnalyses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnergyAnalysisQuestionsID = table.Column<int>(type: "int", nullable: true),
                    CompanyUserID = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEnergyAnalyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEnergyAnalyses_CompanyUser_CompanyUserID",
                        column: x => x.CompanyUserID,
                        principalTable: "CompanyUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserEnergyAnalyses_EnergyAnalysisQuestions_EnergyAnalysisQuestionsID",
                        column: x => x.EnergyAnalysisQuestionsID,
                        principalTable: "EnergyAnalysisQuestions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserFavouriteHelp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyHelpID = table.Column<int>(type: "int", nullable: false),
                    CompanyUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavouriteHelp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavouriteHelp_CompanyHelps_CompanyHelpID",
                        column: x => x.CompanyHelpID,
                        principalTable: "CompanyHelps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavouriteHelp_CompanyUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnergyAnalysisQuestionsId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyUserID = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTip_CompanyUser_CompanyUserID",
                        column: x => x.CompanyUserID,
                        principalTable: "CompanyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTip_EnergyAnalysisQuestions_EnergyAnalysisQuestionsId",
                        column: x => x.EnergyAnalysisQuestionsId,
                        principalTable: "EnergyAnalysisQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentFavouriteTip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentTipId = table.Column<int>(type: "int", nullable: false),
                    CompanyUserId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentFavouriteTip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentFavouriteTip_CompanyUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentFavouriteTip_DepatrmentTip_DepartmentTipId",
                        column: x => x.DepartmentTipId,
                        principalTable: "DepatrmentTip",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikeTip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyUserID = table.Column<int>(type: "int", nullable: false),
                    DepartmentTipId = table.Column<int>(type: "int", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeTip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeTip_CompanyUser_CompanyUserID",
                        column: x => x.CompanyUserID,
                        principalTable: "CompanyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeTip_DepatrmentTip_DepartmentTipId",
                        column: x => x.DepartmentTipId,
                        principalTable: "DepatrmentTip",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavouriteTips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipId = table.Column<int>(type: "int", nullable: false),
                    CompanyUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavouriteTips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFavouriteTips_CompanyUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavouriteTips_Tips_TipId",
                        column: x => x.TipId,
                        principalTable: "Tips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_Name",
                table: "Company",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAdmin_CompanyId",
                table: "CompanyAdmin",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAdmin_Email",
                table: "CompanyAdmin",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDepartmentHelp_DepartmentId",
                table: "CompanyDepartmentHelp",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDepartmentHelp_HelpCategoryId",
                table: "CompanyDepartmentHelp",
                column: "HelpCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHelpCategorys_CompanyId",
                table: "CompanyHelpCategorys",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHelps_CompanyId",
                table: "CompanyHelps",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyHelps_HelpCategoryId",
                table: "CompanyHelps",
                column: "HelpCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_DepartmentID",
                table: "CompanyUser",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_Email",
                table: "CompanyUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_LanguageID",
                table: "CompanyUser",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CompanyId",
                table: "Department",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentEnergyPlans_CompanyUserId",
                table: "DepartmentEnergyPlans",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentEnergyPlans_PlanStatusId",
                table: "DepartmentEnergyPlans",
                column: "PlanStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentEnergyPlans_ResponsiblePersonId",
                table: "DepartmentEnergyPlans",
                column: "ResponsiblePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentEnergyPlans_TipTypeId",
                table: "DepartmentEnergyPlans",
                column: "TipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentFavouriteHelps_CompanyDepartmentHelpId",
                table: "DepartmentFavouriteHelps",
                column: "CompanyDepartmentHelpId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentFavouriteHelps_CompanyUserId",
                table: "DepartmentFavouriteHelps",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentFavouriteTip_CompanyUserId",
                table: "DepartmentFavouriteTip",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentFavouriteTip_DepartmentTipId",
                table: "DepartmentFavouriteTip",
                column: "DepartmentTipId");

            migrationBuilder.CreateIndex(
                name: "IX_DepatrmentTip_EnergyAnalysisQuestionsId",
                table: "DepatrmentTip",
                column: "EnergyAnalysisQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyAnalysisQuestions_EnergyAnalysisID",
                table: "EnergyAnalysisQuestions",
                column: "EnergyAnalysisID");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyPlan_CompanyUserId",
                table: "EnergyPlan",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyPlan_PlanStatusId",
                table: "EnergyPlan",
                column: "PlanStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyPlan_ResponsiblePersonId",
                table: "EnergyPlan",
                column: "ResponsiblePersonId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyPlan_TipTypeId",
                table: "EnergyPlan",
                column: "TipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyScore_CompanyUserID",
                table: "EnergyScore",
                column: "CompanyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyScore_MonthId",
                table: "EnergyScore",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CompanyUserId",
                table: "Feedbacks",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeTip_CompanyUserID",
                table: "LikeTip",
                column: "CompanyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_LikeTip_DepartmentTipId",
                table: "LikeTip",
                column: "DepartmentTipId");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyNotifications_CompanyUserID",
                table: "MonthlyNotifications",
                column: "CompanyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyNotifications_MonthId",
                table: "MonthlyNotifications",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CompanyUserID",
                table: "Notifications",
                column: "CompanyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationTypeId",
                table: "Notifications",
                column: "NotificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tips_EnergyAnalysisQuestionsId",
                table: "Tips",
                column: "EnergyAnalysisQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartmentTip_CompanyUserId",
                table: "UserDepartmentTip",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartmentTip_EnergyAnalysisQuestionsId",
                table: "UserDepartmentTip",
                column: "EnergyAnalysisQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEnergyAnalyses_CompanyUserID",
                table: "UserEnergyAnalyses",
                column: "CompanyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserEnergyAnalyses_EnergyAnalysisQuestionsID",
                table: "UserEnergyAnalyses",
                column: "EnergyAnalysisQuestionsID");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouriteHelp_CompanyHelpID",
                table: "UserFavouriteHelp",
                column: "CompanyHelpID");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouriteHelp_CompanyUserId",
                table: "UserFavouriteHelp",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouriteTips_CompanyUserId",
                table: "UserFavouriteTips",
                column: "CompanyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavouriteTips_TipId",
                table: "UserFavouriteTips",
                column: "TipId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTip_CompanyUserID",
                table: "UserTip",
                column: "CompanyUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTip_EnergyAnalysisQuestionsId",
                table: "UserTip",
                column: "EnergyAnalysisQuestionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "CompanyAdmin");

            migrationBuilder.DropTable(
                name: "CompanyHelpCategorys");

            migrationBuilder.DropTable(
                name: "DepartmentEnergyPlans");

            migrationBuilder.DropTable(
                name: "DepartmentFavouriteHelps");

            migrationBuilder.DropTable(
                name: "DepartmentFavouriteTip");

            migrationBuilder.DropTable(
                name: "EnergyPlan");

            migrationBuilder.DropTable(
                name: "EnergyScore");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "LikeTip");

            migrationBuilder.DropTable(
                name: "MonthlyNotifications");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "UserDepartmentTip");

            migrationBuilder.DropTable(
                name: "UserEnergyAnalyses");

            migrationBuilder.DropTable(
                name: "UserFavouriteHelp");

            migrationBuilder.DropTable(
                name: "UserFavouriteTips");

            migrationBuilder.DropTable(
                name: "UserTip");

            migrationBuilder.DropTable(
                name: "CompanyDepartmentHelp");

            migrationBuilder.DropTable(
                name: "PlanStatus");

            migrationBuilder.DropTable(
                name: "TipType");

            migrationBuilder.DropTable(
                name: "DepatrmentTip");

            migrationBuilder.DropTable(
                name: "Month");

            migrationBuilder.DropTable(
                name: "NotificationTypes");

            migrationBuilder.DropTable(
                name: "CompanyHelps");

            migrationBuilder.DropTable(
                name: "Tips");

            migrationBuilder.DropTable(
                name: "CompanyUser");

            migrationBuilder.DropTable(
                name: "HelpCategory");

            migrationBuilder.DropTable(
                name: "EnergyAnalysisQuestions");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "EnergyAnalysis");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
