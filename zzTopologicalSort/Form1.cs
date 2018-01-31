using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zzTopologicalSort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Main();
        }

        private void Main()
        {
            List<Item2> Tables;
            Tables = new List<Item2>();

            //// HEAD OFFICE TABLES
            //Tables.Add(new Item2("AccpacAutoEntry", "Manifests", "ManifestsPurchaseOrders", "Waybills", "SalesOrders", "Currencies"));
            //Tables.Add(new Item2("AccpacCurrencyRates"));
            //Tables.Add(new Item2("AccpacOpsys", "SalesOrders", "Waybills", "Users"));
            //Tables.Add(new Item2("AudActions", "Users"));
            //Tables.Add(new Item2("AudDataChanges", "AudActions"));
            //Tables.Add(new Item2("BorderPosts", "Users"));
            //Tables.Add(new Item2("ClearingAgents", "Users", "Companies"));
            //Tables.Add(new Item2("Companies", "Users", "ContactDetails", "Contacts", "SalesRepresentatives", "Industries", "Currencies", "RateGroups"));
            //Tables.Add(new Item2("CompanySettings", "OpsysCompanies"));
            //Tables.Add(new Item2("ContactDetails", "Users"));
            //Tables.Add(new Item2("Contacts", "Users", "ContactDetails"));
            //Tables.Add(new Item2("Currencies", "Users"));
            //Tables.Add(new Item2("CustomerTrackingContacts", "Manifests", "SalesOrders", "Companies"));
            //Tables.Add(new Item2("Depots", "Users"));
            //Tables.Add(new Item2("DoveRecordChanges"));
            //Tables.Add(new Item2("DoveTables"));
            //Tables.Add(new Item2("DTNotes"));
            //Tables.Add(new Item2("DTWarehousePosition"));
            //Tables.Add(new Item2("ExtranetUsers", "Companies", "Users"));
            //Tables.Add(new Item2("Fleets", "Transporters", "Users"));
            //Tables.Add(new Item2("Horses", "Users", "Fleets"));
            //Tables.Add(new Item2("INCOTerms", "Users"));
            //Tables.Add(new Item2("IndependantTrailers", "Users", "Fleets"));
            //Tables.Add(new Item2("Industries", "Users"));
            //Tables.Add(new Item2("ItemMappings", "RplBranches"));
            //Tables.Add(new Item2("ManifestLocation", "Manifests", "Trips", "Depots", "TrkEventType", "Users"));
            //Tables.Add(new Item2("Manifests", "OpsysCompanies", "Users", "Transporters", "Routes", "Depots", "BorderPosts"));
            //Tables.Add(new Item2("ManifestsOrders", "Manifests", "SalesOrders", "Waybills"));
            //Tables.Add(new Item2("ManifestsPurchaseOrders", "Manifests", "Users", "Currencies", "Transporters"));
            //Tables.Add(new Item2("MetField", "MetTable"));
            //Tables.Add(new Item2("MetRelationship", "MetField", "MetTable"));
            //Tables.Add(new Item2("MetTable"));
            //Tables.Add(new Item2("Modules"));
            //Tables.Add(new Item2("MrgInstance", "Users", "MrgItem"));
            //Tables.Add(new Item2("MrgItem"));
            //Tables.Add(new Item2("MrgSQLStatement", "MrgInstance", "MrgUpdateTable"));
            //Tables.Add(new Item2("MrgUpdateTable", "MrgItem"));
            //Tables.Add(new Item2("NumberSequences"));
            //Tables.Add(new Item2("OpsysCompanies"));
            //Tables.Add(new Item2("RateGroups"));
            //Tables.Add(new Item2("Rates", "RateGroups"));
            //Tables.Add(new Item2("Regions", "Users"));
            //Tables.Add(new Item2("Revisions", "SalesOrders", "Users"));
            //Tables.Add(new Item2("Routes", "Users", "Depots", "BorderPosts"));
            //Tables.Add(new Item2("RplBranches"));
            //Tables.Add(new Item2("RplBranchTables", "RplBranches", "RplTables"));
            //Tables.Add(new Item2("RplCombinedKeys"));
            //Tables.Add(new Item2("RplRecordChanges"));
            //Tables.Add(new Item2("RplTables"));
            //Tables.Add(new Item2("RplTableStructures", "RplTables"));
            //Tables.Add(new Item2("SalesOrderDetails", "SalesOrders", "Users", "INCOTerms", "Currencies"));
            //Tables.Add(new Item2("SalesOrders", "OpsysCompanies", "Users", "Manifests", "SalesRepresentatives", "Companies", "Depots", "TransportModes", "BorderPosts", "ClearingAgents", "Currencies", "Routes"));
            //Tables.Add(new Item2("SalesOrdersPurchaseOrders", "SalesOrders", "Users", "Currencies"));
            //Tables.Add(new Item2("SalesRepresentatives", "Users", "Industries", "Routes", "Regions", "Depots"));
            //Tables.Add(new Item2("SemiTrailers", "Users", "Fleets"));
            //Tables.Add(new Item2("TempSalesOrderDetails", "TempSalesOrders", "Users", "INCOTerms", "Currencies"));
            //Tables.Add(new Item2("TempSalesOrders", "OpsysCompanies", "Users", "Manifests", "SalesRepresentatives", "Companies", "Depots", "TransportModes", "BorderPosts", "ClearingAgents", "Currencies", "Routes"));
            //Tables.Add(new Item2("Transporters", "Users"));
            //Tables.Add(new Item2("TransportModes", "Users"));
            //Tables.Add(new Item2("Trips", "Manifests"));
            //Tables.Add(new Item2("TrkEventType", "Users"));
            //Tables.Add(new Item2("UserRights", "Users", "Modules"));
            //Tables.Add(new Item2("Users"));
            //Tables.Add(new Item2("VATRates"));
            //Tables.Add(new Item2("VendorMappings", "Transporters", "Currencies"));
            //Tables.Add(new Item2("Waybills", "SalesOrders", "Users"));
            //Tables.Add(new Item2("Waypoints", "Users"));
            //Tables.Add(new Item2("WHBins", "WHLocations", "Depots", "Users"));
            //Tables.Add(new Item2("WHChargeTypes"));
            //Tables.Add(new Item2("WHDocuments"));
            //Tables.Add(new Item2("WHEventTypes"));
            //Tables.Add(new Item2("WHGoods", "WHGRVs", "WHPackagingTypes", "Users"));
            //Tables.Add(new Item2("WHGRVCharges", "WHGRVs", "WHChargeTypes"));
            //Tables.Add(new Item2("WHGRVDocuments", "WHGRVs", "WHDocuments"));
            //Tables.Add(new Item2("WHGRVs", "Users", "WHLocations", "WHBins", "Companies", "BorderPosts", "ClearingAgents", "Depots", "SalesOrders"));
            //Tables.Add(new Item2("WHGRVTracking", "Users", "WHGRVs", "WHLocations", "WHBins", "WHEventTypes"));
            //Tables.Add(new Item2("WHLocations", "Users", "Depots"));
            //Tables.Add(new Item2("WHLogGRVConverts", "WHGRVs", "SalesOrders", "Users"));
            //Tables.Add(new Item2("WHPackagingTypes"));
            //Tables.Add(new Item2("WHTypeOfLocation"));

            // BRANCH TABLES
            //Tables.Add(new Item2("DTNotes"));
            //Tables.Add(new Item2("DTWarehousePosition"));
            //Tables.Add(new Item2("WHTypeOfLocation"));
            //Tables.Add(new Item2("WHTGoods", "WHTGRVs", "Users"));
            //Tables.Add(new Item2("WHTGRVs", "WHLocations", "WHBins", "Companies", "Users", "BorderPosts", "ClearingAgents", "Depots", "WHGRVTracking"));
            //Tables.Add(new Item2("WHCustomerNotifies", "WHGRVs", "WHGRVTracking", "WHCustomerEmails"));
            //Tables.Add(new Item2("WHEventTypes"));
            //Tables.Add(new Item2("WHGRVTracking", "Users", "WHGRVs", "WHLocations", "WHBins", "WHEventTypes"));
            //Tables.Add(new Item2("WHGoods", "WHGRVs", "Users", "WHPackagingTypes"));
            //Tables.Add(new Item2("WHPackagingTypes"));
            //Tables.Add(new Item2("WHGRVCharges", "WHGRVs", "WHChargeTypes"));
            //Tables.Add(new Item2("WHChargeTypes"));
            //Tables.Add(new Item2("WHGRVDocuments", "WHGRVs", "WHDocuments"));
            //Tables.Add(new Item2("WHDocuments"));
            //Tables.Add(new Item2("WHLogGRVConverts", "WHGRVs", "SalesOrders", "Users"));
            //Tables.Add(new Item2("WHGRVs", "Users", "WHLocations", "WHBins", "Companies", "BorderPosts", "ClearingAgents", "Depots", "SalesOrders"));
            //Tables.Add(new Item2("WHBins", "WHLocations", "Users", "Depots"));
            //Tables.Add(new Item2("WHLocations", "Users", "Depots"));
            //Tables.Add(new Item2("AccpacOpsys", "SalesOrders", "WayBills", "Users"));
            //Tables.Add(new Item2("TempSalesOrderDetails", "TempSalesOrders", "Users", "INCOTerms", "Currencies"));
            //Tables.Add(new Item2("TempSalesOrders", "OpsysCompanies", "Users", "SalesRepresentatives", "Companies", "Depots", "TransportModes", "Currencies", "Routes"));
            //Tables.Add(new Item2("TemplateSalesOrdersPurchaseOrders", "TemplateSalesOrders", "Users", "Currencies"));
            //Tables.Add(new Item2("TemplateSalesOrderDetails", "TemplateSalesOrders", "Users", "INCOTerms", "Currencies"));
            //Tables.Add(new Item2("TemplateSalesOrders", "Users", "OpsysCompanies", "SalesRepresentatives", "Companies", "Users", "Depots", "TransportModes", "Currencies", "Routes"));
            //Tables.Add(new Item2("TollExceptions", "TollTickets"));
            //Tables.Add(new Item2("TollTickets", "TollPurchases", "Manifests", "TollBatchNumbers"));
            //Tables.Add(new Item2("TollPurchases", "Users"));
            //Tables.Add(new Item2("TollBatchNumbers"));
            //Tables.Add(new Item2("CustomerTrackingContacts", "Manifests", "SalesOrders", "Companies"));
            //Tables.Add(new Item2("ManifestLocation", "Manifests", "Trips", "Depots", "TrkEventType", "Users"));
            //Tables.Add(new Item2("ManifestsPurchaseOrders", "Manifests", "Users", "Currencies", "Transporters"));
            //Tables.Add(new Item2("ManifestsOrders", "Manifests", "SalesOrders", "Users", "WayBills"));
            //Tables.Add(new Item2("WayBills", "SalesOrders", "Users"));
            //Tables.Add(new Item2("Trips", "Users", "Manifests"));
            //Tables.Add(new Item2("Manifests", "OpsysCompanies", "Users", "Transporters", "Routes", "Depots", "BorderPosts", "Fleets"));
            //Tables.Add(new Item2("SalesOrderDetails", "SalesOrders", "Users", "INCOTerms", "Currencies"));
            //Tables.Add(new Item2("Revisions", "Users", "SalesOrders"));
            //Tables.Add(new Item2("SalesOrdersPurchaseOrders", "SalesOrders", "Users", "Currencies"));
            //Tables.Add(new Item2("SalesOrders", "OpsysCompanies", "Users", "Manifests", "SalesRepresentatives", "Companies", "Depots", "TransportModes", "Currencies", "Routes"));
            //Tables.Add(new Item2("TrkEventType", "Users"));
            //Tables.Add(new Item2("Rates", "RateGroups", "Routes"));
            //Tables.Add(new Item2("RateGroups"));
            //Tables.Add(new Item2("AccpacCurrencyRates"));
            //Tables.Add(new Item2("SemiTrailers", "Users", "Fleets"));
            //Tables.Add(new Item2("IndependantTrailers", "Users", "Fleets"));
            //Tables.Add(new Item2("Horses", "Users", "Fleets"));
            //Tables.Add(new Item2("Fleets", "Transporters", "Users"));
            //Tables.Add(new Item2("TransportModes", "Users"));
            //Tables.Add(new Item2("ClearingAgents", "Users", "Companies"));
            //Tables.Add(new Item2("INCOTerms", "Users"));
            //Tables.Add(new Item2("Currencies", "Users"));
            //Tables.Add(new Item2("SalesRepresentatives", "Users", "Depots", "Industries", "Regions", "Routes"));
            //Tables.Add(new Item2("Industries", "Users"));
            //Tables.Add(new Item2("Regions", "Users"));
            //Tables.Add(new Item2("Transporters", "Users"));
            //Tables.Add(new Item2("Routes", "Users", "BorderPosts", "Depots"));
            //Tables.Add(new Item2("BorderPosts", "Users"));
            //Tables.Add(new Item2("Depots", "Users"));
            //Tables.Add(new Item2("Companies", "ContactDetails", "Users", "Currencies", "Depots", "Industries", "Contacts", "RateGroups", "SalesRepresentatives"));
            //Tables.Add(new Item2("Contacts", "ContactDetails", "Users"));
            //Tables.Add(new Item2("ContactDetails", "Users"));
            //Tables.Add(new Item2("CompanySettings", "OpsysCompanies"));
            //Tables.Add(new Item2("OpsysCompanies"));
            //Tables.Add(new Item2("Modules"));
            //Tables.Add(new Item2("UserRights", "Modules", "Users"));
            //Tables.Add(new Item2("Users"));
            //Tables.Add(new Item2("WHCustomerEmails", "WHGRVs"));
            //Tables.Add(new Item2("AudActions", "Users"));
            //Tables.Add(new Item2("AudDataChanges", "AudActions"));
            //Tables.Add(new Item2("DoveRecordChanges"));
            //Tables.Add(new Item2("DoveTables"));
            //Tables.Add(new Item2("ExtranetUsers", "Companies", "Users"));
            //Tables.Add(new Item2("MrgSQLStatement"));
            //Tables.Add(new Item2("RplRecordChanges"));
            //Tables.Add(new Item2("RplTables"));
            //Tables.Add(new Item2("RplTableStructures", "RplTables"));
            //Tables.Add(new Item2("VATRates"));
            //Tables.Add(new Item2("WHCustomerNotifyTypes"));
            //Tables.Add(new Item2("WHManagerEmails"));
            //Tables.Add(new Item2("WHManagerNotifies", "WHManagerEmails"));

            // Forecasting Workbench Database
            Tables.Add(new Item2("_Seasonality"));
            Tables.Add(new Item2("A_Carl", "SB_Task", "SB_TaskVariant"));
            Tables.Add(new Item2("A_RolledBack", "Site", "Stream", "Customer", "Channel", "MaterialType"));
            Tables.Add(new Item2("aa_Task_Overrides", "SB_Task"));
            Tables.Add(new Item2("Auto Causal Tasks", "Site", "Stream", "Customer", "Channel", "MaterialType", "Company", "SB_TaskVariant"));
            Tables.Add(new Item2("Auto Causal Variants Dev", "Site", "Stream", "Customer", "Channel", "MaterialType", "Company", "SB_TaskVariant"));
            Tables.Add(new Item2("AutoboxObjectiveInfo"));
            Tables.Add(new Item2("AutoboxParameterFile"));
            Tables.Add(new Item2("AutoboxProcessingController"));
            Tables.Add(new Item2("AutoboxProcessingThread"));
            Tables.Add(new Item2("CausalData", "CausalSeries"));
            Tables.Add(new Item2("CausalSeries", "Company"));
            Tables.Add(new Item2("Channel", "Company"));
            Tables.Add(new Item2("ChannelGroup", "Company"));
            Tables.Add(new Item2("Company"));
            Tables.Add(new Item2("CompanySetting", "Company", "CompanySettingDefinition"));
            Tables.Add(new Item2("CompanySettingDefinition"));
            Tables.Add(new Item2("Customer", "Company"));
            Tables.Add(new Item2("CustomerGroup", "Company"));
            Tables.Add(new Item2("DateSource"));
            Tables.Add(new Item2("EngineDefinition"));
            Tables.Add(new Item2("EngineParameterSet", "Company", "User"));
            Tables.Add(new Item2("EngineParameterSetDefinition", "EngineParameterSet", "EngineDefinition"));
            Tables.Add(new Item2("ForecastAccuracy", "SB_Task"));
            Tables.Add(new Item2("ForecastEngine"));
            Tables.Add(new Item2("ForecastMethod"));
            Tables.Add(new Item2("GlobalSetting"));
            Tables.Add(new Item2("HolidayGroup", "Company"));
            Tables.Add(new Item2("HolidaySeries", "Company"));
            Tables.Add(new Item2("InterestRate"));
            Tables.Add(new Item2("LeadLagOption"));
            Tables.Add(new Item2("Link_Channel_ChannelGroup", "Channel", "ChannelGroup"));
            Tables.Add(new Item2("Link_Customer_CustomerGroup", "Customer", "CustomerGroup"));
            Tables.Add(new Item2("Link_ForecastMethod_ForecastEngine", "ForecastEngine", "ForecastMethod"));
            Tables.Add(new Item2("Link_ForecastParameter_ParameterFile", "SB_File", "SB_ForecastParameter"));
            Tables.Add(new Item2("Link_MaterialType_MaterialTypeGroup", "MaterialType", "MaterialTypeGroup"));
            Tables.Add(new Item2("Link_Model_ModelGroup", "Model", "ModelGroup"));
            Tables.Add(new Item2("Link_OutputControl_Files", "SB_File", "SB_OutputControl"));
            Tables.Add(new Item2("Link_PublicHoliday_HolidayGroup", "HolidaySeries", "HolidayGroup"));
            Tables.Add(new Item2("Link_Site_SiteGroup", "Site", "SiteGroup"));
            Tables.Add(new Item2("Link_Stream_StreamGroup", "Stream", "StreamGroup"));
            Tables.Add(new Item2("Link_Task_CausalSeries", "CausalSeries", "SB_TaskParameterGroup"));
            Tables.Add(new Item2("Link_TaskVariant_CausalSeries", "CausalSeries", "SB_TaskVariant"));
            Tables.Add(new Item2("Link_TaskVariant_ParameterFile", "SB_File", "SB_TaskVariant"));
            Tables.Add(new Item2("Link_User_UserList", "User", "UserList"));
            Tables.Add(new Item2("Link_User_UserRole", "User", "UserRole"));
            Tables.Add(new Item2("MakeLiveQueue", "Company", "User"));
            Tables.Add(new Item2("MaterialType", "Company", "UnitOfMeasure"));
            Tables.Add(new Item2("MaterialTypeGroup", "Company"));
            Tables.Add(new Item2("MenuOption", "UserRole"));
            Tables.Add(new Item2("Model", "Company", "_Seasonality"));
            Tables.Add(new Item2("ModelGroup", "Company", "_Seasonality"));
            Tables.Add(new Item2("MonthlySiteLimits", "Site"));
            Tables.Add(new Item2("MultiTaskOverrideControl", "User"));
            Tables.Add(new Item2("MultiTaskOverrideTask", "MultiTaskOverrideControl", "SB_Task"));
            Tables.Add(new Item2("NoteSortingYields", "Site", "Company", "MaterialType"));
            Tables.Add(new Item2("Notification", "Company", "UserList", "User", "NotificationType"));
            Tables.Add(new Item2("NotificationQueue", "Notification", "User"));
            Tables.Add(new Item2("NotificationType"));
            Tables.Add(new Item2("PlanningBucket", "SiteModelStreamCustomer", "Channel"));
            Tables.Add(new Item2("PublicHoliday", "Company", "HolidaySeries"));
            Tables.Add(new Item2("QtyCategory"));
            Tables.Add(new Item2("QtyType", "Company", "QtyCategory"));
            Tables.Add(new Item2("QtyTypeConversion", "MaterialType", "QtyType"));
            Tables.Add(new Item2("Quantity", "PlanningBucket", "MaterialType", "QtyType"));
            Tables.Add(new Item2("QuantityHistory", "Quantity", "User", "ReasonCode", "SB_Task"));
            Tables.Add(new Item2("ReasonCategory", "Company"));
            Tables.Add(new Item2("ReasonCode", "Company", "ReasonCategory"));
            Tables.Add(new Item2("SB_AutoAssignedPreferredForecasts", "SB_Task"));
            Tables.Add(new Item2("SB_AutoMultiOriginFC"));
            Tables.Add(new Item2("SB_BankSplit", "Site", "Stream", "Customer", "Channel", "MaterialType", "SB_Task"));
            Tables.Add(new Item2("SB_File", "AutoboxParameterFile"));
            Tables.Add(new Item2("SB_ForecastJob", "Company", "SB_ForecastSchedule"));
            Tables.Add(new Item2("SB_ForecastParameter", "Company", "HolidayGroup"));
            Tables.Add(new Item2("SB_ForecastParameter_OptionalParameters", "ForecastEngine", "SB_ForecastParameter", "SB_TaskVariant"));
            Tables.Add(new Item2("SB_ForecastParameter_OptionalParameterTypes", "ForecastEngine"));
            Tables.Add(new Item2("SB_ForecastSchedule", "Company"));
            Tables.Add(new Item2("SB_ForecastTaskSet", "SB_ForecastParameter", "Company", "Model", "SiteGroup", "Site"
                , "StreamGroup", "Stream", "CustomerGroup", "Customer", "ChannelGroup", "MaterialTypeGroup", "MaterialType"));
            Tables.Add(new Item2("SB_GroupToIndividualID_Convert", "Company", "ModelGroup", "Model", "SiteGroup", "Site", "StreamGroup", "Stream", "CustomerGroup", "Customer", "ChannelGroup", "Channel", "MaterialTypeGroup", "MaterialType"));
            Tables.Add(new Item2("SB_Interventions", "SB_Task", "SB_TaskVariant"));
            Tables.Add(new Item2("SB_NoteSplit", "Site", "Stream", "MaterialType", "SB_Task"));
            Tables.Add(new Item2("SB_OutputControl"));
            Tables.Add(new Item2("SB_OutputFiles", "SB_SubmittedTasks"));
            Tables.Add(new Item2("SB_Override", "SB_Quantity", "SB_OverrideType", "MultiTaskOverrideControl", "User", "ReasonCode"));
            Tables.Add(new Item2("SB_OverrideControl", "Company", "Model", "SiteGroup", "Site", "StreamGroup", "Stream", "CustomerGroup", "Customer", "ChannelGroup", "Channel", "MaterialTypeGroup", "MaterialType"));
            Tables.Add(new Item2("SB_OverrideType", "Company"));
            Tables.Add(new Item2("SB_ParentChild_Tasks", "SB_Task"));
            Tables.Add(new Item2("SB_Qty_Transient", "SB_Task"));
            Tables.Add(new Item2("SB_Quantity", "SB_Task"));
            Tables.Add(new Item2("SB_QueueProcessingStep", "ForecastEngine"));
            Tables.Add(new Item2("SB_RatioSplit", "Company", "Site", "Stream", "Customer", "Channel", "MaterialType"));
            Tables.Add(new Item2("SB_RatioSplit_PerBank", "Company", "Site", "Stream", "Customer", "Channel", "MaterialType"));
            Tables.Add(new Item2("SB_Regression_Temp", "SB_Task"));
            Tables.Add(new Item2("SB_RelatedTaskSet", "SB_ForecastTaskSet", "Site", "Stream", "Customer", "Channel", "MaterialType", "SB_Task", "SB_ForecastParameter"));
            Tables.Add(new Item2("SB_RelatedTaskSet_Transient", "SB_ForecastTaskSet", "Site", "Stream", "Customer", "Channel", "MaterialType", "SB_Task", "SB_ForecastParameter"));
            Tables.Add(new Item2("SB_SubmittedTasks", "Company", "SB_Task", "SB_TaskVariant", "SB_TaskFile", "User"));
            Tables.Add(new Item2("SB_Task", "Company", "ForecastMethod", "ForecastEngine", "SB_ForecastTaskSet"
                , "SB_ForecastParameter", "SB_TaskParameterGroup", "ModelGroup", "Model", "SiteGroup", "Site"
                , "StreamGroup", "Stream", "CustomerGroup", "Customer", "ChannelGroup", "MaterialTypeGroup", "MaterialType"
                , "SB_OutputControl", "User"));
            Tables.Add(new Item2("SB_Task_Accuracy", "SB_Task", "SB_TaskVariant"));
            Tables.Add(new Item2("SB_Task_Accuracy_Detail", "Company", "SB_Task", "SB_TaskVariant"));
            Tables.Add(new Item2("SB_Task_and_Variant_PaydaySeries_Source", "Company", "SB_Task", "Site"
                , "Stream", "Channel", "MaterialType", "SB_TaskVariant"));
            Tables.Add(new Item2("SB_Task_carl", "Company", "SB_ForecastJob", "SB_ForecastParameter", "Model", "SiteGroup", "Site", "StreamGroup", "Stream", "CustomerGroup", "Customer", "ChannelGroup", "MaterialTypeGroup", "MaterialType"));
            Tables.Add(new Item2("SB_Task_FC_Temp", "SB_Task", "SB_TaskVariant"));
            Tables.Add(new Item2("SB_Task_Metadata", "SB_Task"));
            Tables.Add(new Item2("SB_Task_RelatedFiles", "SB_Task"));
            Tables.Add(new Item2("SB_Task_Sparseness", "Company", "SB_Task"));
            Tables.Add(new Item2("SB_Task_UserMarked", "SB_Task", "User"));
            Tables.Add(new Item2("SB_TaskFavourite", "SB_Task", "User"));
            Tables.Add(new Item2("SB_TaskFile", "Company", "User"));
            Tables.Add(new Item2("SB_TaskParameterGroup", "Company", "SB_ForecastParameter", "ForecastEngine", "EngineParameterSet"));
            Tables.Add(new Item2("SB_TaskQueue", "SB_Task", "SB_TaskVariant", "SB_TaskFile", "ForecastEngine", "Company", "User", "SB_SubmittedTasks"));
            Tables.Add(new Item2("SB_TaskQueue_Log", "SB_TaskQueue"));
            Tables.Add(new Item2("SB_TaskSetAccuracy_Persisted", "SB_Task", "SB_TaskVariant"));
            Tables.Add(new Item2("SB_TaskSetAccuracy_Transient", "SB_ForecastTaskSet", "Site", "Stream", "Customer", "Channel", "MaterialType", "SB_Task"));
            Tables.Add(new Item2("SB_TaskSetAccuracy_Transient_New", "Company", "SB_Task"));
            Tables.Add(new Item2("SB_TaskSetAccuracy_Transient_New_RemoveMe", "Company", "SB_Task"));
            Tables.Add(new Item2("SB_TaskSetAccuracy_UserApplicationFilter_Transient", "UserApplicationFilter", "Site", "Stream", "Customer", "Channel", "MaterialType", "SB_Task"));
            Tables.Add(new Item2("SB_TaskVariant", "Company", "SB_Task", "ForecastEngine", "User", "HolidayGroup", "EngineParameterSet"));
            Tables.Add(new Item2("SB_TaskVariant_Accuracy", "SB_TaskVariant", "SB_Task"));
            Tables.Add(new Item2("SB_TaskVariant_UserMarked", "SB_TaskVariant", "User"));
            Tables.Add(new Item2("SB_Variant_MO_Quantity", "SB_TaskVariant", "ForecastEngine"));
            Tables.Add(new Item2("SB_VariantQuantity", "SB_TaskVariant"));
            Tables.Add(new Item2("SB_YearNormalisation"));
            Tables.Add(new Item2("ServiceMessage", "ServiceStatus"));
            Tables.Add(new Item2("ServiceStatus", "ServiceType", "Company"));
            Tables.Add(new Item2("ServiceType"));
            Tables.Add(new Item2("Shifts", "Company"));
            Tables.Add(new Item2("Site", "Company", "SiteType"));
            Tables.Add(new Item2("SiteGroup", "Company"));
            Tables.Add(new Item2("SiteModel", "Model", "Site"));
            Tables.Add(new Item2("SiteModelStream", "SiteModel", "Stream"));
            Tables.Add(new Item2("SiteModelStreamCustomer", "SiteModelStream", "Customer"));
            Tables.Add(new Item2("SiteReadiness", "Site"));
            Tables.Add(new Item2("SiteReadinessCheck", "Site"));
            Tables.Add(new Item2("SiteSortingCapacity", "Site"));
            Tables.Add(new Item2("SiteType", "Company"));
            Tables.Add(new Item2("SNOPOverrideType", "Company", "User"));
            Tables.Add(new Item2("Stream", "Company"));
            Tables.Add(new Item2("StreamGroup", "Company"));
            Tables.Add(new Item2("TAP_Forecast_DecemberAdjustments", "Company", "Stream", "Channel"));
            Tables.Add(new Item2("TAP_Forecast_MonthlyRatios", "Company", "Stream", "Channel"));
            Tables.Add(new Item2("TAP_Forecast_PHAdjustments", "Company", "Stream", "Channel"));
            Tables.Add(new Item2("TaskActivationLog", "SB_Task"));
            Tables.Add(new Item2("TaskCausalData", "TaskCausalSeries"));
            Tables.Add(new Item2("TaskCausalData_FutureDatesCaptured", "TaskCausalSeries"));
            Tables.Add(new Item2("TaskCausalSeries", "Company", "SB_Task", "LeadLagOption"));
            Tables.Add(new Item2("TaskVariantCausalData", "TaskVariantCausalSeries"));
            Tables.Add(new Item2("TaskVariantCausalSeries", "Company", "SB_Task", "SB_TaskVariant", "LeadLagOption"));
            Tables.Add(new Item2("TempOverride", "SB_Task", "User"));
            Tables.Add(new Item2("Testing_Task_To_Variant", "SB_Task", "SB_TaskVariant"));
            Tables.Add(new Item2("TransferAuthorization", "Company", "User"));
            Tables.Add(new Item2("Transfers", "Site", "Company", "MaterialType", "QtyType", "Shifts"));
            Tables.Add(new Item2("UnitOfMeasure", "Company"));
            Tables.Add(new Item2("UOM_Conversion", "MaterialType", "UnitOfMeasure"));
            Tables.Add(new Item2("User", "Company"));
            Tables.Add(new Item2("UserApplicationFilter", "User", "ModelGroup", "Model", "SiteGroup", "Site"
                , "StreamGroup", "Stream", "CustomerGroup", "Customer", "ChannelGroup", "MaterialTypeGroup"
                , "MaterialType", "SB_TaskParameterGroup"));
            Tables.Add(new Item2("UserList"));
            Tables.Add(new Item2("UserRole"));
            Tables.Add(new Item2("UserSubsetFilter", "User", "ModelGroup", "Model", "SiteGroup", "Site"
                , "StreamGroup", "Stream", "CustomerGroup", "Customer", "ChannelGroup", "MaterialTypeGroup"
                , "MaterialType"));




            var sorted = TopologicalSort.Sort(Tables, x => x.Dependencies, x => x.Name);

            // Sort out reverse dependencies (children) since we already have parents in array
            foreach (var item2 in sorted)
            {
                foreach (string item2Dependency in item2.Dependencies)
                {
                    Item2 TempTable = sorted.FirstOrDefault(f => f.Name == item2Dependency);
                    if (TempTable != null)
                    {
                        if (!TempTable.DependsOn.Contains(item2.Name))
                        {
                            TempTable.DependsOn.Add(item2.Name);
                        }
                    }
                }
            }

            string strDependencies, strDependsOn;

            foreach (var item in sorted)
            {
                TOutput.AppendText(item.Name + "\r\n");
                strDependencies = "";
                foreach (var itemDependency in item.Dependencies)
                {
                    if (!string.IsNullOrEmpty(strDependencies))
                    {
                        strDependencies += ", ";
                    }
                    strDependencies += itemDependency;
                }
                TOutput.AppendText("      Depends On: " + strDependencies + "\r\n");
                strDependsOn = "";
                foreach (var itemDependency in item.DependsOn)
                {
                    if (!string.IsNullOrEmpty(strDependsOn))
                    {
                        strDependsOn += ", ";
                    }
                    strDependsOn += itemDependency;
                }
                TOutput.AppendText("      Dependencies: " + strDependsOn + "\r\n");
            }
        }
    }
}
