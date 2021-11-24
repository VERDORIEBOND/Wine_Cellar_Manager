using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller;

internal static class Queries
{
    #region StoredProcedures for table Wine
    public const string Wine_Get =      "spWine_Get";
    public const string Wine_GetAll =   "spWine_GetAll";
    public const string Wine_Insert =   "spWine_Insert";
    public const string Wine_Delete =   "spWine_Delete";
    public const string Wine_Update =   "spWine_Update";
    #endregion

    #region StoredProcedures for table Country
    public const string Country_Get =       "spCountry_Get";
    public const string Country_GetAll =    "spCountry_GetAll";
    public const string Country_Insert =    "spCountry_Insert";
    public const string Country_Delete =    "spCountry_Delete";
    public const string Country_Update =    "spCountry_Update";
    #endregion

    #region StoredProcedures for table Type
    public const string Type_Get =      "spType_Get";
    public const string Type_GetAll =   "spType_GetAll";
    public const string Type_Insert =   "spType_Insert";
    public const string Type_Delete =   "spType_Delete";
    public const string Type_Update =   "spType_Update";
    #endregion

    #region StoredProcedures for table Note
    public const string Note_Get =          "spNote_Get";
    public const string Note_GetAll =       "spNote_GetAll";
    public const string Note_GetByWine =    "spNote_GetByWine";
    public const string Note_Insert =       "spNote_Insert";
    public const string Note_Delete =       "spNote_Delete";
    public const string Note_Update =       "spNote_Update";
    #endregion

    #region StoredProcedures for table StorageLocation
    public const string StorageLocation_GetAll =    "spStorageLocation_GetAll";
    public const string StorageLocation_GetByWine = "spStorageLocation_GetByWine";
    public const string StorageLocation_Insert =    "spStorageLocation_Insert";
    public const string StorageLocation_Delete =    "spStorageLocation_Delete";
    #endregion
}
