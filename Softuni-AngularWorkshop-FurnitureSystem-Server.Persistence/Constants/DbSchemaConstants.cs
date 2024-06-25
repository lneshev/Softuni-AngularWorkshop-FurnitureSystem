namespace Softuni_AngularWorkshop_FurnitureSystem_Server.Persistence.Constants
{
    /// <summary>
    /// Contains constants related to naming of database objects like schemas, tables, views, etc.
    /// </summary>
    public static class DbSchemaConstants
    {
        #region Schemas
        public const string DboSchema = "dbo";
        #endregion

        #region Tables, Views and Synonyms
        public const string User = "User";
        public const string Role = "Role";
        public const string UserRole = "UserRole";
        public const string Furniture = "Furniture";
        public const string UserFurniture = "UserFurniture";
        #endregion

        #region Sequences
        #endregion
    }
}