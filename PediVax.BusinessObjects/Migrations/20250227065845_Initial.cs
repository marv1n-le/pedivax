using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PediVax.BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ChildProfiles_ChildId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Payments_PaymentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_VaccinePackages_VaccinePackageId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Vaccines_VaccineId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildProfiles_Users_UserId",
                table: "ChildProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_Payments_PaymentId",
                table: "PaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_VaccinePackages_VaccinePackageId",
                table: "PaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_VaccinePackages_VaccinePackageId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Vaccines_VaccineId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationRecords_Appointments_AppointmentId",
                table: "VaccinationRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineDiseases_Diseases_DiseaseId",
                table: "VaccineDiseases");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineDiseases_Vaccines_VaccineId",
                table: "VaccineDiseases");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineDoses_Vaccines_VaccineId",
                table: "VaccineDoses");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccinePackageDetails_VaccinePackages_PackageId",
                table: "VaccinePackageDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccinePackageDetails_Vaccines_VaccineId",
                table: "VaccinePackageDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineProfileDetails_VaccineProfiles_VaccineProfileId",
                table: "VaccineProfileDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineProfileDetails_Vaccines_VaccineId",
                table: "VaccineProfileDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineProfiles_Appointments_AppointmentId",
                table: "VaccineProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineProfiles_ChildProfiles_ChildId",
                table: "VaccineProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccines",
                table: "Vaccines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccineProfiles",
                table: "VaccineProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccineProfileDetails",
                table: "VaccineProfileDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccinePackages",
                table: "VaccinePackages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccinePackageDetails",
                table: "VaccinePackageDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccineDoses",
                table: "VaccineDoses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccineDiseases",
                table: "VaccineDiseases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccinationRecords",
                table: "VaccinationRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentDetails",
                table: "PaymentDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diseases",
                table: "Diseases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChildProfiles",
                table: "ChildProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Vaccines",
                newName: "Vaccine");

            migrationBuilder.RenameTable(
                name: "VaccineProfiles",
                newName: "VaccineProfile");

            migrationBuilder.RenameTable(
                name: "VaccineProfileDetails",
                newName: "VaccineProfileDetail");

            migrationBuilder.RenameTable(
                name: "VaccinePackages",
                newName: "VaccinePackage");

            migrationBuilder.RenameTable(
                name: "VaccinePackageDetails",
                newName: "VaccinePackageDetail");

            migrationBuilder.RenameTable(
                name: "VaccineDoses",
                newName: "VaccineDose");

            migrationBuilder.RenameTable(
                name: "VaccineDiseases",
                newName: "VaccineDisease");

            migrationBuilder.RenameTable(
                name: "VaccinationRecords",
                newName: "VaccinationRecord");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameTable(
                name: "PaymentDetails",
                newName: "PaymentDetail");

            migrationBuilder.RenameTable(
                name: "Diseases",
                newName: "Disease");

            migrationBuilder.RenameTable(
                name: "ChildProfiles",
                newName: "ChildProfile");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineProfiles_ChildId",
                table: "VaccineProfile",
                newName: "IX_VaccineProfile_ChildId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineProfiles_AppointmentId",
                table: "VaccineProfile",
                newName: "IX_VaccineProfile_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineProfileDetails_VaccineProfileId",
                table: "VaccineProfileDetail",
                newName: "IX_VaccineProfileDetail_VaccineProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineProfileDetails_VaccineId",
                table: "VaccineProfileDetail",
                newName: "IX_VaccineProfileDetail_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccinePackageDetails_VaccineId",
                table: "VaccinePackageDetail",
                newName: "IX_VaccinePackageDetail_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccinePackageDetails_PackageId",
                table: "VaccinePackageDetail",
                newName: "IX_VaccinePackageDetail_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineDoses_VaccineId",
                table: "VaccineDose",
                newName: "IX_VaccineDose_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineDiseases_VaccineId",
                table: "VaccineDisease",
                newName: "IX_VaccineDisease_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineDiseases_DiseaseId",
                table: "VaccineDisease",
                newName: "IX_VaccineDisease_DiseaseId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccinationRecords_AppointmentId",
                table: "VaccinationRecord",
                newName: "IX_VaccinationRecord_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_VaccinePackageId",
                table: "Payment",
                newName: "IX_Payment_VaccinePackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_VaccineId",
                table: "Payment",
                newName: "IX_Payment_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDetails_VaccinePackageId",
                table: "PaymentDetail",
                newName: "IX_PaymentDetail_VaccinePackageId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDetails_PaymentId",
                table: "PaymentDetail",
                newName: "IX_PaymentDetail_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildProfiles_UserId",
                table: "ChildProfile",
                newName: "IX_ChildProfile_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_VaccinePackageId",
                table: "Appointment",
                newName: "IX_Appointment_VaccinePackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_VaccineId",
                table: "Appointment",
                newName: "IX_Appointment_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_PaymentId",
                table: "Appointment",
                newName: "IX_Appointment_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ChildId",
                table: "Appointment",
                newName: "IX_Appointment_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccine",
                table: "Vaccine",
                column: "VaccineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccineProfile",
                table: "VaccineProfile",
                column: "VaccineProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccineProfileDetail",
                table: "VaccineProfileDetail",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccinePackage",
                table: "VaccinePackage",
                column: "PackageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccinePackageDetail",
                table: "VaccinePackageDetail",
                column: "PackageDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccineDose",
                table: "VaccineDose",
                column: "DoseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccineDisease",
                table: "VaccineDisease",
                column: "VaccineDiseaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccinationRecord",
                table: "VaccinationRecord",
                column: "RecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentDetail",
                table: "PaymentDetail",
                column: "PaymentDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disease",
                table: "Disease",
                column: "DiseaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChildProfile",
                table: "ChildProfile",
                column: "ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_ChildProfile_ChildId",
                table: "Appointment",
                column: "ChildId",
                principalTable: "ChildProfile",
                principalColumn: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Payment_PaymentId",
                table: "Appointment",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_VaccinePackage_VaccinePackageId",
                table: "Appointment",
                column: "VaccinePackageId",
                principalTable: "VaccinePackage",
                principalColumn: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Vaccine_VaccineId",
                table: "Appointment",
                column: "VaccineId",
                principalTable: "Vaccine",
                principalColumn: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildProfile_User_UserId",
                table: "ChildProfile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_VaccinePackage_VaccinePackageId",
                table: "Payment",
                column: "VaccinePackageId",
                principalTable: "VaccinePackage",
                principalColumn: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Vaccine_VaccineId",
                table: "Payment",
                column: "VaccineId",
                principalTable: "Vaccine",
                principalColumn: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetail_Payment_PaymentId",
                table: "PaymentDetail",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetail_VaccinePackage_VaccinePackageId",
                table: "PaymentDetail",
                column: "VaccinePackageId",
                principalTable: "VaccinePackage",
                principalColumn: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationRecord_Appointment_AppointmentId",
                table: "VaccinationRecord",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineDisease_Disease_DiseaseId",
                table: "VaccineDisease",
                column: "DiseaseId",
                principalTable: "Disease",
                principalColumn: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineDisease_Vaccine_VaccineId",
                table: "VaccineDisease",
                column: "VaccineId",
                principalTable: "Vaccine",
                principalColumn: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineDose_Vaccine_VaccineId",
                table: "VaccineDose",
                column: "VaccineId",
                principalTable: "Vaccine",
                principalColumn: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinePackageDetail_VaccinePackage_PackageId",
                table: "VaccinePackageDetail",
                column: "PackageId",
                principalTable: "VaccinePackage",
                principalColumn: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinePackageDetail_Vaccine_VaccineId",
                table: "VaccinePackageDetail",
                column: "VaccineId",
                principalTable: "Vaccine",
                principalColumn: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineProfile_Appointment_AppointmentId",
                table: "VaccineProfile",
                column: "AppointmentId",
                principalTable: "Appointment",
                principalColumn: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineProfile_ChildProfile_ChildId",
                table: "VaccineProfile",
                column: "ChildId",
                principalTable: "ChildProfile",
                principalColumn: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineProfileDetail_VaccineProfile_VaccineProfileId",
                table: "VaccineProfileDetail",
                column: "VaccineProfileId",
                principalTable: "VaccineProfile",
                principalColumn: "VaccineProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineProfileDetail_Vaccine_VaccineId",
                table: "VaccineProfileDetail",
                column: "VaccineId",
                principalTable: "Vaccine",
                principalColumn: "VaccineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_ChildProfile_ChildId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Payment_PaymentId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_VaccinePackage_VaccinePackageId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Vaccine_VaccineId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_ChildProfile_User_UserId",
                table: "ChildProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_VaccinePackage_VaccinePackageId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Vaccine_VaccineId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetail_Payment_PaymentId",
                table: "PaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetail_VaccinePackage_VaccinePackageId",
                table: "PaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccinationRecord_Appointment_AppointmentId",
                table: "VaccinationRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineDisease_Disease_DiseaseId",
                table: "VaccineDisease");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineDisease_Vaccine_VaccineId",
                table: "VaccineDisease");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineDose_Vaccine_VaccineId",
                table: "VaccineDose");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccinePackageDetail_VaccinePackage_PackageId",
                table: "VaccinePackageDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccinePackageDetail_Vaccine_VaccineId",
                table: "VaccinePackageDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineProfile_Appointment_AppointmentId",
                table: "VaccineProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineProfile_ChildProfile_ChildId",
                table: "VaccineProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineProfileDetail_VaccineProfile_VaccineProfileId",
                table: "VaccineProfileDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineProfileDetail_Vaccine_VaccineId",
                table: "VaccineProfileDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccineProfileDetail",
                table: "VaccineProfileDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccineProfile",
                table: "VaccineProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccinePackageDetail",
                table: "VaccinePackageDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccinePackage",
                table: "VaccinePackage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccineDose",
                table: "VaccineDose");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccineDisease",
                table: "VaccineDisease");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vaccine",
                table: "Vaccine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VaccinationRecord",
                table: "VaccinationRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentDetail",
                table: "PaymentDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disease",
                table: "Disease");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChildProfile",
                table: "ChildProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "VaccineProfileDetail",
                newName: "VaccineProfileDetails");

            migrationBuilder.RenameTable(
                name: "VaccineProfile",
                newName: "VaccineProfiles");

            migrationBuilder.RenameTable(
                name: "VaccinePackageDetail",
                newName: "VaccinePackageDetails");

            migrationBuilder.RenameTable(
                name: "VaccinePackage",
                newName: "VaccinePackages");

            migrationBuilder.RenameTable(
                name: "VaccineDose",
                newName: "VaccineDoses");

            migrationBuilder.RenameTable(
                name: "VaccineDisease",
                newName: "VaccineDiseases");

            migrationBuilder.RenameTable(
                name: "Vaccine",
                newName: "Vaccines");

            migrationBuilder.RenameTable(
                name: "VaccinationRecord",
                newName: "VaccinationRecords");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "PaymentDetail",
                newName: "PaymentDetails");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "Disease",
                newName: "Diseases");

            migrationBuilder.RenameTable(
                name: "ChildProfile",
                newName: "ChildProfiles");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineProfileDetail_VaccineProfileId",
                table: "VaccineProfileDetails",
                newName: "IX_VaccineProfileDetails_VaccineProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineProfileDetail_VaccineId",
                table: "VaccineProfileDetails",
                newName: "IX_VaccineProfileDetails_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineProfile_ChildId",
                table: "VaccineProfiles",
                newName: "IX_VaccineProfiles_ChildId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineProfile_AppointmentId",
                table: "VaccineProfiles",
                newName: "IX_VaccineProfiles_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccinePackageDetail_VaccineId",
                table: "VaccinePackageDetails",
                newName: "IX_VaccinePackageDetails_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccinePackageDetail_PackageId",
                table: "VaccinePackageDetails",
                newName: "IX_VaccinePackageDetails_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineDose_VaccineId",
                table: "VaccineDoses",
                newName: "IX_VaccineDoses_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineDisease_VaccineId",
                table: "VaccineDiseases",
                newName: "IX_VaccineDiseases_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccineDisease_DiseaseId",
                table: "VaccineDiseases",
                newName: "IX_VaccineDiseases_DiseaseId");

            migrationBuilder.RenameIndex(
                name: "IX_VaccinationRecord_AppointmentId",
                table: "VaccinationRecords",
                newName: "IX_VaccinationRecords_AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDetail_VaccinePackageId",
                table: "PaymentDetails",
                newName: "IX_PaymentDetails_VaccinePackageId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDetail_PaymentId",
                table: "PaymentDetails",
                newName: "IX_PaymentDetails_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_VaccinePackageId",
                table: "Payments",
                newName: "IX_Payments_VaccinePackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_VaccineId",
                table: "Payments",
                newName: "IX_Payments_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_ChildProfile_UserId",
                table: "ChildProfiles",
                newName: "IX_ChildProfiles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_VaccinePackageId",
                table: "Appointments",
                newName: "IX_Appointments_VaccinePackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_VaccineId",
                table: "Appointments",
                newName: "IX_Appointments_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_PaymentId",
                table: "Appointments",
                newName: "IX_Appointments_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_ChildId",
                table: "Appointments",
                newName: "IX_Appointments_ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccineProfileDetails",
                table: "VaccineProfileDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccineProfiles",
                table: "VaccineProfiles",
                column: "VaccineProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccinePackageDetails",
                table: "VaccinePackageDetails",
                column: "PackageDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccinePackages",
                table: "VaccinePackages",
                column: "PackageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccineDoses",
                table: "VaccineDoses",
                column: "DoseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccineDiseases",
                table: "VaccineDiseases",
                column: "VaccineDiseaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vaccines",
                table: "Vaccines",
                column: "VaccineId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VaccinationRecords",
                table: "VaccinationRecords",
                column: "RecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentDetails",
                table: "PaymentDetails",
                column: "PaymentDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diseases",
                table: "Diseases",
                column: "DiseaseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChildProfiles",
                table: "ChildProfiles",
                column: "ChildId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ChildProfiles_ChildId",
                table: "Appointments",
                column: "ChildId",
                principalTable: "ChildProfiles",
                principalColumn: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Payments_PaymentId",
                table: "Appointments",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_VaccinePackages_VaccinePackageId",
                table: "Appointments",
                column: "VaccinePackageId",
                principalTable: "VaccinePackages",
                principalColumn: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Vaccines_VaccineId",
                table: "Appointments",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChildProfiles_Users_UserId",
                table: "ChildProfiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_Payments_PaymentId",
                table: "PaymentDetails",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_VaccinePackages_VaccinePackageId",
                table: "PaymentDetails",
                column: "VaccinePackageId",
                principalTable: "VaccinePackages",
                principalColumn: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_VaccinePackages_VaccinePackageId",
                table: "Payments",
                column: "VaccinePackageId",
                principalTable: "VaccinePackages",
                principalColumn: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Vaccines_VaccineId",
                table: "Payments",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinationRecords_Appointments_AppointmentId",
                table: "VaccinationRecords",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineDiseases_Diseases_DiseaseId",
                table: "VaccineDiseases",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "DiseaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineDiseases_Vaccines_VaccineId",
                table: "VaccineDiseases",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineDoses_Vaccines_VaccineId",
                table: "VaccineDoses",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinePackageDetails_VaccinePackages_PackageId",
                table: "VaccinePackageDetails",
                column: "PackageId",
                principalTable: "VaccinePackages",
                principalColumn: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccinePackageDetails_Vaccines_VaccineId",
                table: "VaccinePackageDetails",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineProfileDetails_VaccineProfiles_VaccineProfileId",
                table: "VaccineProfileDetails",
                column: "VaccineProfileId",
                principalTable: "VaccineProfiles",
                principalColumn: "VaccineProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineProfileDetails_Vaccines_VaccineId",
                table: "VaccineProfileDetails",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "VaccineId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineProfiles_Appointments_AppointmentId",
                table: "VaccineProfiles",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineProfiles_ChildProfiles_ChildId",
                table: "VaccineProfiles",
                column: "ChildId",
                principalTable: "ChildProfiles",
                principalColumn: "ChildId");
        }
    }
}
